using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request.Wx
{
    /// <summary>
    /// 微信小程序预支付参数
    /// </summary>
    public class MiniPayStartPara
    {
        /// <summary>
        /// 由微信生成的应用ID，全局唯一。示例值：wxd678efh567hg6787
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 直连商户的商户号，由微信支付生成并下发。示例值：1230000109
        /// </summary>
        public string mchid { get; set; }
        /// <summary>
        /// 商品描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 商户系统内部订单号
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 通知地址。通知URL必须为直接可访问的URL，不允许携带查询串，要求必须为https地址。
        /// </summary>
        public string notify_url { get; set; }
        /// <summary>
        /// 订单总金额，单位为分
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 用户在直连商户appid下的唯一标识
        /// </summary>
        public string openid { get; set; }
    }
}
