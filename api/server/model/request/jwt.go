package request

import (
	"github.com/dgrijalva/jwt-go"
)

// Custom claims structure
type CustomClaims struct {
	Code       string
	ID         uint
	Nick       string
	CompanyId  uint
	BufferTime int64
	jwt.StandardClaims
}
