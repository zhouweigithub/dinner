using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Response.Com;
using Model.Database;

namespace BLL.Interface
{
    /// <summary>
    /// 消息中心
    /// </summary>
    public interface IMessageService : IBaseService
    {
        public Task<RespDataList<TMessage>> GetListAsync(string openid);
    }
}
