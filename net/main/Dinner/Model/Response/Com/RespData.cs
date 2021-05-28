using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Response.Com
{
    /// <summary>
    /// 请求返回值
    /// </summary>
    public class RespData
    {
        /// <summary>
        /// 错误码（0为成功）
        /// </summary>
        public int code { get; set; } = 0;

        /// <summary>
        /// 错误信息
        /// </summary>
        public string msg { get; set; } = "ok";
    }

    /// <summary>
    /// 请求返回值
    /// </summary>
    /// <typeparam name="T">数据模型</typeparam>
    public class RespData<T> : RespData
    {
        /// <summary>
        /// 成功后返回的数据
        /// </summary>
        public T data { get; set; }
    }

    /// <summary>
    /// 请求返回的数据
    /// </summary>
    /// <typeparam name="T">数据模型</typeparam>
    public class RespDataList<T> : RespData
    {
        /// <summary>
        /// 成功后返回的数据
        /// </summary>
        public List<T> datas { get; set; }
    }

    /// <summary>
    /// 请求返回值
    /// </summary>
    /// <typeparam name="T">数据模型</typeparam>
    public class RespDataToken<T> : RespData<T>
    {
        /// <summary>
        /// 登录token
        /// </summary>
        public string token { get; set; }
    }
}
