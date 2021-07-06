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
    /// 个人中心
    /// </summary>
    [TransientService]
    public interface IMyCenter
    {
        //Task<RespDataList<>> GetListAsync(string openid);
    }
}
