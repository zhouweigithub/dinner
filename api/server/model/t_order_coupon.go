package model

type TOrderCoupon struct {
	OrderId	string	`gorm:"primary_key" json:" - "` //订单编号
	CouponId	int	`gorm:"primary_key" json:" - "` //优惠卷id
	Count	int	`json:"count"` //数量
	Money	float64	`json:"money"` //总金额
}


func (TOrderCoupon) TableName() string {
	return "t_order_coupon"
}
