using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Api
{
    public static class ConfigurationManager
    {
        public readonly static IConfiguration Configuration;

        static ConfigurationManager()
        {
            Configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .Build();
        }

        /// <summary>
        /// 读取节点数据
        /// </summary>
        /// <typeparam name="T">节点数据模型</typeparam>
        /// <param name="key">节点名称</param>
        /// <returns></returns>
        public static T GetSection<T>(string key) where T : class, new()
        {
            return new ServiceCollection()
                .AddOptions()
                .Configure<T>(Configuration.GetSection(key))
                .BuildServiceProvider()
                .GetService<IOptions<T>>()
                .Value;
        }

        /// <summary>
        /// 读取节点数据
        /// </summary>
        /// <param name="key">节点名称</param>
        /// <returns></returns>
        public static string GetSection(string key)
        {
            return Configuration.GetValue<string>(key);
        }
    }
}