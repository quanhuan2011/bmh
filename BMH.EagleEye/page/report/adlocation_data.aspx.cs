using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.report;

namespace BMH.EagleEye.page.report
{
    public partial class adlocation_data : System.Web.UI.Page
    {
        public string adLocationId;
        public string adLocationName;
        private string accountType;
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
                accountType = strAccountType;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region 验证登录状态
                if (accountType != "2")
                {
                    Response.Redirect("/page/login.aspx");
                }
                #endregion

                adLocationId = GetRequestVal("adlocationid");
                AdLocationReport adLocation=new AdLocationReport ();
                adLocationName = adLocation.GetAdLocationName(adLocationId);
            }
        }
        private string GetRequestVal(string key)
        {
            string result = "";
            try
            {
                result = Request[key].ToString();
            }
            catch
            {
                result = "1";
            }
            return result;

        }
        
    }
}