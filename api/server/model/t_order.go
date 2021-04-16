package model

import (
	"time"
)

type TOrder struct {
	ID	string	`gorm:"primary_key" json:" - "` //订单编号
	UserId	int	`json:"userid"` //用户id
	Money	float64	`json:"money"` //订单金额
	CouponMoney	float64	`json:"coupon_money"` //优惠金额
	PayMoney	float64	`json:"pay_money"` //实际支付金额
	State	int	`json:"state"` //状态
	CrTime	time.Time	`json:"crtime"` //创建时间
}


func (TOrder) TableName() string {
	return "t_order"
}
