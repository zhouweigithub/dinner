package service

import (
	"errors"
	"gin-vue-admin/global"
	"gin-vue-admin/model"
	"gorm.io/gorm"
)

//@author: [piexlmax](https://github.com/piexlmax)
//@function: Register
//@description: 用户注册
//@param: u model.TUser
//@return: err error, userInter model.TUser

func Register(u model.TUser) (err error, userInter model.TUser) {
	var user model.TUser
	if !errors.Is(global.GVA_DB.Where("code = ?", u.Code).First(&user).Error, gorm.ErrRecordNotFound) { // 判断用户名是否注册
		return errors.New("用户名已注册"), userInter
	}
	err = global.GVA_DB.Create(&u).Error
	return err, u
}

//@author: [piexlmax](https://github.com/piexlmax)
//@function: Login
//@description: 用户登录
//@param: u *model.TUser
//@return: err error, userInter *model.TUser

func Login(u *model.TUser) (err error, userInter *model.TUser) {
	var user model.TUser
	err = global.GVA_DB.Where("code = ?", u.Code).Preload("Authority").First(&user).Error
	return err, &user
}

//@author: [piexlmax](https://github.com/piexlmax)
//@function: ChangePassword
//@description: 修改用户密码
//@param: u *model.TUser, newPassword string
//@return: err error, userInter *model.TUser

//func ChangePassword(u *model.TUser, newPassword string) (err error, userInter *model.TUser) {
//	var user model.TUser
//	u.Password = utils.MD5V([]byte(u.Password))
//	err = global.GVA_DB.Where("username = ? AND password = ?", u.Username, u.Password).First(&user).Update("password", utils.MD5V([]byte(newPassword))).Error
//	return err, u
//}

//@author: [piexlmax](https://github.com/piexlmax)
//@function: GetUserInfoList
//@description: 分页获取数据
//@param: info request.PageInfo
//@return: err error, list interface{}, total int64

//func GetUserInfoList(info request.PageInfo) (err error, list interface{}, total int64) {
//	limit := info.PageSize
//	offset := info.PageSize * (info.Page - 1)
//	db := global.GVA_DB.Model(&model.TUser{})
//	var userList []model.TUser
//	err = db.Count(&total).Error
//	err = db.Limit(limit).Offset(offset).Preload("Authority").Find(&userList).Error
//	return err, userList, total
//}

//@author: [piexlmax](https://github.com/piexlmax)
//@function: SetUserAuthority
//@description: 设置一个用户的权限
//@param: uuid uuid.UUID, authorityId string
//@return: err error

//func SetUserAuthority(uuid uuid.UUID, authorityId string) (err error) {
//	err = global.GVA_DB.Where("uuid = ?", uuid).First(&model.TUser{}).Update("authority_id", authorityId).Error
//	return err
//}

//@author: [piexlmax](https://github.com/piexlmax)
//@function: DeleteUser
//@description: 删除用户
//@param: id float64
//@return: err error

//func DeleteUser(id float64) (err error) {
//	var user model.TUser
//	err = global.GVA_DB.Where("id = ?", id).Delete(&user).Error
//	return err
//}

//@author: [piexlmax](https://github.com/piexlmax)
//@function: SetUserInfo
//@description: 设置用户信息
//@param: reqUser model.TUser
//@return: err error, user model.TUser

func SetUserInfo(reqUser model.TUser) (err error, user model.TUser) {
	err = global.GVA_DB.Updates(&reqUser).Error
	return err, reqUser
}

//@author: [SliverHorn](https://github.com/SliverHorn)
//@function: FindUserById
//@description: 通过id获取用户信息
//@param: id int
//@return: err error, user *model.TUser

func FindUserById(id int) (err error, user *model.TUser) {
	var u model.TUser
	err = global.GVA_DB.Where("`id` = ?", id).First(&u).Error
	return err, &u
}

//@author: [SliverHorn](https://github.com/SliverHorn)
//@function: FindUserByUuid
//@description: 通过uuid获取用户信息
//@param: uuid string
//@return: err error, user *model.TUser

func FindUserByCode(code string) (err error, user *model.TUser) {
	var u model.TUser
	if err = global.GVA_DB.Where("`code` = ?", code).First(&u).Error; err != nil {
		return errors.New("用户不存在"), &u
	}
	return nil, &u
}
