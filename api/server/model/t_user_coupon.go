package model

type TUserCoupon struct {
	UserId	int	`gorm:"primary_key" json:" - "` //用户id
	CouponId	int	`gorm:"primary_key" json:" - "` //优惠卷id
	Count	int	`json:"count"` //数量
}


func (TUserCoupon) TableName() string {
	return "t_user_coupon"
}
