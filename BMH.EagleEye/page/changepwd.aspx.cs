using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.manager;
using Model;
using BMH.EagleEye.pageclass;

namespace BMH.EagleEye.page
{
    public partial class changepwd : System.Web.UI.Page
    {
        #region 参数定义
        AccountManager accountManager;
        Account account;
        //private string accountId;
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

                YYLog.ClassLibrary.Log.WriteLog("YYLog.Login:/page/changepwd", "账户信息:账户id{0},账户名{1},ip{2}", strAccountId, strAccountName, pageclass.IP.GetIP());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            txtUserName.Value = accountUserName;
        }

        protected void confirm_Click(object sender, EventArgs e)
        {
            #region 获取值
            string oldPassWord = txtOldPwd.Value;
            string newPassWord = txtNewPwd.Value;

            #endregion

            #region 校验旧密码
            //验证
            accountManager = new AccountManager();
            account = new Account();
            if (!accountManager.CheckAccount(accountUserName, oldPassWord, out account))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "this", "<script>alert('密码不正确！');</script>");
                return;
            }
            #endregion 

            #region 校验新密码 二次校验
            //长度
            if(newPassWord.Length>16||newPassWord.Length<8)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "this", "<script>alert('新密码长度不合法！');</script>");
                return;               
            }
            //复杂度
            if (newPassWord==oldPassWord)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "this", "<script>alert('新密码不与旧密码相同！');</script>");
                return;
            }
            #endregion

            #region 写入新密码
            if (!accountManager.UpdatePassWord(account.accountid, newPassWord,account.accountid))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "this", "<script>alert('修改失败,请重试！');</script>");
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


            #region 返回登录
            ClientScript.RegisterClientScriptBlock(this.GetType(), "this", "<script>alert('修改成功！');location.href='/page/logout.aspx';</script>");
            return;
            #endregion
          
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/page/logout.aspx");
        }

        
    }
}