using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Common.pub
{
    /// <summary>
    /// 工具类日志记录信息类，亦可为服务记录日志
    /// by wjc ，20161017
    /// </summary>
    public class LogApi
    {
        /// <summary>
        /// 判断文件夹是否存在，不存在则创建
        /// </summary>
        /// <returns>日志文件夹路径</returns>
        private static string IsExists()
        {
            string sFilePath = AppDomain.CurrentDomain.BaseDirectory;
            string sFilePathFull = Path.Combine(sFilePath, "LogText");//可以忽略双斜杠问题
            if (!Directory.Exists(sFilePathFull))//判断该文件夹是否存在
            {
                Directory.CreateDirectory(sFilePathFull);//如果不存在则创建“LogText”文件夹
            }
            return sFilePathFull;
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        public static void DebugInfo(Exception ep)
        {
            string sFilePath = IsExists();
            sFilePath += String.Format("\\errorlog_{0}.txt", DateTime.Today.ToString("yyyy-MM-dd"));
            using (FileStream pFileStream = new FileStream(sFilePath, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter pStreamWrite = new StreamWriter(pFileStream))
                {
                    pStreamWrite.WriteLine();
                    pStreamWrite.WriteLine("errortime：" + DateTime.Now);
                    pStreamWrite.WriteLine("errorinfo：" + ep.Message);
                    pStreamWrite.Flush();
                }
            }
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        public static void DebugInfo(Exception ep, string str)
        {
            string sFilePath = IsExists();
            sFilePath += String.Format("\\errorlog_{0}.txt", DateTime.Today.ToString("yyyy-MM-dd"));
            using (FileStream pFileStream = new FileStream(sFilePath, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter pStreamWrite = new StreamWriter(pFileStream))
                {
                    pStreamWrite.WriteLine();
                    pStreamWrite.WriteLine("errortime：" + DateTime.Now);
                    pStreamWrite.WriteLine("errorinfo：" + ep.Message);
                    pStreamWrite.WriteLine("errorflag：" + str);
                    pStreamWrite.Flush();
                }
            }
        }
        /// <summary>
        /// 服务推送信息
        /// </summary>
        /// <param name="sName">服务名称</param>
        /// <param name="str">具体信息</param>
        public static void WriteDary(string sName, string str)
        {
            string sFilePath = IsExists();
            sFilePath += String.Format("\\putlog_{0}.txt");
            using (FileStream pFileStream = new FileStream(sFilePath, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter pStreamWrite = new StreamWriter(pFileStream))
                {
                    pStreamWrite.WriteLine();
                    pStreamWrite.WriteLine("puttime：" + DateTime.Now);
                    pStreamWrite.WriteLine("putinfo：" + str);
                    pStreamWrite.Flush();
                }
            }
        }
        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="logKey"></param>
        /// <param name="logMsg"></param>
        public static void LogInfo(string logKey, string logMsg)
        {
            string sFilePath = IsExists();
            sFilePath += String.Format("\\log_{0}.txt", DateTime.Today.ToString("yyyy-MM-dd"));
            using (FileStream pFileStream = new FileStream(sFilePath, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter pStreamWrite = new StreamWriter(pFileStream))
                {
                    pStreamWrite.WriteLine();
                    pStreamWrite.WriteLine("time：" + DateTime.Now);
                    pStreamWrite.WriteLine(string.Format("{0}：{1}", logKey, logMsg));
                    pStreamWrite.Flush();
                }
            }
        }

    }
}
