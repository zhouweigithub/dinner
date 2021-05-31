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
    public class PayService : BaseService, IPayService
    {
        private readonly ILogger<PayService> _logger;

        public PayService(DbService context, ILogger<PayService> logger) : base(context, logger)
        {
            _logger = logger;
        }

        public Task<RespDataList<TOrder>> PayAsync(String userCode)
        {
            throw new NotImplementedException();
        }
    }
}
