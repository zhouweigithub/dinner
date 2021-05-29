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
using Model.Response.Com;

namespace Api.Controllers
{
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly JwtSetting jwt;

        public HomeController(IUserService userService, IOptions<JwtSetting> option)
        {
            this.userService = userService;
            this.jwt = option.Value;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="openid">微信openid</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{openid}")]
        public RespDataToken<TUser> Login(string openid)
        {
            var entity = userService.GetEntity(openid);
            if (entity.code != -1)
                entity.token = GenerateToken(openid);
            return entity;
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
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddMinutes(1)).ToUnixTimeSeconds().ToString()),
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
