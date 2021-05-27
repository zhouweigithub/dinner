package request
type Company struct {
	Code	string	`json:"code"` //唯一编码
	Name	string	`json:"name"` //公司名字
	Address	string	`json:"address"` //公司地址
}
