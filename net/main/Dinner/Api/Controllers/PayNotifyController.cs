using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interface;
using Microsoft.AspNetCore.Mvc;
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
            return await _services.ReceiveWxPayNotyfy(notifyInfo);
        }
    }
}
