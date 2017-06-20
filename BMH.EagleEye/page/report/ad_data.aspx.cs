using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.report;
using System.Data;

namespace BMH.EagleEye.page.report
{
    public partial class ad_data : System.Web.UI.Page
    {
        #region 变量定义
        public string adId;
        public string adName;
        private string accountType;
        public string billingType="1";//默认cpc
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

                adId = GetRequestVal("adid");
                AdvertisementReport ad = new AdvertisementReport();
                //adName = ad.GetAdName(adId);
                DataTable dt = ad.GetAdInfoDT(adId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    adName = dt.Rows[0]["name"].ToString();
                    billingType = dt.Rows[0]["billingtype"].ToString();
                }
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