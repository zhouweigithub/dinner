using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class CouponService : BaseService, ICouponService
    {
        private readonly ILogger<CouponService> _logger;

        public CouponService(DbService context, ILogger<CouponService> logger) : base(context, logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 添加优惠券
        /// </summary>
        /// <param name="data">参数</param>
        /// <returns></returns>
        public async Task<RespData<TCoupon>> AddAsync(CouponAdd data)
        {
            RespData<TCoupon> result = new();
            try
            {
                //先检查是否已存在同名优惠券
                var tmpCoupon = context.Set<TCoupon>().FirstOrDefault(a => a.Name == data.Name);
                if (tmpCoupon == null)
                {
                    var t = new TCoupon()
                    {
                        Name = data.Name,
                        Money = data.Money,
                        Crtime = DateTime.Now,
                        StartTime = data.StartTime,
                        EndTime = data.EndTime,
                    };

                    result.data = await AddAsync(t);
                }
                else
                {
                    result.code = -1;
                    result.msg = "该优惠券已存在";
                    result.data = null;
                }
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                result.data = null;
                _logger.LogError(e.ToString());
            }

            return result;
        }

        /// <summary>
        /// 删除优惠券
        /// </summary>
        /// <param name="couponid">优惠券id</param>
        /// <returns></returns>
        public async Task<RespData> DeleteAsync(Int32 couponid)
        {
            RespData result = new();
            try
            {
                context.Remove(new TCoupon() { Id = couponid });
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

        /// <summary>
        /// 获取用户的所有优惠券
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public async Task<RespDataList<TUserCoupon>> GetListAsync(String openid)
        {
            RespDataList<TUserCoupon> result = new RespDataList<TUserCoupon>();

            try
            {
                int userid = GetUserIdByCode(openid);
                var datas = context.Set<TUserCoupon>().AsNoTracking().Include(a => a.Coupon).Where(a => a.Userid == userid);
                result.datas = await datas.ToListAsync();
            }

            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                result.code = -1;
                result.msg = "服务内部错误";
                result.datas = new List<TUserCoupon>();
            }

            return result;
        }

        /// <summary>
        /// 给用户分配优惠券
        /// </summary>
        /// <param name="couponid">优惠券id</param>
        /// <param name="openid">用户码</param>
        /// <param name="count">优惠券数量</param>
        /// <returns></returns>
        public async Task<RespData> GrantToUserAsync(Int32 couponid, String openid, int count)
        {
            RespData result = new();
            try
            {
                int userid = GetUserIdByCode(openid);
                context.Set<TUserCoupon>().Add(new TUserCoupon()
                {
                    Userid = userid,
                    Couponid = couponid,
                    Count = count,
                    Crtime = DateTime.Now
                });
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
