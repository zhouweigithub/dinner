package model

import (
	"time"
)

type TUser struct {
	ID        uint      `json:"id" gorm:"comment:用户ID"`
	Code      string    `json:"code" gorm:"comment:用户唯一标识"`
	Nick      string    `json:"nick" gorm:"default:系统用户;comment:用户昵称" `
	CompanyId uint      `json:"companyId" gorm:"default:0;comment:公司ID"`
	CrTime    time.Time `json:"crTime" gorm:"comment:创建时间"`
}

func (TUser) TableName() string {
	return "t_user"
}
