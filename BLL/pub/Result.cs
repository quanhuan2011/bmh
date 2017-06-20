using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace BLL.pub
{
   public static  class Result
    {

       public static string errCode { get; set; }
       public static string errMsg { get; set; }
       public const string failCode = "-1";
       public const string successCode = "1";
       /// <summary>
       /// 返回结果
       /// </summary>
       /// <param name="errcode"></param>
       /// <param name="errmsg"></param>
       /// <param name="data"></param>
       /// <returns></returns>
       public static string GetResultString(string errcode, string errmsg, string data)
       {
           return string.Format("{ errcode:\"{0}\",errmsg:\"{1}\",data:{2} }", errcode, errmsg, data);
       }

       /// <summary>
       /// 返回普通格式结果
       /// </summary>
       /// <param name="errcode">状态值</param>
       /// <param name="errmsg">错误提示</param>
       /// <param name="obj">数据</param>
       /// <returns></returns>
       public static string GetResult(string errcode, string errmsg, object obj)
       {
           var temp = new
           {
               errcode = errcode,
               errmsg = errmsg,
               data = obj
           };
           string tempstr = JsonConvert.SerializeObject(temp);
           return tempstr;           
       }
       /// <summary>
       /// 获取失败结果
       /// </summary>
       /// <param name="errmsg"></param>
       /// <returns></returns>
       public static string GetFailResult(string errmsg)
       {
           var temp = new
           {
               errcode = failCode,
               errmsg = errmsg
             
           };
           string tempstr = JsonConvert.SerializeObject(temp);
           return tempstr;
       }
       /// <summary>
       /// 获取成功结果
       /// </summary>
       /// <param name="errmsg"></param>
       /// <returns></returns>
       public static string GetSuccessResult(string errmsg)
       {
           var temp = new
           {
               errcode = successCode,
               errmsg = errmsg

           };
           string tempstr = JsonConvert.SerializeObject(temp);
           return tempstr;
       }
       /// <summary>
       /// 获取成功结果
       /// </summary>
       /// <param name="errmsg"></param>
       /// <param name="obj"></param>
       /// <returns></returns>
       public static string GetSuccessResult(string errmsg, object obj)
       {
           var temp = new
           {
               errcode = successCode,
               errmsg = errmsg,
               data = obj
           };
           string tempstr = JsonConvert.SerializeObject(temp);
           return tempstr;
       }


    }
}
