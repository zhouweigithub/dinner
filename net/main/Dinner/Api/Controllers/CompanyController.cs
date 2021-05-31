using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
using Model.Database;
using Model.Request;
using Model.Response.Com;

namespace Api.Controllers
{
    /// <summary>
    /// 公司信息
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : BaseAuthController
    {
        private readonly ICompanyService _services;

        public CompanyController(ICompanyService service)
        {
            _services = service;
        }

        /// <summary>
        /// 添加公司
        /// </summary>
        /// <param name="company">公司基本信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public Task<RespData<TCompany>> Add(CompanyAdd company)
        {
            return _services.AddAsync(company);
        }
    }
}
