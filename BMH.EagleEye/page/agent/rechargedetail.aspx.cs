using BLL.manager;
using BLL.users;
using BMH.EagleEye.pageclass;
using Common.pub;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMH.EagleEye.page.agent
{
    public partial class rechargedetail : System.Web.UI.Page
    {
        #region 变量定义
        AdUserUsers adUserUsers;
        AccountManager accountManager;
        public AdUser adUser;
        string accountId = "";
        string sqlWhere = string.Empty;
        protected string headImageUrl;
        protected string accountName;
        public string accountType = string.Empty;
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
                accountName = strAccountName;
                accountType = strAccountType;
                if (string.IsNullOrEmpty(headImageUrl))
                {
                    headImageUrl = "http://yingyan.baomihua.com/page/images/head.jpg";
                }
                accountId = strAccountId;
                //aduserid = strAdUserId;
                YYLog.ClassLibrary.Log.WriteLog("YYLog.Login:/page/agent/rechargedetail", "账户信息:账户id{0},账户名{1},ip{2}", strAccountId, strAccountName, pageclass.IP.GetIP());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int pageNo = int.Parse(CommonBase.GetRequestVal("page", "1"));
                string defaultPageSize = appReader.GetValue("DefaultPageSize", typeof(string)).ToString();
                string defaultPageNumber = appReader.GetValue("DefaultPageNumber", typeof(string)).ToString();
                int pageSize = string.IsNullOrEmpty(defaultPageSize) ? 15 : int.Parse(defaultPageSize);
                int pageNumber = string.IsNullOrEmpty(defaultPageNumber) ? 4 : int.Parse(defaultPageNumber);

                accountManager = new AccountManager();
                adUser = new AdUser();
                adUser = accountManager.GetAdUserInfo(accountId);
                GetListData(adUser.aduserid, pageSize, pageNo, pageNumber, sqlWhere);
            }

        }
        public void GetListData(string adUserId, int pageSize, int pageNo, int pageNumber, string sqlWhere)
        {
            adUserUsers = new AdUserUsers();
            int dataCount = 0;
            DataTable dt = adUserUsers.GetReChargeListDT(adUserId, pageSize, pageNo, sqlWhere, out dataCount);
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