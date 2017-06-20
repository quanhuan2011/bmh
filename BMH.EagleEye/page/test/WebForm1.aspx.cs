using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BMH.EagleEye.pageclass;
using CVBUtility;
using BLL.redis;

namespace BMH.EagleEye.page.test
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

           // System.Configuration.AppSettingsReader appReader = new System.Configuration.AppSettingsReader();
           //string tempStr= DatabasePool.GetDatabaseConnectStr(appReader.GetValue("DBClient", typeof(string)).ToString(), "chinavb234123489");
        }

        protected void btnTest_Click(object sender, EventArgs e)
        {
            //string key = "a";
            //string val = "123";
            ////Request.Url
            //string url = CommonBase.GetMatchUrl( key, val);
            //Response.Redirect(url);
            RedisBase redisBase = new RedisBase();
            redisBase.UpdatePrice("2187", "90", "0.1");
        }

        protected void btnPutInfo_Click(object sender, EventArgs e)
        {
            bool boolStatus = false;
            RedisBase redisBase = new RedisBase();
            string adId = txtAdId.Text.ToString();
            string putMax =   txtPutMax.Text.ToString();
            string putMaxByDay = txtPutMaxByDay.Text.ToString();
           redisBase.SetPutMaxInfo(adId, putMax, putMaxByDay,out boolStatus);
        }
        private void test()
        {
            
        }

        protected void btnPutInfoByAdU_Click(object sender, EventArgs e)
        {
            bool boolStatus = false;
            RedisBase redisBase = new RedisBase();
            string adUserId = txtAdUId.Text.ToString();           
            string putMaxByDay = txtPutMaxByDayOfAdU.Text.ToString();
            redisBase.SetPutMaxInfoByAdU(adUserId, putMaxByDay, out boolStatus);

        }
    }
}