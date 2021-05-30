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
    }
}
