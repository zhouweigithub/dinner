package model

import (
	"time"
)

type TProduct struct {
	ID	int	`gorm:"primary_key" json:" - "` //自增主键
	Name	string	`json:"name"` //商品名称
	Category	int	`json:"category"` //商品分类
	Price	float64	`json:"price"` //价格
	Sales	int	`json:"sales"` //销量
	CrTime	time.Time	`json:"crtime"` //创建时间
}


func (TProduct) TableName() string {
	return "t_product"
}
