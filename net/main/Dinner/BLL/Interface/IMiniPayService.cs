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
    /// <summary>
    /// 微信小程序支付功能
    /// </summary>
    [TransientService]
    public interface IMiniPayService
    {
        /// <summary>
        /// 预支付
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public RespData<string> PreMiniPay(string orderid, string host);
        /// <summary>
        /// 关闭订单
        /// </summary>
        /// <param name="out_trade_no"></param>
        /// <returns></returns>
        public RespData ClosePay(string out_trade_no);
        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="transaction_id"></param>
        /// <returns></returns>
        public RespData<QueryWxPayResp> QueryPay(string transaction_id);
    }
}
