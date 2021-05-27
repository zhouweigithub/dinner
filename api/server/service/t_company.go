package service

import (
	"errors"
	"gin-vue-admin/global"
	"gin-vue-admin/model"
	"gin-vue-admin/model/request"
	"gin-vue-admin/utils"
	"gorm.io/gorm"
)

//添加公司信息

func Add(c request.Company) (error, model.TCompany) {
	var randChars = utils.RandomString(5)
	c.Code = randChars

	var company model.TCompany
	if !errors.Is(global.GVA_DB.Where("name = ?", c.Name).First(&company).Error, gorm.ErrRecordNotFound) { // 判断公司名是否注册
		return errors.New("用户名已注册"), company
	}
	err := global.GVA_DB.Create(&company).Error
	return err, company
}

//根据公司code获取公司信息

func GetByCode(code string) (error, model.TCompany) {
	var company model.TCompany
	if !errors.Is(global.GVA_DB.Where("code = ?", code).First(&company).Error, gorm.ErrRecordNotFound) { // 判断公司名是否注册
		return errors.New("用户名已注册"), company
	}
	err := global.GVA_DB.Create(&company).Error
	return err, company
}
