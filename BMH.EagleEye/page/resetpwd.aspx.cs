using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.manager;
using Model;
using BMH.EagleEye.pageclass;
using Common.pub;

namespace BMH.EagleEye.page
{
    public partial class resetpwd : System.Web.UI.Page
    {
        #region 定义参数
        AccountManager accountManager;
        Account account;
        private string accountUserName;
        private string accountType;
        #endregion
        protected void Page_Init(object sender, EventArgs e)
        {
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

                accountUserName = strAccountUserName;
                accountType = strAccountType;

                YYLog.ClassLibrary.Log.WriteLog("YYLog.Login:/page/resetpwd", "账户信息:账户id{0},账户名{1},ip{2}", strAccountId, strAccountName, pageclass.IP.GetIP());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //限制管理
            if (accountUserName != "admin" && accountType != "2")
            {
                Response.Redirect("/page/Login.aspx");
            }
            txtUserName.Value = accountUserName;
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            #region 获取值
            //管理员密码
            string managerPassWord = txtPwd.Value;
            string newUserName=txtNewUserName.Value;
            string newPassWord = "";
            #endregion

            #region 校验管理员密码
            //验证
            accountManager = new AccountManager();
            account = new Account();
            Account accountByUser = new Account();  
            if (!accountManager.CheckAccount(accountUserName, managerPassWord,  out account))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "this", "<script>alert('密码不正确！');</script>");
                return;
            }
            #endregion

            #region 获取帐号信息及验证
            if (string.IsNullOrWhiteSpace(newUserName))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "this", "<script>alert('请输入账户！');</script>");
                return;
            }   
            accountByUser = accountManager.GetAccountInfo(newUserName);
            if (accountByUser == null||accountByUser.accountid==null)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "this", "<script>alert('账户不存在！');</script>");
                return;
            }                       
            #endregion

            #region 随机12位密码
            newPassWord = Utility.GetRandomStr(12);
            #endregion

            #region 写入新密码
            if (!accountManager.UpdatePassWord(accountByUser.accountid, newPassWord,account.accountid))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "this", "<script>alert('重置失败,请重试！');</script>");
                return;
            }
            #endregion

            #region 日志记录
            //日志
            string ip = IP.GetIP();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("accountid", account.accountid);
            dict.Add("accounttype", account.type);
            dict.Add("type", "2");//2 修改密码            
            dict.Add("ip", ip);
            dict.Add("ua", Request.UserAgent);
            accountManager.LogInfo(dict);
            #endregion


            #region 返回提示
            txtNewPwd.Value = newPassWord;
            ClientScript.RegisterClientScriptBlock(this.GetType(), "this", "<script>alert('重置成功！');</script>");
            return;
            #endregion
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/page/logout.aspx");
        }
    }
}