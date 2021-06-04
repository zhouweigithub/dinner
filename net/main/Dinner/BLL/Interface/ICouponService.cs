using System;
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
    /// 优惠券信息
    /// </summary>
    public interface ICouponService : IBaseService
    {
        public Task<RespDataList<TUserCoupon>> GetListAsync(string openid);

        public Task<RespData<TCoupon>> AddAsync(CouponAdd data);

        public Task<RespData> DeleteAsync(int couponid);

        public Task<RespData> GrantToUserAsync(int couponid, string openid, int count);

    }
}
