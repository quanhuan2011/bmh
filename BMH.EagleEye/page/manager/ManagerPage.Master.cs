using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using BLL.permission;

namespace BMH.EagleEye.page.manager
{
    public partial class ManagerPage : System.Web.UI.MasterPage
    {
        #region 定义参数
        VerifyPermission verifyPermission;
        public string accountId;
        public string accountName;
        public string accountUserName;
        public string accountType;
        public string headImageUrl;
        protected string resetHtml;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region 验证登录状态 暂时不用
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
                    if (accountUserName == "admin")
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<span class='line'>");
                        sb.Append("|");
                        sb.Append("</span>");
                        sb.Append("<a href='/page/resetpwd.aspx'class='exit' target='_blank'>");
                        sb.Append("重置密码");
                        sb.Append("</a>");
                        resetHtml = sb.ToString();                    
                    }
                    //add 创建广告主 20170330 
                    if (strAccountId == "8"|| strAccountId=="9" || strAccountId == "92" || strAccountId == "48")
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<span class='line'>");
                        sb.Append("|");
                        sb.Append("</span>");
                        sb.Append("<a href='/page/manager/adu_add.aspx'class='exit' target='_blank'>");
                        sb.Append("创建广告主");
                        sb.Append("</a>");
                        resetHtml = sb.ToString();
                    }
                    #region 权限控制
                    verifyPermission = new VerifyPermission();
                    if (verifyPermission.GetPermissionLevel(strAccountId) == "1")
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<span class='line'>");
                        sb.Append("|");
                        sb.Append("</span>");
                        sb.Append("<a href='/page/manager/recharge.aspx'class='exit' target='_blank'>");
                        sb.Append("管理员充值");
                        sb.Append("</a>");
                        sb.Append("<span class='line'>");
                        sb.Append("|");
                        sb.Append("</span>");
                        sb.Append("<a href='/page/manager/adu_setting.aspx'class='exit' target='_blank'>");
                        sb.Append("广告主设置");
                        sb.Append("</a>");
                        resetHtml = resetHtml+sb.ToString();
                    }
                    #endregion
                }
                #endregion
            }

        }
    }
}