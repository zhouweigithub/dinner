package request

import uuid "github.com/satori/go.uuid"

// User register structure
type Register struct {
	Code    string `json:"code"`
	//Password    string `json:"passWord"`
	Nick    string `json:"nickName" gorm:"default:'QMPlusUser'"`
	HeaderImg   string `json:"headerImg" gorm:"default:'http://www.henrongyi.top/avatar/lufu.jpg'"`
	//AuthorityId string `json:"authorityId" gorm:"default:888"`
}

// User login structure
type Login struct {
	Code  string `json:"code"`
}

// Modify password structure
type ChangePasswordStruct struct {
	Username    string `json:"username"`
	Password    string `json:"password"`
	NewPassword string `json:"newPassword"`
}

// Modify  user's auth structure
type SetUserAuth struct {
	UUID        uuid.UUID `json:"uuid"`
	AuthorityId string    `json:"authorityId"`
}
