using BLL.agent;
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
    public partial class linkdetail : System.Web.UI.Page
    {
        #region 变量定义        
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

        public string linkUrlDefault = string.Empty;
        public string linkUrlList = string.Empty;
        public string accountType = string.Empty;
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
                accountType = strAccountType;
                if (string.IsNullOrEmpty(headImageUrl))
                {
                    headImageUrl = "http://yingyan.baomihua.com/page/images/head.jpg";
                }
                YYLog.ClassLibrary.Log.WriteLog("YYLog.Login:/page/agent/linkdetail", "账户信息:账户id{0},账户名{1},ip{2}", strAccountId, strAccountName, pageclass.IP.GetIP());
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

#if TalentDebug
                //临时测试
                aduserid = "6";
#else

#endif

                #region 数据获取
                int pageNo = CommonBase.GetRequestIntVal("page", 1);
                string defaultPageSize = appReader.GetValue("DefaultPageSize", typeof(string)).ToString();
                string defaultPageNumber = appReader.GetValue("DefaultPageNumber", typeof(string)).ToString();
                int pageSize = string.IsNullOrEmpty(defaultPageSize) ? 15 : int.Parse(defaultPageSize);
                int pageNumber = string.IsNullOrEmpty(defaultPageNumber) ? 4 : int.Parse(defaultPageNumber);
                // sqlWhere = GetSqlWhere(key, userType);
                string sTime = CommonBase.GetRequestVal("stime", DateTime.Now.ToString("yyyyMMdd"));
                string eTime = CommonBase.GetRequestVal("etime", DateTime.Now.ToString("yyyyMMdd"));
                linkUrlList= GetLinkUrlList(aduserid, out linkUrlDefault);
                string linkUrl = HttpUtility.UrlDecode(CommonBase.GetRequestVal("lurl", linkUrlDefault));
                GetListData(pageSize, pageNo, pageNumber, aduserid, sTime, eTime, linkUrl);
                //GetListDataByAdl(pageSize, pageNo, pageNumber, sqlWhere);
                //GetListDataByAdv(pageSize, pageNo, pageNumber, sqlWhere);
                #endregion
            }
        }
        public void GetListData(int pageSize, int pageNo, int pageNumber, string adUserId, string sTime, string eTime, string linkUrl)
        {
            int dataCount = 0;
            rAgent = new ReportAgent();
            DataTable dt = rAgent.GetLinkDetailListDT(pageSize, pageNo, adUserId, sTime, eTime, linkUrl, out dataCount);
            if (dt != null && dt.Rows.Count > 0)
            {
                this.repList1.DataSource = dt;
                this.repList1.DataBind();
                litPage1.Text = PageTxt.GetPageText(Request.Url.ToString(), pageNumber, dataCount, pageSize, pageNo);
                var pageCount = Convert.ToInt32(System.Math.Ceiling(Convert.ToDouble(dataCount) / Convert.ToDouble(pageSize)));
                litPageInfo1.Text = string.Format("共有<font style='color: red;'>{0}</font>条&nbsp;&nbsp;<font style='color: red;'>{1}</font>/<span>{2}</span>页", dataCount, pageNo, pageCount);
            }
            else
            {
                litNoInfo1.Text = "<tr><td colspan='9'><p style='width:100%; line-height:200px; text-align:center'>无数据...</p></td></tr>";
            }


        }

        private string GetLinkUrlList(string adUserId,out string typeDefault)
        {
            string strData = string.Empty;
            typeDefault = "";
            rAgent = new ReportAgent();
            DataTable dt = rAgent.GetLinkUrlListByAgentDT(string.Format(" s1.aduserid={0} order by 1", adUserId));
           
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        typeDefault = dt.Rows[i]["linkurl"].ToString();
                        strData += string.Format(" <dd class='cur'><em>{0}</em><span style='display:none'>{1}</span></dd>", dt.Rows[i]["linkurl"].ToString(), HttpUtility.UrlEncode( dt.Rows[i]["linkurl"].ToString()));
                    }
                    else
                        strData += string.Format(" <dd><em>{0}</em><span style='display:none'>{1}</span></dd>",dt.Rows[i]["linkurl"].ToString(), HttpUtility.UrlEncode(dt.Rows[i]["linkurl"].ToString()));
                }
            }

            return strData;
        }

    }
}