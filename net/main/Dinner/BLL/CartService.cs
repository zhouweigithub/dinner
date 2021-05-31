using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;
using DAL;
using Microsoft.Extensions.Logging;
using Model.Database;
using Model.Request;
using Model.Response.Com;

namespace BLL
{
    public class CartService : BaseService, ICartService
    {
        private readonly ILogger<CartService> _logger;

        public CartService(DbService context, ILogger<CartService> logger) : base(context)
        {
            _logger = logger;
        }

        public Task<RespData> AddAsync(Int32 openid, CartAdd t)
        {
            throw new NotImplementedException();
        }

        public Task<RespData> AddAsync(CartAdd data)
        {
            throw new NotImplementedException();
        }

        public Task<RespData> DeleteAsync(CartDelete data)
        {
            throw new NotImplementedException();
        }

        public Task<RespData<List<TCart>>> GetListAsync(String openid)
        {
            throw new NotImplementedException();
        }
    }
}
