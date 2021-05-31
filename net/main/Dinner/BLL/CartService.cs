using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;
using DAL;
using Microsoft.Extensions.Logging;
using Model.Database;
using Model.Request;
using Model.Response.Com;

namespace BLL
{
    public class CartService : BaseService, ICartService
    {
        private readonly ILogger<CartService> _logger;

        public CartService(DbService context, ILogger<CartService> logger) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<RespData> AddAsync(String openid, CartAdd data)
        {
            RespData result = new RespData();

            try
            {
                int userid = GetUserIdByCode(openid);
                await AddAsync(new TCart()
                {
                    Productid = data.Productid,
                    Count = data.Count,
                    Userid = userid,
                    Crtime = DateTime.Now,
                });
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                logger.LogError(e.ToString());
            }

            return result;
        }

        public async Task<RespData> DeleteAsync(String openid, CartDelete data)
        {
            RespData result = new RespData();

            try
            {
                int userid = GetUserIdByCode(openid);

                List<TCart> models = new List<TCart>();
                foreach (var item in data.Ids)
                {
                    models.Add(new TCart()
                    {
                        Id = item
                    });
                }

                await DeleteMultipleAsync(models);
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                logger.LogError(e.ToString());
            }

            return result;
        }

        public Task<RespData<List<TCart>>> GetListAsync(String openid)
        {
            throw new NotImplementedException();
        }
    }
}
