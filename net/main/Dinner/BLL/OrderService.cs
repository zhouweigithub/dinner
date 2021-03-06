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
using Microsoft.EntityFrameworkCore;
using ZwUtil;

namespace BLL
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IMiniPayService _payService;

        public OrderService(DbService context, ILogger<OrderService> logger, IMiniPayService payService) : base(context, logger)
        {
            _logger = logger;
            _payService = payService;
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="data">订单信息</param>
        /// <param name="openid">openid</param>
        /// <param name="host">域名信息</param>
        /// <returns></returns>
        public async Task<RespData<string>> AddAsync(OrderAdd data, string openid, string host)
        {
            RespData<string> result = new RespData<string>();

            try
            {
                //订单号
                string orderid = CreateOrderId();

                int userid = GetUserIdByCode(openid);

                //商品信息
                var productids = data.Products.Select(a => a.Productid);

                //服务器上存储的商品信息
                var productDatas = context.Set<TProduct>().Where(a => productids.Contains(a.Id)).ToList();


                //需要分早中晚送货的餐饮商品
                List<ProductInfo> foodProducts = new List<ProductInfo>();

                foreach (ProductInfo item in data.Products)
                {
                    //验证提交的商品信息与服务器上的是否一致
                    TProduct productData = productDatas.FirstOrDefault(a => a.Id == item.Productid);
                    if (productData == null || productData.Price != item.Price)
                    {
                        result.code = -2;
                        result.msg = "创建订单失败：订单商品信息异常，请重新下单";
                        result.data = null;
                        return result;
                    }

                    TOrderProduct t = new TOrderProduct()
                    {
                        Orderid = orderid,
                        Productid = item.Productid,
                        Count = item.Count,
                        Money = item.Money,
                        Price = item.Price,
                        ProductName = productData.Name,
                        Type = item.Type,
                        Img = productData.Img
                    };

                    await context.Set<TOrderProduct>().AddAsync(t);


                    //收集餐饮商品
                    if (productData.Type == 1)
                    {
                        foodProducts.Add(item);
                    }

                    //商品销量
                    productData.Sales++;
                    context.Update(productData);
                }


                //优惠券信息
                var couponids = data.Coupons.Select(a => a.Couponid);

                //服务器上存储的优惠券信息
                var couponDatas = context.Set<TCoupon>().Where(a => couponids.Contains(a.Id)).ToList();

                //用户的优惠券信息
                var userCoupons = context.Set<TUserCoupon>().Where(a => a.Userid == userid).ToList();


                foreach (CouponInfo item in data.Coupons)
                {
                    //验证提交的优惠券信息与服务器上的是否一致
                    TCoupon couponData = couponDatas.FirstOrDefault(a => a.Id == item.Couponid);
                    TUserCoupon userCoupon = userCoupons.FirstOrDefault(a => a.Userid == userid && a.Couponid == item.Couponid && a.Count > 0);

                    if (couponData == null || userCoupon == null || couponData.Money != couponData.Money)
                    {
                        result.code = -3;
                        result.msg = "创建订单失败：优惠券信息异常，请重新下单";
                        result.data = null;
                        return result;
                    }
                    else if (couponData.StartTime > DateTime.Now || couponData.EndTime < DateTime.Now)
                    {
                        result.code = -4;
                        result.msg = "创建订单失败：优惠券未在有效使用期内，请重新下单";
                        result.data = null;
                        return result;
                    }

                    TOrderCoupon t = new TOrderCoupon()
                    {
                        Orderid = orderid,
                        Couponid = item.Couponid,
                        Count = item.Count,
                        Price = item.Price,
                        Money = item.Money,
                        CouponName = couponData.Name
                    };

                    await context.Set<TOrderCoupon>().AddAsync(t);


                    //优惠券数量
                    userCoupon.Count--;
                    context.Update(userCoupon);
                }


                //订单信息
                //校验优惠总金额
                decimal allCouponMoney = data.Coupons.Sum(a => a.Money);
                decimal allProductMoney = data.Products.Sum(a => a.Money);

                if (data.Money != allProductMoney || data.CouponMoney != allCouponMoney || data.PayMoney != allProductMoney - allCouponMoney)
                {
                    result.code = -5;
                    result.msg = "创建订单失败：订单信息异常，请重新下单";
                    result.data = null;
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
                    Phone = data.Phone,
                    Crtime = DateTime.Now,
                };

                await context.Set<TOrder>().AddAsync(order);

                await context.SaveChangesAsync();



                //给订单中的商品分配供货商
                //此方法会异步执行，因为无返回值无法await
                SetProductSupplier(orderid, userid, foodProducts);



                //预支付
                var preInfo = _payService.PreMiniPay(orderid, host);

                //预支付参数
                result.data = preInfo.data;
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "创建订单失败：服务内部错误";
                result.data = string.Empty;
                _logger.LogError(e.ToString());
            }

            return result;
        }

        /// <summary>
        /// 添加订单评论
        /// </summary>
        /// <param name="orderid">订单id</param>
        /// <param name="comment">评论内容</param>
        /// <returns></returns>
        public async Task<RespData> AddCommentAsync(String orderid, String comment)
        {
            RespData result = new RespData();

            try
            {
                context.Set<TComment>().Add(new TComment()
                {
                    Orderid = orderid,
                    Msg = comment,
                    Crtime = DateTime.Now,
                });

                await context.SaveChangesAsync();
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
        /// 取消订单
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="openid"></param>
        /// <returns></returns>
        public async Task<RespData> CancelAsync(String orderid, string openid)
        {
            RespData result = new RespData();

            try
            {
                int userid = GetUserIdByCode(openid);

                TOrder serverOrderInfo = await context.Set<TOrder>().FindAsync(orderid);

                //订单不存在或者订单用户不正确
                if (serverOrderInfo == null || serverOrderInfo.Userid != userid)
                {
                    result.code = -2;
                    result.msg = "订单信息异常，请重试";
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

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="openid"></param>
        /// <returns></returns>
        public async Task<RespData> DeleteAsync(string orderid, string openid)
        {
            RespData result = new();
            try
            {
                int userid = GetUserIdByCode(openid);
                var order = context.Set<TOrder>().Find(orderid);
                if (order.Userid != userid)
                {
                    result.code = -1;
                    result.msg = "订单信息异常，请重试";
                }
                else
                {
                    if (order.State == 0)
                    {
                        //未支付的订单直接删除
                        context.Remove(new TOrderProduct() { Orderid = orderid });
                        context.Remove(new TOrderCoupon() { Orderid = orderid });
                        context.Remove(new TOrder() { Id = orderid });
                    }
                    else
                    {
                        //已支付过的订单只能修改其状态
                        order.State = 10;
                        context.Update(order);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                _logger.LogError(e.ToString());
            }

            return result;
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="productName"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<RespDataList<TOrder>> GetListAsync(string openid, String productName, Int32 pageSize, Int32 page)
        {
            RespDataList<TOrder> result = new RespDataList<TOrder>();

            try
            {
                int userid = GetUserIdByCode(openid);
                var datas = context.Set<TOrder>().AsNoTracking().Include(a => a.TOrderProduct).Include(b => b.TOrderCoupon).Where(a => a.Userid == userid);
                if (!string.IsNullOrWhiteSpace(productName))
                    datas = datas.Where(a => a.TOrderProduct.Any(b => b.ProductName.Contains(productName)));

                result.datas = await datas.Skip(pageSize * (page - 1)).Take(pageSize).ToListAsync();
            }

            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                result.code = -1;
                result.msg = "服务内部错误";
                result.datas = new List<TOrder>();
            }

            return result;
        }

        /// <summary>
        /// 查询订单信息
        /// </summary>
        /// <param name="orderid">订单号</param>
        /// <param name="openid">用户代码</param>
        /// <returns></returns>
        public async Task<RespData<TOrder>> GetEntity(string orderid, string openid)
        {
            RespData<TOrder> result = new RespData<TOrder>();

            try
            {
                int userid = GetUserIdByCode(openid);
                var data = await context.Set<TOrder>().AsNoTracking().Include(a => a.TOrderProduct).Include(b => b.TOrderCoupon).FirstOrDefaultAsync(a => a.Id == orderid && a.Userid == userid);

                result.data = data;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                result.code = -1;
                result.msg = "服务内部错误";
                result.data = null;
            }

            return result;
        }

        /// <summary>
        /// 根据用户的openid获取当前需要取的餐品信息
        /// </summary>
        /// <param name="openid">openid</param>
        /// <returns></returns>
        public async Task<RespDataList<TOrderProduct>> GetTodayOrderAsync(String openid)
        {
            RespDataList<TOrderProduct> result = new RespDataList<TOrderProduct>();

            try
            {
                DateTime now = DateTime.Now;

                //11点前为早餐 16点前为午餐 其他为晚餐
                int type = now.Hour < 11 ? 1 : now.Hour < 16 ? 2 : 3;
                int userid = GetUserIdByCode(openid);

                //该用户当天的所有订单
                //todo:应该限制一下商品类型，仅为餐品
                var orders = await context.Set<TOrder>().AsNoTracking().Include(a => a.TOrderProduct).Where(a => a.Userid == userid && a.Crdate == DateTime.Today).ToListAsync();

                List<TOrderProduct> orderProducts = new List<TOrderProduct>();
                foreach (var item in orders)
                {
                    var lst = item.TOrderProduct.Where(a => a.Type == type);

                    //对同名商品进行合并（数量叠加）
                    foreach (var iitem in lst)
                    {
                        var tmpProduct = orderProducts.FirstOrDefault(a => a.ProductName == iitem.ProductName);
                        if (tmpProduct != null)
                        {
                            //如果已存在该商品，就对数量进行累加
                            tmpProduct.Count += iitem.Count;
                        }
                        else
                        {
                            //如果不存在该商品，就添加进去
                            orderProducts.Add(iitem);
                        }
                    }
                }

                result.datas = orderProducts;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                result.code = -1;
                result.msg = "服务内部错误";
                result.datas = new List<TOrderProduct>();
            }

            return result;
        }



        /// <summary>
        /// 给订单中的商品分配供货商
        /// </summary>
        /// <param name="orderid">订单id</param>
        /// <param name="userid">用户id</param>
        /// <param name="products">订单中的餐饮商品信息</param>
        private async void SetProductSupplier(string orderid, int userid, List<ProductInfo> products)
        {
            try
            {
                //用户详情
                var userInfo = await context.Set<TUser>().Include(a => a.Company).FirstAsync(b => b.Id == userid);


                //所有商品id集
                var productids = products.Select(a => a.Productid).ToList();

                //部分商品的指定供货商
                var specialSuppliers = await context.Set<RProductSuplier>().AsNoTracking().Where(a => productids.Contains(a.Productid) && a.StartDate <= DateTime.Today && a.EndDate >= DateTime.Today).ToListAsync();

                foreach (var item in products)
                {
                    //需要查找的供货商id
                    int supplierid = 0;

                    //先分配指定了供货商的商品
                    var special = specialSuppliers.FirstOrDefault(a => a.Productid == item.Productid);
                    if (special != null)
                    {
                        supplierid = special.Suplierid;
                    }
                    else
                    {
                        //用户公司分配的供货商信息
                        var supplierInfo = context.Set<RCompanySupplier>().AsNoTracking().Where(a => a.Companyid == userInfo.Companyid && a.StartDate <= DateTime.Today && a.EndDate >= DateTime.Today).FirstOrDefaultAsync();

                        if (supplierInfo == null)
                        {
                            //查找历史记录中最近一次的供货商
                            var lastSupplier = await context.Set<HisCompanySupplier>().AsNoTracking().Where(a => a.Companyid == userInfo.Companyid).OrderByDescending(a => a.Crdate).FirstOrDefaultAsync();

                            if (lastSupplier != null)
                            {
                                supplierid = lastSupplier.Suplierid;
                            }
                            else
                            {
                                //历史记录也没数据，就随机分配一个供货商
                                var randSupplier = await context.Set<SpUser>().AsNoTracking().Where(a => a.State == 0).OrderBy(b => Guid.NewGuid()).FirstOrDefaultAsync();

                                if (randSupplier == null)
                                {
                                    logger.LogError("给订单中商品随机分配供货商时，未找到有效供货商");
                                }
                                else
                                {
                                    supplierid = randSupplier.Id;
                                }
                            }

                            //未找到供货商
                            logger.LogError("发现公司未分配供货商，公司ID：{0} 公司名称：{1}", userInfo.Companyid, userInfo.Company.Name);
                        }
                        else
                        {
                            //已分配供货商
                        }
                    }


                    //分配订单商品与供货商关系
                    await AddAsync(new ROrderproductSupplier()
                    {
                        Orderid = orderid,
                        Productid = item.Productid,
                        Supplierid = supplierid,
                        State = 0,
                        Msg = string.Empty
                    });


                    //公司与对应供货商的历史记录
                    //历史记录
                    var hisData = await context.Set<HisCompanySupplier>().AsNoTracking().FirstOrDefaultAsync(a => a.Companyid == userInfo.Companyid && a.Crdate == DateTime.Today);

                    //不存在则添加历史记录
                    if (hisData == null)
                    {
                        await AddAsync(new HisCompanySupplier()
                        {
                            Companyid = userInfo.Companyid,
                            Suplierid = supplierid,
                            Crdate = DateTime.Today
                        });
                    }
                }

            }
            catch (Exception e)
            {
                _logger.LogError("给订单中的商品分配供货商时出错！orderid:{0}, userid:{1}\r\n{2}", orderid, userid, e.ToString());
            }
        }


        /// <summary>
        /// 生成订单号
        /// </summary>
        /// <returns></returns>
        private string CreateOrderId()
        {
            return DateTime.Now.ToString("yyMMddHH") + Strings.GetRandomNumberString(8);
        }
    }
}
