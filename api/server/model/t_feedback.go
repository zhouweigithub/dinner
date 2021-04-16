package model

import (
	"time"
)

type TFeedback struct {
	ID	int	`gorm:"primary_key" json:" - "` //自增id
	UserId	int	`json:"userid"` //用户id
	Msg	string	`json:"msg"` //内容
	CrTime	time.Time	`json:"crtime"` //创建时间
}

func (TFeedback) TableName() string {
	return "t_feedback"
}
