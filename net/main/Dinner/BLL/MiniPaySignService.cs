using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;
using Microsoft.Extensions.Options;
using Model.Request.Wx;
using ZwUtil;

namespace BLL
{

    /// <summary>
    /// 微信签名信息
    /// </summary>
    public class MiniPaySignService : IMiniPaySignService
    {
        private readonly IOptions<WxOpenidConfigModel> _wxconfig;

        public MiniPaySignService(IOptions<WxOpenidConfigModel> wxconfig)
        {
            _wxconfig = wxconfig;
        }


        /// <summary>
        /// 生成微信接口签名内容
        /// </summary>
        /// <param name="para">参数</param>
        /// <returns></returns>
        public string Sign(MiniPaySignPara para)
        {
            //文档 https://pay.weixin.qq.com/wiki/doc/apiv3/wechatpay/wechatpay4_0.shtml

            //请求中的请求报文主体（request body）。
            //请求方法为GET时，报文主体为空。
            //请求方法为POST或PUT时，请使用真实发送的JSON报文。
            //图片上传API，请使用meta对应的JSON报文。
            string content = string.Empty;

            if (para.Method == "Post" || para.Method == "Put")
            {
                using (var reader = new StreamReader(para.Body, Encoding.UTF8))
                {
                    content = reader.ReadToEnd();
                }
            }

            //请求的绝对URL，并去除域名部分得到参与签名的URL。如果请求中有查询参数，URL末尾应附加有'?'和对应的查询字符串。
            string uri = para.Path;

            //发起请求时的系统当前时间戳
            string tiemstamp = ZqUtils.Core.Extensions.DateTimeExtensions.ToTimeStamp(DateTime.Now);

            //随机窃取符串
            string randChars = Strings.GetRandomString(32, Strings.RandStringType.StringAndNumber, Strings.LetterType.UpperOnly);

            //签名内容
            var msg = string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n", para.Method, uri, tiemstamp, randChars, content);

            //签名结果
            string sign = MiniPaySignHelper.Sign(msg, _wxconfig.Value.MchPrivateKey);

            //对签名结果进行base64编码
            string base64Sign = ZqUtils.Core.Extensions.StringExtensions.ToBase64(sign);


            //随机字符串
            string nonce_str = Strings.GetRandomString(32, Strings.RandStringType.StringAndNumber, Strings.LetterType.UpperOnly);

            //最终签名内容
            string headSign = string.Format("WECHATPAY2-SHA256-RSA2048 mchid=\"{0}\",nonce_str=\"{1}\",signature=\"{2}\",serial_no=\"{3}\"", _wxconfig.Value.Mchid, nonce_str, base64Sign, _wxconfig.Value.MchSerialNo);

            return headSign;
        }


        /// <summary>
        /// 验证签名是否正确
        /// </summary>
        /// <param name="para">参数</param>
        /// <returns></returns>
        public bool Verify(MiniPayDeSignPara para)
        {
            //先验证序列号是否跟商户当前所持有的 微信支付平台证书的序列号一致
            if (para.WechatpaySerial != _wxconfig.Value.PlatformSerialNo)
                return false;


            //验证应答内容的签名是否与实际签名一致

            //应答内容
            string content = string.Format("{0}\n{1}\n{2}\n", para.WechatpayTimestamp, para.WechatpayNonce, para.ResponseBody);

            return MiniPaySignHelper.Verify(para.WechatpaySignature, content, _wxconfig.Value.PlatformPublicKey);
        }
    }
}
