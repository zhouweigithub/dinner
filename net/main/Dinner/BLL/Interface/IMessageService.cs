using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Response.Com;
using Model.Database;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace BLL.Interface
{
    /// <summary>
    /// 消息中心
    /// </summary>
    [TransientService]
    public interface IMessageService
    {
        public Task<RespDataList<TMessage>> GetListAsync(string openid);
    }
}
