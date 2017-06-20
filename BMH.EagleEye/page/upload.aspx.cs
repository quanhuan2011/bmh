using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.pub;
using Common.ftp;
using System.IO;

namespace BMH.EagleEye.page
{
    /// <summary>
    /// 文件上传后台处理程序
    /// by wangjc 20161020
    /// </summary>
    public partial class upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpFileCollection files = Request.Files;//这里只能用<input type="file" />才能有效果,因为服务器控件是HttpInputFile类型
            string msg = string.Empty;
            string error = string.Empty;
            string fileurl = string.Empty;
            string fileName = string.Empty;
            if (files.Count > 0)
            {
                //files[0].SaveAs(Server.MapPath("/") + System.IO.Path.GetFileName(files[0].FileName));
                Random rd = new Random();
               
                string fileFormat = Path.GetExtension(files[0].FileName);

                //Path.GetFileName(files[0].FileName)
                fileName = string.Format("{0}_{1}{2}", DateTime.Now.ToString("yyyyMMddHHmmss"), rd.Next(1000, 9999).ToString(),fileFormat);
                files[0].SaveAs(ConnomMethod.GetFileUploadPath() + "\\" + fileName);
                string fileType = Request["type"] == null ? "pic" : Request["type"].ToString();
                bool isOk = ArchiveFtpManager.UploadFile(fileName, fileType == "pic" ? Model.UpLoadType.pic : Model.UpLoadType.app);

                //msg = " 成功! 文件大小为:" + files[0].ContentLength;
                //fileurl = "/FileUpload/" + fileName;
                if (isOk)
                {
                    fileurl = string.Format("{0}{1}{2}/{3}", fileType == "pic" ? CommonVariables.PtUrl : CommonVariables.AppUrl, DateTime.Now.Year, DateTime.Now.Month, fileName);
                }

                string res = "{ error:'" + error + "', msg:'" + msg + "',fileurl:'" + fileurl + "'}";
                Response.Write(res);
                Response.End();
            }
        }
    }
}