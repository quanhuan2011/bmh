using CVBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMH.EagleEye.page.Other
{
    public partial class SwitchAdUserId : System.Web.UI.Page
    {
        public static readonly string strCookieUrl = System.Configuration.ConfigurationManager.AppSettings["Yingyan_CookieUrl"];
        public static readonly string strCookieName = System.Configuration.ConfigurationManager.AppSettings["Yingyan_CookieName"];

        protected string bindAdUserId = "";
        protected string bindAccountName = "";
        protected string bindName = "";

        protected void Page_Init(object sender, EventArgs e)
        {

            //System.Configuration.AppSettingsReader objAppReader = new System.Configuration.AppSettingsReader();
            //string aa = DatabasePool.GetDatabaseConnectStr("bee", "chinavb234123489");

            System.Configuration.AppSettingsReader objAppReader = new System.Configuration.AppSettingsReader();
            DatabasePool.GetDatabaseConnectStr(objAppReader.GetValue("DBService", typeof(string)).ToString(), "chinavb234123489");

            AddChannel("0", 0);
        }

        protected void AddChannel(string FatherID, int deep)
        {

            string strSelectSql = "SELECT * FROM BEE.BEE_ADUSERINFO WHERE STATUS = 1 ORDER BY ADUSERID ASC";

            CommonDBOperation objDB = null;
            try
            {
                objDB = new CommonDBOperation();
                DataTable dt = objDB.GetTable(strSelectSql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    char nbsp = (char)0xA0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string Before = "|-";
                        if (deep > 0)
                            Before = Before.PadLeft(deep * 2 + 2, nbsp);
                        else
                            Before = "";
                        ddlAdUser.Items.Add(new System.Web.UI.WebControls.ListItem(Before + dt.Rows[i]["NAME"].ToString(), dt.Rows[i]["ADUSERID"].ToString()));
                        deep--;
                    }
                }
                else
                {
                    //eventLog1.WriteEntry("未获取到任务数据");
                    //Thread.Sleep(1000 * 60);
                }
                dt.Dispose();
            }
            catch (Exception ex)
            {
                //eventLog1.WriteEntry(ex.Message);
            }
            finally
            {
                if (null != objDB)
                {
                    objDB.Close();
                    objDB.Dispose();
                    objDB = null;

                }
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            BaseClass.POMOHOCookie cookies = new BaseClass.POMOHOCookie();

            if (!cookies.IsLogin)
            {
                Response.Redirect("/page/Login.aspx");
            }
            else
            {
                bindAdUserId = cookies.BeeAdUserId;



                #region 获取广告主信息

                string strSql = "SELECT * FROM BEE.BEE_ADUSERINFO WHERE STATUS = 1 AND ADUSERID = :ADUSERID";

                CommonDBOperation objDB = null;
                try
                {
                    objDB = new CommonDBOperation();

                    List<ParamItem> parmList = new List<ParamItem>();
                    parmList.Add(new ParamItem(":ADUSERID", bindAdUserId));

                    DataTable dt = objDB.GetTable(strSql, parmList);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        bindAccountName = dt.Rows[0]["AccountName"].ToString();
                        bindName = dt.Rows[0]["Name"].ToString();
                    }
                    else
                    {
                        //eventLog1.WriteEntry("未获取到任务数据");
                        //Thread.Sleep(1000 * 60);
                    }
                    dt.Dispose();
                }
                catch (Exception ex)
                {
                    //eventLog1.WriteEntry(ex.Message);
                }
                finally
                {
                    if (null != objDB)
                    {
                        objDB.Close();
                        objDB.Dispose();
                        objDB = null;

                    }
                }


                #endregion
            }
        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            string strSelectUserId = ddlAdUser.SelectedValue;




            BaseClass.POMOHOCookie cookies = new BaseClass.POMOHOCookie();

            if (!cookies.IsLogin)
            {
                Response.Redirect("/Account/Login.aspx");
            }
            else
            {

                string strAccountId = cookies.BeeAccountId;
                string strAccountName = cookies.BeeAccountName;
                string strAccountUserName = cookies.BeeAccountUserName;
                string strAccountType = cookies.BeeAccountType;
                string strHeadImageUrl = cookies.BeeHeadImageUrl;

                //accountType = cookies.BeeAccountType;


                System.Web.HttpCookie cookie = new System.Web.HttpCookie(strCookieName);
                cookie.Values.Add("AccountId", Encryptor.DesEncrypt((strAccountId).ToString(), strCookieUrl));
                cookie.Values.Add("AccountName", Encryptor.DesEncrypt((strAccountName).ToString(), strCookieUrl));
                cookie.Values.Add("AccountUserName", Encryptor.DesEncrypt((strAccountUserName).ToString(), strCookieUrl));
                cookie.Values.Add("AccountType", Encryptor.DesEncrypt((strAccountType).ToString(), strCookieUrl));
                cookie.Values.Add("HeadImageUrl", Encryptor.DesEncrypt((strHeadImageUrl).ToString(), strCookieUrl));

                cookie.Values.Add("AdUserId", Encryptor.DesEncrypt((strSelectUserId).ToString(), strCookieUrl));




                //cookie.Domain = strCookieUrl;
                System.Web.HttpContext.Current.Response.Cookies.Add(cookie);

                VVFF.Common.MyClass.ShowMessage("切换完毕！~！", 1, "", true);

            }
        }
    }
}