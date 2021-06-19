using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ZwUtil
{
    /// <summary>
    /// 微信请求接口的签名信息
    /// </summary>
    public class MiniPaySignHelper
    {
        /// <summary>
        /// 生成微信请求接口的签名信息
        /// </summary>
        /// <param name="message">签名的内容</param>
        /// <param name="privateKey">商户API证书中的私钥</param>
        /// <returns></returns>
        public static string Sign(string message, string privateKey)
        {
            // NOTE： 私钥不包括私钥文件起始的-----BEGIN PRIVATE KEY-----
            //        亦不包括结尾的-----END PRIVATE KEY-----
            byte[] keyData = Convert.FromBase64String(privateKey);
            using (CngKey cngKey = CngKey.Import(keyData, CngKeyBlobFormat.Pkcs8PrivateBlob))
            using (RSACng rsa = new RSACng(cngKey))
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                return Convert.ToBase64String(rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));
            }
        }


        /// <summary>
        /// 验证签名是否正确
        /// </summary>
        /// <param name="signedData">已签名的数据（base64编码）</param>
        /// <param name="verifyData">需要验证的数据</param>
        /// <param name="publicKey">公钥</param>
        /// <returns></returns>
        public static bool Verify(string signedData, string verifyData, string publicKey)
        {
            // NOTE： 私钥不包括私钥文件起始的-----BEGIN PRIVATE KEY-----
            //        亦不包括结尾的-----END PRIVATE KEY-----
            byte[] keyData = Convert.FromBase64String(publicKey);
            using (CngKey cngKey = CngKey.Import(keyData, CngKeyBlobFormat.Pkcs8PrivateBlob))
            using (RSACng rsa = new RSACng(cngKey))
            {
                //已签名的内容
                byte[] signContent = Convert.FromBase64String(signedData);
                //需要验证签名的内容
                byte[] verifyContent = Encoding.UTF8.GetBytes(verifyData);

                return rsa.VerifyData(signContent, verifyContent, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            }
        }
    }
}
