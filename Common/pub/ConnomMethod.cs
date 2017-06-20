using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Common.pub
{
    /// <summary>
    /// 通用方法类
    /// by wangjc 20161017
    /// </summary>
    public class ConnomMethod
    {
        /// <summary>
        /// Http下载文件
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="path">保存地址</param>
        /// <returns></returns>
        public static bool HttpDownloadFile(string url, string path)
        {
            bool isSaveOk = false;
            try
            {
                // 设置参数
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //发送请求并获取相应回应数据
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                Stream responseStream = response.GetResponseStream();
                //创建本地文件写入流
                Stream stream = new FileStream(path, FileMode.Create);
                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    stream.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                }
                stream.Close();
                responseStream.Close();
            }
            catch (Exception ex)
            {
                LogApi.DebugInfo(ex, string.Format("url={0};path={1}", url, path));
            }
            return isSaveOk;

        }

        /// <summary>
        /// 根据图片路径返回图片的字节流byte[]
        /// </summary>
        /// <param name="imagePath">图片路径</param>
        /// <returns>返回的字节流</returns>
        public static byte[] GetFileByte(string filePath)
        {
            try
            {
                FileStream files = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                byte[] fileByte = new byte[(int)files.Length];
                files.Read(fileByte, 0, fileByte.Length);
                files.Close();
                return fileByte;
            }
            catch (Exception ex)
            {
                LogApi.DebugInfo(ex, filePath);
                return null;
            }
        }

        /// <summary>
        /// 获取物料上传本地路径
        /// </summary>
        /// <returns>物料上传路径</returns>
        public static string GetFileUploadPath()
        {
            string sFilePath = AppDomain.CurrentDomain.BaseDirectory;
            string sFilePathFull = Path.Combine(sFilePath, "FileUpload");//可以忽略双斜杠问题
            if (!Directory.Exists(sFilePathFull))//判断该文件夹是否存在
            {
                Directory.CreateDirectory(sFilePathFull);//如果不存在则创建“FileUploadPath”文件夹
            }
            return sFilePathFull;
        }

        /// <summary>
        /// 获取用户设置的Encoding
        /// </summary>
        /// <returns></returns>
        public static Encoding GetCurrentEncoding()
        {
            string strEncodingType = CommonVariables.EncodingType;// System.Configuration.ConfigurationManager.AppSettings["EncodingType"].ToString().Trim();
            Encoding mEncoding = Encoding.UTF8;
            switch (strEncodingType.ToLower())
            {
                case "default":
                    mEncoding = Encoding.Default;
                    break;
                case "unicode":
                    mEncoding = Encoding.Unicode;
                    break;
                case "utf8":
                case "utf-8":
                default:
                    mEncoding = Encoding.UTF8;
                    break;
            }
            return mEncoding;
        }

    }
}
