﻿using BLL.manager;
using BLL.permission;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMH.EagleEye.page.manager
{
    public partial class recharge : System.Web.UI.Page
    {
        #region 变量定义
        AdUserManager adUserManager;
        VerifyPermission verifyPermission;
        protected string adUserList = string.Empty;
        protected string adUId = string.Empty;
        protected string balance = string.Empty;
        protected string strBindAccountId;
        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            BaseClass.POMOHOCookie cookies = new BaseClass.POMOHOCookie();
            if (!cookies.IsLogin)
            {

                Response.Redirect(string.Format("/page/login.aspx?re_url={0}", HttpUtility.UrlEncode(Request.Url.ToString())));
            }
            else
            {
                string strAccountId = cookies.BeeAccountId;
                string strAccountName = cookies.BeeAccountName;
                string strAccountUserName = cookies.BeeAccountUserName;
                string strAccountType = cookies.BeeAccountType;
                string strHeadImageUrl = cookies.BeeHeadImageUrl;
                string strAdUserId = cookies.BeeAdUserId;
                strBindAccountId = strAccountId;
                YYLog.ClassLibrary.Log.WriteLog("YYLog.Login:/page/manager/recharge", "账户信息:账户id{0},账户名{1},ip{2}", strAccountId, strAccountName, pageclass.IP.GetIP());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region 权限控制
                verifyPermission = new VerifyPermission();
                if (verifyPermission.GetPermissionLevel(strBindAccountId) != "1")
                {
                    Response.Redirect("/page/manger/index.aspx");
                }
                #endregion

                #region 获取数据
                adUserList = GetAdUserList(out adUId);
                balance=GetBalance(adUId,"");
                #endregion
            }
        }
        /// <summary>
        /// 获取广告主列表
        /// </summary>
        /// <returns></returns>
        private string GetAdUserList(out string strDefault)
        {
            strDefault = "";
            adUserManager = new AdUserManager();
            DataTable dt = adUserManager.GetAdUserNameDT("");
            StringBuilder sb = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        strDefault = dt.Rows[i]["aduserid"].ToString();
                        sb.Append(string.Format("<option value='{0}' selected='selected'>", dt.Rows[i]["aduserid"].ToString()));
                    }
                    else
                    {
                        sb.Append(string.Format("<option value='{0}'>", dt.Rows[i]["aduserid"].ToString()));
                    }
                    sb.Append(dt.Rows[i]["adusername"].ToString());
                    sb.Append("</option>");

                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 获取余额
        /// </summary>
        /// <param name="adUserId"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        private string GetBalance(string adUserId, string sqlWhere)
        {
            string strRet = "";
            adUserManager = new AdUserManager();
            DataTable dt = adUserManager.GetAdUserInfoDT(adUserId, sqlWhere);
            if (dt != null && dt.Rows.Count > 0)
            {
                strRet = dt.Rows[0]["balance"].ToString();
            }

           return strRet;
        }
    }
}