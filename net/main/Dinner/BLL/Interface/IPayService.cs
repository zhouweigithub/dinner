using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Database;
using Model.Response.Com;

namespace BLL.Interface
{
    /// <summary>
    /// 支付信息
    /// </summary>
    public interface IPayService : IBaseService
    {
        public Task<RespDataList<TOrder>> PayAsync(string userCode);

    }
}
