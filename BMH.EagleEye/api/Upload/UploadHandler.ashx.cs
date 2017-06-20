using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace BMH.EagleEye.api.Upload
{
    /// <summary>
    /// UploadHandler 的摘要说明
    /// </summary>
    public class UploadHandler : IHttpHandler
    {
        private string _filedir = "";    //文件目录  
        public void ProcessRequest(HttpContext context)
        {
            _filedir = context.Server.MapPath(@"/file/temp/");
            try
            {
                string result = "3";
                string fileType = context.Request.QueryString["fileType"]; //获取上传文件类型  
                if (fileType == "file")
                {
                    result = UploadFile(context);  //文档上传  
                }
                else if (fileType == "img")
                {
                    result = UploadImg(context);   //图片上传  
                }
                context.Response.Write(result);
            }
            catch
            {
                context.Response.Write("3");//3文件上传失败  
            } 
        }

        /// <summary>  
        /// 文档上传  
        /// </summary>  
        /// <param name="context"></param>  
        /// <returns></returns>  
        private string UploadFile(HttpContext context)
        {
            int cout = context.Request.Files.Count;
            if (cout > 0)
            {
                HttpPostedFile hpf = context.Request.Files[0];
                if (hpf != null)
                {
                    string fileExt = Path.GetExtension(hpf.FileName).ToLower();
                    //只能上传文件，过滤不可上传的文件类型    
                    string fileFilt = ".rar|.zip|.pdf|.pdfx|.txt|.csv|.xls|.xlsx|.doc|.docx......";
                    if (fileFilt.IndexOf(fileExt) <= -1)
                    {
                        return "1";
                    }

                    //判断文件大小    
                    int length = hpf.ContentLength;
                    if (length > 2097152)
                    {
                        return "2";
                    }

                    Random rd = new Random();
                    DateTime nowTime = DateTime.Now;
                    string newFileName = nowTime.Year.ToString() + nowTime.Month.ToString() + nowTime.Day.ToString() + nowTime.Hour.ToString() + nowTime.Minute.ToString() + nowTime.Second.ToString() + rd.Next(1000, 1000000) + Path.GetExtension(hpf.FileName);
                    if (!Directory.Exists(_filedir))
                    {
                        Directory.CreateDirectory(_filedir);
                    }
                    string fileName = _filedir + newFileName;
                    hpf.SaveAs(fileName);
                    return newFileName;
                }

            }
            return "3";
        }

        /// <summary>  
        /// 图片上传  
        /// </summary>  
        /// <param name="context"></param>  
        /// <returns></returns>  
        private string UploadImg(HttpContext context)
        {
            int cout = context.Request.Files.Count;
            if (cout > 0)
            {
                HttpPostedFile hpf = context.Request.Files[0];
                if (hpf != null)
                {
                    string fileExt = Path.GetExtension(hpf.FileName).ToLower();
                    //只能上传文件，过滤不可上传的文件类型    
                    string fileFilt = ".gif|.jpg|.php|.jsp|.jpeg|.png|......";
                    if (fileFilt.IndexOf(fileExt) <= -1)
                    {
                        return "1";
                    }

                    //判断文件大小    
                    int length = hpf.ContentLength;
                    if (length > 204800)
                    {
                        return "2";
                    }

                    Random rd = new Random();
                    DateTime nowTime = DateTime.Now;
                    string newFileName = nowTime.Year.ToString() + nowTime.Month.ToString() + nowTime.Day.ToString() + nowTime.Hour.ToString() + nowTime.Minute.ToString() + nowTime.Second.ToString() + rd.Next(1000, 1000000) + Path.GetExtension(hpf.FileName);
                    if (!Directory.Exists(_filedir))
                    {
                        Directory.CreateDirectory(_filedir);
                    }
                    string fileName = _filedir + newFileName;
                    hpf.SaveAs(fileName);
                    return newFileName;
                }

            }
            return "3";
        } 

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}