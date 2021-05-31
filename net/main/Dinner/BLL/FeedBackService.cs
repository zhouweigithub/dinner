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
using Model.Response.Com;

namespace BLL
{
    public class FeedBackService : BaseService, IFeedBackService
    {
        private readonly ILogger<FeedBackService> _logger;

        public FeedBackService(DbService context, ILogger<FeedBackService> logger) : base(context)
        {
            _logger = logger;
        }

        public Task<RespData> AddAsync(FeedbackAdd data)
        {
            throw new NotImplementedException();
        }


        public Task<RespDataList<TFeedback>> GetListAsync(String openid)
        {
            throw new NotImplementedException();
        }
    }
}
