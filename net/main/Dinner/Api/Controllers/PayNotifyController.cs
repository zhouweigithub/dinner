using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;
using Microsoft.AspNetCore.Mvc;
using Model.Request.Wx;
using Model.Response.Com;
using Model.Response.Wx;

namespace Api.Controllers
{
    /// <summary>
    /// 微信支付回调功能
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class PayNotifyController : ControllerBase
    {
        private readonly IMiniPayNotifyService _services;


        public PayNotifyController(IMiniPayNotifyService payService)
        {
            _services = payService;
        }


        //https://pay.weixin.qq.com/wiki/doc/apiv3/wechatpay/wechatpay3_3.shtml
        //需要签名与验证签名

        /// <summary>
        /// 接收微信小程序支付回调数据
        /// </summary>
        /// <param name="notifyInfo">回调数据</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<RespData> ReceiveWxPayNotyfy(WxPayNotify notifyInfo)
        {
            MiniPayDeSignPara data = GetSignInfo();
            return await _services.ReceiveWxPayNotyfy(notifyInfo, data);
        }


        /// <summary>
        /// 获取验证签名需要的参数
        /// </summary>
        /// <returns></returns>
        private MiniPayDeSignPara GetSignInfo()
        {
            byte[] bytes = new byte[Request.Body.Length];
            Request.Body.Read(bytes, 0, (int)Request.Body.Length);
            string postData = Encoding.UTF8.GetString(bytes);

            return new MiniPayDeSignPara()
            {
                WechatpayTimestamp = Request.Headers["Wechatpay-Timestamp"],
                ResponseBody = postData,
                WechatpayNonce = Request.Headers["Wechatpay-Nonce"],
                WechatpaySerial = Request.Headers["Wechatpay-Serial"],
                WechatpaySignature = Request.Headers["Wechatpay-Signature"],
            };
        }

    }
}
