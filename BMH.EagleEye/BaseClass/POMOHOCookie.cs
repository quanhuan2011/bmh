using System;
using System.Collections.Generic;
using System.Web;

namespace BMH.EagleEye.BaseClass
{
    public class POMOHOCookie
    {
        public static readonly string strCookieUrl = System.Configuration.ConfigurationManager.AppSettings["Yingyan_CookieUrl"];
        public static readonly string strCookieName = System.Configuration.ConfigurationManager.AppSettings["Yingyan_CookieName"];



        //Session.Remove("BeeAccountId");
        //Session.Remove("BeeAccountName");
        //Session.Remove("BeeUserName");//////////////
        //Session.Remove("BeeAccountType");
        //Session.Remove("BeeHeadImageUrl");



        /// <summary>
        /// 是否登录
        /// </summary>
        public Boolean IsLogin
        {
            get
            {
                return (HttpContext.Current.Request.Cookies[strCookieName] != null);
            }
        }

        /// <summary>
        /// 账户ID
        /// </summary>
        public string BeeAccountId
        {
            get
            {
                if ((HttpContext.Current.Request.Cookies[strCookieName] != null) && (HttpContext.Current.Request.Cookies[strCookieName]["AccountId"] != null))
                {
                    //return HttpContext.Current.Request.Cookies[strCookieName]["AccountId"];
                    return Encryptor.DesDecrypt(HttpContext.Current.Request.Cookies[strCookieName]["AccountId"], strCookieName);
                }
                return null;
            }
        }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string BeeAccountName
        {
            get
            {
                if ((HttpContext.Current.Request.Cookies[strCookieName] != null) && (HttpContext.Current.Request.Cookies[strCookieName]["AccountName"] != null))
                {
                    //return HttpContext.Current.Request.Cookies[strCookieName]["AccountName"].ToLower();
                    return Encryptor.DesDecrypt(HttpContext.Current.Request.Cookies[strCookieName]["AccountName"], strCookieName);
                }
                return null;
            }
        }

        /// <summary>
        /// 用户登录帐号
        /// </summary>
        public string BeeAccountUserName
        {
            get
            {
                if ((HttpContext.Current.Request.Cookies[strCookieName] != null) && (HttpContext.Current.Request.Cookies[strCookieName]["AccountUserName"] != null))
                {
                    //return HttpContext.Current.Request.Cookies[strCookieName]["AccountName"].ToLower();
                    return Encryptor.DesDecrypt(HttpContext.Current.Request.Cookies[strCookieName]["AccountUserName"], strCookieName);
                }
                return null;
            }
        }

        /// <summary>
        /// 账户类型
        /// </summary>
        public string BeeAccountType
        {
            get
            {
                if ((HttpContext.Current.Request.Cookies[strCookieName] != null) && (HttpContext.Current.Request.Cookies[strCookieName]["AccountType"] != null))
                {
                    return Encryptor.DesDecrypt(HttpContext.Current.Request.Cookies[strCookieName]["AccountType"], strCookieName);
                }
                return null;
            }
        }

        /// <summary>
        /// 账户头像
        /// </summary>
        public string BeeHeadImageUrl
        {
            get
            {
                if ((HttpContext.Current.Request.Cookies[strCookieName] != null) && (HttpContext.Current.Request.Cookies[strCookieName]["HeadImageUrl"] != null))
                {
                    return Encryptor.DesDecrypt(HttpContext.Current.Request.Cookies[strCookieName]["HeadImageUrl"], strCookieName);
                }
                return null;
            }
        }

        public string BeeAdUserId
        {
            get
            {
                if ((HttpContext.Current.Request.Cookies[strCookieName] != null) && (HttpContext.Current.Request.Cookies[strCookieName]["AdUserId"] != null))
                {
                    return Encryptor.DesDecrypt(HttpContext.Current.Request.Cookies[strCookieName]["AdUserId"], strCookieName);
                }
                return null;
            }
        }
    }
}