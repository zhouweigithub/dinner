using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BLL;
using BLL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Model;
using Model.Database;
using Model.Request;
using Model.Response.Com;

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

    }
}
