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
using Model.Response.Com;

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
        private readonly IDbInitService _dbservice;
        private readonly JwtSetting jwt;


        public HomeController(IUserService userService, IWxService wxService, IOptions<WxOpenidConfigModel> wxconfig, IOptions<JwtSetting> option, IDbInitService dbservice)
        {
            this.userService = userService;
            _wxservices = wxService;
            _wxconfig = wxconfig;
            _dbservice = dbservice;
            jwt = option.Value;
        }

        /// <summary>
        /// 登录，若该用户无未注册，则进行注册
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
                if (addResult.code == 0)
                {
                    entity.data = addResult.data;
                }
            }

            //如果没抛异常，则获则生成token
            if (entity.code != -1)
            {
                entity.token = GenerateToken(user.OpenId);
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


    }
}
