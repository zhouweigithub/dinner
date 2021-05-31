using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request
{

    /// <summary>
    /// 提交订单的参数
    /// </summary>
    public class OrderAdd
    {
        /// <summary>
        /// 订单金额
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
        /// 商品信息
        /// </summary>
        public List<ProductInfo> Products { get; set; }

        /// <summary>
        /// 优惠券信息
        /// </summary>
        public List<CouponInfo> Coupons { get; set; }
    }

    /// <summary>
    /// 商品信息
    /// </summary>
    public class ProductInfo
    {
        /// <summary>
        /// 商品id
        /// </summary>
        public int Productid { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal Money => Price * Count;
    }

    /// <summary>
    /// 优惠券信息
    /// </summary>
    public class CouponInfo
    {
        /// <summary>
        /// 优惠券id
        /// </summary>
        public int Couponid { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 优惠券面额
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 优惠券总金额
        /// </summary>
        public decimal Money => Price * Count;
    }
}
