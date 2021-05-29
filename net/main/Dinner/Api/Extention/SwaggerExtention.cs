using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Api
{
    /// <summary>
    /// swagger服务相关
    /// </summary>
    public static class SwaggerExtention
    {
        /// <summary>
        /// 注册Swagger服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwaggerService(this IServiceCollection services)
        {
            //注册Swagger生成器
            services.AddSwaggerGen(opti =>
            {
                opti.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Shop API",
                    Version = "v1"
                });
                opti.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });
                opti.AddSecurityRequirement(
                    new OpenApiSecurityRequirement {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference()
                                {
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                                }
                            },
                            Array.Empty<string>()
                        }
                });

                // 获取xml注释文件的目录
                var xmlPath1 = Path.Combine(AppContext.BaseDirectory, "Api.xml");
                var xmlPath2 = Path.Combine(AppContext.BaseDirectory, "Model.xml");
                opti.IncludeXmlComments(xmlPath1);
                opti.IncludeXmlComments(xmlPath2);
            });
        }

        /// <summary>
        /// 引入Swagger中间件
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwaggerWare(this IApplicationBuilder app)
        {
            app.UseSwagger();

            //SwaggerUI中间件
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint("/swagger/v1/swagger.json", "Shop API v1");
            });
        }
    }
}
