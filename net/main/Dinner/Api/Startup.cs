using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using BLL.EasyCaching;
using BLL.Interface;
using BLL.MiddleWare;
using DAL;
using EasyCaching.Serialization.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Model;
using Model.Request.Wx;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public static readonly ILoggerFactory efLogger = LoggerFactory.Create(builder =>
        {
            builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information).AddConsole();
        });

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(opt =>
            {
                //忽略循环引用
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //不使用驼峰样式的key
                //opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //设置时间格式
                opt.SerializerSettings.DateFormatString = "yyyy-MM-dd";
            });
            services.AddDbContext<DbService>(
                options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection"), MySqlServerVersion.LatestSupportedServerVersion).UseLoggerFactory(efLogger)
            ); ;

            JwtSetting jswSetting = Configuration.GetSection("JwtSetting").Get<JwtSetting>();

            services.Configure<JwtSetting>(Configuration.GetSection("JwtSetting"));
            services.Configure<WxOpenidConfigModel>(Configuration.GetSection("wx:openid"));

            services.AddCache(opt =>
            {
                opt.UseCSRedis(Configuration, "mine_redis", "easycaching:csredis").WithJson(SetJsonPara, "mine_redis");
            });
            services.AddSwaggerService();
            services.AddJwt(jswSetting);
            services.AddDataServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DbService>();
                context.Database.EnsureCreated();
            }

            app.UseStaticFiles();

            app.UseSwaggerWare();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseMiddleware<CustomExceptionMiddleWare>();

            app.UseMiddleware<ValidUserMiddleWare>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }



        /// <summary>
        /// 配置Cache序列化的JSON策略
        /// </summary>
        /// <param name="t"></param>
        private void SetJsonPara(EasyCachingJsonSerializerOptions t)
        {
            t.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }
    }
}
