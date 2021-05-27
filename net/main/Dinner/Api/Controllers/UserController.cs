using BLL;
using BLL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Model;
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
    public class UserController : ControllerBase
    {
        private readonly IBaseService _services;
        private readonly ILogger<UserController> _logger;

        public UserController(IBaseService service, ILogger<UserController> logger)
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
        [Authorize]
        public TUser Add(TUser user)
        {
            Console.WriteLine(User.Identity.Name);
            Console.WriteLine(User.Identity.AuthenticationType);
            return _services.Add(user);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{id}")]
        public TUser Show(int id)
        {
            var user = new TUser()
            {
                Code = "xjdsa",
                Companyid = 3,
                Nick = "sunny baby",
                Crtime = DateTime.Now
            };

            return _services.Add(user);
        }

    }
}
