using System;
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
using Model.Response.Com;

namespace BLL
{
    public class ProductService : BaseService, IProductService
    {
        private readonly ILogger<ProductService> _logger;

        private readonly ICache _cache;


        public ProductService(DbService context, ILogger<ProductService> logger, ICache cache) : base(context)
        {
            _logger = logger;
            _cache = cache;
        }

        public RespDataList<TProduct> GetList(Int32 categoryid, int pageSize, int page)
        {
            RespDataList<TProduct> result = new RespDataList<TProduct>();
            try
            {
                var datas = context.Set<TProduct>().Where(a => true);
                if (categoryid != default)
                    datas = datas.Where(a => a.Category == categoryid);

                datas = datas.Skip(pageSize * (page - 1)).Take(pageSize);

                result.datas = datas.ToList();
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                _logger.LogError(e.ToString());
            }

            return result;
        }

        public RespData<TProduct> GetEntityAsync(Int32 productid)
        {
            RespData<TProduct> result = new RespData<TProduct>();
            try
            {
                result.data = context.Set<TProduct>().Include(a => a.CategoryNavigation).FirstOrDefault(b => b.Id == productid);

                var yyy = _cache.TryAdd("prod", result.data, TimeSpan.FromMinutes(1));
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
