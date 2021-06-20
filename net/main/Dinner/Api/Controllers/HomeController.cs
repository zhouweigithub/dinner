using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model;
using Model.Database;
using Model.Request;
using Model.Request.Wx;
using Model.Response;
using Model.Response.Com;
using ZwUtil;
using ZwUtil.QrCode;

namespace Api.Controllers
{
    /// <summary>
    /// 登录相关
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IWxService _wxservices;
        private readonly IOptions<WxOpenidConfigModel> _wxconfig;
        private readonly IUserService userService;
        private readonly ISupplierService supplierService;
        private readonly IDelivererService deliverService;
        private readonly IDbInitService _dbservice;
        private readonly JwtSetting jwt;


        public HomeController(IUserService userService, ISupplierService supplierService, IDelivererService deliverService, IWxService wxService, IOptions<WxOpenidConfigModel> wxconfig, IOptions<JwtSetting> option, IDbInitService dbservice)
        {
            this.userService = userService;
            this.supplierService = supplierService;
            this.deliverService = deliverService;
            _wxservices = wxService;
            _wxconfig = wxconfig;
            _dbservice = dbservice;
            jwt = option.Value;
        }

        /// <summary>
        /// 普通用户登录，若该用户未注册，则进行注册
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<RespDataToken<TUser>> Login(UserAdd user)
        {
            var entity = await userService.GetEntityAsync(user.OpenId);

            //如果没有该用户，则为其注册
            if (entity.code == -2)
            {
                var addResult = await userService.AddAsync(user);

                //生成二维码地址
                var userInfo = CreateUserInfo(addResult.data);

                entity.code = addResult.code;
                entity.msg = addResult.msg;
                entity.data = userInfo;

                if (addResult.code == 0)
                {
                    entity.token = GenerateToken(user.OpenId);
                    //创建二维码图片
                    CreateUserQrcode(user.OpenId);
                }
            }
            //如果没抛异常，则获则生成token
            else if (entity.code != -1)
            {
                entity.token = GenerateToken(user.OpenId);
                var userInfo = CreateUserInfo(entity.data);
                entity.data = userInfo;

                //若二维码图片不存在，则创建
                if (!FileHelper.IsFileExists(userInfo.QrCode))
                    CreateUserQrcode(user.OpenId);
            }

            return entity;
        }

        /// <summary>
        /// 供货商登录
        /// </summary>
        /// <param name="user">参数</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<RespDataToken<SpUser>> SupplierLogin(LoginInfo user)
        {
            var entity = await supplierService.LoginAsync(user);

            //如果登录成功，则生成token
            if (entity.code == 0)
            {
                entity.token = GenerateToken(entity.data.Id.ToString());
            }

            return entity;
        }

        /// <summary>
        /// 送货员登录
        /// </summary>
        /// <param name="user">参数</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<RespDataToken<DlvUser>> DeliverLogin(LoginInfo user)
        {
            var entity = await deliverService.LoginAsync(user);

            //如果登录成功，则生成token
            if (entity.code == 0)
            {
                entity.token = GenerateToken(entity.data.Id.ToString());
            }

            return entity;
        }


        /// <summary>
        /// 获取微信用户的openid
        /// </summary>
        /// <param name="loginCode">登录码</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{loginCode}")]
        public RespData<string> GetOpenId(string loginCode)
        {
            return _wxservices.GetOpenId(loginCode, _wxconfig.Value);
        }


        /// <summary>
        /// 重置并生成初始数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<RespData> ResetDatas()
        {
            var v1 = await _dbservice.ClearDatas();

            var v2 = await _dbservice.CreateInitDatas();

            RespData result = new RespData();

            if (v1.code != 0 || v2.code != 0)
            {
                result.code = -1;
                result.msg = "操作失败";
            }

            return result;
        }


        /// <summary>
        /// 生成token
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        private string GenerateToken(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return string.Empty;

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Iss, jwt.Issuer),
                new Claim(JwtRegisteredClaimNames.Aud, jwt.Audience),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddMonths(1)).ToUnixTimeSeconds().ToString()),
            };

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey)),
                    SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// 生成用户的二维码图片
        /// </summary>
        /// <param name="openid">openid</param>
        private void CreateUserQrcode(string openid)
        {
            //生成二维码
            byte[] imgBytes = QrCodeUtil.CreateQrCode($"userid:{openid}", ErrorCorrectionLevel.L, QrSize.Middle);

            string filePath = string.Format("{0}{1}\\{2}.jpg", AppDomain.CurrentDomain.BaseDirectory, "statics\\imgs\\qrcode\\user", openid);

            FileHelper.CreateFile(filePath, imgBytes);
        }


        /// <summary>
        /// 创建UserInfo实例，包含二维码图片地址
        /// </summary>
        /// <param name="user">TUser信息</param>
        /// <returns></returns>
        private UserInfo CreateUserInfo(TUser user)
        {
            UserInfo userInfo = new(user);
            userInfo.QrCode = string.Format("{0}://{1}/statics/imgs/qrcode/user/{2}.jpg", Request.Scheme, Request.Host.Value, user.Code);
            return userInfo;
        }

    }
}
