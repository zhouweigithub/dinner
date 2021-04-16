package model

import (
	"time"
)

type TCoupon struct {
	ID	int	`gorm:"primary_key" json:" - "` //主键
	Name	string	`json:"name"` //优惠卷名称
	Money	float64	`json:"money"` //优惠金额
	StartTime	time.Time	`json:"start_time"` //使用开始日期
	EndTime	time.Time	`json:"end_time"` //使用截止日期
	CrTime	time.Time	`json:"crtime"` //优惠卷创建时间
}


func (TCoupon) TableName() string {
	return "t_coupon"
}
