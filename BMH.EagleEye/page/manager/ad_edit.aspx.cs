using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.manager;
using System.Data;
using System.Text;
using Model;
using BMH.EagleEye.pageclass;
using BLL.permission;

namespace BMH.EagleEye.page.manager
{
    public partial class ad_edit : System.Web.UI.Page
    {
        #region 参数定义
        AdvertisementManager adManager = new AdvertisementManager();
        AdLocationManager adLocationManager = new AdLocationManager();
        AdUserManager adUserManager = new AdUserManager();
        PageManager pageManager = new PageManager();
        DirectManager directManager = new DirectManager();
        AccountManager accountManager;
        VerifyPermission verifyPermission;

        // 广告主ID
        public string adUserId = string.Empty;
        protected string strBindAccountId = "";
        public string adId = "";
        public string eType = "";
        string sqlWhere = string.Empty;
        public AdInfo adInfo = new AdInfo();
        //列表
        public string typeDefault = string.Empty;
        public string typeList = string.Empty;
        public string adLocationNameDefault = string.Empty;
        public string adLocationNameList = string.Empty;
        public string pageNameDefault = string.Empty;
        public string pageNameList = string.Empty;
        public string directTypeDefault = string.Empty;
        public string directTypeList = string.Empty;
        public string adUserNameDefault = string.Empty;
        public string adUserNameList = string.Empty;
        //广告形式 20170221
        public string subAdTypeDefault = string.Empty;
        public string subAdTypeList = string.Empty;
        //账户信息
        public string accountId;
        public string accountName;
        public string accountUserName;
        public string accountType;
        public string headImageUrl;

        //广告主信息 -管理员生效
        public string aduserHtml;
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
               // adUserId = strAdUserId;

                accountId = strAccountId;
                accountName = strAccountName;
                accountUserName = strAccountUserName;
                accountType = strAccountType;
                headImageUrl = strHeadImageUrl;

                if (string.IsNullOrEmpty(headImageUrl))
                {
                    headImageUrl = "http://yingyan.baomihua.com/page/images/head.jpg";
                }
                YYLog.ClassLibrary.Log.WriteLog("YYLog.Login:/page/manager/ad_edit", "账户信息:账户id{0},账户名{1},ip{2}", strAccountId, strAccountName, pageclass.IP.GetIP());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region 权限控制
                verifyPermission = new VerifyPermission();
                if (verifyPermission.GetPermissionLevel(strBindAccountId) == "-1")
                {
                    Response.Redirect("/page/manager/index.aspx");
                }   
                #endregion

                #region 参数获取
                adId = CommonBase.GetRequestVal("adid", "-1");
                eType = CommonBase.GetRequestVal("etype", "1");
                #endregion

                #region 数据获取
                typeList = GetAdTypeList(out typeDefault);
                subAdTypeList = GetSubAdTypeList(out subAdTypeDefault);
                adLocationNameList = GetAdLocationNameList(out adLocationNameDefault);
                pageNameList = GetPageNameList(out pageNameDefault);
                directTypeList = GetDirectTypeList(out directTypeDefault);
                //aduserHtml = GetAdUserHtml();
                adUserNameList = GetAdUserNameList(out adUserNameDefault);
                #endregion
            }
        }

        /// <summary>
        /// 获取类型列表
        /// </summary>
        /// <returns></returns>
        private string GetAdTypeList(out string strDefault)
        {
            string strData = string.Empty;
            strDefault = "";
            DataTable dt = adManager.GetAdTypeDT("");
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        strDefault = dt.Rows[i]["adtypename"].ToString();
                        strData += string.Format(" <dd class='cur'><em>{0}</em><span style='display:none'>{1}</span></dd>", dt.Rows[i]["adtypename"].ToString(), dt.Rows[i]["adtypeid"].ToString());
                    }
                    else
                        strData += string.Format(" <dd><em>{0}</em><span style='display:none'>{1}</span></dd>", dt.Rows[i]["adtypename"].ToString(), dt.Rows[i]["adtypeid"].ToString());
                }
            }

            return strData;
        }
        /// <summary>
        /// 获取广告形式列表
        /// </summary>
        /// <param name="strDefault"></param>
        /// <returns></returns>
        private string GetSubAdTypeList(out string strDefault)
        {
            string strData = string.Empty;
            strDefault = "";
            DataTable dt = adManager.GetSubAdTypeDT("s1.adtypeid=1 ");
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        strDefault = dt.Rows[i]["subadtypename"].ToString();
                        strData += string.Format(" <dd class='cur'><em>{0}</em><span style='display:none'>{1}</span></dd>", dt.Rows[i]["subadtypename"].ToString(), dt.Rows[i]["subadtypeid"].ToString());
                    }
                    else
                        strData += string.Format(" <dd><em>{0}</em><span style='display:none'>{1}</span></dd>", dt.Rows[i]["subadtypename"].ToString(), dt.Rows[i]["subadtypeid"].ToString());
                }
            }

            return strData;
        }
        /// <summary>
        /// 获取广告位名称列表
        /// </summary>
        /// <param name="typeDefault"></param>
        /// <returns></returns>
        private string GetAdLocationNameList(out string strDefault)
        {
            string strData = string.Empty;
            strDefault = "";
            DataTable dt = adLocationManager.GetAdLocationNameDT("s1.pageid = 1 and s1.isbid=0");
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        strDefault = dt.Rows[i]["adlocationname"].ToString();
                        strData += string.Format(" <dd class='cur'><em>{0}</em><span style='display:none'>{1}</span></dd>", dt.Rows[i]["adlocationname"].ToString(), dt.Rows[i]["adlocationid"].ToString());
                    }
                    else
                        strData += string.Format(" <dd><em>{0}</em><span style='display:none'>{1}</span></dd>", dt.Rows[i]["adlocationname"].ToString(), dt.Rows[i]["adlocationid"].ToString());
                }
            }

            return strData;
        }
        /// <summary>
        /// 页面名称列表
        /// </summary>
        /// <param name="strDefault"></param>
        /// <returns></returns>
        private string GetPageNameList(out string strDefault)
        {
            string strData = string.Empty;
            strDefault = "";
            DataTable dt = pageManager.GetPageNameDT("");
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        strDefault = dt.Rows[i]["pagename"].ToString();
                        strData += string.Format(" <dd class='cur'><em>{0}</em><span style='display:none'>{1}</span></dd>", dt.Rows[i]["pagename"].ToString(), dt.Rows[i]["pageid"].ToString());
                    }
                    else
                        strData += string.Format(" <dd><em>{0}</em><span style='display:none'>{1}</span></dd>", dt.Rows[i]["pagename"].ToString(), dt.Rows[i]["pageid"].ToString());
                }
            }

            return strData;
        }
        /// <summary>
        /// 获取广告主列表
        /// </summary>
        /// <param name="typeDefault"></param>
        /// <returns></returns>
        private string GetAdUserNameList(out string strDefault)
        {
            string strData = string.Empty;
            strDefault = "";            
            DataTable dt = adUserManager.GetAdUserNameDT("");
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        strDefault = dt.Rows[i]["adusername"].ToString();
                        strData += string.Format(" <dd class='cur'><em>{0}</em><span style='display:none'>{1}</span></dd>", dt.Rows[i]["adusername"].ToString(), dt.Rows[i]["aduserid"].ToString());
                    }
                    else
                        strData += string.Format(" <dd><em>{0}</em><span style='display:none'>{1}</span></dd>", dt.Rows[i]["adusername"].ToString(), dt.Rows[i]["aduserid"].ToString());
                }
            }

            return strData;
        }
        /// <summary>
        /// 定向类型列表
        /// </summary>
        /// <param name="strDefault"></param>
        /// <returns></returns>
        private string GetDirectTypeList(out string strDefault)
        {
            string strData = string.Empty;
            strDefault = "";
            DataTable dt = directManager.GetDirectTypeDT("");
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        strDefault = dt.Rows[i]["directtypename"].ToString();
                        strData += string.Format(" <dd class='cur'><em>{0}</em><span style='display:none'>{1}</span></dd>", dt.Rows[i]["directtypename"].ToString(), dt.Rows[i]["directtypeid"].ToString());
                    }
                    else
                        strData += string.Format(" <dd><em>{0}</em><span style='display:none'>{1}</span></dd>", dt.Rows[i]["directtypename"].ToString(), dt.Rows[i]["directtypeid"].ToString());
                }
            }

            return strData;
        }

        private string GetAdUserHtml()
        {
            adUserNameList = GetAdUserNameList(out adUserNameDefault);
            StringBuilder sb = new StringBuilder();
            sb.Append("<li>");
            sb.Append("<div class='slidebox slidebox2 fl' id='info_aduser'>");
            sb.Append("<span class='fl'>广告主：</span>");
            sb.Append("<div class='choice fl'>");
            sb.Append("<div class='box'>");
            sb.Append("<span class='title'>");
            sb.Append(adUserNameDefault);
            sb.Append("</span>");
            sb.Append("<div class='sanjiao'>");
            sb.Append("<span class='triangle'></span>");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("<div class='slide'>");
            sb.Append("<dl>");
            sb.Append(adUserNameList);
            sb.Append("</dl></div>");
            sb.Append("</div>");
            sb.Append("<em class='red'>（必填*）</em></div>");
            sb.Append("</li>");
            sb.Append("<div style = 'clear:left' ></ div>");           
            return sb.ToString();
        }

    }
}