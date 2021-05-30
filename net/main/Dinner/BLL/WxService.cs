using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;
using Microsoft.Extensions.Logging;
using Model.Request.Wx;
using Model.Response.Com;
using ZqUtils.Core.Helpers;
using ZqUtils.Core.WeChat.Helpers;

namespace BLL
{
    public class WxService : IWxService
    {
        private readonly ILogger<WxService> _logger;

        public WxService(ILogger<WxService> logger)
        {
            _logger = logger;
        }

        public RespData<String> GetOpenId(String loginCode, WxOpenidConfigModel config)
        {

            RespData<String> result = new RespData<string>();
            try
            {
                string url = "https://api.weixin.qq.com/sns/jscode2session?appid={0}&secret={1}&grant_type=authorization_code&js_code={2}";
                url = string.Format(url, config.AppId, config.AppSecret, loginCode);

                using var http = new HttpHelper(CreateRequest(url));
                result.data = http.GetResult().ResultString;
            }
            catch (Exception e)
            {
                result.code = -1;
                result.msg = "服务内部错误";
                _logger.LogError(e.ToString());
            }

            return result;
        }

        private HttpRequest CreateRequest(string url)
        {
            return new HttpRequest()
            {
                Url = url,
                HttpMethod = HttpMethod.Get,
                ContentType = "application/x-www-form-urlencoded",
                KeepAlive = true
            };
        }
    }
}
