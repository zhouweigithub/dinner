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
    public class CommentService : BaseService, ICommentService
    {
        private readonly ILogger<CommentService> _logger;

        public CommentService(DbService context, ILogger<CommentService> logger) : base(context, logger)
        {
            _logger = logger;
        }
    }
}
