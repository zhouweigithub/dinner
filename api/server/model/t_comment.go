package model

import (
	"time"
)

type TComment struct {
	ID	int	`gorm:"primary_key" json:" - "` //自增id
	OrderId	string	`json:"orderid"` //订单编号
	Msg	string	`json:"msg"` //内容
	CrTime	time.Time	`json:"crtime"` //创建时间
}


func (TComment) TableName() string {
	return "t_comment"
}
