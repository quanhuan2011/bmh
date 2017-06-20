using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.Material;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using Common.pub;
using Ajax;
using BMH.EagleEye.pageclass;
using BLL.manager;
using BLL.permission;

namespace BMH.EagleEye.page.manager
{
    public partial class material_info : System.Web.UI.Page
    {
        #region 参数定义
        AdUserManager adUserManager;
        MaterialManager materialManager;
        AdvertisementManager adManager;       
        MaterialBll materialBll = new MaterialBll();
        VerifyPermission verifyPermission;
        System.Configuration.AppSettingsReader appReader = new System.Configuration.AppSettingsReader();

        public string aduserid = string.Empty;
        protected string strBindAccountId = "";
        public string key = string.Empty;
        protected string hfAduserIdValue = "";
        protected string adUserDefault = string.Empty;
        protected string adUserList = string.Empty;
        protected string adTypeDefault = string.Empty;
        protected string adTypeList = string.Empty;
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

                YYLog.ClassLibrary.Log.WriteLog("YYLog.Login:/page/manager/material_info", "账户信息:账户id{0},账户名{1},ip{2}", strAccountId, strAccountName, pageclass.IP.GetIP());
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
           // hfAduserIdValue = aduserid;
            Ajax.Utility.RegisterTypeForAjax(typeof(BMH.EagleEye.page.manager.material_info));
            if (!IsPostBack)
            {
                #region 权限控制
                verifyPermission = new VerifyPermission();
                permLevel = verifyPermission.GetPermissionLevel(strBindAccountId);
                #endregion

                #region 数据获取
                string key = CommonBase.GetRequestVal("key");//搜索内容
                string adUserId = CommonBase.GetRequestVal("aduid");
                string materialType = CommonBase.GetRequestVal("mattype");
                int pageNo = CommonBase.GetRequestIntVal("page", 1);
                string defaultPageSize = appReader.GetValue("DefaultPageSize", typeof(string)).ToString();
                string defaultPageNumber = appReader.GetValue("DefaultPageNumber", typeof(string)).ToString();
                int pageSize = string.IsNullOrEmpty(defaultPageSize) ? 15 : int.Parse(defaultPageSize);
                int pageNumber = string.IsNullOrEmpty(defaultPageNumber) ? 4 : int.Parse(defaultPageNumber);
                adUserList = GetAdUserList(out adUserDefault);
                adTypeList = GetAdTypeList(out adTypeDefault);
                string sqlWhere = GetSqlWhere(key, adUserId, materialType);
                GetListData(pageSize, pageNo, pageNumber, sqlWhere);
                #endregion
            }
        }
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageNumber"></param>
        /// <param name="sqlWhere"></param>
        public void GetListData(int pageSize, int pageNo, int pageNumber, string sqlWhere)
        {
            int dataCount = 0;
            materialManager = new MaterialManager();
            DataTable dt = materialManager.GetMaterialListDT(pageSize, pageNo, sqlWhere, out dataCount); // adManager.GetAdListDT(pageSize, pageNo, sqlWhere, out dataCount);
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
        #region 
        /// <summary>
        /// 查询数据
        /// </summary>
        //public void GetLstData()
        //{
        //    string whereStr = string.Empty;
        //    if (!string.IsNullOrEmpty(key.Trim()))
        //    {
        //        whereStr += string.Format(" and mname like'%{0}%'", key.Trim());
        //    }
        //    if (aduserid != string.Empty)
        //    {
        //        whereStr += string.Format(" and aduserid={0}", aduserid);
        //    }
        //    DataTable dt = materialBll.GetMaterialinfoData(whereStr, pageNum, pageSize, out dataCount);
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        this.repMaterialList.DataSource = dt;
        //        this.repMaterialList.DataBind();
        //        litPage.Text = PageTxt.GetPageText(GetUrl(), CommonVariables.PageNumber, dataCount, pageSize, pageNum);
        //        var pageCount = Convert.ToInt32(System.Math.Ceiling(Convert.ToDouble(dataCount) / Convert.ToDouble(pageSize)));
        //        litPageInfo.Text = string.Format("共有<font style='color: red;'>{0}</font>条&nbsp;&nbsp;<font style='color: red;'>{1}</font>/<span>{2}</span>页", dataCount, pageNum, pageCount);
        //    }
        //    else
        //    {
        //        litNoInfo.Text = "<tr><td colspan='7'><p style='width:100%; line-height:100px; text-align:center'>无数据...</p></td></tr>";
        //    }
        //}

        /// <summary>
        /// 拼装URL
        /// </summary>
        public string GetUrl()
        {
            string url = Request.Url.ToString();
            return url;
        }
        private string GetSqlWhere(string key, string adUserId, string materialType)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(key))
            {
                sb.Append(string.Format("( s1.name like '%{0}%' or s1.materialid like '%{0}%')", key));
            }
            if (!string.IsNullOrWhiteSpace(adUserId) && adUserId != "all")
            {
                if (sb.Length > 0)
                {
                    sb.Append(" ");
                    sb.Append("and");
                    sb.Append(" ");
                }
                sb.Append(string.Format("s1.aduserid={0}", adUserId));
            }
            if (!string.IsNullOrWhiteSpace(materialType) && materialType != "all")
            {
                if (sb.Length > 0)
                {
                    sb.Append(" ");
                    sb.Append("and");
                    sb.Append(" ");
                }
                sb.Append(string.Format("s1.materialtype={0}", materialType));
            }
            return sb.ToString();
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
        /// 物料类型即广告类型（一个类型）
        /// </summary>
        /// <param name="strDefault"></param>
        /// <returns></returns>
        private string GetAdTypeList(out string strDefault)
        {
            string strData = string.Empty;
            adManager = new AdvertisementManager();
            DataTable dt = adManager.GetAdTypeDT("");
            strDefault = "全部";
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
        ///// <summary>
        ///// 组合查询url
        ///// </summary>
        ///// <param name="key">查询关键字</param>
        ///// <param name="aduserid">广告主id</param>
        ///// <returns>跳转链接url</returns>
        //[Ajax.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
        //public string GetSearchUrl(string key, string aduserid)
        //{
        //    string rurl = string.Format("material_info.aspx?aduserid={0}", aduserid);
        //    if (!string.IsNullOrEmpty(key.Trim()))
        //    {
        //        rurl += string.Format("&key={0}", key);
        //    }
        //    return rurl;
        //    //Response.Redirect(rurl, true);
        //}
        #endregion

        #region 无效
        ///// <summary>
        ///// 查询
        ///// </summary>
        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
        //    //GetSearchUrl();
        //}
        ///// <summary>
        ///// 查询数据
        ///// </summary>
        //public void GetLstData()
        //{
        //    string whereStr = string.Empty;
        //    if (!string.IsNullOrEmpty(key.Value.Trim()))
        //    {
        //        whereStr += string.Format(" and mname like'%{0}%'", key.Value.Trim());
        //    }
        //    if (aduserid != string.Empty)
        //    {
        //        whereStr += string.Format(" and aduserid={0}", aduserid);
        //    }
        //    DataTable dt = materialBll.GetMaterialinfoData(whereStr, pageNum, pageSize, out dataCount);
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        this.repMaterialList.DataSource = dt;
        //        this.repMaterialList.DataBind();
        //        litPage.Text = PageTxt.GetPageText(GetUrl(), 6, dataCount, pageSize, pageNum);
        //        var pageCount = Convert.ToInt32(System.Math.Ceiling(Convert.ToDouble(dataCount) / Convert.ToDouble(pageSize)));
        //        litPageInfo.Text = string.Format("共有<font style='color: red;'>{0}</font>条&nbsp;&nbsp;<font style='color: red;'>{1}</font>/<span>{2}</span>页", dataCount, pageNum, pageCount);
        //    }
        //    else
        //    {
        //        litNoInfo.Text = "<tr><td colspan='7'><p style='width:100%; line-height:100px; text-align:center'>无数据...</p></td></tr>";
        //    }
        //}

        ///// <summary>
        ///// 拼装URL
        ///// </summary>
        //public string GetUrl()
        //{
        //    StringBuilder url = new StringBuilder();
        //    url.Append("material_info.aspx?aduserid=" + aduserid);
        //    if (!string.IsNullOrEmpty(key.Value))
        //    {
        //        url.AppendFormat("&key={0}", key.Value.Trim());
        //    }
        //    url.Append("&page=");
        //    return url.ToString();
        //}

        ///// <summary>
        ///// 查询
        ///// </summary>
        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
        //    //GetSearchUrl();
        //}

        ///// <summary>
        ///// 组合查询Url
        ///// </summary>
        //[Ajax.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
        //public void GetSearchUrl(string key) 
        //{
        //    string rurl = string.Format("material_info.aspx?aduserid={0}", aduserid);
        //    if (!string.IsNullOrEmpty(key.Value.Trim()))
        //    {
        //        rurl += string.Format("&key={0}", key.Value);
        //    }
        //    Response.Redirect(rurl, true);
        //} 
        #endregion


        /// <summary>
        /// 转换物料类型
        /// </summary>
        /// <param name="materialtype">物料类型</param>
        /// <returns>物料类型结果值</returns>
        public string ChangeData(string materialtype)
        {
            return materialtype == "1" ? "图片" : "信息流";
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
                str = string.Format("<td style='width:336px'><img style='width:255px;height:63px;' src='{0}' alt='无效果图' class='imgs'></td>", imageUrl);
            }
            else if (materialType == "2")
            {
                str = string.Format("<td class='pic' style='width:336px'><p>{0}</p><img style='width:76px;height:63;' src='{1}' alt='无效果图' class='textImg'></td>", title, imageUrl);
            }
            else if (materialType == "3")
            {
                str = string.Format("<td style='width:336px'><img style='width:255px;height:63px;' src='{0}' alt='无效果图' class='imgs'></td>", imageUrl);
            }
            else if (materialType == "4")
            {
                str = string.Format("<td class='case'><h3>{0}</h3><p>{1}</p><button>{2}</button><button>{3}</button></td>", title, remark, confirmText, cancelText);
            }
            else if (materialType == "5")
            {
                str = string.Format("<td style='width:336px'><img style='width:255px;height:63px;' src='{0}' alt='无效果图' class='imgs'></td>", imageUrl);
            }
            else if (materialType == "6")
            {
                str = string.Format("<td style='width:336px'><img style='width:255px;height:63px;' src='{0}' alt='无效果图' class='imgs'></td>", imageUrl);
            }
            else 
            {
                str = string.Format("<td style='width:336px'><img style='width:255px;height:63px;' src='{0}' alt='无效果图' class='imgs'></td>", imageUrl);
            }
            return str;
        }


    }
}