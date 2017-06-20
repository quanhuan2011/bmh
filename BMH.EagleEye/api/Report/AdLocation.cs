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
        [WebMethod(EnableSession = true, Description = "获取物料报表中总数量数据")]
        public void GetAdLocationSum(string adlocationid, string starttime, string endtime)
        {            
            AdLocationReport adLocation = new AdLocationReport();
            string resultData = adLocation.GetAdLocationSum(adlocationid, starttime, endtime);
            Context.Response.Charset = "utf-8"; //设置字符集类型  
            Context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            Context.Response.Write(resultData);
            Context.Response.End();
        }
        [WebMethod(EnableSession = true, Description = "获取物料报表中列表数据")]
        public void GetAdLocationList(string adlocationid, string starttime, string endtime, string dimensiontype)
        {
            AdLocationReport adLocation = new AdLocationReport();
            string resultData = adLocation.GetAdLocationList(adlocationid, starttime, endtime, dimensiontype);
            Context.Response.Charset = "utf-8"; //设置字符集类型  
            Context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            Context.Response.Write(resultData);
            Context.Response.End();
        }

    }
}
