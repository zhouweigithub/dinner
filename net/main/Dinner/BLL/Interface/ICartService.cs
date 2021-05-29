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
        RespData<List<TCart>> GetList(String openid);

        Task<RespData> AddAsync(int openid, CartAdd t);
    }
}
