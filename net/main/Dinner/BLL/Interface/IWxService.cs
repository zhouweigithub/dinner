using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Request.Wx;
using Model.Response.Com;

namespace BLL.Interface
{
    public interface IWxService
    {
        public RespData<string> GetOpenId(string loginCode, WxOpenidConfigModel config);
    }
}
