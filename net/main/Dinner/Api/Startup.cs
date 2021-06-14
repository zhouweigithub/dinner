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
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Model;
using Model.Request.Wx;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;
using ZwUtil;

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
                //����ѭ������
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //��ʹ���շ���ʽ��key
                //opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //����ʱ���ʽ
                //opt.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
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

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddNLog();

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DbService>();
                context.Database.EnsureCreated();
            }


            //��̬�ļ�·��
            string staticFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "statics");
            CreateStaticFolder(staticFolder + "\\readme.txt");

            app.UseStaticFiles(new StaticFileOptions
            {
                //��Դ���ڵľ���·����
                FileProvider = new PhysicalFileProvider(staticFolder),
                //��ʾ����·��,����'/'��ͷ
                RequestPath = "/statics"
            });

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
        /// ����Cache���л���JSON����
        /// </summary>
        /// <param name="t"></param>
        private void SetJsonPara(EasyCachingJsonSerializerOptions t)
        {
            t.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }


        /// <summary>
        /// �����ļ�����Ŀ¼�����ڣ��ȴ���Ŀ¼
        /// </summary>
        /// <param name="file"></param>
        private void CreateStaticFolder(string file)
        {
            byte[] content = Encoding.UTF8.GetBytes("��Ŀ¼Ϊվ�����о�̬�ļ����Ŀ¼");
            FileHelper.CreateFile(file, content);
        }
    }
}
