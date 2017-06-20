using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Web.Services;
using BLL.manager;


namespace BMH.EagleEye.api
{
    
    public partial class Manager
    {
        [WebMethod(EnableSession = true, Description = "获取地域定向列表数据")]
        public void GetRegionData(string userid)
        {
            #region 定义变量
            DirectManager directManager = new DirectManager();            
            #endregion

            #region 获取数据
            string resultData = directManager.GetRegionData(userid);
            #endregion

            Context.Response.Charset = "utf-8"; //设置字符集类型  
            Context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            Context.Response.Write(resultData);
            Context.Response.End();
        }
        [WebMethod(EnableSession = true, Description = "获取地域定向城市列表数据")]
        public void GetRegionOfCityData(string userid, string provinceid)
        {
            #region 定义变量
            DirectManager directManager = new DirectManager();            
            #endregion

            #region 获取数据
            string resultData = directManager.GetRegionOfCityData(userid,provinceid);
            #endregion

            Context.Response.Charset = "utf-8"; //设置字符集类型  
            Context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            Context.Response.Write(resultData);
            Context.Response.End();
        }
    }
}
