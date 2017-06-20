using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Security;

namespace Common.pub
{
    public class Utility
    {
        /// <summary>
        /// 获取int型数据
        /// </summary>
        /// <param name="obj">需要解析的数据</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int GetRequestVal(object obj, int defaultValue)
        {
            if (IsNum(obj) && obj.ToString().Length < Int32.MaxValue.ToString().Length)
                return Convert.ToInt32(obj);
            else
                return defaultValue;
        }        
        /// <summary>
        /// 判断是否是整数
        /// </summary>
        /// <param name="num">输入字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsNum(object num)
        {
            if (num == null || num == DBNull.Value || string.IsNullOrEmpty(num.ToString()))
                return false;
            if (Regex.IsMatch(num.ToString().Replace("-", ""), @"[^\d]+", RegexOptions.IgnoreCase))
                return false;
            else return true;
        }
        /// <summary>
        /// md5加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string Md5Encrypt(string str, EncryptType type)
        {
            string sEncrypt = string.Empty;
            if (type == EncryptType.Md5Code16)
            {
                sEncrypt = FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").Substring(8, Convert.ToInt32(type));
            }
            if (type == EncryptType.Md5Code32)
            {
                sEncrypt = FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
            }
            return sEncrypt;
        }
        /// <summary>
        /// md5加密-32位加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Md5Encrypt32(string str)
        {
            return Md5Encrypt(str, EncryptType.Md5Code32);
        }
        /// <summary>
        /// 生成随机密码
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetRandomStr(int length)
        {
            string str="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";
            Random random = new Random();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            { 
                var temp=str[random.Next(str.Length)];
                sb.Append(temp);            
            }
            return sb.ToString();  
        }
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp()
        {
            DateTime dt = DateTime.Now;
            TimeSpan ts = dt.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetTimeStamp(DateTime dt)
        {            
            TimeSpan ts = dt.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

    }
    /// <summary>
    /// 加密编码类型
    /// </summary>
    public enum EncryptType
    {
        Md5Code16 = 16,
        Md5Code32 = 32
    }
}
