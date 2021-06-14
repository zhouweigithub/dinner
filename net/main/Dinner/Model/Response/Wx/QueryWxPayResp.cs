using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Response.Wx
{

    /// <summary>
    /// 微信支付查询结果信息
    /// </summary>
    public class QueryWxPayResp
    {
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 微信支付订单号
        /// </summary>
        public string transaction_id { get; set; }
        /// <summary>
        /// 交易状态
        /// </summary>
        public string trade_state { get; set; }
        /// <summary>
        /// 交易状态描述
        /// </summary>
        public string trade_state_desc { get; set; }
        /// <summary>
        /// 支付完成时间
        /// </summary>
        public string success_time { get; set; }
        /// <summary>
        /// 用户标识
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 用户支付金额，单位为分
        /// </summary>
        public string payer_total { get; set; }
    }
}
