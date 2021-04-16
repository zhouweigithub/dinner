package model

import (
	"time"
)

type TCategory struct {
	ID	int	`gorm:"primary_key" json:" - "` //自增id
	Name	string	`json:"name"` //名称
	State	int	`json:"state"` //状态0正常 1禁用
	CrTime	time.Time	`json:"crtime"` //创建时间
}


func (TCategory) TableName() string {
	return "t_category"
}
