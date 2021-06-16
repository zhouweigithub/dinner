using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Model.Request.Wx;
using Model.Response.Com;
using Model.Response.Wx;
using ZqUtils.Core.Helpers;

namespace BLL
{
    public class MiniPayNotifyService
    {
        //文档地址 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter3_5_3.shtml

        private readonly ILogger<MiniPayNotifyService> _logger;

        public MiniPayNotifyService(ILogger<MiniPayNotifyService> logger)
        {
            _logger = logger;
        }

        public void ReceivePayNotyfy(string notifyContent)
        {
            try
            {
                WxPayNotify notifyInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<WxPayNotify>(notifyContent);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }

        }

    }
}
