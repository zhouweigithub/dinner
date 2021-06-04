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
    /// 用户信息
    /// </summary>
    [Route("[controller]")]
    public class UserController : BaseAuthController
    {
        private readonly IUserService _services;
        private readonly IWxService _wxservices;

        public UserController(IUserService service, IWxService wxService)
        {
            _services = service;
            _wxservices = wxService;
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
        /// 更新用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<RespData<TUser>> Update(TUser user)
        {
            return await _services.UpdateAsync(user);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="openid">用户代码</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{openid}")]
        public async Task<RespData> Delete(String openid)
        {
            return await _services.DeleteAsync(openid);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="openid">用户代码</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{openid}")]
        public async Task<RespData<TUser>> GetEntity(String openid)
        {
            return await _services.GetEntityAsync(openid);
        }
    }
}
