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
using Model.Response.Com;

namespace BLL
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly ILogger<OrderService> _logger;

        public OrderService(DbService context, ILogger<OrderService> logger) : base(context)
        {
            _logger = logger;
        }

        public Task<RespData<TOrder>> GetEntityAsync(String orderid)
        {
            throw new NotImplementedException();
        }

        public RespDataList<TOrder> GetList(String userCode)
        {
            throw new NotImplementedException();
        }

        public Task<RespData<TOrder>> SaveAsync(TOrder data)
        {
            throw new NotImplementedException();
        }
    }
}
