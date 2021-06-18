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
    public interface IOrderService
    {
        public Task<RespDataList<TOrder>> GetListAsync(string openid, string productName, int pageSize, int page);

        public Task<RespData<TOrder>> GetEntity(string orderid, string openid);

        public Task<RespData<string>> AddAsync(OrderAdd data, string openid, string host);

        public Task<RespData> DeleteAsync(string orderid, string openid);

        public Task<RespData> CancelAsync(string orderid, string openid);

        /// <summary>
        /// 根据用户的openid获取当前需要取的餐品信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public Task<RespDataList<TOrderProduct>> GetTodayOrderAsync(string openid);

        public Task<RespData> AddCommentAsync(string orderid, string comment);

    }
}
