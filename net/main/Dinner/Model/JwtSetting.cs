using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    /// <summary>
    /// JWT配置参数
    /// </summary>
    public class JwtSetting
    {
        /// <summary>
        /// 颁发者
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// 可以给哪些客户端使用
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 秘钥
        /// </summary>
        public string SecretKey { get; set; }
    }
}
