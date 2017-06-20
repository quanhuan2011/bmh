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
        [WebMethod(EnableSession = true, Description = "获取广告主报表中总数量数据")]
        public void GetAdSumOfAdUser(string aduserid)
        {
            AdUserReport ad = new AdUserReport();
            string resultData = ad.GetAdSumOfAdUser(aduserid);
            Context.Response.Charset = "utf-8"; //设置字符集类型  
            Context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            Context.Response.Write(resultData);
            Context.Response.End();
        }
        [WebMethod(EnableSession = true, Description = "获取广告主报表中总数量数据")]
        public void GetAdSumOfAdUserByTime(string aduserid, string starttime, string endtime)
        {
            AdUserReport ad = new AdUserReport();
            string resultData = ad.GetAdSumOfAdUserByTime(aduserid, starttime, endtime);
            Context.Response.Charset = "utf-8"; //设置字符集类型  
            Context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            Context.Response.Write(resultData);
            Context.Response.End();
        }
        [WebMethod(EnableSession = true, Description = "获取广告主报表中列表数据")]
        public void GetAdListOfAdUser(string aduserid, string starttime, string endtime, string dimensiontype)
        {
            AdUserReport ad = new AdUserReport();
            string resultData = ad.GetAdListOfAdUser(aduserid, starttime, endtime, dimensiontype);
            Context.Response.Charset = "utf-8"; //设置字符集类型  
            Context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            Context.Response.Write(resultData);
            Context.Response.End();
        }
    }
}
