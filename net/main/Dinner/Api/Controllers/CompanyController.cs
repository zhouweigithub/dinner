﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
using Model.Request;
using Model.Response.Com;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
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
        public RespData<TCompany> Add(CompanyAdd company)
        {
            return _services.Add(company);
        }
    }
}
