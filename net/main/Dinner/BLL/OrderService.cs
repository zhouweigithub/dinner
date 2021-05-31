using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;
using DAL;
using Microsoft.Extensions.Logging;
using Model;
using Model.Database;
using Model.Request;
using Model.Response;
using Model.Response.Com;
using Util;
using Microsoft.EntityFrameworkCore;

namespace BLL
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly ILogger<OrderService> _logger;

        public OrderService(DbService context, ILogger<OrderService> logger) : base(context, logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="data">订单信息</param>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public async Task<RespData<TOrder>> AddAsync(OrderAdd data, int userid)
        {
            RespData<TOrder> result = new RespData<TOrder>();

            try
            {
                //订单号
                string orderid = CreateOrderId();


                //商品信息
                var productids = data.Products.Select(a => a.Productid);
                //服务器上存储的商品信息
                var productDatas = context.Set<TProduct>().Where(a => productids.Contains(a.Id)).ToList();

                foreach (ProductInfo item in data.Products)
                {
                    //验证提交的商品信息与服务器上的是否一致
                    TProduct productData = productDatas.FirstOrDefault(a => a.Id == item.Productid);
                    if (productData == null || productData.Price != item.Price)
                    {
                        result.code = -2;
                        result.msg = "订单商品信息异常，请重新下单";
                        return result;
                    }

                    TOrderProduct t = new TOrderProduct()
                    {
                        Orderid = orderid,
                        Productid = item.Productid,
                        Count = item.Count,
                        Money = item.Money,
                        Price = item.Price,
                    };

                    await context.Set<TOrderProduct>().AddAsync(t);
                }


                //优惠券信息
                var couponids = data.Coupons.Select(a => a.Couponid);
                //服务器上存储的优惠券信息
                var couponDatas = context.Set<TCoupon>().Where(a => couponids.Contains(a.Id)).ToList();

                foreach (CouponInfo item in data.Coupons)
                {
                    //验证提交的优惠券信息与服务器上的是否一致
                    TCoupon couponData = couponDatas.FirstOrDefault(a => a.Id == item.Couponid);
                    if (couponData == null || couponData.Money != couponData.Money)
                    {
                        result.code = -2;
                        result.msg = "优惠券信息异常，请重新下单";
                        return result;
                    }
                    else if (couponData.StartTime > DateTime.Now || couponData.EndTime < DateTime.Now)
                    {
                        result.code = -3;
                        result.msg = "优惠券未在有效使用期内，请重新下单";
                        return result;
                    }

                    TOrderCoupon t = new TOrderCoupon()
                    {
                        Orderid = orderid,
                        Couponid = item.Couponid,
                        Count = item.Count,
                        Price = item.Price,
                        Money = item.Money,
                    };

                    await context.Set<TOrderCoupon>().AddAsync(t);
                }


                //订单信息
                //校验优惠总金额
                decimal allCouponMoney = data.Coupons.Sum(a => a.Money);
                decimal allProductMoney = data.Products.Sum(a => a.Money);

                if (data.Money != allProductMoney || data.CouponMoney != allCouponMoney || data.PayMoney != allProductMoney - allCouponMoney)
                {
                    result.code = -4;
                    result.msg = "订单信息异常，请重新下单";
                    return result;
                }

                TOrder order = new TOrder()
                {
                    Id = orderid,
                    Userid = userid,
                    Money = data.Money,
                    CouponMoney = data.CouponMoney,
                    PayMoney = data.PayMoney,
                    State = 0,
                    Crtime = DateTime.Now,
                };

                await context.Set<TOrder>().AddAsync(order);
                result.data = order;

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

        public async Task<RespData<Boolean>> CancelAsync(String orderid, int userid)
        {
            RespData<Boolean> result = new RespData<Boolean>();

            try
            {
                TOrder serverOrderInfo = await context.Set<TOrder>().FindAsync(orderid);

                //订单不存在或者订单用户不正确
                if (serverOrderInfo == null || serverOrderInfo.Userid != userid)
                {
                    result.code = -2;
                    result.msg = "订单异常，请重试";
                }
                else
                {
                    //更新订单状态
                    serverOrderInfo.State = 9;
                    context.Update(serverOrderInfo);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                result.code = -1;
                result.msg = "服务内部错误";
            }

            return result;
        }

        public async Task<RespDataList<TOrder>> GetListAsync(String productName, Int32 pageSize, Int32 page)
        {
            RespDataList<TOrder> result = new RespDataList<TOrder>();

            try
            {
                var datas = context.Set<TOrder>().AsNoTracking().Include(a => a.TOrderProduct).Include(b => b.TOrderCoupon).Where(a => true);
                if (!string.IsNullOrWhiteSpace(productName))
                    datas = datas.Where(a => a.TOrderProduct.Any(b => b.ProductName.Contains(productName)));

                result.datas = await datas.Skip(pageSize * (page - 1)).Take(pageSize).ToListAsync();
            }

            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                result.code = -1;
                result.msg = "服务内部错误";
            }

            return result;
        }


        /// <summary>
        /// 生成订单号
        /// </summary>
        /// <returns></returns>
        private string CreateOrderId()
        {
            return DateTime.Now.ToString("yyMMddHH") + Strings.GetRandomNumberString(24);
        }
    }
}
