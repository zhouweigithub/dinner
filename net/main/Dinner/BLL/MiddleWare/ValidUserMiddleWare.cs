using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Http;
using Model.Database;
using Model.Response.Com;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using BLL.Interface;

namespace BLL.MiddleWare
{
    /// <summary>
    /// 自定义错误处理
    /// </summary>
    public class ValidUserMiddleWare
    {
        protected readonly IUserService _userService;
        private readonly RequestDelegate _next;

        /// <summary>
        /// 检测用户信息是否正常
        /// </summary>
        /// <param name="next"></param>
        /// <param name="context"></param>
        public ValidUserMiddleWare(RequestDelegate next, IUserService userService)
        {
            _next = next;
            _userService = userService;
        }

        public async Task Invoke(HttpContext context)
        {
            var openid = context.User.Identity.Name;

            if (string.IsNullOrWhiteSpace(openid))
            {
                await _next.Invoke(context);
            }
            else
            {
                var user = await _userService.GetEntityAsync(openid);

                bool isOk = true;
                string msg = string.Empty;
                if (user.code != 0)
                {
                    isOk = false;
                    msg = "无效用户信息";
                }
                else if (user.data.State == 1)
                {
                    isOk = false;
                    msg = "用户状态异常，请联系管理员";
                }

                if (isOk)
                {
                    await _next.Invoke(context);
                }
                else
                {
                    await HandleErrorAsync(context, msg);
                }
            }
        }

        private static Task HandleErrorAsync(HttpContext context, string msg)
        {
            context.Response.ContentType = "application/json";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new RespData
            {
                code = statusCode,
                msg = msg
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}
