using BLL.manager;
using BLL.permission;
using BMH.EagleEye.pageclass;
using Common.pub;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMH.EagleEye.page.manager
{
    public partial class agent_info : System.Web.UI.Page
    {
        #region 变量定义        
        AdUserManager adUserManager;
        AdLocationManager adLocationManager;
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
#if TalentDebug
            strBindAccountId = "16";
            aduserid="6";
#else
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
                YYLog.ClassLibrary.Log.WriteLog("YYLog.Login:/page/manager/agent_info", "账户信息:账户id{0},账户名{1},ip{2}", strAccountId, strAccountName, pageclass.IP.GetIP());
            }
#endif
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
                sqlWhere = GetSqlWhere(key, userType);

                //statusList = GetStatusList(out statusDefault);
                //pageList = GetPageList(out pageDefault);
                //adUserList = GetAdUserList(out adUserDefault);
                //adLocationList = GetAdLocationList(pageId, out adLocationDefault);
                GetListData(pageSize, pageNo, pageNumber, sqlWhere);
#endregion
            }
        }

        public void GetListData(int pageSize, int pageNo, int pageNumber, string sqlWhere)
        {
            int dataCount = 0;
            adUserManager = new AdUserManager();
            DataTable dt = adUserManager.GetAdUserListDT(pageSize, pageNo, sqlWhere, out dataCount);
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
        private string GetSqlWhere(string key, string userType)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(key))
            {
                sb.Append(string.Format("( s1.name like '%{0}%' or s1.contact like '%{0}%' or s1.aduserno like '%{0}%' )", key));
            }
            if (!string.IsNullOrWhiteSpace(userType) && userType != "all")
            {
                if (sb.Length > 0)
                {
                    sb.Append(" ");
                    sb.Append("and");
                    sb.Append(" ");
                }
                sb.Append(string.Format("s1.usertype={0}", userType));
            }
           
            return sb.ToString();
        }

        //private string GetProvinceList(out string strDefault)
        //{
        //    strDefault = "";
        //    adUserManager = new AdUserManager();
        //    DataTable dt = adUserManager.GetAdUserNameDT("");
        //    StringBuilder sb = new StringBuilder();
        //    if (dt != null && dt.Rows.Count > 0)
        //    {

        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            if (i == 0)
        //            {
        //                strDefault = dt.Rows[i]["aduserid"].ToString();
        //                sb.Append(string.Format("<option value='{0}' selected='selected'>", dt.Rows[i]["aduserid"].ToString()));
        //            }
        //            else
        //            {
        //                sb.Append(string.Format("<option value='{0}'>", dt.Rows[i]["aduserid"].ToString()));
        //            }
        //            sb.Append(dt.Rows[i]["adusername"].ToString());
        //            sb.Append("</option>");

        //        }
        //    }
        //    return sb.ToString();
        //}
        //private string GetCityList()
        //{
         
        //    adUserManager = new AdUserManager();
        //    DataTable dt = adUserManager.GetAdUserNameDT("");
        //    StringBuilder sb = new StringBuilder();
        //    if (dt != null && dt.Rows.Count > 0)
        //    {

        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            if (i == 0)
        //            {                        
        //                sb.Append(string.Format("<option value='{0}' selected='selected'>", dt.Rows[i]["aduserid"].ToString()));
        //            }
        //            else
        //            {
        //                sb.Append(string.Format("<option value='{0}'>", dt.Rows[i]["aduserid"].ToString()));
        //            }
        //            sb.Append(dt.Rows[i]["adusername"].ToString());
        //            sb.Append("</option>");

        //        }
        //    }
        //    return sb.ToString();
        //}
    }
}