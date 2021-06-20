using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request
{
    /// <summary>
    /// 供货商添加参数
    /// </summary>
    public class SpUserAdd
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 供货商名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 供货商地址
        /// </summary>
        public string Address { get; set; }
    }
}
