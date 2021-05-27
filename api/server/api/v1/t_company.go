package v1

import (
	"gin-vue-admin/global"
	"gin-vue-admin/model"
	"gin-vue-admin/model/request"
	"gin-vue-admin/model/response"
	"gin-vue-admin/service"
	"gin-vue-admin/utils"
	"github.com/gin-gonic/gin"
	"go.uber.org/zap"
)

func Add(c *gin.Context) {
	var L request.Company
	_ = c.ShouldBindJSON(&L)
	if err := utils.Verify(L, utils.CompanyVerify); err != nil {
		response.FailWithMessage(err.Error(), c)
		return
	}
	U := &model.TCompany{ Name: L.Name}
	if err, user := service.Login(U); err != nil {
		global.GVA_LOG.Error("登陆失败! 用户名不存在或者密码错误", zap.Any("err", err))
		response.FailWithMessage("用户名不存在或者密码错误", c)
	} else {
		tokenNext(c, *user)
	}

}
