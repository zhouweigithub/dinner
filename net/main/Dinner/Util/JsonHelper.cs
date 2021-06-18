using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ZwUtil
{
    public class JsonHelper
    {
        /// <summary>
        /// 反序列化JSON为对象实例
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// 序列化对象为JSON串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
