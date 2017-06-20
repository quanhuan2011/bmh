using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using BLL.pub;
using System.Web.Services;
using BLL.manager;


namespace BMH.EagleEye.api
{
    public partial class Manager
    {
        [WebMethod(EnableSession = true, Description = "获取广告管理列表数据")]
        public void GetAdListHtml(string pagesize, string pageno)
        {
            #region 定义变量
            AdvertisementManager adManager = new AdvertisementManager();
            #endregion

            #region 获取数据
            string resultData = adManager.GetAdListHtml(int.Parse(pagesize), int.Parse(pageno));
            #endregion

            Context.Response.Charset = "utf-8"; //设置字符集类型  
            Context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            Context.Response.Write(resultData);
            Context.Response.End();
        }
        [WebMethod(EnableSession = true, Description = "获取广告管理列表数据")]
        public void GetAdInfo(string userid,string adid)
        {
            #region 定义变量
            AdvertisementManager adManager = new AdvertisementManager();
            #endregion

            #region 获取数据
            string resultData = adManager.GetAdInfo( userid,adid);
            #endregion

            Context.Response.Charset = "utf-8"; //设置字符集类型  
            Context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            Context.Response.Write(resultData);
            Context.Response.End();
        }
        [WebMethod(EnableSession = true, Description = "获取广告管理列表数据2")]
        public void GetAdInfoByAdu(string adid)
        {
            #region 定义变量
            AdvertisementManager adManager = new AdvertisementManager();
            #endregion

            #region 获取数据
            string resultData = adManager.GetAdInfoByAdu(adid);
            #endregion

            Context.Response.Charset = "utf-8"; //设置字符集类型  
            Context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            Context.Response.Write(resultData);
            Context.Response.End();
        }
    }
}
