using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Database;
using Model.Request;
using Model.Response.Com;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace BLL.Interface
{
    /// <summary>
    /// 反馈信息
    /// </summary>
    [TransientService]
    public interface IFeedBackService
    {
        public Task<RespDataList<TFeedback>> GetListAsync(string openid);

        public Task<RespData> AddAsync(string openid, FeedbackAdd data);
    }
}
