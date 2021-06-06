using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRCoder;
using static QRCoder.QRCodeGenerator;

namespace ZwUtil.QrCode
{
    /// <summary>
    /// 二维码生成
    /// </summary>
    public class QrCodeUtil
    {
        /// <summary>
        /// 创建二维码
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="level">容错级别</param>
        /// <param name="size">尺寸</param>
        /// <returns></returns>
        public static byte[] CreateQrCode(string content, ErrorCorrectionLevel level, QrSize size)
        {
            int _size = (int)size;
            ECCLevel _level = Correction(level);
            QRCodeGenerator generator = new QRCodeGenerator();
            QRCodeData data = generator.CreateQrCode(content, _level);
            BitmapByteQRCode qrCode = new BitmapByteQRCode(data);
            return qrCode.GetGraphic(_size);
        }

        /// <summary>
        /// 容错处理
        /// </summary>
        /// <param name="level">容错级别</param>
        private static ECCLevel Correction(ErrorCorrectionLevel level)
        {
            ECCLevel _level;
            switch (level)
            {
                case ErrorCorrectionLevel.L:
                    _level = ECCLevel.L;
                    break;
                case ErrorCorrectionLevel.M:
                    _level = ECCLevel.M;
                    break;
                case ErrorCorrectionLevel.Q:
                    _level = ECCLevel.Q;
                    break;
                case ErrorCorrectionLevel.H:
                    _level = ECCLevel.H;
                    break;
                default:
                    _level = ECCLevel.L;
                    break;
            }

            return _level;
        }
    }
}
