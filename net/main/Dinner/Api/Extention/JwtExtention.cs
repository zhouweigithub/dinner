using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Model;

namespace Api
{
    /// <summary>
    /// jwt服务相关
    /// </summary>
    public static class JwtExtention
    {
        /// <summary>
        /// 注册JWT服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddJwt(this IServiceCollection services, JwtSetting jswSetting)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Jwt";
                options.DefaultChallengeScheme = "Jwt";
            }).AddJwtBearer("Jwt", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,

                    ValidIssuer = jswSetting.Issuer,
                    ValidAudience = jswSetting.Audience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jswSetting.SecretKey)),

                    ValidateLifetime = true, //validate the expiration and not before values in the token

                    //token过期等待时间,0为立即过期
                    ClockSkew = TimeSpan.FromMinutes(0) //5 minute tolerance for the expiration date
                };
            });

        }
    }
}
