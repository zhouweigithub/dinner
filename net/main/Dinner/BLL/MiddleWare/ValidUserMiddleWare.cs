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

namespace BLL.MiddleWare
{
    /// <summary>
    /// 自定义错误处理
    /// </summary>
    public class ValidUserMiddleWare
    {
        protected readonly DbService _context;
        private readonly RequestDelegate _next;

        /// <summary>
        /// 检测用户信息是否正常
        /// </summary>
        /// <param name="next"></param>
        /// <param name="context"></param>
        public ValidUserMiddleWare(RequestDelegate next, DbService context)
        {
            _next = next;
            _context = context;
        }

        public async Task Invoke(HttpContext context)
        {
            var openid = context.User.Identity.Name;

            if (string.IsNullOrWhiteSpace(openid))
            {
                _next.Invoke(context);
            }
            else
            {
                var user = await _context.Set<TUser>().FirstOrDefaultAsync(a => a.Code == openid);

                bool isOk = true;
                string msg = string.Empty;
                if (user == null)
                {
                    isOk = false;
                    msg = "无效用户信息";
                }
                else if (user.State == 1)
                {
                    isOk = false;
                    msg = "用户状态异常，请联系管理员";
                }

                if (isOk)
                {
                    _next.Invoke(context);
                }
                else
                {
                    HandleErrorAsync(context, msg);
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
