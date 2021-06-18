using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Model.Request.Wx;
using Model.Response.Com;
using Model.Response.Wx;
using ZqUtils.Core.Helpers;
using ZwUtil;

namespace BLL
{
    public class MiniPayNotifyService
    {
        //文档地址 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter3_5_3.shtml

        private readonly ILogger<MiniPayNotifyService> _logger;
        private readonly IOptions<WxOpenidConfigModel> _wxconfig;

        public MiniPayNotifyService(ILogger<MiniPayNotifyService> logger, IOptions<WxOpenidConfigModel> wxconfig)
        {
            _wxconfig = wxconfig;
            _logger = logger;
        }

        /// <summary>
        /// 接收并处理微信支付通知结果
        /// </summary>
        /// <param name="notifyContent">通知内容</param>
        public void ReceivePayNotyfy(string notifyContent)
        {
            try
            {
                //反序列化通知内容为对象实例
                WxPayNotify notifyInfo = JsonHelper.DeserializeObject<WxPayNotify>(notifyContent);

                if (notifyInfo == null)
                {
                    _logger.LogError("解析微信支付回调数据失败！原文：" + notifyContent);
                    return;
                };

                //解密后的JSON串
                string sourceJson = AesGcmHelper.AesGcmDecrypt(notifyInfo.resource.associated_data, notifyInfo.resource.nonce, notifyInfo.resource.ciphertext, _wxconfig.Value.AesKey);

                //解密后的数据
                WxPayNotifyData data = JsonHelper.DeserializeObject<WxPayNotifyData>(sourceJson);

                if (data == null)
                {
                    _logger.LogError("解密微信支付回调数据失败！原文：" + sourceJson);
                    return;
                }


            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }

        }

    }
}
