using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request
{
    /// <summary>
    /// 更新公司参数
    /// </summary>
    public class CompanyUpdate : CompanyAdd
    {
        /// <summary>
        /// 公司id
        /// </summary>
        public int Id { get; set; }
    }
}
