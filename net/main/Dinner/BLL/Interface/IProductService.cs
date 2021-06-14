﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Database;
using Model.Request;
using Model.Response.Com;

namespace BLL.Interface
{
    /// <summary>
    /// 商品信息
    /// </summary>
    public interface IProductService
    {
        public Task<RespDataList<TProduct>> GetListAsync(int categoryid, int pageSize, int page);

        public Task<RespData<TProduct>> GetEntityAsync(int productid);

        public Task<RespData<TProduct>> AddAsync(ProductAdd data);

        public Task<RespData<TProduct>> UpdateAsync(TProduct data);

        public Task<RespData> DeleteAsync(int productid);
    }
}
