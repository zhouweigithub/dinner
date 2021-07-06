using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Request.Wx;
using Model.Response.Com;
using Model.Response.Wx;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace BLL.Interface
{
    [TransientService]
    public interface IMiniPayNotifyService
    {
        public Task<RespData> ReceiveWxPayNotyfy(WxPayNotify notifyInfo, MiniPayDeSignPara para);
    }
}
