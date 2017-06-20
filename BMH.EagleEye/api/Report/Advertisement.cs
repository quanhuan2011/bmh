using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Web.Services;
using BLL.report;


namespace BMH.EagleEye.api
{
    public partial class Report 
    {
        [WebMethod(EnableSession = true, Description = "获取广告报表中总数量数据")]
        public void GetAdSum(string adid, string starttime, string endtime)
        {
            AdvertisementReport ad = new AdvertisementReport();
            string resultData = ad.GetAdSum(adid, starttime, endtime);
            Context.Response.Charset = "utf-8"; //设置字符集类型  
            Context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            Context.Response.Write(resultData);
            Context.Response.End();
        }
        [WebMethod(EnableSession = true, Description = "获取广告报表中列表数据")]
        public void GetAdList(string adid, string starttime, string endtime, string dimensiontype)
        {
            AdvertisementReport ad = new AdvertisementReport();
            string resultData = ad.GetAdList(adid, starttime, endtime, dimensiontype);
            Context.Response.Charset = "utf-8"; //设置字符集类型  
            Context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            Context.Response.Write(resultData);
            Context.Response.End();
        }
        
    }
}
