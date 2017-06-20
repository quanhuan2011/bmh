using BLL.manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMH.EagleEye.api
{
    /// <summary>
    /// BidHandler 的摘要说明
    /// </summary>
    public class BidHandler : IHttpHandler
    {
        AdUserManager adUserManager;
        AdLocationManager adLManager;
        AdvertisementManager advManager;
        PageManager pageManager;
        string _strAccountId = string.Empty;
        string _strAccountName = string.Empty;
        string _strAccountUserName = string.Empty;
        string _strAccountType = string.Empty;
        string _strHeadImageUrl = string.Empty;
        string _strAdUserId = string.Empty;
        public void ProcessRequest(HttpContext context)
        {
            #region 登录cookie信息
            BaseClass.POMOHOCookie cookies = new BaseClass.POMOHOCookie();
            _strAccountId = cookies.BeeAccountId;
            _strAccountName = cookies.BeeAccountName;
            _strAccountUserName = cookies.BeeAccountUserName;
            _strAccountType = cookies.BeeAccountType;
            _strHeadImageUrl = cookies.BeeHeadImageUrl;
            _strAdUserId = cookies.BeeAdUserId;
            #endregion

            context.Response.ContentType = "text/plain";

            string method = GetRequestVal("method");
            string result = "";
            switch (method)
            {
                case "UpdateAduWeight":
                    result = UpdateAduWeight();
                    break;
                case "UpdateAdlBid":
                    result = UpdateAdlBid();
                    break;
                case "GetAdlListByBid":
                    result = UpdateAdlBid();
                    break;
                case "GetAdlListByNoBid":
                    result = GetAdlListByNoBid();
                    break;
                case "UpdateAdlIsBid":
                    result = UpdateAdlIsBid();
                    break;
                case "GetAdvListByNoBid":
                    result = GetAdvListByNoBid();
                    break;
                case "GetPageListByBid":
                    result = GetPageListByBid();
                    break;
                case "GetPageListBySubAdType":
                    result = GetPageListBySubAdType();
                    break;
                case "UpdateIsBidByAdList":
                    result = UpdateIsBidByAdList();
                    break;
                default:
                    result = BLL.pub.Result.GetFailResult("错误的调用方式，此方法不存在");
                    break;
            }

            context.Response.Write(result);
        }
        /// <summary>
        /// 日志记录操作方法
        /// </summary>
        /// <param name="funcName"></param>
        /// <param name="data"></param>
        private void LogModify(string funcName, string data)
        {
            YYLog.ClassLibrary.Log.WriteLog(string.Format("YYLog.Modify:/api/BidHandler/{0}", funcName), "账户信息:账户id{0},账户名{1},ip{2};操作信息:{3}", _strAccountId, _strAccountName, pageclass.IP.GetIP(), data);
        }
        /// <summary>
        /// 修改代理商(广告主)权重
        /// </summary>
        /// <returns></returns>
        private string UpdateAduWeight()
        {
            string adUserId = GetRequestVal("aduid");
            string weight = GetRequestVal("weight");
            LogModify("UpdateAduWeight", string.Format("aduserid:{0},weight:{1}",adUserId,weight));
            adUserManager = new AdUserManager();
            return adUserManager.UpdateAduWeight(adUserId, weight);
        }
        /// <summary>
        /// 修改广告位是否竞价
        /// </summary>
        /// <returns></returns>
        private string UpdateAdlBid()
        {
            string adLocationId = GetRequestVal("adlid");
            string isBid = GetRequestVal("isbid");
            LogModify("UpdateAdlBid", string.Format("adlocationid:{0},isbid:{1}",adLocationId,isBid));
            adLManager = new AdLocationManager();
            return adLManager.UpdateAdlBid(adLocationId, isBid);
        }
        private string GetAdlListByNoBid()
        {
            adLManager = new AdLocationManager();
            return adLManager.GetAdlListByNoBid();

        }
        private string UpdateAdlIsBid()
        {
            string updatetype = GetRequestVal("updatetype");
            string data = GetRequestVal("data");
            LogModify("UpdateAdlIsBid", string.Format("updatetype:{0},data:{1}",updatetype,data));
            adLManager = new AdLocationManager();
            return adLManager.UpdateAdlIsBid(updatetype, data);
        }

        private string GetAdvListByNoBid()
        {
            advManager = new AdvertisementManager();
            return advManager.GetAdvListByNoBid();
        }

        private string GetPageListByBid()
        {
            string adId = GetRequestVal("adid");
            pageManager = new PageManager();
            return pageManager.GetPageListByBid(adId);
        }

        private string GetPageListBySubAdType()
        {
            string subAdTypeId = GetRequestVal("subadtypeid");
            pageManager = new PageManager();
            return pageManager.GetPageListBySubAdType(subAdTypeId);
        }
        //
        private string UpdateIsBidByAdList()
        {
            string data = GetRequestVal("data");
            LogModify("UpdateIsBidByAdList", data);
            advManager = new AdvertisementManager();
            return advManager.UpdateIsBidByAdList(data);
        }
        private string GetRequestVal(string key)
        {
            try
            {
                return HttpContext.Current.Request[key].ToString();
            }
            catch
            {

                return "";
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}