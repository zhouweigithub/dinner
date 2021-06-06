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
    /// 商品分类信息
    /// </summary>
    public class CategoryController : BaseAuthController
    {
        private readonly ICategoryService _services;

        /// <summary>
        /// 商品分类
        /// </summary>
        /// <param name="service"></param>
        public CategoryController(ICategoryService service)
        {
            _services = service;
        }

        /// <summary>
        /// 获取商品分类信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<RespDataList<TCategory>> GetList()
        {
            return await _services.GetListAsync();
        }

        /// <summary>
        /// 修改商品分类信息
        /// </summary>
        /// <param name="data">商品分类信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<RespData> Update(CategoryUpdate data)
        {
            return await _services.UpdateCountAsync(data);
        }

        /// <summary>
        /// 删除商品分类
        /// </summary>
        /// <param name="id">商品分类id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<RespData> Delete(int id)
        {
            return await _services.DeleteProductsAsync(id);
        }


        /// <summary>
        /// 添加商品分类
        /// </summary>
        /// <param name="data">商品分类信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<RespData> Add(CategoryAdd data)
        {
            return await _services.AddAsync(data);
        }
    }
}
