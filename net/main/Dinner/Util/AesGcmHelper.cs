using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;

namespace ZwUtil
{
    public class AesGcmHelper
    {
        private static string ALGORITHM = "AES/GCM/NoPadding";
        private static int TAG_LENGTH_BIT = 128;
        private static int NONCE_LENGTH_BYTE = 12;

        /// <summary>
        /// AEAD_AES_256_GCM 算法解密
        /// </summary>
        /// <param name="associatedData">associatedData参数</param>
        /// <param name="nonce">nonce参数</param>
        /// <param name="ciphertext">密文</param>
        /// <param name="aeskey">APIv3密钥</param>
        /// <returns></returns>
        public static string AesGcmDecrypt(string associatedData, string nonce, string ciphertext, string aeskey)
        {
            GcmBlockCipher gcmBlockCipher = new GcmBlockCipher(new AesEngine());
            AeadParameters aeadParameters = new AeadParameters(
                new KeyParameter(Encoding.UTF8.GetBytes(aeskey)),
                128,
                Encoding.UTF8.GetBytes(nonce),
                Encoding.UTF8.GetBytes(associatedData));
            gcmBlockCipher.Init(false, aeadParameters);

            byte[] data = Convert.FromBase64String(ciphertext);
            byte[] plaintext = new byte[gcmBlockCipher.GetOutputSize(data.Length)];
            int length = gcmBlockCipher.ProcessBytes(data, 0, data.Length, plaintext, 0);
            gcmBlockCipher.DoFinal(plaintext, length);
            return Encoding.UTF8.GetString(plaintext);
        }
    }
}
