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
        /// <param name="company">公司信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public Task<RespData<TCompany>> Add(CompanyAdd company)
        {
            return _services.AddAsync(company);
        }

        /// <summary>
        /// 更新公司信息
        /// </summary>
        /// <param name="data">公司信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<RespData<TCompany>> Update(TCompany data)
        {
            return await _services.UpdateAsync(data);
        }

        /// <summary>
        /// 删除公司信息
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{companyid}")]
        public async Task<RespData> Delete(Int32 companyid)
        {
            return await _services.DeleteAsync(companyid);
        }

        /// <summary>
        /// 获取公司信息
        /// </summary>
        /// <param name="companyid">公司id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{companyid}")]
        public async Task<RespData<TCompany>> GetEntity(int companyid)
        {
            return await _services.GetEntity(companyid);
        }

    }
}
