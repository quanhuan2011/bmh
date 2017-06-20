using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using BLL.manager;
using BMH.EagleEye.pageclass;
using Common.pub;
using BLL.users;
using Model;

namespace BMH.EagleEye.page.users
{
    public partial class recharge_info : System.Web.UI.Page
    {
        #region 变量定义
        AdUserUsers adUserUsers;
        AccountManager accountManager;
        public  AdUser adUser;
        string accountId = "";
        string sqlWhere = string.Empty;
        System.Configuration.AppSettingsReader appReader = new System.Configuration.AppSettingsReader();
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


                accountId = strAccountId;
                //aduserid = strAdUserId;

            }
        }




        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (Session["BeeUserName"] != null && Session["BeeAccountType"] != null)
                //{
                //    accountId = Session["BeeAccountId"].ToString();
                //}
                int pageNo = int.Parse(CommonBase.GetRequestVal( "page", "1"));
                string defaultPageSize = appReader.GetValue("DefaultPageSize", typeof(string)).ToString();
                string defaultPageNumber = appReader.GetValue("DefaultPageNumber", typeof(string)).ToString();
                int pageSize = string.IsNullOrEmpty(defaultPageSize) ? 15 : int.Parse(defaultPageSize);
                int pageNumber = string.IsNullOrEmpty(defaultPageNumber) ? 4 : int.Parse(defaultPageNumber);

                accountManager = new AccountManager();
                adUser = new AdUser();
                adUser= accountManager.GetAdUserInfo(accountId);
                GetListData(adUser.aduserid, pageSize, pageNo, pageNumber, sqlWhere);
            }

        }
        public void GetListData(string adUserId, int pageSize, int pageNo, int pageNumber, string sqlWhere)
        {
            adUserUsers = new AdUserUsers();
            int dataCount = 0;
            DataTable dt = adUserUsers.GetReChargeListDT(adUserId,pageSize, pageNo, sqlWhere, out dataCount);
            if (dt != null && dt.Rows.Count > 0)
            {
                this.repList.DataSource = dt;
                this.repList.DataBind();
                litPage.Text = PageTxt.GetPageText(GetUrl(), pageNumber, dataCount, pageSize, pageNo);
                var pageCount = Convert.ToInt32(System.Math.Ceiling(Convert.ToDouble(dataCount) / Convert.ToDouble(pageSize)));
                litPageInfo.Text = string.Format("共有<font style='color: red;'>{0}</font>条&nbsp;&nbsp;<font style='color: red;'>{1}</font>/<span>{2}</span>页", dataCount, pageNo, pageCount);
            }
            else
            {
                litNoInfo.Text = "<tr><td colspan='9'><p style='width:100%; line-height:200px; text-align:center'>无数据...</p></td></tr>";
            }

        }
        public string GetUrl()
        {
            StringBuilder url = new StringBuilder();
            url.Append("recharge_info.aspx?");            
            url.Append("&page=");
            return url.ToString();
        }       
    }
}