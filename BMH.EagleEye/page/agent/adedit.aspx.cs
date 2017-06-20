using BLL.manager;
using BLL.permission;
using BMH.EagleEye.pageclass;
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
    public partial class adedit : System.Web.UI.Page
    {
        #region 参数定义
        AdvertisementManager adManager = new AdvertisementManager();
        AdLocationManager adLocationManager = new AdLocationManager();
        AdUserManager adUserManager = new AdUserManager();
        PageManager pageManager = new PageManager();
        DirectManager directManager = new DirectManager();

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
                adUserId = strAdUserId;
                accountId = strAccountId;
                accountName = strAccountName;
                accountUserName = strAccountUserName;
                accountType = strAccountType;
                headImageUrl = strHeadImageUrl;

                if (string.IsNullOrEmpty(headImageUrl))
                {
                    headImageUrl = "http://yingyan.baomihua.com/page/images/head.jpg";
                }
                YYLog.ClassLibrary.Log.WriteLog("YYLog.Login:/page/agent/adedit", "账户信息:账户id{0},账户名{1},ip{2}", strAccountId, strAccountName, pageclass.IP.GetIP());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region 参数获取
                adId = CommonBase.GetRequestVal("adid", "-1");
                eType = CommonBase.GetRequestVal("etype", "1");
                #endregion

                #region 数据获取
                typeList = GetAdTypeList(out typeDefault);
                subAdTypeList = GetSubAdTypeList(out subAdTypeDefault);                            
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
            DataTable dt = adManager.GetSubAdTypeDTByAdu("s1.adtypeid=1 ");
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        strDefault = dt.Rows[i]["subadtypename"].ToString();
                        strData += string.Format(" <dd class='cur'><em>{0}</em><span style='display:none'>{1}</span><p class='mat_width' style='display:none'>{2}</p><p class='mat_height' style='display:none'>{3}</p></dd>", dt.Rows[i]["subadtypename"].ToString(), dt.Rows[i]["subadtypeid"].ToString(),Convert.ToString(dt.Rows[i]["width"]), Convert.ToString(dt.Rows[i]["height"]));
                    }
                    else
                        strData += string.Format(" <dd><em>{0}</em><span style='display:none'>{1}</span><p class='mat_width' style='display:none'>{2}</p><p class='mat_height' style='display:none'>{3}</p></dd>", dt.Rows[i]["subadtypename"].ToString(), dt.Rows[i]["subadtypeid"].ToString(), Convert.ToString(dt.Rows[i]["width"]), Convert.ToString(dt.Rows[i]["height"]));
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
        private string GetAdUserNameList(string adUserId, out string strDefault)
        {
            string strData = string.Empty;
            strDefault = "";
            DataTable dt = adUserManager.GetAdUserNameDT("aduserid="+adUserId);
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
    }
}