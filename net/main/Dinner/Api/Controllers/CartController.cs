﻿using System;
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
    public class CartController : BaseAuthController
    {
        private readonly ICartService _services;

        public CartController(ICartService service)
        {
            _services = service;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<RespDataList<TCart>> GetList()
        {
            string openid = GetUserCode();
            return await _services.GetListAsync(openid);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<RespData> Delete(CartDelete data)
        {
            string openid = GetUserCode();
            return await _services.DeleteAsync(openid, data);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<RespData> Add(CartAdd data)
        {
            string openid = GetUserCode();
            return await _services.AddAsync(openid, data);
        }
    }
}