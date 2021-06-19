using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Request.Wx
{
    /// <summary>
    /// 微信小程序调接口时，生成签名需要的参数
    /// </summary>
    public class MiniPaySignPara
    {
        /// <summary>
        /// 请求的绝对URL，并去除域名部分得到参与签名的URL。如果请求中有查询参数，URL末尾应附加有'?'和对应的查询字符串
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// HTTP请求的方法（GET,POST，PUT）等
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// 请求中的请求报文主体（request body）
        /// 请求方法为GET时，报文主体为空。
        /// 当请求方法为POST或PUT时，请使用真实发送的JSON报文。
        /// 图片上传API，请使用meta对应的JSON报文。
        /// </summary>
        public string Body { get; set; }
    }
}
