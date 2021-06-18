using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request.Wx
{
    /// <summary>
    /// 微信小程序获取openid的参数
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
    }
}
