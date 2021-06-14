using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using BLL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.Database;
using Model.Request;
using Model.Response.Com;

namespace Api.Controllers
{
    /// <summary>
    /// 商品信息
    /// </summary>
    [Route("[controller]")]
    public class ProductController : BaseAuthController
    {

        private readonly IProductService _services;

        public ProductController(IProductService service)
        {
            _services = service;
        }

        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="categoryid">商品分类id</param>
        /// <param name="pageSize">每页数据量</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<RespDataList<TProduct>> GetList(int categoryid, int pageSize = 10, int page = 1)
        {
            return await _services.GetListAsync(categoryid, pageSize, page);
        }


        /// <summary>
        /// 获取商品详情
        /// </summary>
        /// <param name="productid">商品id</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]/{productid}")]
        public async Task<RespData<TProduct>> GetEntity(int productid)
        {
            return await _services.GetEntityAsync(productid);
        }

        /// <summary>
        /// 修改商品信息
        /// </summary>
        /// <param name="data">参数</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<RespData> Update(ProductUpdate data)
        {
            return await _services.UpdateAsync(data);
        }

        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="id">商品id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<RespData> Delete(int id)
        {
            return await _services.DeleteAsync(id);
        }


        /// <summary>
        /// 添加商品信息
        /// </summary>
        /// <param name="data">参数</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<RespData> Add(ProductAdd data)
        {
            return await _services.AddAsync(data);
        }

    }
}
