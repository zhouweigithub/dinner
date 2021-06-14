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
    /// 订单信息
    /// </summary>
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
        [Route("[action]")]
        public async Task<RespDataList<TOrder>> GetList(string productName, int pageSize = 10, int page = 1)
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
        [HttpPost]
        [Route("[action]")]
        public async Task<RespData> Cancel(string orderid)
        {
            string openid = GetUserCode();
            return await _services.CancelAsync(orderid, openid);
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="orderid">订单id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<RespData> Delete(string orderid)
        {
            string openid = GetUserCode();
            return await _services.DeleteAsync(orderid, openid);
        }

        /// <summary>
        /// 添加订单评论
        /// </summary>
        /// <param name="orderid">订单id</param>
        /// <param name="comment">评论内容</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<RespData> AddComment(string orderid, string comment)
        {
            return await _services.AddCommentAsync(orderid, comment);
        }

        /// <summary>
        /// 用户获取自己现在需要取货的商品信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<RespDataList<TOrderProduct>> GetTodayOrder()
        {
            string openid = GetUserCode();
            return await _services.GetTodayOrderAsync(openid);
        }


        /// <summary>
        /// 获取目标用户现在需要取的餐品信息
        /// </summary>
        /// <param name="userCode">目标用户代码(openid)</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{userCode}")]
        public async Task<RespDataList<TOrderProduct>> GetTodayOrderByUserCode(string userCode)
        {
            return await _services.GetTodayOrderAsync(userCode);
        }
    }
}
