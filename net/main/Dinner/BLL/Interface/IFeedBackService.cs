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
    /// 反馈信息
    /// </summary>
    public interface IFeedBackService : IBaseService
    {
        RespDataList<TFeedback> GetList(string userCode);
    }
}
