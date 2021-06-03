using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Database;
using Model.Request;
using Model.Response;
using Model.Response.Com;

namespace BLL.Interface
{
    /// <summary>
    /// 订单信息
    /// </summary>
    public interface IOrderService : IBaseService
    {
        public Task<RespDataList<TOrder>> GetListAsync(string openid, string productName, int pageSize, int page);

        //Task<RespData<TOrder>> GetEntityAsync();

        public Task<RespData<TOrder>> AddAsync(OrderAdd data, string openid);

        public Task<RespData> CancelAsync(string orderid, string openid);

    }
}
