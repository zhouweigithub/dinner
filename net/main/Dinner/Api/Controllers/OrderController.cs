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
    [Route("[controller]")]
    public class OrderController : BaseAuthController
    {
        private readonly IOrderService _services;

        public OrderController(IOrderService service)
        {
            _services = service;
        }

        [HttpGet]
        [Route("[action]/{productName}/{pageSize}/{page}")]
        public async Task<RespDataList<TOrder>> GetList(string productName, int pageSize, int page)
        {
            string openid = GetUserCode();
            return await _services.GetListAsync(openid, productName, pageSize, page);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<RespData> Add(OrderAdd data)
        {
            string openid = GetUserCode();
            return await _services.AddAsync(data, openid);
        }

        [HttpGet]
        [Route("[action]/{orderid}")]
        public async Task<RespData> Add(string orderid)
        {
            string openid = GetUserCode();
            return await _services.CancelAsync(orderid, openid);
        }
    }
}
