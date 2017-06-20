using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BMH.EagleEye.pageclass;
using System.Data;
using BLL.manager;
using Common.pub;
using System.Text;

namespace BMH.EagleEye.page.manager
{
    public partial class material_analy : System.Web.UI.Page
    {
        #region 变量定义
        AdvertisementManager adManager = new AdvertisementManager();
        MaterialManager materialManager = new MaterialManager();
        AdLocationManager adLocationManager = new AdLocationManager();
        ClassManager classManager = new ClassManager();
        PageManager pageManager;
        // 广告主ID
        public string aduserid = string.Empty;
        protected string strBindAccountId = "";

        string sqlWhere = string.Empty;
        public string statusDefault = string.Empty;
        public string statusList = string.Empty;
        public string pageDefault = string.Empty;
        public string pageList = string.Empty;
        public string typeDefault = string.Empty;
        public string typeList = string.Empty;
        protected string adLocationList = string.Empty;
        protected string classList = string.Empty;
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
                strBindAccountId = strAccountId;
                aduserid = strAdUserId;
                YYLog.ClassLibrary.Log.WriteLog("YYLog.Login:/page/manager/material_analy", "账户信息:账户id{0},账户名{1},ip{2}", strAccountId, strAccountName, pageclass.IP.GetIP());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region 数据获取
                int pageNo = CommonBase.GetRequestIntVal("page", 1);
                string defaultPageSize = appReader.GetValue("DefaultPageSize", typeof(string)).ToString();
                string defaultPageNumber = appReader.GetValue("DefaultPageNumber", typeof(string)).ToString();
                int pageSize = string.IsNullOrEmpty(defaultPageSize) ? 15 : int.Parse(defaultPageSize);
                int pageNumber = string.IsNullOrEmpty(defaultPageNumber) ? 4 : int.Parse(defaultPageNumber);
                string key = CommonBase.GetRequestVal("key");//搜索内容
                string status = CommonBase.GetRequestVal("sid");//状态
                string pageId = CommonBase.GetRequestVal("pid");//页面
                string adLocationArr = CommonBase.GetRequestVal("adlid");//广告位id
                string classArr = CommonBase.GetRequestVal("claid");//分类id
                string materialType = CommonBase.GetRequestVal("mattype");//物料类型
                string startTime = CommonBase.GetRequestVal("stime", DateTime.Now.AddDays(-8).ToString("yyyyMMdd"));//默认8天
                string endTime = CommonBase.GetRequestVal("etime", DateTime.Now.AddDays(-1).ToString("yyyyMMdd"));//默认昨天
                sqlWhere = GetSqlWhere(key, status, pageId, adLocationArr, classArr, materialType, startTime, endTime);
                statusList = GetStatusList(out statusDefault);
                pageList = GetPageList(out pageDefault);
                typeList = GetTypeList(out typeDefault);
                adLocationList = GetAdLocationList(pageId);
                classList = GetClassList();

                GetListData(aduserid, pageSize, pageNo, pageNumber, sqlWhere);

                #endregion
            }
        }
        public void GetListData(string adUserId, int pageSize, int pageNo, int pageNumber, string sqlWhere)
        {
            int dataCount = 0;
            //if (string.IsNullOrWhiteSpace(sqlWhere))
            //    sqlWhere = string.Format(" p1.aduserid={0}", adUserId);
            //else
            //{
            //    sqlWhere = string.Format(" p1.aduserid={0} and {1}", adUserId, sqlWhere);
            //}

            DataTable dt = materialManager.GetMaterialAnalyListDT(pageSize, pageNo, sqlWhere, out dataCount);
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
        /// <summary>
        /// 转换物料图片预览方式 
        /// </summary>
        /// <param name="materialtype">物料类型</param>
        /// <param name="title">信息流文字</param>
        /// <param name="imageurl">图片地址</param>
        /// <returns></returns>
        public string GetmaterialPreview(string materialType, string title, string imageUrl, string remark, string confirmText, string cancelText)
        {
            string str = string.Empty;
            if (materialType == "1")
            {
                str = string.Format("<td width='25%'><img style='width:259px;height:58px;' src='{0}' alt='无效果图' class='imgs'></td>", imageUrl);
            }
            else if (materialType == "2")
            {
                str = string.Format("<td width='25%' class='pic'><p>{0}</p><img style='width:90px;height:58;' src='{1}' alt='无效果图' class='textImg'></td>", title, imageUrl);
            }
            else if (materialType == "3")
            {
                str = string.Format("<td width='25%'><img style='width:259px;height:58px;' src='{0}' alt='无效果图' class='imgs'></td>", imageUrl);
            }
            else if (materialType == "4")
            {
                str = string.Format("<td width='25%' class='case'><h3>{0}</h3><p>{1}</p><button>{2}</button><button>{3}</button></td>", title, remark, confirmText, cancelText);
            }
            return str;
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
        private string GetSqlWhere(string key, string status, string pageId, string adLocationArr, string classArr, string materialType, string startTime, string endTime)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(key))
            {
                sb.Append(string.Format("( p1.materialname like '%{0}%')", key));
            }
            //状态
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
            //页面
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
            //物料类型
            if (!string.IsNullOrWhiteSpace(materialType) && materialType != "all")
            {
                if (sb.Length > 0)
                {
                    sb.Append(" ");
                    sb.Append("and");
                    sb.Append(" ");
                }
                sb.Append(string.Format("p1.materialtype={0}", materialType));
            }
            //开始时间-结束时间
            if (!string.IsNullOrWhiteSpace(startTime)&& !string.IsNullOrWhiteSpace(endTime))
            {
                if (sb.Length > 0)
                {
                    sb.Append(" ");
                    sb.Append("and");
                    sb.Append(" ");
                }
                sb.Append(string.Format("to_char(p1.createtime,'yyyymmdd') between  '{0}' and '{1}'", startTime,endTime));
            }            
            if (!string.IsNullOrWhiteSpace(adLocationArr))
            {
                Array tempArr = adLocationArr.Split(',');
                if (tempArr.Length > 0)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(" ");
                        sb.Append("and");
                        sb.Append(" ");
                    }
                    string tempStr = "";
                    foreach (var item in tempArr)
                    {
                        int temp;
                        if (int.TryParse(item.ToString(), out temp))
                            tempStr += temp.ToString() + ",";
                    }
                    tempStr = tempStr.Substring(0, tempStr.Length - 1);
                    sb.Append(string.Format("p1.adlocationid in ({0})", tempStr));
                }
            }
            //if (!string.IsNullOrWhiteSpace(classArr))
            //{
            //    Array tempArr = classArr.Split(',');
            //    if (tempArr.Length > 0)
            //    {
            //        if (sb.Length > 0)
            //        {
            //            sb.Append(" ");
            //            sb.Append("and");
            //            sb.Append(" ");
            //        }
            //        string tempStr = "";
            //        foreach (var item in tempArr)
            //        {
            //            int temp;
            //            if (int.TryParse(item.ToString(), out temp))
            //                tempStr += temp.ToString() + ",";
            //        }
            //        tempStr = tempStr.Substring(0, tempStr.Length - 1);
            //        sb.Append(string.Format("p1.classid in ({0})", tempStr));
            //    }
            //}

            #region 先查找匹配出来的物料id 然后再转换成sqlwhere

            string result = "1=2";//默认不满足
            DataTable dt = materialManager.GetMaterialIdByAnalyDT(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                string tempStr = "";
                foreach (DataRow dr in dt.Rows)
                {
                    int temp;
                    if (int.TryParse(dr["materialid"].ToString(), out temp))
                        tempStr += temp.ToString() + ",";
                }
                tempStr = tempStr.Substring(0, tempStr.Length - 1);
                result = string.Format("p1.materialid in ({0})", tempStr);

            }

            #endregion
            return result;
            // return sb.ToString();
        }
        /// <summary>
        /// 获取类型列表
        /// </summary>
        /// <returns></returns>
        private string GetStatusList(out string typeDefault)
        {
            string strData = string.Empty;            
            DataTable dt = materialManager.GetAdStatusListDT("");
            typeDefault = "全部";
            strData += string.Format(" <dd class='cur'><em>{0}</em><span style='display:none'>{1}</span></dd>", "全部", "all");
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strData += string.Format(" <dd><em>{0}</em><span style='display:none'>{1}</span></dd>", dt.Rows[i]["statusname"].ToString(), dt.Rows[i]["status"].ToString());
                }
            }

            return strData;
        }

        /// <summary>
        /// 获取类型列表
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

        private string GetTypeList(out string typeDefault)
        {
            string strData = string.Empty;            
            DataTable dt = adManager.GetAdTypeDT("");
            typeDefault = "全部";
            strData += string.Format(" <dd class='cur'><em>{0}</em><span style='display:none'>{1}</span></dd>", "全部", "all");
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strData += string.Format(" <dd><em>{0}</em><span style='display:none'>{1}</span></dd>", dt.Rows[i]["adtypename"].ToString(), dt.Rows[i]["adtypeid"].ToString());
                }
            }

            return strData;
        }
        /// <summary>
        /// 获取广告位
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns></returns>
        private string GetAdLocationList(string pageId)
        {
            //如果页面id为空则默认取第一个页面的广告位
            if (string.IsNullOrWhiteSpace(pageId) || pageId == "all")
                pageId = "1";
            string strData = string.Empty;
            StringBuilder sb = new StringBuilder();
            DataTable dt = adLocationManager.GetAdLocationNameDT(string.Format(" s1.pageid={0}", pageId));
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        sb.Append("<tr>");
                    }
                    sb.Append("<td>");
                    sb.Append("<div class='checkbox-con clearfix'>");
                    sb.Append("<span class='wrap'>");
                    sb.Append("<input type='checkbox' class='ipt-hide' checked='checked'>");
                    sb.Append("<label class='checkbox'></label></span><em>");
                    sb.Append(dt.Rows[i]["adlocationname"].ToString());
                    sb.Append("</em>");
                    sb.Append("<span class='hide'>");
                    sb.Append(dt.Rows[i]["adlocationid"].ToString());
                    sb.Append("</span>");
                    sb.Append("</div></td>");
                    if ((i + 1) % 3 == 0 && (i + 1) != dt.Rows.Count)
                    {
                        sb.Append("</tr><tr>");
                    }
                    if ((i + 1) == dt.Rows.Count)
                    {
                        sb.Append("</tr>");
                    }
                }
                strData = sb.ToString();
            }

            return strData;
        }
        /// <summary>
        /// 获取分类信息
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns></returns>
        private string GetClassList()
        {
            string strData = string.Empty;
            StringBuilder sb = new StringBuilder();
            DataTable dt = classManager.GetClassNameDT("");
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        sb.Append("<tr>");
                    }
                    sb.Append("<td>");
                    sb.Append("<div class='checkbox-con clearfix'>");
                    sb.Append("<span class='wrap'>");
                    sb.Append("<input type='checkbox' class='ipt-hide' checked='checked'>");
                    sb.Append("<label class='checkbox'></label></span><em>");
                    sb.Append(dt.Rows[i]["classname"].ToString());
                    sb.Append("</em></div></td>");
                    if ((i + 1) % 3 == 0 && (i + 1) != dt.Rows.Count)
                    {
                        sb.Append("</tr><tr>");
                    }
                    if ((i + 1) == dt.Rows.Count)
                    {
                        sb.Append("</tr>");
                    }
                }
                strData = sb.ToString();
            }

            return strData;
        }
    }

}