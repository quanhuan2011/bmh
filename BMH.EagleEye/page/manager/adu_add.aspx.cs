using BLL.permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMH.EagleEye.page.manager
{
    public partial class adu_add : System.Web.UI.Page
    {
        protected string permLevel = "-1";//权限等级 -1 无编辑 1 可编辑
        protected VerifyPermission verifyPermission;
        protected string strBindAccountId = "";

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
                strBindAccountId = strAccountId;
                YYLog.ClassLibrary.Log.WriteLog("YYLog.Login:/page/manager/adu_add", "账户信息:账户id{0},账户名{1},ip{2}", strAccountId, strAccountName, pageclass.IP.GetIP());

#if TalentDebug
              
#else
                //特殊权限
                if (strAccountId != "92"&& strAccountId != "9" && strAccountId != "48")
                    Response.Redirect("/page/Login.aspx");
#endif
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //#region 权限控制
                //verifyPermission = new VerifyPermission();
                //permLevel = verifyPermission.GetPermissionLevel(strBindAccountId);

                //#endregion

            }
        }
    }
}