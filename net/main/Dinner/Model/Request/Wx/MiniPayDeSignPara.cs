using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request.Wx
{
    /// <summary>
    /// 解析微信传递过来的支付请求签名信息
    /// </summary>
    public class MiniPayDeSignPara
    {
        /// <summary>
        /// 微信支付的平台证书序列号
        /// </summary>
        public string WechatpaySerial { get; set; }
        /// <summary>
        /// 应答时间戳
        /// </summary>
        public string WechatpayTimestamp { get; set; }
        /// <summary>
        /// 应答随机串
        /// </summary>
        public string WechatpayNonce { get; set; }
        /// <summary>
        /// 应答主体
        /// </summary>
        public string ResponseBody { get; set; }
        /// <summary>
        /// 微信支付的应答签名（Base64编码）
        /// </summary>
        public string WechatpaySignature { get; set; }
    }
}
