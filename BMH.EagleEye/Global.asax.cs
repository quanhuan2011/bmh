using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using YYLog.ClassLibrary;

namespace BMH.EagleEye
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            //查找服务器ip地址
            int serverId = 28;
            //string localIp = Convert.ToString(HttpContext.Current.Request.ServerVariables.Get("Local_Addr"));
            //if (!string.IsNullOrEmpty(localIp))
            //{
            //    string[] tempStr = localIp.Split(',');
            //    if (tempStr.Length > 0)
            //    {
            //        int.TryParse(tempStr[tempStr.Length - 1], out serverId);
            //    }
            //}           
            Log.Init(serverId, 1024 * 1024 * 4, "yyyyMMdd", System.AppDomain.CurrentDomain.BaseDirectory + "/log", LogType.Debug);            
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}