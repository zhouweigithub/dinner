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
    public class MyCenter : BaseService, IMyCenter
    {
        private readonly ILogger<MyCenter> _logger;

        public MyCenter(DbService context, ILogger<MyCenter> logger) : base(context, logger)
        {
            _logger = logger;
        }
    }
}
