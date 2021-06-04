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
using Microsoft.EntityFrameworkCore;

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

                var serverModel = context.Set<TCart>().FirstOrDefault(a => a.Userid == userid && a.Productid == data.Productid);

                if (serverModel == null)
                {
                    //添加新商品

                    await AddAsync(new TCart()
                    {
                        Productid = data.Productid,
                        Count = data.Count,
                        Userid = userid,
                        Crtime = DateTime.Now,
                    });
                }
                else
                {
                    //修改商品数量
                    serverModel.Count += data.Count;
                    context.SaveChanges();
                }

            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                logger.LogError(e.ToString());
            }

            return result;
        }

        public async Task<RespData> UpdateCountAsync(String openid, CartUpdate data)
        {
            RespData result = new RespData();

            try
            {
                int userid = GetUserIdByCode(openid);

                var model = new TCart()
                {
                    Userid = userid,
                    Productid = data.Productid,
                    Count = data.Count,
                };

                if (data.Count == 0)
                {
                    //如果数量为零，则直接删除
                    context.Set<TCart>().Remove(model);
                }
                else
                {
                    //以下两行代码更新实体时，只会更新实体中有的属性
                    context.Attach(model);
                    context.Entry(model).Property(a => a.Count).IsModified = true;
                }

                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                logger.LogError(e.ToString());
            }

            return result;
        }

        public async Task<RespDataList<TCart>> GetListAsync(String openid)
        {
            RespDataList<TCart> result = new RespDataList<TCart>();
            try
            {
                int userid = GetUserIdByCode(openid);
                var datas = await context.Set<TCart>().AsNoTracking().Where(a => a.Userid == userid).ToListAsync();
                result.datas = datas;
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                _logger.LogError(e.ToString());
            }

            return result;
        }

        public async Task<RespData> DeleteProductsAsync(String openid, CartDelete data)
        {
            RespData result = new RespData();

            try
            {
                int userid = GetUserIdByCode(openid);

                List<TCart> carts = new List<TCart>();
                foreach (int item in data.Productids)
                {
                    carts.Add(new TCart()
                    {
                        Userid = userid,
                        Productid = item,
                    });
                }

                await DeleteMultipleAsync(carts);
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                logger.LogError(e.ToString());
            }

            return result;
        }


    }
}
