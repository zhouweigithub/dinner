package model

import (
	"time"
)

type TOrderCallback struct {
	OrderId	string	`gorm:"primary_key" json:" - "` //订单编号
	WxOrderId	string	`json:"wx_orderid"` //微信支付订单号
	State	int	`json:"state"` //状态
	CrTime	time.Time	`json:"crtime"` //创建时间
}


func (TOrderCallback) TableName() string {
	return "t_order_callback"
}
