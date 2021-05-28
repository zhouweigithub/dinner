using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request
{
    /// <summary>
    /// 注册用户时提交的信息
    /// </summary>
    public class UserAdd
    {
        /// <summary>
        /// 微信OPENID
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCode { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string Nick { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string HeadIng { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

    }
}
