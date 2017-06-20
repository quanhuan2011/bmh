using BLL.pub;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMH.EagleEye.page.test
{
    public partial class showimg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string fileno = Request["fileno"].ToString();
            if (string.IsNullOrWhiteSpace(fileno))
            {
                return;
            }

            byte[] bytes = null;
            bytes = GetImgBytes(fileno);
            if (bytes != null)
            {
                MemoryStream ms = new MemoryStream(bytes);
                Response.ContentType = "image/gif";
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
                image.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
                Response.End();
            }
        }
        private byte[] GetImgBytes(string fileno)
        {
            return UploadHelper.GetImgBytes(fileno);
        }
    }
}