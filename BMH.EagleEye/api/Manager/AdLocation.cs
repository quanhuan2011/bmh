using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Web.Services;
using System.Text;
using Model;
using BLL.manager;


namespace BMH.EagleEye.api
{    
    public partial class Manager
    {
        [WebMethod(EnableSession = true, Description = "获取广告位管理列表数据")]
        public void GetAdLocationListHtml(string pagesize, string pageno,string sqlwhere)
        {
            #region 定义变量
            AdLocationManager adLocationManager = new AdLocationManager();
            #endregion

            #region 获取数据
            string resultData = adLocationManager.GetAdLocationListHtml(int.Parse(pagesize), int.Parse(pageno),sqlwhere);
            #endregion

            Context.Response.Charset = "utf-8"; //设置字符集类型  
            Context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            Context.Response.Write(resultData);
            Context.Response.End();
        }       
    }
}