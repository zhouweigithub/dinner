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
        public Task<RespDataList<TOrder>> GetListAsync(string productName, int pageSize, int page);

        //Task<RespData<TOrder>> GetEntityAsync();

        public Task<RespData<TOrder>> AddAsync(OrderAdd data, int userid);

        public Task<RespData<bool>> CancelAsync(string orderid, int userid);

    }
}
