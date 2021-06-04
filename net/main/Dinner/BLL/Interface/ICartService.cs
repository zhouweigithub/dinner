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
        public Task<RespDataList<TCart>> GetListAsync(String openid);

        public Task<RespData> AddAsync(string openid, CartAdd data);

        public Task<RespData> UpdateCountAsync(string openid, CartUpdate data);

        public Task<RespData> DeleteProductsAsync(string openid, CartDelete data);
    }
}
