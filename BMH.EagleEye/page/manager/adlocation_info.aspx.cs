using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL.manager;
using System.Text;
using System.Data;
using Common.pub;
using BMH.EagleEye.pageclass;

namespace BMH.EagleEye.page.manager
{
    public partial class adlocation_info : System.Web.UI.Page
    {
        #region 变量定义
        AdLocationManager adLocationManager = new AdLocationManager();
        PageManager pageManager = new PageManager();
        string userId = "";
        string sqlWhere = string.Empty;
        public string pageDefault = string.Empty;
        public string pageList = string.Empty;
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

                YYLog.ClassLibrary.Log.WriteLog("YYLog.Login:/page/manager/adlocation_info", "账户信息:账户id{0},账户名{1},ip{2}", strAccountId, strAccountName, pageclass.IP.GetIP());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int pageNo = CommonBase.GetRequestIntVal("page", 1);
                string defaultPageSize = appReader.GetValue("DefaultPageSize", typeof(string)).ToString();
                string defaultPageNumber = appReader.GetValue("DefaultPageNumber", typeof(string)).ToString();
                int pageSize = string.IsNullOrEmpty(defaultPageSize) ? 15 : int.Parse(defaultPageSize);
                int pageNumber = string.IsNullOrEmpty(defaultPageNumber) ? 4 : int.Parse(defaultPageNumber);
                string key = CommonBase.GetRequestVal("key");//搜索内容
                string pageId = CommonBase.GetRequestVal("pid");//页面id
                pageList = GetPageNameList(out pageDefault);
                sqlWhere = GetSqlWhere(key, pageId);                                
                GetListData(userId, pageSize, pageNo, pageNumber, sqlWhere);
            }
        }
        /// <summary>
        /// 获取列表数据并绑定
        /// </summary>
        /// <param name="userId">用户id(待用)</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageNo">当前页面索引</param>
        /// <param name="sqlWhere">数据条件</param>
        public void GetListData(string userId,int pageSize,int pageNo,int pageNumber,  string sqlWhere)
        {
            #region 变量定义
            int dataCount = 0;
            #endregion

            #region 数据获取
            DataTable dt = adLocationManager.GetAdLocationListDT(pageSize, pageNo, sqlWhere, out dataCount);
            #endregion

            #region 数据绑定
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
                litNoInfo.Text = "<tr><td colspan='7'><p style='width:100%; line-height:200px; text-align:center'>无数据...</p></td></tr>";
            }
            #endregion
        }
        /// <summary>
        /// 获取分页跳转地址
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUrl()
        {
            return Request.Url.ToString();
        }
        private string RequestVal(string key, string defaultVal)
        {
            string resultStr = "";
            try
            {
                resultStr = Request[key].ToString();
                if (string.IsNullOrEmpty(resultStr))
                    resultStr = defaultVal;
            }
            catch
            {
                resultStr = defaultVal;
            }
            return resultStr;
        }

        /// <summary>
        /// 获取sql条件
        /// </summary>
        /// <returns></returns>
        private string GetSqlWhere(string key,string pageId)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(key))
            {
                sb.Append(string.Format("( name like '%{0}%' or bcode like '%{0}%')", key));               
            }
            if (!string.IsNullOrWhiteSpace(pageId)&&pageId!="all")
            {
                if (sb.Length > 0)
                {
                    sb.Append(" ");
                    sb.Append("and");
                    sb.Append(" ");
                }
                sb.Append(string.Format("s1.pageid={0}", pageId));
            }
            return sb.ToString();        
        }

        /// <summary>
        /// 获取类型列表
        /// </summary>
        /// <returns></returns>
        private  string GetTypeList(out string typeDefault)
        {           
           string strData = string.Empty;
           typeDefault = "";
           DataTable dt= adLocationManager.GetAdLocationTypeListDT("");
           if (dt != null && dt.Rows.Count > 0)
           {
               for (int i = 0; i < dt.Rows.Count; i++)
               {
                   if (i == 0)
                   {
                       typeDefault = dt.Rows[i]["adlocationname"].ToString();
                       strData += string.Format(" <dd class='cur'>{0}</dd>", dt.Rows[i]["tag"].ToString());
                   }
                   else
                       strData += string.Format(" <dd>{0}</dd>", dt.Rows[i]["tag"].ToString());
               }
           }
          
            return strData;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeDefault"></param>
        /// <returns></returns>
        private string GetPageNameList(out string strDefault)
        {
            string strData = string.Empty;
            strDefault = "";
            DataTable dt = pageManager.GetPageNameDT("");
            strDefault = "全部";
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

    }
}