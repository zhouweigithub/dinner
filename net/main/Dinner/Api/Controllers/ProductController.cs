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
using Model.Response.Com;

namespace Api.Controllers
{
    [Route("[controller]")]
    public class ProductController : BaseAuthController
    {

        private readonly IProductService _services;

        public ProductController(IProductService service)
        {
            _services = service;
        }

        [HttpGet]
        [Route("[action]/{categoryid}/{pageSize}/{page}")]
        public RespDataList<TProduct> GetList(int categoryid, int pageSize, int page)
        {
            return _services.GetList(categoryid, pageSize, page);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("[action]/{productid}")]
        public RespData<TProduct> GetEntity(int productid)
        {
            return _services.GetEntityAsync(productid);
        }
    }
}
