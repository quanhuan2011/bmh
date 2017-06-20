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
using BLL.permission;

namespace BMH.EagleEye.page.manager
{
    public partial class ad_info : System.Web.UI.Page
    {
        #region 变量定义
        AdvertisementManager adManager = new AdvertisementManager();
        AdUserManager adUserManager;
        AdLocationManager adLocationManager;
        PageManager pageManager;
        VerifyPermission verifyPermission;
        System.Configuration.AppSettingsReader appReader = new System.Configuration.AppSettingsReader();
        // 广告主ID
        public string aduserid = string.Empty;
        protected string strBindAccountId = "";

        string sqlWhere = string.Empty;
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
                YYLog.ClassLibrary.Log.WriteLog("YYLog.Login:/page/manager/ad_info", "账户信息:账户id{0},账户名{1},ip{2}", strAccountId, strAccountName, pageclass.IP.GetIP());
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
                int pageNo = CommonBase.GetRequestIntVal( "page", 1);
                string defaultPageSize = appReader.GetValue("DefaultPageSize", typeof(string)).ToString();
                string defaultPageNumber = appReader.GetValue("DefaultPageNumber", typeof(string)).ToString();
                int pageSize = string.IsNullOrEmpty(defaultPageSize) ? 15 : int.Parse(defaultPageSize);
                int pageNumber = string.IsNullOrEmpty(defaultPageNumber) ? 4 : int.Parse(defaultPageNumber);
                string key =CommonBase.GetRequestVal("key");//搜索内容
                string status =CommonBase.GetRequestVal("stype","1");//状态
                string pageId = CommonBase.GetRequestVal( "ptype");//页面
                string adUserId = CommonBase.GetRequestVal("aduid");//广告主
                string adLocationId = CommonBase.GetRequestVal("adlid");//广告位
                sqlWhere = GetSqlWhere(key, status, pageId,adUserId,adLocationId);
                statusList = GetStatusList(out statusDefault);
                pageList = GetPageList(out pageDefault);
                adUserList = GetAdUserList(out adUserDefault);
                adLocationList = GetAdLocationList(pageId,out adLocationDefault);
                GetListData( pageSize, pageNo, pageNumber, sqlWhere);
                #endregion
            }

        }
        public void GetListData(int pageSize, int pageNo, int pageNumber, string sqlWhere)
        {
            int dataCount = 0;
            //if (string.IsNullOrWhiteSpace(sqlWhere))
            //    sqlWhere = string.Format(" p1.aduserid={0}", adUserId);
            //else {
            //    sqlWhere = string.Format(" p1.aduserid={0} and {1}", adUserId,sqlWhere);
            //}
            DataTable dt = adManager.GetAdListDT(pageSize, pageNo, sqlWhere, out dataCount);
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
            if ((!string.IsNullOrWhiteSpace(pageId) && pageId != "all")|| (!string.IsNullOrWhiteSpace(adLocationId) && adLocationId != "all"))
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
                else {
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

        /// <summary>
        /// 获取类型列表
        /// </summary>
        /// <returns></returns>
        private string GetStatusList(out string typeDefault)
        {
            string strData = string.Empty;
            typeDefault = "";
            DataTable dt = adManager.GetAdStatusListDT("");
            //typeDefault = "全部";
            strData += string.Format(" <dd><em>{0}</em><span style='display:none'>{1}</span></dd>", "全部", "all");
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["status"].ToString()=="1")
                    {
                        typeDefault = dt.Rows[i]["statusname"].ToString();
                        strData += string.Format(" <dd class='cur'><em>{0}</em><span style='display:none'>{1}</span></dd>", dt.Rows[i]["statusname"].ToString(), dt.Rows[i]["status"].ToString());
                    }
                    else
                        strData += string.Format(" <dd><em>{0}</em><span style='display:none'>{1}</span></dd>", dt.Rows[i]["statusname"].ToString(), dt.Rows[i]["status"].ToString());
                }
            }

            return strData;
        }

        /// <summary>
        /// 获取页面列表
        /// </summary>
        /// <returns></returns>
        private string GetPageList(out string typeDefault)
        {
            string strData = string.Empty;
            pageManager = new PageManager();
            DataTable dt = pageManager.GetPageNameDT("");               
            typeDefault = "全部";
            strData += string.Format(" <dd class='cur'><em>{0}</em><span style='display:none'>{1}</span></dd>", "全部", "all");
            if (dt != null && dt.Rows.Count > 0)
            {             
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                        strData += string.Format(" <dd><em>{0}</em><span style='display:none'>{1}</span></dd>", dt.Rows[i]["pagename"].ToString(), dt.Rows[i]["pageid"].ToString());
                }
            }

            return strData;
        }
        /// <summary>
        /// 广告主列表
        /// </summary>
        /// <param name="strDefault"></param>
        /// <returns></returns>
        private string GetAdUserList(out string strDefault)
        {
            string strData = string.Empty;
            adUserManager = new AdUserManager();
            DataTable dt = adUserManager.GetAdUserNameDT("");
            strDefault = "全部";
            strData += string.Format(" <dd class='cur'><em>{0}</em><span style='display:none'>{1}</span></dd>", "全部", "all");
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strData += string.Format(" <dd><em>{0}</em><span style='display:none'>{1}</span></dd>", dt.Rows[i]["adusername"].ToString(), dt.Rows[i]["aduserid"].ToString());
                }
            }

            return strData;
        }
        /// <summary>
        /// 广告主列表
        /// </summary>
        /// <param name="strDefault"></param>
        /// <returns></returns>
        private string GetAdLocationList(string pageId, out string strDefault)
        {
            int temp;
            if (string.IsNullOrWhiteSpace(pageId) || pageId == "all"||!int.TryParse(pageId,out temp))
            {
                pageId = "1";
            }
            string strData = string.Empty;
            adLocationManager = new AdLocationManager();
            DataTable dt = adLocationManager.GetAdLocationNameDT(string.Format("s1.pageid={0}",pageId));
            strDefault = "全部";
            strData += string.Format(" <dd class='cur'><em>{0}</em><span style='display:none'>{1}</span></dd>", "全部", "all");
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strData += string.Format(" <dd><em>{0}</em><span style='display:none'>{1}</span></dd>", dt.Rows[i]["adlocationname"].ToString(), dt.Rows[i]["adlocationid"].ToString());
                }
            }

            return strData;          
        }

    }
}