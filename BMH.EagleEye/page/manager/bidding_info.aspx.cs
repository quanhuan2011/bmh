using BLL.manager;
using BLL.permission;
using BMH.EagleEye.pageclass;
using Common.pub;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMH.EagleEye.page.manager
{
    public partial class bidding_info : System.Web.UI.Page
    {
        #region 变量定义        
        AdUserManager adUserManager;
        AdLocationManager adLocationManager;
        AdvertisementManager advertisementManager;
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
                YYLog.ClassLibrary.Log.WriteLog("YYLog.Login:/page/manager/bidding_info", "账户信息:账户id{0},账户名{1},ip{2}", strAccountId, strAccountName, pageclass.IP.GetIP());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region 权限控制
                verifyPermission = new VerifyPermission();
                permLevel = verifyPermission.GetPermissionLevel(strBindAccountId);
                #endregion

                #region 数据获取
                int pageNo = CommonBase.GetRequestIntVal("page", 1);
                string defaultPageSize = appReader.GetValue("DefaultPageSize", typeof(string)).ToString();
                string defaultPageNumber = appReader.GetValue("DefaultPageNumber", typeof(string)).ToString();
                int pageSize = string.IsNullOrEmpty(defaultPageSize) ? 15 : int.Parse(defaultPageSize);
                int pageNumber = string.IsNullOrEmpty(defaultPageNumber) ? 4 : int.Parse(defaultPageNumber);
                string key = CommonBase.GetRequestVal("key");//搜索内容
                string userType = CommonBase.GetRequestVal("stype");//状态
                                                                    // sqlWhere = GetSqlWhere(key, userType);

                //statusList = GetStatusList(out statusDefault);
                //pageList = GetPageList(out pageDefault);
                //adUserList = GetAdUserList(out adUserDefault);
                //adLocationList = GetAdLocationList(pageId, out adLocationDefault);
                GetListDataByAdu(pageSize, pageNo, pageNumber, sqlWhere);
                GetListDataByAdl(pageSize, pageNo, pageNumber, sqlWhere);
                GetListDataByAdv(pageSize, pageNo, pageNumber, sqlWhere);
                #endregion
            }
        }
        /// <summary>
        /// 代理商列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageNumber"></param>
        /// <param name="sqlWhere"></param>
        public void GetListDataByAdu(int pageSize, int pageNo, int pageNumber, string sqlWhere)
        {
            int dataCount = 0;
            adUserManager = new AdUserManager();
            DataTable dt = adUserManager.GetAdUserListDT(pageSize, pageNo, sqlWhere, out dataCount);
            if (dt != null && dt.Rows.Count > 0)
            {
                this.repList1.DataSource = dt;
                this.repList1.DataBind();
                litPage1.Text = PageTxt.GetPageText(GetUrl(), pageNumber, dataCount, pageSize, pageNo);
                var pageCount = Convert.ToInt32(System.Math.Ceiling(Convert.ToDouble(dataCount) / Convert.ToDouble(pageSize)));
                litPageInfo1.Text = string.Format("共有<font style='color: red;'>{0}</font>条&nbsp;&nbsp;<font style='color: red;'>{1}</font>/<span>{2}</span>页", dataCount, pageNo, pageCount);
            }
            else
            {
                litNoInfo1.Text = "<tr><td colspan='9'><p style='width:100%; line-height:200px; text-align:center'>无数据...</p></td></tr>";
            }

        }
        /// <summary>
        /// 广告位管理
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageNumber"></param>
        /// <param name="sqlWhere"></param>
        public void GetListDataByAdl(int pageSize, int pageNo, int pageNumber, string sqlWhere)
        {
            int dataCount = 0;
            adLocationManager = new AdLocationManager();
              DataTable dt = adLocationManager.GetAdLocationListByBiddingDT(pageSize, pageNo, sqlWhere, out dataCount);
            if (dt != null && dt.Rows.Count > 0)
            {
                this.repList2.DataSource = dt;
                this.repList2.DataBind();
                litPage2.Text = PageTxt.GetPageText(GetUrl(), pageNumber, dataCount, pageSize, pageNo);
                var pageCount = Convert.ToInt32(System.Math.Ceiling(Convert.ToDouble(dataCount) / Convert.ToDouble(pageSize)));
                litPageInfo2.Text = string.Format("共有<font style='color: red;'>{0}</font>条&nbsp;&nbsp;<font style='color: red;'>{1}</font>/<span>{2}</span>页", dataCount, pageNo, pageCount);
            }
            else
            {
                litNoInfo2.Text = "<tr><td colspan='9'><p style='width:100%; line-height:200px; text-align:center'>无数据...</p></td></tr>";
            }

        }
        /// <summary>
        /// 广告管理
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageNumber"></param>
        /// <param name="sqlWhere"></param>
        public void GetListDataByAdv(int pageSize, int pageNo, int pageNumber, string sqlWhere)
        {
            int dataCount = 0;
            advertisementManager = new AdvertisementManager();            
            DataTable dt = advertisementManager.GetAdListByBiddingDT(pageSize, pageNo, sqlWhere, out dataCount);
            if (dt != null && dt.Rows.Count > 0)
            {
                this.repList3.DataSource = dt;
                this.repList3.DataBind();
                litPage3.Text = PageTxt.GetPageText(GetUrl(), pageNumber, dataCount, pageSize, pageNo);
                var pageCount = Convert.ToInt32(System.Math.Ceiling(Convert.ToDouble(dataCount) / Convert.ToDouble(pageSize)));
                litPageInfo3.Text = string.Format("共有<font style='color: red;'>{0}</font>条&nbsp;&nbsp;<font style='color: red;'>{1}</font>/<span>{2}</span>页", dataCount, pageNo, pageCount);
            }
            else
            {
                litNoInfo3.Text = "<tr><td colspan='9'><p style='width:100%; line-height:200px; text-align:center'>无数据...</p></td></tr>";
            }

        }

        public string GetUrl()
        {
            string url = Request.Url.ToString();
            return url;
        }
    }
}