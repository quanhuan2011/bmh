using BLL.agent;
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

namespace BMH.EagleEye.page.agent
{
    public partial class aduinfo : System.Web.UI.Page
    {
        #region 变量定义        
        AdvertisementManager adManager = new AdvertisementManager();
        ReportAgent rAgent;
        VerifyPermission verifyPermission;
        System.Configuration.AppSettingsReader appReader = new System.Configuration.AppSettingsReader();
        // 广告主ID
        public string aduserid = string.Empty;
        protected string strBindAccountId = "";
        protected string headImageUrl;
        protected string accountName;
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
                accountName = strAccountName;
                if (string.IsNullOrEmpty(headImageUrl))
                {
                    headImageUrl = "http://yingyan.baomihua.com/page/images/head.jpg";
                }
                if (cookies.BeeAccountType != "3")
                {
                    Response.Redirect("/page/agent/index.aspx");
                }
                YYLog.ClassLibrary.Log.WriteLog("YYLog.Login:/page/agent/aduinfo", "账户信息:账户id{0},账户名{1},ip{2}", strAccountId, strAccountName, pageclass.IP.GetIP());
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
                // sqlWhere = GetSqlWhere(key, userType);
                string sTime = CommonBase.GetRequestVal("stime", DateTime.Now.ToString("yyyyMMdd"));
                string eTime = CommonBase.GetRequestVal("etime", DateTime.Now.ToString("yyyyMMdd"));
                GetListData(pageSize, pageNo, pageNumber, aduserid, "");
                //GetListDataByAdl(pageSize, pageNo, pageNumber, sqlWhere);
                //GetListDataByAdv(pageSize, pageNo, pageNumber, sqlWhere);
                #endregion
            }
        }
        public void GetListData(int pageSize, int pageNo, int pageNumber, string adUserId, string sqlWhere)
        {
            int dataCount = 0;
            if (string.IsNullOrWhiteSpace(sqlWhere))
                sqlWhere = string.Format(" p1.aduserid={0}", adUserId);
            else
            {
                sqlWhere = string.Format(" p1.aduserid={0} and {1}", adUserId, sqlWhere);
            }
            DataTable dt = adManager.GetAdListDTByAdu(pageSize, pageNo, sqlWhere, out dataCount);
            if (dt != null && dt.Rows.Count > 0)
            {
                this.repMaterialList.DataSource = dt;
                this.repMaterialList.DataBind();
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
            string url = Request.Url.ToString();
            return url;
        }

        /// <summary>
        /// 获取sql条件
        /// </summary>
        /// <returns></returns>
        private string GetSqlWhere(string key, string status, string pageId, string adUserId, string adLocationId)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(key))
            {
                sb.Append(string.Format("( p1.name like '%{0}%' or p1.adid like '%{0}%')", key));
            }
            if (!string.IsNullOrWhiteSpace(status) && status != "all")
            {
                if (sb.Length > 0)
                {
                    sb.Append(" ");
                    sb.Append("and");
                    sb.Append(" ");
                }
                sb.Append(string.Format("p1.status={0}", status));
            }
            if ((!string.IsNullOrWhiteSpace(pageId) && pageId != "all") || (!string.IsNullOrWhiteSpace(adLocationId) && adLocationId != "all"))
            {
                StringBuilder sb2 = new StringBuilder();
                if (!string.IsNullOrWhiteSpace(pageId) && pageId != "all")
                {
                    sb2.Append(string.Format("s2.pageid={0}", pageId));
                }
                if (!string.IsNullOrWhiteSpace(adLocationId) && adLocationId != "all")
                {
                    if (sb2.Length > 0)
                    {
                        sb2.Append(" ");
                        sb2.Append("and");
                        sb2.Append(" ");
                    }
                    sb2.Append(string.Format("s2.adlocationid={0}", adLocationId));
                }
                adManager = new AdvertisementManager();
                DataTable dt = adManager.GetAdIdByScreenDT(sb2.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    string tempStr = "";
                    foreach (DataRow dr in dt.Rows)
                    {
                        int temp;
                        if (int.TryParse(dr["adid"].ToString(), out temp))
                            tempStr += temp.ToString() + ",";
                    }
                    tempStr = tempStr.Substring(0, tempStr.Length - 1);
                    if (sb.Length > 0)
                    {
                        sb.Append(" ");
                        sb.Append("and");
                        sb.Append(" ");
                    }
                    sb.Append(string.Format("p1.adid in ({0})", tempStr));
                }
                else
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(" ");
                        sb.Append("and");
                        sb.Append(" ");
                    }
                    sb.Append("1=2");
                }
            }
            if (!string.IsNullOrWhiteSpace(adUserId) && adUserId != "all")
            {
                if (sb.Length > 0)
                {
                    sb.Append(" ");
                    sb.Append("and");
                    sb.Append(" ");
                }
                sb.Append(string.Format("p1.aduserid={0}", adUserId));
            }

            return sb.ToString();
        }
    }
}