using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;
using DAL;
using Microsoft.Extensions.Logging;

namespace BLL
{
    public class MessageService : BaseService, IMessageService
    {
        private readonly ILogger<MessageService> _logger;

        public MessageService(DbService context, ILogger<MessageService> logger) : base(context)
        {
            _logger = logger;
        }
    }
}
