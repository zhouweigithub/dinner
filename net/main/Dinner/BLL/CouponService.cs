using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;
using DAL;
using Microsoft.Extensions.Logging;
using Model;
using Model.Response.Com;

namespace BLL
{
    public class CouponService : BaseService, ICouponService
    {
        private readonly ILogger<CouponService> _logger;

        public CouponService(DbService context, ILogger<CouponService> logger) : base(context)
        {
            _logger = logger;
        }

        public RespDataList<TCoupon> GetList(String userCode)
        {
            throw new NotImplementedException();
        }
    }
}
