using System;
using System.Collections.Generic;
using System.Web;

namespace BMH.EagleEye.BaseClass
{
    public class POMOHOClass
    {
        public POMOHOClass()
        {

        }

        #region-----------弹出消息框（带重载）--------------------------------------------
        /// <summary>
        /// 弹出消息框
        /// </summary>
        /// <param name="StrMessage">弹出的信息</param>
        /// <param name="MessageType">信息类型：1－确定后退；2－确定指向页面；3－确定跳出父级框架；4-关闭当前窗口</param>
        /// <param name="GoToUTL">当MessageType＝2或3时要跳的地址</param>
        /// <param name="StopNext">是否要停止执行下面和程序True False</param>
        /// <returns></returns>
        public static void ShowMessage(string StrMessage, int MessageType, string GoToUTL, bool StopNext)
        {
            string TempMsg = "<script language='javascript'>alert('" + StrMessage + "');";
            switch (MessageType)
            {
                case 1:
                    TempMsg += "history.back();";
                    break;
                case 2:
                    TempMsg += "this.location.href='" + GoToUTL + "';";
                    break;
                case 3:
                    TempMsg += "window.parent.location='" + GoToUTL + "';";
                    break;
                case 4:
                    TempMsg += "window.close();";
                    break;
            }
            TempMsg += "</script>";

            System.Web.HttpContext.Current.Response.Write(TempMsg);
            if (StopNext)
            {
                System.Web.HttpContext.Current.Response.End();
            }
        }
        /// <summary>
        /// （重载）弹出对话弹，停止程序往下执行，确定后退
        /// </summary>
        /// <param name="StrMesseage">要弹出的信息提示</param>
        public static void ShowMessage(string StrMesseage)
        {
            ShowMessage(StrMesseage, 1, "", true);
        }
        /// <summary>
        /// （重载）弹出对话弹，停止程序往下执行，确定跳到指定页面
        /// </summary>
        /// <param name="StrMessage">要弹出的信息提示</param>
        /// <param name="Url">要转跳的地址</param>
        public static void ShowMessage(string StrMessage, string Url)
        {
            ShowMessage(StrMessage, 2, Url, true);
        }
        /// <summary>
        /// （重载）弹出对话弹，停止程序往下执行，确定跳到出框架
        /// </summary>
        /// <param name="StrMessage">要弹出的信息提示</param>
        /// <param name="ToParent">不管是真假都跳出框架</param>
        public static void ShowMessage(string StrMessage, bool ToParent)
        {
            ShowMessage(StrMessage, 3, "", true);
        }
        /// <summary>
        /// （重载）弹出对话弹，确定关闭窗口,关停止向下执行代码
        /// </summary>
        /// <param name="StrMessage">信息</param>
        /// <param name="MessageType">不管是多少数字都关闭窗口</param>
        public static void ShowMessage(string StrMessage, int MessageType)
        {
            ShowMessage(StrMessage, 4, "", true);
        }
        /// <summary>
        /// （重载）弹出对话弹，可选是否停止程序往下执行，确定跳到指定页面
        /// </summary>
        /// <param name="StrMessage">要弹出的信息提示</param>
        /// <param name="Url">要转跳的地址</param>
        /// <param name="StopNext">是否停止执行：true false</param>
        public static void ShowMessage(string StrMessage, string Url, bool StopNext)
        {
            ShowMessage(StrMessage, 2, Url, StopNext);
        }

        #endregion-----------------------------------------------------------------------------
    }
}