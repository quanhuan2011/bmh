using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.manager;

namespace BMH.EagleEye.page.users
{
    public partial class users_data : System.Web.UI.Page
    {
        public string adUserId;
        public string accountId;

        //tyuragsdv



        protected void Page_Load(object sender, EventArgs e)
        {


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
                string strAdUserId = cookies.BeeAdUserId;

                accountId = strAccountId;
                adUserId = strAdUserId;

                //if (accountType != "2")
                //{//判断是否是管理员登录
                //    Response.Redirect("/page/login.aspx");
                //}
                //if (string.IsNullOrEmpty(headImageUrl))
                //{
                //    headImageUrl = "http://yingyan.baomihua.com/page/images/head.jpg";
                //}


                //if (Session["BeeUserName"] != null && Session["BeeAccountType"] != null)
                //{
                //    adUserId = GetAdUserId(Session["BeeAccountId"].ToString());
                //}
            }

            #endregion
        }
        //public string GetAdUserId(string accountId)
        //{
        //    AccountManager accountManager = new AccountManager();
        //    return accountManager.GetAdUserId(accountId);            
        
        //}

        
    }
}