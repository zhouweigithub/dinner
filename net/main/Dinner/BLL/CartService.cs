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
    public class CartService : BaseService, ICartService
    {
        private readonly ILogger<CartService> _logger;

        public CartService(DbService context, ILogger<CartService> logger) : base(context)
        {
            _logger = logger;
        }
    }
}
