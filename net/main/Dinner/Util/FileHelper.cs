using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZwUtil
{
    /// <summary>
    /// 文件操作
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// 检测目标目录是否存在
        /// </summary>
        /// <param name="folderPath">目录路径</param>
        /// <returns></returns>
        public static bool IsFolderExists(string folderPath)
        {
            if (string.IsNullOrWhiteSpace(folderPath))
                return false;

            return Directory.Exists(folderPath);
        }

        /// <summary>
        /// 检测是否存在目标文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static bool IsFileExists(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return false;

            return File.Exists(filePath);
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="folderPath"></param>
        public static void CreateFolder(string folderPath)
        {
            if (string.IsNullOrWhiteSpace(folderPath))
                return;

            Directory.CreateDirectory(folderPath);
        }

        /// <summary>
        /// 创建文件，若目录不存在，则先创建目录，若文件已存在，则覆盖
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="bytes">文件内容</param>
        public static void CreateFile(string filePath, byte[] bytes)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return;

            //获取文件的目录信息
            string directoryName = Path.GetDirectoryName(filePath);

            //如果目录不存在，则先创建目录
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            //创建文件，若已存在，则覆盖
            using (FileStream fs = File.Create(filePath, bytes.Length))
            {
                fs.Write(bytes);
            }
        }
    }
}
