package response

import (
	"gin-vue-admin/model"
)

type SysUserResponse struct {
	User model.TUser `json:"user"`
}

type LoginResponse struct {
	User      model.TUser `json:"user"`
	Token     string        `json:"token"`
	ExpiresAt int64         `json:"expiresAt"`
}
