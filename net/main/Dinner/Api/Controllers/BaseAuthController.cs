using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{

    /// <summary>
    /// 带权限
    /// </summary>
    [Authorize]
    [ApiController]
    public class BaseAuthController : ControllerBase
    {
        /// <summary>
        /// 获取当前登录的用户ID
        /// </summary>
        /// <returns></returns>
        protected string GetUserId()
        {
            return User.Identity.Name;
        }
    }
}
