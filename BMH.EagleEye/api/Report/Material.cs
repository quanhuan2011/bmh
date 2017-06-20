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
        public void GetMaterialSum(string materialid, string starttime, string endtime)
        {
            MaterialReport material = new MaterialReport();
            string resultData = material.GetMaterialSum(materialid, starttime, endtime);
            Context.Response.Charset = "utf-8"; //设置字符集类型  
            Context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            Context.Response.Write(resultData);
            Context.Response.End();
        }
        [WebMethod(EnableSession = true, Description = "获取物料报表中列表数据,showlist为逗号分隔")]
        public void GetMaterialList(string materialid, string starttime, string endtime, string dimensiontype)
        {
            MaterialReport material = new MaterialReport();
            string resultData = material.GetMaterialList(materialid, starttime, endtime, dimensiontype);
            Context.Response.Charset = "utf-8"; //设置字符集类型  
            Context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            Context.Response.Write(resultData);
            Context.Response.End();
        }

    }
}
