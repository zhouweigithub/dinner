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

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="productName">商品名称</param>
        /// <param name="pageSize">页数据量</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{productName}/{pageSize}/{page}")]
        public async Task<RespDataList<TOrder>> GetList(string productName, int pageSize, int page)
        {
            string openid = GetUserCode();
            return await _services.GetListAsync(openid, productName, pageSize, page);
        }

        /// <summary>
        /// 提交订单
        /// </summary>
        /// <param name="data">订单信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<RespData> Add(OrderAdd data)
        {
            string openid = GetUserCode();
            return await _services.AddAsync(data, openid);
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="orderid">订单id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{orderid}")]
        public async Task<RespData> Cancel(string orderid)
        {
            string openid = GetUserCode();
            return await _services.CancelAsync(orderid, openid);
        }
    }
}
