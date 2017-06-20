using BLL.agent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMH.EagleEye.api
{
    /// <summary>
    /// AgentHandler 的摘要说明
    /// </summary>
    public class AgentHandler : IHttpHandler
    {
        ReportAgent rAgent;
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
                case "GetDetailData":
                    result = GetDetailData();
                    break;
                case "GetDetailForPieData":
                    result = GetDetailForPieData();
                    break;
                case "GetDeductDetail":
                    result = GetDeductDetail();
                    break;
                case "GetLinkUrlDetail":
                    result = GetLinkUrlDetail();
                    break;
                case "GetLinkUrlDetailByLink":
                    result = GetLinkUrlDetailByLink();
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
            YYLog.ClassLibrary.Log.WriteLog(string.Format("YYLog.Modify:/api/AgentHandler/{0}", funcName), "账户信息:账户id{0},账户名{1},ip{2};操作信息:{3}", _strAccountId, _strAccountName, pageclass.IP.GetIP(), data);
        }
        private string GetDetailData()
        {
            string adUserId = GetRequestVal("aduserid");
            rAgent = new ReportAgent();
            return rAgent.GetDetailData(adUserId);
        }
        private string GetDetailForPieData()
        {
            string adUserId = GetRequestVal("aduserid");
            rAgent = new ReportAgent();
            return rAgent.GetDetailForPieData(adUserId);
        }

        private string GetDeductDetail()
        {
            string adUserId = GetRequestVal("aduserid");
            string sTime = GetRequestVal("stime");
            string eTime = GetRequestVal("etime");
            rAgent = new ReportAgent();
            return rAgent.GetDeductDetail(adUserId, sTime, eTime);
        }
        private string GetLinkUrlDetail()
        {
            string adUserId = GetRequestVal("aduserid");
            string sTime = GetRequestVal("stime");
            string eTime = GetRequestVal("etime");
            rAgent = new ReportAgent();
            return rAgent.GetLinkUrlDetail(adUserId, sTime, eTime);
        }
        private string GetLinkUrlDetailByLink()
        {
            string adUserId = GetRequestVal("aduserid");
            string sTime = GetRequestVal("stime");
            string eTime = GetRequestVal("etime");
            string lUrl = HttpUtility.UrlDecode( GetRequestVal("lurl"));
            rAgent = new ReportAgent();
            return rAgent.GetLinkUrlDetailByLink(adUserId, sTime, eTime,lUrl);
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