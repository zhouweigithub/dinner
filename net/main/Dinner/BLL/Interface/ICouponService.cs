using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Database;
using Model.Response.Com;

namespace BLL.Interface
{
    /// <summary>
    /// 优惠券信息
    /// </summary>
    public interface ICouponService : IBaseService
    {
        public RespDataList<TCoupon> GetList(string openid);
    }
}
