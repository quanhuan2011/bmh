using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL.manager;
using BMH.EagleEye.pageclass;
using BLL.users;
using System.Data;
using Common.pub;
using System.Text;

namespace BMH.EagleEye.page.manager
{
    public partial class index : System.Web.UI.Page
    {
        #region 变量定义
        AdvertisementManager adManager;
        AdUserManager adUserManager;
        AdLocationManager adLocationManager;
        //AdUserUsers adUserUsers;
        AccountManager accountManager;
        PageManager pageManager;
        public AdUser adUser;
        string accountId = "";
        string sqlWhere = string.Empty;
        //public float incomSum = 0.00f;
        public string incomSum = 0.ToString("f2");
        public int clickCnt = 0;
        public int requestCnt = 0;
        public float clickRate = 0.00f;
        public string tableHeadHtml;
        public string pageHtml = "";
        public string clickTotal = 0.ToString("f2");
        public string incomeTotal = 0.ToString("f2");

                
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
                YYLog.ClassLibrary.Log.WriteLog("YYLog.Login:/page/manager/index", "账户信息:账户id{0},账户名{1},ip{2}", strAccountId, strAccountName, pageclass.IP.GetIP());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int pageNo = CommonBase.GetRequestIntVal("page", 1);
                string startTime = CommonBase.GetRequestVal( "starttime", DateTime.Now.AddDays(-1).ToString("yyyyMMdd"));//默认8天
                string endTime = CommonBase.GetRequestVal( "endTime", DateTime.Now.AddDays(-1).ToString("yyyyMMdd"));//默认昨天
                string key = CommonBase.GetRequestVal( "key");
                string sType = CommonBase.GetRequestVal( "stype","1");
                string pageId = CommonBase.GetRequestVal("pid");//广告页
                string defaultPageSize = appReader.GetValue("DefaultPageSize", typeof(string)).ToString();
                string defaultPageNumber = appReader.GetValue("DefaultPageNumber", typeof(string)).ToString();
                int pageSize = string.IsNullOrEmpty(defaultPageSize) ? 15 : int.Parse(defaultPageSize);
                int pageNumber = string.IsNullOrEmpty(defaultPageNumber) ? 4 : int.Parse(defaultPageNumber);
                //账户
                accountManager = new AccountManager();
               // adUser = new AdUser();
               // adUser = accountManager.GetAdUserInfo(accountId);

                //if (!string.IsNullOrWhiteSpace(key))
                //{
                //    sqlWhere = string.Format(" p3.name like '%{0}%'", key);
                //}
                sqlWhere = GetSqlWhere( sType,key,pageId);

                GetTableHeadHtml(sType);
                if (sType == "1")
                {
                    GetPageHtml(pageId);
                    GetListSumByAdLocation(startTime, endTime, sqlWhere);
                    GetListDataByAdLocation(startTime, endTime, pageSize, pageNo, pageNumber, sqlWhere);
                }
                else if (sType == "2")
                {
                    GetListSumByAdUser(startTime, endTime, sqlWhere);
                    GetListDataByAdUser(startTime, endTime, pageSize, pageNo, pageNumber, sqlWhere);
                }
                else
                {
                    GetPageHtml(pageId);
                    GetListSumByAdLocation(startTime, endTime, sqlWhere);
                    GetListDataByAdLocation(startTime, endTime, pageSize, pageNo, pageNumber, sqlWhere);
                }
                GetSumByAd();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sType"></param>
        private void GetTableHeadHtml(string sType)
        {
            StringBuilder sb = new StringBuilder();
            if (sType == "1")
            {
                sb.Append("<th width='40%'>广告位名称</th>");                
                sb.Append("<th width='30%'>总点击量</th>");
                sb.Append("<th width='30%'>总收入/元</th>");

            }
            else if (sType == "2")
            {
                sb.Append("<th width='30%'>广告主名称</th>");
                sb.Append("<th width='20%'>总点击量</th>");
                sb.Append("<th width='25%'>余额</th>");
                sb.Append("<th width='25%'>总收入/元</th>");
            }
            else
            {
                sb.Append("<th width='40%'>广告位名称</th>");
                sb.Append("<th width='30%'>总点击量</th>");
                sb.Append("<th width='30%'>总收入/元</th>");
            }

            tableHeadHtml = sb.ToString();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageId"></param>
        private void GetPageHtml(string pageId)
        {
            string matchPageName = "广告页";
            StringBuilder sb1 = new StringBuilder();
            sb1.Append("<p><em>全部</em><span class='hide'>all</span></p>");
            pageManager = new PageManager();
            DataTable dt = pageManager.GetPageNameDT("");
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (pageId == dt.Rows[i]["pageid"].ToString())
                    {
                        matchPageName = dt.Rows[i]["pagename"].ToString();
                    }
                    sb1.Append(string.Format("<p><em>{0}</em><span class='hide'>{1}</span></p>", dt.Rows[i]["pagename"].ToString(), dt.Rows[i]["pageid"].ToString()));
                }
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='box select fl'>");
            sb.Append("<input type='text' value='" + matchPageName + "'>");
            sb.Append("<button>");
            sb.Append("</button>");
            sb.Append("<div class='slide-down select_page'>");
            sb.Append(sb1.ToString());
            sb.Append("</div>");
            sb.Append("</div>");
            pageHtml = sb.ToString();

        }
        /// <summary>
        /// 广告
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageNumber"></param>
        /// <param name="sqlWhere"></param>
        //public void GetListDataByAd(string startTime, string endTime, int pageSize, int pageNo, int pageNumber, string sqlWhere)
        //{
        //    adManager = new AdvertisementManager();
        //    int dataCount = 0;
        //    DataTable dt = adManager.GetAdStatDetailDT(startTime, endTime, pageSize, pageNo, sqlWhere, out dataCount);                
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        this.repList.DataSource = dt;
        //        this.repList.DataBind();
        //        litPage.Text = PageTxt.GetPageText(GetUrl(), pageNumber, dataCount, pageSize, pageNo);
        //        var pageCount = Convert.ToInt32(System.Math.Ceiling(Convert.ToDouble(dataCount) / Convert.ToDouble(pageSize)));
        //        litPageInfo.Text = string.Format("共有<font style='color: red;'>{0}</font>条&nbsp;&nbsp;<font style='color: red;'>{1}</font>/<span>{2}</span>页", dataCount, pageNo, pageCount);
        //    }
        //    else
        //    {
        //        litNoInfo.Text = "<tr><td colspan='9'><p style='width:100%; line-height:200px; text-align:center'>无数据...</p></td></tr>";
        //    }

        //}
        /// <summary>
        /// 广告主
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageNumber"></param>
        /// <param name="sqlWhere"></param>
        public void GetListDataByAdUser( string startTime, string endTime, int pageSize, int pageNo, int pageNumber, string sqlWhere)
        {
            adUserManager=new AdUserManager ();
            int dataCount = 0;
            DataTable dt = adUserManager.GetAdUserStatDetailDT(startTime, endTime, pageSize, pageNo, sqlWhere, out dataCount);                
            if (dt != null && dt.Rows.Count > 0)
            {
                this.repList.Visible = false;
                this.repListByAdUser.DataSource = dt;
                this.repListByAdUser.DataBind();

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
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="sqlWhere"></param>
        public void GetListSumByAdUser(string startTime, string endTime, string sqlWhere)
        {
            adUserManager = new AdUserManager();
            DataTable dt = adUserManager.GetAdUserStatSumDT(startTime, endTime,sqlWhere);
            if (dt != null && dt.Rows.Count > 0)
            {
                clickTotal = dt.Rows[0]["clicktotal"].ToString();
                incomeTotal = dt.Rows[0]["incometotal"].ToString();
            }            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageNumber"></param>
        /// <param name="sqlWhere"></param>
        public void GetListDataByAdLocation(string startTime, string endTime, int pageSize, int pageNo, int pageNumber, string sqlWhere)
        {
            adLocationManager=new AdLocationManager ();
            int dataCount = 0;
            DataTable dt = adLocationManager.GetAdLocationStatDetailDT(startTime, endTime, pageSize, pageNo, sqlWhere, out dataCount);
            if (dt != null && dt.Rows.Count > 0)
            {
                this.repListByAdUser.Visible = false;
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
        public void GetListSumByAdLocation(string startTime, string endTime, string sqlWhere)
        {
            adLocationManager = new AdLocationManager();
            DataTable dt = adLocationManager.GetAdLocationStatSumDT(startTime, endTime, sqlWhere);
            if (dt != null && dt.Rows.Count > 0)
            {
                clickTotal = dt.Rows[0]["clicktotal"].ToString();
                incomeTotal = dt.Rows[0]["incometotal"].ToString();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetUrl()
        {
            return Request.Url.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="status"></param>
        /// <param name="pageId"></param>
        /// <returns></returns>
        private string GetSqlWhere(string sType,string key,string pageId)
        {
            StringBuilder sb = new StringBuilder();
            if (sType == "1")
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    sb.Append(string.Format("( p1.adlocationname like '%{0}%')", key));
                }
                if (!string.IsNullOrWhiteSpace(pageId) && pageId != "all")
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(" ");
                        sb.Append("and");
                        sb.Append(" ");
                    }
                    sb.Append(string.Format("p1.pageid={0}", pageId));
                }


            }
            else if (sType == "2")
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    sb.Append(string.Format("( p1.adusername like '%{0}%')", key));
                }  

            }
            return sb.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        private void GetSumByAd()
        {
            adManager = new AdvertisementManager();

            DataTable dt = adManager.GetADStatSumDT();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    //incomSum = float.Parse(dt.Rows[0]["incomsum"].ToString());
                    incomSum = Convert.ToDouble(dt.Rows[0]["incomesum"].ToString()).ToString("f2");
                    clickCnt = int.Parse(dt.Rows[0]["clickcnt"].ToString());
                    clickRate = float.Parse(dt.Rows[0]["clickrate"].ToString());
                    requestCnt = int.Parse(dt.Rows[0]["requestcnt"].ToString());
                }
            }
        }
    }
}