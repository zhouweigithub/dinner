using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Model;
using Model.Response.Com;

namespace Api.Controllers
{
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        public IUserService userService { get; }

        public HomeController(IUserService userService)
        {
            this.userService = userService;
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
            string token = GenerateToken(openid);

            return new RespDataToken<TUser>()
            {
                code = entity != null ? 0 : -1,
                msg = "ok",
                data = entity,
                token = token
            };
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
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("the secret that needs to be at least 16 characeters long for HmacSha256")),
                                             SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
