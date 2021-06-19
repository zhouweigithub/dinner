using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request.Wx
{
    /// <summary>
    /// 微信小程序相关参数
    /// </summary>
    public class WxOpenidConfigModel
    {
        /// <summary>
        /// AppId
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 密钥
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string Mchid { get; set; }

        /// <summary>
        /// 商户平台上设置的APIv3密钥
        /// </summary>
        public string AesKey { get; set; }

        /// <summary>
        /// 商户API证书中的私钥
        /// </summary>
        public string MchPrivateKey { get; set; }

        /// <summary>
        /// 商户API证书serial_no
        /// </summary>
        public string MchSerialNo { get; set; }

        /// <summary>
        /// 微信支付平台证书中的公钥
        /// </summary>
        public string PlatformPublicKey { get; set; }

        /// <summary>
        /// 微信支付平台证书中的序列号
        /// </summary>
        public string PlatformSerialNo { get; set; }
    }
}
