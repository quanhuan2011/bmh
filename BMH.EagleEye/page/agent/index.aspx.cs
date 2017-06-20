using BLL.agent;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMH.EagleEye.page.agent
{
    public partial class index : System.Web.UI.Page
    {
        ReportAgent rAgent;
        //余额
        protected string  balanceSum = 0.ToString("f2");
        //返点
        protected string rebateSum = 0.ToString("f2");
        protected string lastRebateSum = 0.ToString("f2");
        //消费
        protected string deductSum = 0.ToString("f2");
        protected string lastDeductSum = 0.ToString("f2");
        // 广告主ID
        public string aduserid = string.Empty;
        protected string strBindAccountId = "";
        protected string headImageUrl;
        protected string accountName;
        protected  string accountType="";
        protected void Page_Init(object sender, EventArgs e)
        {
#if TalentDebug
            aduserid="10";
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
                accountName = strAccountName;
                accountType = strAccountType;
                if (string.IsNullOrEmpty(headImageUrl))
                {
                    headImageUrl = "http://yingyan.baomihua.com/page/images/head.jpg";
                }
                YYLog.ClassLibrary.Log.WriteLog("YYLog.Login:/page/agent/index", "账户信息:账户id{0},账户名{1},ip{2}", strAccountId, strAccountName, pageclass.IP.GetIP());
            }
#endif
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            GetAgentSum(aduserid);
        }
        /// <summary>
        /// 获取总数据
        /// </summary>
        /// <param name="adUserId"></param>
        private void GetAgentSum(string adUserId)
        {
            rAgent = new ReportAgent();
            balanceSum =Convert.ToDouble( rAgent.GetAdSumOfAdUser(adUserId)).ToString("f2");
           // rebateSum= Convert.ToDouble(rAgent.GetRebateSumb(adUserId)).ToString("f2");
            DataTable dt1 = rAgent.GetRebateSumbDT(adUserId);
            if (dt1 != null && dt1.Rows.Count > 0)
            {
                lastRebateSum = Convert.ToDouble(dt1.Rows[0]["lastrbtamt"].ToString()).ToString("f2");
                rebateSum = Convert.ToDouble(dt1.Rows[0]["rbtamt"].ToString()).ToString("f2");
            }
            DataTable dt2 = rAgent.GetDeductSumbDT(adUserId);
            if (dt2 != null && dt2.Rows.Count > 0)
            {                
                deductSum = Convert.ToDouble(dt2.Rows[0]["incomesum"].ToString()).ToString("f2");
            }
            DataTable dt3 = rAgent.GetLastDeductSumbDT(adUserId);
            if (dt3 != null && dt3.Rows.Count > 0)
            {
                lastDeductSum = Convert.ToDouble(dt3.Rows[0]["incomesum"].ToString()).ToString("f2");            
            }
            //top10
            DataTable dt4 = rAgent.GetLinkUrlTop10DT(adUserId);
            if (dt4 != null && dt4.Rows.Count > 0)
            {
                repList1.DataSource = dt4;
                repList1.DataBind();
            }
            else
            {
                litNoInfo1.Text = "<tr><td colspan='9'><p style='width:100%; line-height:200px; text-align:center'>无数据...</p></td></tr>";
            }
        }


    }
}