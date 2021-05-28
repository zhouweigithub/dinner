using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    /// <summary>
    /// 字符操作
    /// </summary>
    public static class Strings
    {
        private const string UpperLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string LowerLetters = "abcdefghijklmnopqrstuvwxyz";
        private const string NumberLetters = "0123456789";


        /// <summary>
        /// 获取随机字符串
        /// </summary>
        /// <param name="length">长度</param>
        /// <param name="stringType">字符类型</param>
        /// <param name="letterType">字母大小写类型</param>
        /// <returns></returns>
        public static string GetRandomString(int length, RandStringType stringType, LetterType letterType)
        {
            string sources = GetSourceChars(stringType, letterType);

            StringBuilder sb = new(length);
            Random rnd = new();
            for (int i = 0; i < length; i++)
            {
                int index = rnd.Next(0, sources.Length);
                sb.Append(sources[index]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取数据源字符
        /// </summary>
        /// <param name="stringType">字符类型</param>
        /// <param name="letterType">字母大小写类型</param>
        /// <returns></returns>
        private static string GetSourceChars(RandStringType stringType, LetterType letterType)
        {
            if (stringType == RandStringType.NumberOnly)
            {
                return NumberLetters;
            }
            else if (stringType == RandStringType.StringOnly)
            {
                if (letterType == LetterType.LowerOnly)
                    return LowerLetters;
                else if (letterType == LetterType.UpperOnly)
                    return UpperLetters;
                else
                    return UpperLetters + LowerLetters;
            }
            else if (stringType == RandStringType.StringAndNumber)
            {
                if (letterType == LetterType.LowerOnly)
                    return LowerLetters + NumberLetters;
                else if (letterType == LetterType.UpperOnly)
                    return UpperLetters + NumberLetters;
                else
                    return UpperLetters + LowerLetters + NumberLetters;
            }
            else
            {
                return "";
            }
        }


        /// <summary>
        /// 字符类型
        /// </summary>
        public enum RandStringType
        {
            /// <summary>
            /// 仅字符
            /// </summary>
            StringOnly,
            /// <summary>
            /// 仅数字
            /// </summary>
            NumberOnly,
            /// <summary>
            /// 字符与数字
            /// </summary>
            StringAndNumber,
        }

        /// <summary>
        /// 字母大小写类型
        /// </summary>
        public enum LetterType
        {
            /// <summary>
            /// 全大写
            /// </summary>
            UpperOnly,
            /// <summary>
            /// 全小写
            /// </summary>
            LowerOnly,
            /// <summary>
            /// 大小写都有
            /// </summary>
            UpperAndLower
        }

    }
}
