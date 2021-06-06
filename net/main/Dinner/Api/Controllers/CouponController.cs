using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interface;
using Microsoft.AspNetCore.Mvc;
using Model.Database;
using Model.Request;
using Model.Response.Com;

namespace Api.Controllers
{
    /// <summary>
    /// 优惠券信息
    /// </summary>
    [Route("[controller]")]
    public class CouponController : BaseAuthController
    {
        private readonly ICouponService _services;

        public CouponController(ICouponService service)
        {
            _services = service;
        }

        /// <summary>
        /// 添加优惠券
        /// </summary>
        /// <param name="data">优惠券信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public Task<RespData<TCoupon>> Add(CouponAdd data)
        {
            return _services.AddAsync(data);
        }

        /// <summary>
        /// 给用户分配优惠券
        /// </summary>
        /// <param name="couponid">优惠券id</param>
        /// <param name="openid">用户码</param>
        /// <param name="count">优惠券数量</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<RespData> GrantToUser(int couponid, string openid, int count)
        {
            return await _services.GrantToUserAsync(couponid, openid, count);
        }

        /// <summary>
        /// 删除优惠券信息
        /// </summary>
        /// <param name="couponid">优惠券id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{couponid}")]
        public async Task<RespData> Delete(Int32 couponid)
        {
            return await _services.DeleteAsync(couponid);
        }

        /// <summary>
        /// 获取当前用户的优惠券信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<RespDataList<TUserCoupon>> GetList()
        {
            string openid = User.Identity.Name;
            return await _services.GetListAsync(openid);
        }

    }
}
