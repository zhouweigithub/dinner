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
    /// 购物车信息
    /// </summary>
    [Route("[controller]")]
    public class CartController : BaseAuthController
    {
        private readonly ICartService _services;

        /// <summary>
        /// 购物车
        /// </summary>
        /// <param name="service"></param>
        public CartController(ICartService service)
        {
            _services = service;
        }

        /// <summary>
        /// 获取购物车商品
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<RespDataList<TCart>> GetList()
        {
            string openid = GetUserCode();
            return await _services.GetListAsync(openid);
        }

        /// <summary>
        /// 修改购物车商品的数量
        /// </summary>
        /// <param name="data">商品信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<RespData> Update(CartUpdate data)
        {
            string openid = GetUserCode();
            return await _services.UpdateCountAsync(openid, data);
        }

        /// <summary>
        /// 删除购物车里的商品
        /// </summary>
        /// <param name="data">商品信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<RespData> Delete(CartDelete data)
        {
            string openid = GetUserCode();
            return await _services.DeleteProductsAsync(openid, data);
        }


        /// <summary>
        /// 添加商品到购物车
        /// </summary>
        /// <param name="data">商品信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<RespData> Add(CartAdd data)
        {
            string openid = GetUserCode();

            return await _services.AddAsync(openid, data);
        }
    }
}
