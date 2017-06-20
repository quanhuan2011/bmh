using BLL.img;
using CVBUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMH.EagleEye.api
{
    /// <summary>
    /// ImgHandler 的摘要说明
    /// </summary>
    public class ImgHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string method = GetRequestVal("method");
            string result = "";
            switch (method)
            {
                case "GetImgByAdv":
                    result = GetImgByAdv();
                    break;               
                default:

                    result = BLL.pub.Result.GetFailResult("错误的调用方式，此方法不存在");
                    break;
            }

            context.Response.Write(result);
        }
        private string GetImgByAdv()
        {
            //广告id
            string adId = GetRequestVal("adid");
            if(string.IsNullOrEmpty(adId))
                return BLL.pub.Result.GetFailResult("adid为空！");
            AdvImg advimg = new AdvImg();
            return advimg.GetImgByAdv(adId);
        }
        private string GetRequestVal(string key)
        {
            try
            {
                return HttpContext.Current.Request[key].ToString();
            }
            catch
            {
                return "";
            }

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