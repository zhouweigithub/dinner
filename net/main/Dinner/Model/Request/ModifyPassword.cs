using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request
{
    /// <summary>
    /// 修改密码参数
    /// </summary>
    public class ModifyPassword
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int Userid { get; set; }
        /// <summary>
        /// 旧密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPassword { get; set; }

    }
}
