using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.manager;
using Model;
using BMH.EagleEye.pageclass;
using BMH.EagleEye.BaseClass;
using YYLog.ClassLibrary;

namespace BMH.EagleEye.page
{
    public partial class login : System.Web.UI.Page
    {
        #region 定义变量
        public static readonly string strCookieUrl = System.Configuration.ConfigurationManager.AppSettings["Yingyan_CookieUrl"];
        public static readonly string strCookieName = System.Configuration.ConfigurationManager.AppSettings["Yingyan_CookieName"];

        AccountManager accountManager = null;
        Account account = null;
        protected string accountType = "";
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {            
            POMOHOCookie cookies = new POMOHOCookie();
            if (!cookies.IsLogin)
            {
                //Response.Redirect("/Account/Login.aspx");
            }
            else
            {
                string strAccountId = cookies.BeeAccountId;
                string strAccountName = cookies.BeeAccountName;
                string strAccountUserName = cookies.BeeAccountUserName;
                string strAccountType = cookies.BeeAccountType;
                string strHeadImageUrl = cookies.BeeHeadImageUrl;
                accountType = cookies.BeeAccountType;
                if (accountType == "2")
                {
                    Response.Redirect("/page/manager/index.aspx");
                }
                //else if (accountType == "1")
                //{
                //    Response.Redirect("/page/users/users_data.aspx");
                //}
                else if (accountType == "3"|| accountType == "1")
                {
                    Response.Redirect("/page/agent/index.aspx");
                }
                else
                {
                    Response.Write("<script>alert('未开放此用户类型');</script>");
                }
            
            }

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //获取
            string userName = txtUserName.Value;
            string passWord = txtPassWord.Value;
            //string accountType = sltAccountType.Value;
            if (string.IsNullOrWhiteSpace(userName))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "this", "<script>alert('账号不为空！');</script>");
                return;
            }
            if (string.IsNullOrWhiteSpace(passWord))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "this", "<script>alert('密码不为空！');</script>");
                return;
            }

            //验证
            accountManager = new AccountManager();
            account = new Account();
            if (!accountManager.CheckAccount(userName, passWord, out account))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "this", "<script>alert('账号或密码不正确！');</script>");
                return;
            }

            //登录成功，写Cookie
            System.Web.HttpCookie cookie = new System.Web.HttpCookie(strCookieName);
            cookie.Values.Add("AccountId", Encryptor.DesEncrypt((account.accountid).ToString(), strCookieUrl));
            cookie.Values.Add("AccountName", Encryptor.DesEncrypt((account.name).ToString(), strCookieUrl));
            cookie.Values.Add("AccountUserName", Encryptor.DesEncrypt((account.username).ToString(), strCookieUrl));
            cookie.Values.Add("AccountType", Encryptor.DesEncrypt((account.type).ToString(), strCookieUrl));
            cookie.Values.Add("HeadImageUrl", Encryptor.DesEncrypt((account.headimageurl).ToString(), strCookieUrl));

            //if (account.username == "admin")
            //if (account.type == "2")
            //{
            //    cookie.Values.Add("AdUserId", Encryptor.DesEncrypt(("1").ToString(), strCookieUrl));
            //}
            //else
            //{
             cookie.Values.Add("AdUserId", Encryptor.DesEncrypt((account.aduserid).ToString(), strCookieUrl));
            //}

            // cookie.Domain = strCookieUrl;
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);

            //日志
            string ip = IP.GetIP();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("accountid", account.accountid);
            dict.Add("accounttype", account.type);
            dict.Add("type", "1");//1 登录            
            dict.Add("ip", ip);
            dict.Add("ua", Request.UserAgent);
            accountManager.LogInfo(dict);

            //跳转 
            var reUrl = RequestVal("re_url");
            //http解码            
            reUrl = HttpUtility.UrlDecode(reUrl);
            //验证有效reurl
            if (!string.IsNullOrWhiteSpace(reUrl))
            {
                Response.Redirect(reUrl);
            }
            //else
            //{            
            if (account.type == "2")
            {
                Response.Redirect("/page/manager/index.aspx");
            }           
            else if (account.type == "3" || account.type == "1")
            {
                Response.Redirect("/page/agent/index.aspx");
            }
            else
            {
                Response.Write("<script>alert('未开放此用户类型');</script>");
            }
            // }
        }
        private string RequestVal(string key)
        {
            string requestVal = "";
            try
            {
                requestVal = Request[key].ToString();
            }
            catch
            {

                requestVal = "";
            }
            return requestVal;

        }

    }
}