using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Database;
using Model.Response.Com;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace BLL.Interface
{
    /// <summary>
    /// 支付信息
    /// </summary>
    [TransientService]
    public interface IPayService
    {
        public Task<RespDataList<TOrder>> PayAsync(string userCode);

    }
}
