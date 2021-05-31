using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Database;
using Model.Request;
using Model.Response.Com;

namespace BLL.Interface
{
    /// <summary>
    /// 反馈信息
    /// </summary>
    public interface IFeedBackService : IBaseService
    {
        public Task<RespDataList<TFeedback>> GetListAsync(string openid);

        public Task<RespData> AddAsync(FeedbackAdd data);
    }
}
