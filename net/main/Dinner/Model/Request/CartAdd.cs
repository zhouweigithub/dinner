using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request
{
    /// <summary>
    /// 添加商品到购物车参数
    /// </summary>
    public class CartAdd
    {
        /// <summary>
        /// 商品id
        /// </summary>
        public int Productid { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int Count { get; set; }
    }


    /// <summary>
    /// 从购物车修改商品数量模型
    /// </summary>
    public class CartUpdate
    {
        /// <summary>
        /// 商品id
        /// </summary>
        public int Productid { get; set; }

        /// <summary>
        /// 商品数量
        /// </summary>
        public int Count { get; set; }
    }


    /// <summary>
    /// 购物车删除商品模型
    /// </summary>
    public class CartDelete
    {
        /// <summary>
        /// 商品id
        /// </summary>
        public List<int> Productids { get; set; }
    }

}
