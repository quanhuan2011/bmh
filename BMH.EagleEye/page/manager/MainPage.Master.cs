using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.pub;

namespace BMH.EagleEye.page.manager
{
    public partial class MainPage : System.Web.UI.MasterPage
    {
        public string accountId;
        public string accountName;
        public string accountUserName;
        public string accountType;
        public string headImageUrl;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                #region 验证登录状态 Seesion 暂时不用
                //if (Session["BeeUserName"] != null && Session["BeeAccountType"] != null)
                //{
                //    accountName = Session["BeeAccountName"].ToString();
                //    string accountType = Session["BeeAccountType"].ToString();
                //    headImageUrl = Session["BeeHeadImageUrl"].ToString();
                //    if (accountType != "2")
                //    {
                //        Response.Redirect("../login.aspx");
                //    }
                //    if (string.IsNullOrEmpty(headImageUrl))
                //    {
                //        headImageUrl = "http://yingyan.baomihua.com/page/images/head.jpg";
                //    }
                //}
                //else
                //{
                //    string url = Request.Url.ToString();
                //    url = HttpUtility.UrlDecode(url);
                //    Response.Redirect(string.Format("../login.aspx?re_url={0}", url));
                //}
                #endregion


                #region 登录验证 Cookie验证
                BaseClass.POMOHOCookie cookies = new BaseClass.POMOHOCookie();
                if (!cookies.IsLogin)
                {
                    Response.Redirect("/page/Login.aspx");
                }
                else
                {
                    string strAccountId = cookies.BeeAccountId;
                    string strAccountName = cookies.BeeAccountName;
                    string strAccountUserName = cookies.BeeAccountUserName;
                    string strAccountType = cookies.BeeAccountType;
                    string strHeadImageUrl = cookies.BeeHeadImageUrl;

                    accountId = strAccountId;
                    accountName = strAccountName;
                    accountUserName = strAccountUserName;
                    accountType = strAccountType;
                    headImageUrl = strHeadImageUrl;

                    if (accountType != "2")
                    {//判断是否是管理员登录
                        Response.Redirect("/page/login.aspx");
                    }
                    if (string.IsNullOrEmpty(headImageUrl))
                    {
                        headImageUrl = "http://yingyan.baomihua.com/page/images/head.jpg";
                    }
                }

                #endregion
            }

        }
    }
}