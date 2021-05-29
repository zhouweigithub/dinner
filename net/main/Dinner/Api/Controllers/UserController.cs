using BLL;
using BLL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Model;
using Model.Request;
using Model.Response.Com;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : BaseAuthController
    {
        private readonly IUserService _services;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService service, ILogger<UserController> logger)
        {
            _services = service;
            _logger = logger;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [AllowAnonymous]
        public async Task<RespData<TUser>> Add(UserAdd user)
        {
            return await _services.AddAsync(user);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<TUser> Show(int id)
        {
            var user = new TUser()
            {
                Code = "xjdsa",
                Companyid = 3,
                Nick = "sunny baby",
                Crtime = DateTime.Now
            };

            return await _services.AddAsync(user);
        }

    }
}
