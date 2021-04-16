package model

import (
	"time"
)

type TCompany struct {
	ID	int	`gorm:"primary_key" json:" - "` //自增主键
	Name	string	`json:"name"` //公司名字
	Code	string	`json:"code"` //唯一编码
	Address	string	`json:"address"` //公司地址
	CrTime	time.Time	`json:"crtime"` //创建时间
}


func (TCompany) TableName() string {
	return "t_company"
}
