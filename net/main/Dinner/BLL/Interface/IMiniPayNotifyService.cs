﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Request.Wx;
using Model.Response.Com;
using Model.Response.Wx;

namespace BLL.Interface
{
    public interface IMiniPayNotifyService
    {
        public Task<RespData> ReceiveWxPayNotyfy(WxPayNotify notifyInfo, MiniPayDeSignPara para);
    }
}
