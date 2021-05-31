using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Database;
using Model.Request;
using Model.Response.Com;

namespace BLL.Interface
{
    /// <summary>
    /// 购物车
    /// </summary>
    public interface ICartService : IBaseService
    {
        public Task<RespData<List<TCart>>> GetListAsync(String openid);

        public Task<RespData> AddAsync(string openid, CartAdd data);

        public Task<RespData> DeleteAsync(string openid, CartDelete data);
    }
}
