using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.EasyCaching;
using BLL.Interface;
using DAL;
using Microsoft.Extensions.Logging;
using Model.Database;
using Model.Response.Com;

namespace BLL
{
    public class MessageService : BaseService, IMessageService
    {
        private readonly ILogger<MessageService> _logger;
        private readonly ICache _iCache;

        public MessageService(DbService context, ILogger<MessageService> logger, ICache cache) : base(context, logger)
        {
            _logger = logger;
            _iCache = cache;
        }

        public Task<RespDataList<TMessage>> GetListAsync(String openid)
        {
            throw new NotImplementedException();
        }
    }
}
