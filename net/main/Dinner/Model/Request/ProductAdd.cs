using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request
{
    /// <summary>
    /// 添加商品参数
    /// </summary>
    public class ProductAdd
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 商品分类
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 销量
        /// </summary>
        public int Sales { get; set; }
        /// <summary>
        /// 商品图片
        /// </summary>
        public string Img { get; set; }
    }
}
