using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Response.Wx
{
    /// <summary>
    /// 微信支付结果通知
    /// </summary>
    public class WxPayNotify
    {
        /// <summary>
        /// 通知ID	
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 通知创建时间	
        /// </summary>
        public string create_time { get; set; }
        /// <summary>
        /// 通知类型
        /// </summary>
        public string event_type { get; set; }
        /// <summary>
        /// 通知数据类型
        /// </summary>
        public string resource_type { get; set; }
        /// <summary>
        /// 通知数据
        /// </summary>
        public WxPayNotifyDetail resource { get; set; }
        /// <summary>
        /// 回调摘要
        /// </summary>
        public string summary { get; set; }
    }

    /// <summary>
    /// 通知数据
    /// </summary>
    public class WxPayNotifyDetail
    {
        /// <summary>
        /// 加密算法类型	
        /// </summary>
        public string algorithm { get; set; }
        /// <summary>
        /// 数据密文
        /// </summary>
        public string ciphertext { get; set; }
        /// <summary>
        /// 附加数据
        /// </summary>
        public string associated_data { get; set; }
        /// <summary>
        /// 原始类型
        /// </summary>
        public string original_type { get; set; }
        /// <summary>
        /// 随机串
        /// </summary>
        public string nonce { get; set; }
    }


    /// <summary>
    /// 通知解密后的有效数据
    /// </summary>
    public class WxPayNotifyData
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        public string mchid { get; set; }
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 微信支付订单号
        /// </summary>
        public string transaction_id { get; set; }
        /// <summary>
        /// 交易类型
        /// </summary>
        public string trade_type { get; set; }
        /// <summary>
        /// 交易状态
        /// </summary>
        public string trade_state { get; set; }
        /// <summary>
        /// 交易状态描述
        /// </summary>
        public string trade_state_desc { get; set; }
        /// <summary>
        /// 付款银行
        /// </summary>
        public string bank_type { get; set; }
        /// <summary>
        /// 附加数据，在查询API和支付通知中原样返回，可作为自定义参数使用
        /// </summary>
        public string attach { get; set; }
        /// <summary>
        /// 支付完成时间
        /// </summary>
        public string success_time { get; set; }
        /// <summary>
        /// 支付者
        /// </summary>
        public WxPayNotifyPayer payer { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public WxPayNotifyAmount amount { get; set; }
        /// <summary>
        /// 场景信息
        /// </summary>
        public WxPayNotifySceneInfo scene_info { get; set; }
    }

    /// <summary>
    /// 支付者
    /// </summary>
    public class WxPayNotifyPayer
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        public string openid { get; set; }
    }


    /// <summary>
    /// 订单金额
    /// </summary>
    public class WxPayNotifyAmount
    {
        /// <summary>
        /// 总金额
        /// </summary>
        public string total { get; set; }
        /// <summary>
        /// 用户支付金额
        /// </summary>
        public string payer_total { get; set; }
        /// <summary>
        /// 货币类型
        /// </summary>
        public string currency { get; set; }
        /// <summary>
        /// 用户支付币种
        /// </summary>
        public string payer_currency { get; set; }
    }

    /// <summary>
    /// 场景信息
    /// </summary>
    public class WxPayNotifySceneInfo
    {
        /// <summary>
        /// 商户端设备号
        /// </summary>
        public string device_id { get; set; }
    }
}
