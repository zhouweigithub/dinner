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

        public async Task<RespDataList<TUserCoupon>> GetListAsync(String userCode)
        {
            RespDataList<TUserCoupon> result = new RespDataList<TUserCoupon>();

            try
            {
                int userid = GetUserIdByCode(userCode);
                var datas = context.Set<TUserCoupon>().AsNoTracking().Include(a => a.Coupon).Where(a => a.Userid == userid);
                result.datas = await datas.ToListAsync();
            }

            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                result.code = -1;
                result.msg = "服务内部错误";
            }

            return result;
        }
    }
}
