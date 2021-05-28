using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Response.Com;

namespace BLL.Interface
{
    /// <summary>
    /// 订单信息
    /// </summary>
    public interface IOrderService : IBaseService
    {
        RespDataList<TOrder> GetList(string userCode);

        Task<RespData<TOrder>> SaveAsync(TOrder data);

        Task<RespData<TOrder>> GetEntityAsync(string orderid);

    }
}
