using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Model.Request.Wx;
using Model.Response.Com;
using Model.Response.Wx;
using ZwUtil;

namespace Api.Controllers
{
    /// <summary>
    /// 支付相关功能
    /// </summary>
    [Route("[controller]")]
    public class PayController : BaseAuthController
    {
        private readonly IMiniPayService _services;
        private readonly IOptions<WxOpenidConfigModel> _wxconfig;

        public PayController(IMiniPayService service, IOptions<WxOpenidConfigModel> wxconfig)
        {
            _services = service;
            _wxconfig = wxconfig;
        }

        /// <summary>
        /// 生成微信预支付订单
        /// </summary>
        /// <param name="orderid">商户订单号</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public RespData<string> PreMiniPay(string orderid)
        {
            string hostInfo = GetHostInfo();
            return _services.PreMiniPay(orderid, hostInfo);
        }

        /// <summary>
        /// 关闭微信支付订单
        /// </summary>
        /// <param name="orderid">商户订单号</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public RespData CloseMiniPay(string orderid)
        {
            return _services.ClosePay(orderid);
        }

        /// <summary>
        /// 查询微信支付订单
        /// </summary>
        /// <param name="transaction_id">微信订单号</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{transaction_id}")]
        public RespData<QueryWxPayResp> QueryMiniPay(string transaction_id)
        {
            return _services.QueryPay(transaction_id);
        }

    }
}
