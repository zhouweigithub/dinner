using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request
{
    /// <summary>
    /// 公司信息
    /// </summary>
    public class CompanyAdd
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 公司地址
        /// </summary>
        public string Address { get; set; }
    }
}
