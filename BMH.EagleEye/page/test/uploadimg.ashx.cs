using BLL.pub;
using MongoUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BMH.EagleEye.page.test
{
    /// <summary>
    /// uploadimg1 的摘要说明
    /// </summary>
    public class uploadimg1 : IHttpHandler
    {
        #region 定义变量
        public string strUserId = string.Empty;
        public string strFileName = string.Empty;
        public string strFileType = string.Empty;
        public string strFileMD5 = string.Empty;
        public string strFileUrl = string.Empty;
        public int intImgHeight = 0;
        public int intImgWidth = 0;
        public int intFileSize = 0;

        public DateTime dtUploadTime = DateTime.Now;

        #endregion
        public void ProcessRequest(HttpContext context)
        {
            //  0：图片存储成功
            //  1：缺少UserId和加密字符串
            //  2：加密字符串错误
            //  3：缺少图片文件
            //  4：图片文件格式错误（png,jpg,gif格式）
            //  5：图片存储失败
            //  6：数据存储失败

            //  999：未知错误

           // context.Response.ContentType = "text/plain";

            int intCode = 999;
            string strReason = "未知错误";
            string tempNo = "";
            Common.pub.LogApi.LogInfo("uploadimg", "step1");
            Hashtable result = new Hashtable();

            try
            {
                HttpFileCollection files = HttpContext.Current.Request.Files;

                if (files.Count > 0)
                {
                    System.Drawing.Image img = null;

                    img = System.Drawing.Image.FromStream(files[0].InputStream, true);

                    strFileType = files[0].FileName.Substring(files[0].FileName.LastIndexOf('.') + 1).ToLower();

                    byte[] fileBytes = null;
                    using (var memoryStream = new MemoryStream())
                    {
                        img.Save(memoryStream, img.RawFormat);
                        fileBytes = memoryStream.ToArray();
                    }

                    string strFileBytes = fileBytes[0] + "," + fileBytes[1];

                    if (strFileBytes.Contains("255,216") || strFileBytes.Contains("71,73") || strFileBytes.Contains("66,77") || strFileBytes.Contains("137,80"))//判断文件类型
                    {
                        //保存到数据库
                        if (UploadHelper.SaveAgentImg(fileBytes, ref tempNo))
                        {
                            BLL.pub.Result.errCode = BLL.pub.Result.successCode;
                            BLL.pub.Result.errMsg = "上传成功";
                        }
                        else
                        {
                            BLL.pub.Result.errCode = BLL.pub.Result.failCode;
                            BLL.pub.Result.errMsg = "上传失败";
                        }

                    }
                    else
                    {
                        Common.pub.LogApi.LogInfo("uploadimg", "图片文件格式错误");
                        intCode = 4;
                        strReason = "图片文件格式错误";
                    }
                }
                else
                {
                    Common.pub.LogApi.LogInfo("uploadimg", "缺少图片文件");
                    intCode = 3;
                    strReason = "缺少图片文件";
                }

            }
            catch (Exception ex)
            {
                Common.pub.LogApi.LogInfo("uploadimg", ex.ToString());
                BLL.pub.Result.errCode = BLL.pub.Result.failCode;
                BLL.pub.Result.errMsg = "上传异常"+ex.Message;

            }
            //BLL.pub.Result.GetResult(BLL.pub.Result.errCode, BLL.pub.Result.errMsg, tempNo);
           // string res = "{ error:'" + tempNo + "', msg:'" + tempNo + "',fileurl:'" + tempNo + "'}";
            context.Response.Write(BLL.pub.Result.GetResult(BLL.pub.Result.errCode, BLL.pub.Result.errMsg, tempNo));
            context.Response.End();
         
            ////// MemoryStream ms = new MemoryStream();
            //context.Response.ContentType = "image/gif";
            //System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
            //image.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public class ImgClass
        {

            public ImgClass()
            {
            }

            /// <summary>
            /// 上传文件名
            /// </summary>
            public string FileName { get; set; }

            /// <summary>
            /// 上传用户ID
            /// </summary>
            public string UserID { get; set; }

            /// <summary>
            /// 图片宽度
            /// </summary>
            public int Width { get; set; }

            /// <summary>
            /// 图片高度
            /// </summary>
            public int Heigth { get; set; }

            /// <summary>
            /// 图片大小
            /// </summary>
            public int FileSize { get; set; }

            /// <summary>
            /// 上传时间
            /// </summary>
            public DateTime UploadTime { get; set; }

            /// <summary>
            /// 文件类型
            /// </summary>
            public string FileType { get; set; }

            /// <summary>
            /// 图片状态
            /// </summary>
            public string Status { get; set; }

            /// <summary>
            /// 图片MD5
            /// </summary>
            public string MD5 { get; set; }

        }

    }
}