using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.main;
using Model;

namespace BMH.EagleEye.page
{
    public partial class main : System.Web.UI.Page
    {
        #region 变量定义
        MainData mainData = new MainData();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //页面登录验证

            if (!IsPostBack)
            {

            }
        }

        /// <summary>
        /// 根据BeeDataType获取对应数据
        /// </summary>
        public string GetDataInfo(BeeDataType dtType)
        {

            string strData = string.Empty;
            switch (dtType)
            {
                case BeeDataType.adlocation:
                    List<AdLocation> adLocationList = mainData.GetAdLocationList();
                    if (adLocationList != null && adLocationList.Count > 0)
                    {
                        foreach (var item in adLocationList)
                        {
                            strData += string.Format("<li><a href=\"report/adlocation_data.aspx?adlocationid={0}\" target=\"_blank\">{1}</a></li>", item.adlocationid, item.name);                            
                        }
                    }
                    break;
                case BeeDataType.ad:
                    List<Advertisement> adList = mainData.GetAdList();
                    if (adList != null && adList.Count > 0)
                    {
                        foreach (var item in adList)
                        {
                            strData += string.Format("<li><a href=\"report/ad_data.aspx?adid={0}\" target=\"_blank\">{1}</a></li>", item.adid, item.name);
                        }
                    }
                    break;
                case BeeDataType.material:
                    List<Material> materialList = mainData.GetMaterialList();
                    if (materialList != null && materialList.Count > 0)
                    {
                        foreach (var item in materialList)
                        {
                            strData += string.Format("<li><a href=\"report/material_data.aspx?materialid={0}\" target=\"_blank\">{1}</a></li>", item.materialid, item.name);
                        }
                    }
                    break;
            }

            return strData;
        }
    }
}