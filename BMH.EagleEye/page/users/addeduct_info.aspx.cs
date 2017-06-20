using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.manager;
using System.Data;
using Common.pub;
using System.Text;
using BMH.EagleEye.pageclass;
using BLL.users;
using Model;

namespace BMH.EagleEye.page.users
{
    public partial class addeduct_info : System.Web.UI.Page
    {
        #region 变量定义
        AdvertisementUsers adUsers;
        AdUserUsers adUserUsers;
        AccountManager accountManager;
        public AdUser adUser;
        string accountId = "";
        string sqlWhere = string.Empty;
        public float deductSum = 0.00f;
        public float balanceSum = 0.00f;

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
                int pageNo = int.Parse(CommonBase.GetRequestVal( "page", "1"));
                string startTime = CommonBase.GetRequestVal( "starttime", DateTime.Now.AddDays(-8).ToString("yyyyMMdd"));//默认8天
                string endTime = CommonBase.GetRequestVal( "endTime", DateTime.Now.AddDays(-1).ToString("yyyyMMdd"));//默认昨天
                string key = CommonBase.GetRequestVal( "key");
                string defaultPageSize = appReader.GetValue("DefaultPageSize", typeof(string)).ToString();
                string defaultPageNumber = appReader.GetValue("DefaultPageNumber", typeof(string)).ToString();
                int pageSize = string.IsNullOrEmpty(defaultPageSize) ? 15 : int.Parse(defaultPageSize);
                int pageNumber = string.IsNullOrEmpty(defaultPageNumber) ? 4 : int.Parse(defaultPageNumber);
                //账户
                accountManager = new AccountManager();
                adUser = new AdUser();
                adUser = accountManager.GetAdUserInfo(accountId);

                if (!string.IsNullOrWhiteSpace(key))
                {
                    sqlWhere = string.Format(" p1.adname like '%{0}%'", key);
                }
                GetListData(adUser.aduserid, startTime, endTime, pageSize, pageNo, pageNumber, sqlWhere);
                GetSum(adUser.aduserid, startTime, endTime);
            }
        }
        /// <summary>
        /// 获取列表并绑定数据
        /// </summary>
        /// <param name="adUserId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageNumber"></param>
        /// <param name="sqlWhere"></param>
        public void GetListData(string adUserId,string startTime,string endTime, int pageSize, int pageNo, int pageNumber, string sqlWhere)
        {
            adUsers = new AdvertisementUsers();
            int dataCount = 0;
            DataTable dt = adUsers.GetAdDeductListDT(adUserId, startTime, endTime, pageSize, pageNo, sqlWhere, out dataCount);
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetUrl()
        {
            StringBuilder url = new StringBuilder();
            url.Append("addeduct_info.aspx?");
            url.Append("&page=");
            return url.ToString();
        }
        private void GetSum(string adUserId,string startTime,string endTime)
        {

            adUserUsers = new AdUserUsers();
            //DataTable dt = adUserUsers.GetBalanceOfAduserDT(adUserId);
            //if (dt != null)
            //{
            //    if (dt.Rows.Count > 0)
            //    {
            //        balanceSum = float.Parse(dt.Rows[0]["balance"].ToString());
            //    }
            //}
            DataTable dt1 = adUserUsers.GetADSumOfAduserDT(adUserId, startTime,endTime);
            if (dt1 != null)
            {
                if (dt1.Rows.Count > 0)
                {
                  
                    deductSum = float.Parse(dt1.Rows[0]["deductsum"].ToString());
                }
            }
        }
    }
}