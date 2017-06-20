using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMH.EagleEye.pageclass
{
    /// <summary>
    /// 基于页面基类
    /// </summary>
    public  class CommonBase 
    {

        /// <summary>
        /// 根据指定key获取页面请求值
        /// </summary>
        /// <param name="request"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetRequestVal(string key)
        {
            string requestVal = "";
            try
            {

                requestVal = HttpContext.Current.Request[key].ToString();
            }
            catch 
            {
                requestVal = "";
            }

            return requestVal;
        }
        /// <summary>
        /// 根据指定key获取页面请求值，如果为空则返回默认值
        /// </summary>
        /// <param name="request"></param>
        /// <param name="key"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        public static string GetRequestVal( string key,string defaultVal)
        {
            string requestVal = "";
            try
            {
                requestVal = HttpContext.Current.Request[key].ToString();
                if (string.IsNullOrWhiteSpace(requestVal))
                    requestVal = defaultVal;
            }
            catch
            {
                requestVal = defaultVal;
            }

            return requestVal;
        }
        /// <summary>
        /// 根据指定key获取页面请求值，并转换成int类型，失败则返回默认值
        /// </summary>
        /// <param name="request"></param>
        /// <param name="key"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        public static int GetRequestIntVal( string key, int defaultVal)
        {
            int requestVal = 0;
            try
            {
                if (int.TryParse(HttpContext.Current.Request[key].ToString(), out requestVal))
                    return requestVal;
                else
                    return defaultVal;
            }
            catch
            {
                requestVal = defaultVal;
            }
            return requestVal;
        }
        /// <summary>
        /// 获取匹配地址-可以指定地址
        /// </summary>
        /// <param name="request"></param>
        /// <param name="inUrl"></param>
        /// <param name="inKey"></param>
        /// <param name="inVal"></param>
        /// <returns></returns>
        //public static string GetMatchUrl( string inUrl, string inKey, string inVal)
        //{

        //    var url = HttpUtility.UrlDecode(inUrl); ;            
        //    var tempVal = GetRequestVal(inKey);
        //    if (url.IndexOf('?') > -1)
        //    {
        //        if (url.Length == url.IndexOf('?') + 1)
        //        {
        //            url = url + inKey + "=" + inVal;
        //        }
        //        else
        //        {
        //            if (!string.IsNullOrWhiteSpace(tempVal))
        //            {
        //                string oldStr = inKey + "=" + tempVal;
        //                string newStr = inKey + "=" + inVal;
        //                url = url.Replace(oldStr, newStr);
        //            }
        //            else
        //            {
        //                url = url + "&" + inKey + "=" + inVal;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        url = url + "?" + inKey + "=" + inVal;
        //    }
        //    return url;
        //}

        /// <summary>
        /// 获取匹配地址-当前请求地址
        /// </summary>
        /// <param name="request"></param>
        /// <param name="inKey"></param>
        /// <param name="inVal"></param>
        /// <returns></returns>
        public static string GetMatchUrl( string inKey, string inVal)
        {
            var url = HttpUtility.UrlDecode(HttpContext.Current.Request.Url.ToString()); ;
            var tempVal = GetRequestVal( inKey);
            if (url.IndexOf('?') > -1)
            {
                if (url.Length == url.IndexOf('?') + 1)
                {
                    url = url + inKey + "=" + inVal;
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(tempVal))
                    {
                        string oldStr = inKey + "=" + tempVal;
                        string newStr = inKey + "=" + inVal;
                        url = url.Replace(oldStr, newStr);
                    }
                    else
                    {
                        url = url + "&" + inKey + "=" + inVal;
                    }
                }
            }
            else
            {
                url = url + "?" + inKey + "=" + inVal;
            }
            return url;
        }
        /// <summary>
        /// 获取匹配地址-url匹配替换
        /// </summary>
        /// <param name="inUrl"></param>
        /// <param name="inKey"></param>
        /// <param name="inVal"></param>
        /// <returns></returns>
        public static string GetMatchUrl(string inUrl, string inKey, string inVal)
        {

            var url = inUrl;
            if (url.IndexOf('?') > -1)
            {
                if (url.Length == url.IndexOf('?') + 1)
                {
                    url = url + inKey + "=" + inVal;
                }
                else
                {
                    string tempStr = url.Substring(url.IndexOf("?") + 1);
                    if (tempStr.IndexOf(inKey) > -1)
                    {
                        tempStr = tempStr.Substring(tempStr.IndexOf(inKey));
                        if (tempStr.IndexOf("&") > -1)
                        {
                            tempStr = tempStr.Substring(0, tempStr.IndexOf("&"));
                        }
                        string oldStr = tempStr;
                        string newStr = inKey + "=" + inVal;
                        url = url.Replace(oldStr, newStr);
                    }
                    else
                    {
                        url = url + "&" + inKey + "=" + inVal;
                    }
                }
            }
            else
            {
                url = url + "?" + inKey + "=" + inVal;
            }
            return url;
        }

      
    }
}