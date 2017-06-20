using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMH.EagleEye.page
{
    public partial class LogOut : System.Web.UI.Page
    {
        public static readonly string strCookieUrl = System.Configuration.ConfigurationManager.AppSettings["Yingyan_CookieUrl"];
        public static readonly string strCookieName = System.Configuration.ConfigurationManager.AppSettings["Yingyan_CookieName"];

        protected void Page_Load(object sender, EventArgs e)
        {
            ClearAndAbandon(Session);
            CleanCookie();
            Response.Redirect("/page/login.aspx");
        }

        private void CleanCookie()
        {
            HttpCookie cookie = new HttpCookie(strCookieName)
            {
                Expires = DateTime.Now.AddDays(-300.0)
            };
            cookie.Values.Add("userid", null);
            cookie.Values.Add("username", null);
            //cookie.Domain = strCookieUrl;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void ClearAndAbandon(HttpSessionState session)
        {//清空所有对象并取消会话
            session.Clear();
            session.Abandon();
        }

    }
}