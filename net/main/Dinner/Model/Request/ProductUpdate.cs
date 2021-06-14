using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request
{
    /// <summary>
    /// 修改商品参数
    /// </summary>
    public class ProductUpdate : ProductAdd
    {
        /// <summary>
        /// 商品id
        /// </summary>
        public int Id { get; set; }
    }
}
