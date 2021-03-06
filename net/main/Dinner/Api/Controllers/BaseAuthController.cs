using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using BLL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{

    /// <summary>
    /// 带权限功能
    /// </summary>
    [Authorize]
    [ApiController]
    public class BaseAuthController : ControllerBase
    {

        /// <summary>
        /// 获取当前登录的用户Code
        /// </summary>
        /// <returns></returns>
        protected string GetUserCode()
        {
            return User.Identity.Name;
        }

        /// <summary>
        /// 获取域名信息
        /// </summary>
        /// <returns></returns>
        protected string GetHostInfo()
        {
            return string.Format("{0}://{1}", Request.Scheme, Request.Host.Value);
        }
    }
}
