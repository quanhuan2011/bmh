using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.pub;
using System.Net;
using System.IO;
using Model;

namespace Common.ftp
{
    /// <summary>
    /// FTP上传工具类
    /// </summary>
    public class ArchiveFtpManager
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="upType">物料类型，pic or app</param>
        public static bool UploadFile(string fileName, UpLoadType upType)
        {
            bool isSuccess = false;
            string name_dir = string.Format("{0}{1}", DateTime.Now.Year, DateTime.Now.Month);
            bool isExist = FtpCheckDirectoryExist(name_dir, upType);
            bool isOk = false;
            if (!isExist)
            {
                isOk = FtpMakeDir(name_dir, upType);
            }

            if (isExist || isOk)
            {
                string fileFullName = string.Format("{0}\\{1}", ConnomMethod.GetFileUploadPath(), fileName);
                FileInfo fileinfo = new FileInfo(fileFullName);
                FtpWebRequest ftp = GetRequest(string.Format("{0}/{1}", name_dir, fileName), upType);
                ftp.KeepAlive = false;
                //设置FTP命令 设置所要执行的FTP命令，
                ftp.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
                //指定文件传输的数据类型
                ftp.UseBinary = true;
                ftp.UsePassive = true;

                //告诉ftp文件大小
                ftp.ContentLength = fileinfo.Length;
                //缓冲大小设置为2KB
                const int BufferSize = 2048;
                byte[] content = new byte[BufferSize - 1 + 1];
                int dataRead;

                //打开一个文件流 (System.IO.FileStream) 去读上传的文件
                using (FileStream fs = fileinfo.OpenRead())
                {
                    try
                    {
                        //把上传的文件写入流
                        using (Stream rs = ftp.GetRequestStream())
                        {
                            do
                            {
                                //每次读文件流的2KB
                                dataRead = fs.Read(content, 0, BufferSize);
                                rs.Write(content, 0, dataRead);
                            }
                            while (!(dataRead < BufferSize));
                            rs.Close();
                        }
                        isSuccess = true;
                        LogApi.LogInfo("上传信息", string.Format("上传【{0}】到【FTP:{1}】成功！", fileFullName, ftp.RequestUri));
                    }
                    catch (Exception ex)
                    {
                        LogApi.DebugInfo(ex);
                    }
                    finally
                    {
                        fs.Close();
                    }
                }
                fileinfo.Delete();

            }

            return isSuccess;

            #region MyRegion
            //ftp = null;
            ////设置FTP命令
            //ftp = GetRequest(URI, username, password);
            //ftp.Method = System.Net.WebRequestMethods.Ftp.Rename; //改名
            //ftp.RenameTo = fileinfo.Name;
            //try
            //{
            //    ftp.GetResponse();
            //}
            //catch (Exception ex)
            //{
            //    ftp = GetRequest(URI, username, password);
            //    ftp.Method = System.Net.WebRequestMethods.Ftp.DeleteFile; //删除
            //    ftp.GetResponse();
            //    LogApi.DebugInfo(ex);
            //}
            //finally
            //{
            //    //fileinfo.Delete();
            //    ftp = null;
            //} 
            #endregion

            // 可以记录一个日志  "上传" + fileinfo.FullName + "上传到" + "FTP://" + hostname + "/" + targetDir + "/" + fileinfo.Name + "成功." );
        }

        /// <summary>
        /// 检查文件夹是否存在
        /// </summary>
        /// <param name="name_dir">文件夹名称</param>
        /// <param name="upType">上传文件类型</param>
        public static bool FtpCheckDirectoryExist(string name_dir, UpLoadType upType)
        {
            bool isExist = false;

            FtpWebRequest ftpWebRequest = null;
            WebResponse webResponse = null;
            StreamReader reader = null;
            try
            {
                Encoding mEncoding = ConnomMethod.GetCurrentEncoding();
                ftpWebRequest = GetRequest(string.Empty, upType);
                ftpWebRequest.KeepAlive = false;
                ftpWebRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                webResponse = ftpWebRequest.GetResponse();

                reader = new StreamReader(webResponse.GetResponseStream(), mEncoding);

                string line = reader.ReadLine();
                while (line != null)
                {
                    if (line == name_dir)
                    {
                        isExist = true;
                        break;
                    }
                    line = reader.ReadLine();
                }
            }
            catch (Exception ex)
            {
                LogApi.DebugInfo(ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (webResponse != null)
                {
                    webResponse.Close();
                }
            }

            return isExist;
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        public static bool FtpMakeDir(string name_dir, UpLoadType upType)
        {
            bool IsMakeDir = false;

            FtpWebRequest req = GetRequest(name_dir, upType);
            req.KeepAlive = false;
            req.Method = WebRequestMethods.Ftp.MakeDirectory;
            try
            {
                FtpWebResponse response = (FtpWebResponse)req.GetResponse();
                response.Close();
                IsMakeDir = true;
            }
            catch (Exception ex)
            {
                LogApi.DebugInfo(ex, name_dir);
                req.Abort();
                IsMakeDir = false;
            }
            finally
            {
                req.Abort();
            }
            return IsMakeDir;
        }

        /// <summary>
        /// 获取FtpWebRequest
        /// </summary>
        /// <param name="fileName">文件名称 或 文件夹名称</param>
        /// <param name="upType">物料类型</param>
        /// <returns></returns>
        private static FtpWebRequest GetRequest(string name, UpLoadType upType)
        {
            try
            {
                string ftpUrl = string.Empty;
                string userName = string.Empty;
                string userPwd = string.Empty;
                switch (upType)
                {
                    case UpLoadType.pic:
                        ftpUrl = CommonVariables.FtpUrlPt;
                        userName = CommonVariables.FtpUserPt;
                        userPwd = CommonVariables.FtpPwdPt;
                        break;
                    case UpLoadType.app:
                        ftpUrl = CommonVariables.FtpUrlApp;
                        userName = CommonVariables.FtpUserApp;
                        userPwd = CommonVariables.FtpPwdApp;
                        break;
                }
                FtpWebRequest ftpWebRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpUrl + name));
                ftpWebRequest.Credentials = new NetworkCredential(userName, userPwd);
                return ftpWebRequest;
            }
            catch (Exception ex)
            {
                LogApi.DebugInfo(ex);
                return null;
            }
        }


    }
}
