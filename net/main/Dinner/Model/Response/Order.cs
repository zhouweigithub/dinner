using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Response
{
    /// <summary>
    /// 订单信息
    /// </summary>
    public class OrderResp
    {
        /// <summary>
        /// 订单id
        /// </summary>
        public string Orderid { get; set; }
        /// <summary>
        /// 订单金总额
        /// </summary>
        public decimal Money { get; set; }
        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal CouponMoney { get; set; }
        /// <summary>
        /// 实际支付金额
        /// </summary>
        public decimal PayMoney { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Crtime { get; set; }

        /// <summary>
        /// 商品信息
        /// </summary>
        public List<OrderProductResp> Products { get; set; }

    }

    /// <summary>
    /// 订单中的商品信息
    /// </summary>
    public class OrderProductResp
    {
        /// <summary>
        /// 商品id
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal Money => Price * Count;
    }
}
