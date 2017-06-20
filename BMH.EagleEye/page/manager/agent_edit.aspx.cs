using BLL.manager;
using BLL.permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMH.EagleEye.page.manager
{
    public partial class agent_edit : System.Web.UI.Page
    {
        #region 变量定义        
        AdUserManager adUserManager;
        AdLocationManager adLocationManager;
        PageManager pageManager;
        VerifyPermission verifyPermission;
        System.Configuration.AppSettingsReader appReader = new System.Configuration.AppSettingsReader();
        // 广告主ID
        public string aduserid = string.Empty;
        protected string strBindAccountId = "";

        string sqlWhere = string.Empty;
        public string provinceList = string.Empty;
        public string cityList = string.Empty;


        public string statusDefault = string.Empty;
        public string statusList = string.Empty;
        public string pageDefault = string.Empty;
        public string pageList = string.Empty;
        public string adUserDefault = string.Empty;
        public string adUserList = string.Empty;
        public string adLocationDefault = string.Empty;
        public string adLocationList = string.Empty;
        protected string permLevel = "-1";//权限等级 -1 无编辑 1 可编辑

        #endregion
        protected void Page_Init(object sender, EventArgs e)
        {
#if TalentDebug
            strBindAccountId = "16";
            aduserid = "6";
#else
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
                strBindAccountId = strAccountId;
                aduserid = strAdUserId;
                YYLog.ClassLibrary.Log.WriteLog("YYLog.Login:/page/manager/agent_edit", "账户信息:账户id{0},账户名{1},ip{2}", strAccountId, strAccountName, pageclass.IP.GetIP());
            }
#endif
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}