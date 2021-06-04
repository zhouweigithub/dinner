﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.EasyCaching;
using BLL.Interface;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model;
using Model.Database;
using Model.Request;
using Model.Response.Com;

namespace BLL
{
    public class ProductService : BaseService, IProductService
    {
        private readonly ILogger<ProductService> _logger;

        private readonly ICache _cache;


        public ProductService(DbService context, ILogger<ProductService> logger, ICache cache) : base(context, logger)
        {
            _logger = logger;
            _cache = cache;
        }

        public async Task<RespDataList<TProduct>> GetListAsync(Int32 categoryid, int pageSize, int page)
        {
            RespDataList<TProduct> result = new RespDataList<TProduct>();
            try
            {
                var datas = context.Set<TProduct>().AsNoTracking().Where(a => true);
                if (categoryid != default)
                    datas = datas.Where(a => a.Category == categoryid);

                datas = datas.Skip(pageSize * (page - 1)).Take(pageSize);

                result.datas = await datas.ToListAsync();
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                _logger.LogError(e.ToString());
            }

            return result;
        }

        public async Task<RespData<TProduct>> GetEntityAsync(Int32 productid)
        {
            RespData<TProduct> result = new RespData<TProduct>();
            try
            {
                result.data = await context.Set<TProduct>().AsNoTracking().Include(a => a.CategoryNavigation).FirstOrDefaultAsync(b => b.Id == productid);

                var yyy = _cache.TryAdd("prod", result.data, TimeSpan.FromMinutes(10));
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                _logger.LogError(e.ToString());
            }

            return result;
        }

        public async Task<RespData<TProduct>> AddAsync(ProductAdd data)
        {
            RespData<TProduct> result = new();
            try
            {
                var t = new TProduct()
                {
                    Name = data.Name,
                    Category = data.Category,
                    Crtime = DateTime.Now,
                    Price = data.Price,
                    Sales = 0,
                    Img = data.Img,
                };

                result.data = await AddAsync(t);
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                _logger.LogError(e.ToString());
            }

            return result;
        }

        public async Task<RespData<TProduct>> UpdateAsync(TProduct data)
        {
            RespData<TProduct> result = new();
            try
            {
                context.Update(data);
                await context.SaveChangesAsync();
                result.data = data;
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                _logger.LogError(e.ToString());
            }

            return result;
        }

        public async Task<RespData> DeleteAsync(Int32 productid)
        {
            RespData result = new();
            try
            {
                context.Remove(new TProduct() { Id = productid });
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                _logger.LogError(e.ToString());
            }

            return result;
        }
    }
}
