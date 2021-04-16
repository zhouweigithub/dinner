package model

import (
	"time"
)

type TOrderProduct struct {
	OrderId	string	`gorm:"primary_key" json:" - "` //订单编号
	ProductId	int	`gorm:"primary_key" json:" - "` //商品id
	Price	float64	`json:"price"` //单价
	Count	int	`json:"count"` //数量
	Money	float64	`json:"money"` //金额
	CrTime	time.Time	`json:"crtime"` //创建时间
}

func (TOrderProduct) TableName() string {
	return "t_order_product"
}
