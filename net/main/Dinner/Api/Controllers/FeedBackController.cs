using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interface;
using Microsoft.AspNetCore.Mvc;
using Model.Database;
using Model.Request;
using Model.Response.Com;

namespace Api.Controllers
{
    /// <summary>
    /// 用户反馈信息
    /// </summary>
    [Route("[controller]")]
    public class FeedBackController : BaseAuthController
    {
        private readonly IFeedBackService _services;

        public FeedBackController(IFeedBackService service)
        {
            _services = service;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<RespDataList<TFeedback>> GetList()
        {
            string openid = GetUserCode();
            return await _services.GetListAsync(openid);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<RespData> Add(FeedbackAdd data)
        {
            string openid = GetUserCode();
            return await _services.AddAsync(openid, data);
        }
    }
}
