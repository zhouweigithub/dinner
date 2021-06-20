using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Response
{
    /// <summary>
    /// 供货商任务
    /// </summary>
    public class SupplierTask
    {
        /// <summary>
        /// 商品id
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 类别 0普通 1早餐 2午餐 3晚餐
        /// </summary>
        public int Type { get; set; }
    }
}
