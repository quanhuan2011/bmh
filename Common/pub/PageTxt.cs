using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.pub
{
    public class PageTxt
    {
        /// <summary>
        /// 通用翻页控件
        /// </summary>
        /// <param name="strUrl"></param>
        /// <param name="iPage"></param>
        /// <param name="iTotal"></param>
        /// <param name="iPageSize"></param>
        /// <param name="iPageNo"></param>
        /// <returns></returns>
        public static string GetPageText(string strUrl, int iPage, int iTotal, int iPageSize, int iPageNo)
        {
            var sb = new StringBuilder();
            var pageCount = Convert.ToInt32(System.Math.Ceiling(Convert.ToDouble(iTotal) / Convert.ToDouble(iPageSize)));
            if (iPageNo <= 1)
            {
                sb.Append("<span class=\"disabled\"> 首页 </span>");
            }
            else
            {
                sb.AppendFormat("<a href=\"{0}\"> 首页 </a> ", GetMatchUrl(strUrl,"page","1"));
            }
            //上一页
            if (iPageNo <= 1)
            {
                sb.Append("<span class=\"disabled\"> 上一页 </span>");
            }
            else
            {
                if (iPageNo == 2)
                {
                    sb.AppendFormat("<a href=\"{0}\"> 上一页 </a>", GetMatchUrl(strUrl, "page", "1"));
                }
                else
                {
                    sb.AppendFormat("<a href=\"{0}\"> 上一页 </a>", GetMatchUrl(strUrl, "page", (iPageNo - 1).ToString()));
                }

            }
            //中间队列
            if (pageCount <= iPage)
            {
                for (int i = 1; i <= pageCount; i++)
                {
                    if (i == iPageNo)
                    {
                        sb.AppendFormat("<span class=\"current\">{0}</span>", i);
                    }
                    else
                    {
                        if (i == 1)
                        {
                            sb.AppendFormat("<a href=\"{0}\">1</a>", GetMatchUrl(strUrl, "page", "1"));
                        }
                        else
                        {
                            sb.AppendFormat("<a href=\"{0}\">{1}</a>", GetMatchUrl(strUrl, "page", i.ToString()),i);
                        }
                    }
                }
            }
            else
            {
                var iEndPage = 0;
                if (iPageNo <= iPage / 2)
                {
                    iEndPage = (iPageNo + iPage) > pageCount ? pageCount : (iPageNo + iPage);
                    for (var i = 1; i <= iEndPage; i++)
                    {
                        if (i == iPageNo)
                        {
                            sb.AppendFormat("<span class=\"current\">{0}</span>", i);
                        }
                        else
                        {
                            sb.AppendFormat("<a href=\"{0}\">{1}</a>", GetMatchUrl(strUrl, "page", i.ToString()),i);
                        }
                    }
                }
                else
                {
                    iEndPage = (iPageNo - (iPage / 2) + iPage) > pageCount ? pageCount : (iPageNo - (iPage / 2) + iPage);
                    for (int i = iEndPage - iPage; i <= iEndPage; i++)
                    {
                        if (i == iPageNo)
                        {
                            sb.AppendFormat("<span class=\"current\">{0}</span>", i.ToString());
                        }
                        else
                        {
                            sb.AppendFormat("<a href=\"{0}\">{1}</a>", GetMatchUrl(strUrl, "page", i.ToString()),i);
                        }
                    }
                }
            }

            //下一页
            if (iPageNo >= pageCount)
            {
                sb.Append("<span class=\"disabled\"> 下一页 </span>");
            }
            else
            {
                sb.AppendFormat("<a href=\"{0}\"> 下一页 </a>", GetMatchUrl(strUrl, "page", (iPageNo + 1).ToString()), iPageNo + 1);
            }
            //尾页
            if (iPageNo >= pageCount)
            {
                sb.Append("<span class=\"disabled\"> 尾页 </span>");
            }
            else
            {
                sb.AppendFormat("<a href=\"{0}\"> 尾页 </a> ", GetMatchUrl(strUrl, "page", pageCount.ToString()), pageCount);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 匹配地址
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
        /// <summary>
        /// 通用翻页控件
        /// </summary>
        /// <param name="strUrl"></param>
        /// <param name="iPage"></param>
        /// <param name="iTotal"></param>
        /// <param name="iPageSize"></param>
        /// <param name="iPageNo"></param>
        /// <returns></returns>
        public static string GetProPageText(string strUrl, int iPage, int iTotal, int iPageSize, int iPageNo)
        {
            var sb = new StringBuilder();
            var pageCount = Convert.ToInt32(System.Math.Ceiling(Convert.ToDouble(iTotal) / Convert.ToDouble(iPageSize)));
            if (iPageNo <= 1)
            {
                sb.Append("<span class=\"disabled\"> 首页 </span>");
            }
            else
            {
                sb.AppendFormat("<a href=\"{0}\"> 首页 </a> ", strUrl);
            }
            //上一页
            if (iPageNo <= 1)
            {
                sb.Append("<span class=\"disabled\"> 上一页 </span>");
            }
            else
            {
                if (iPageNo == 2)
                {
                    sb.AppendFormat("<a href=\"{0}\"> 上一页 </a>", strUrl);
                }
                else
                {
                    sb.AppendFormat("<a href=\"{0}p{1}/\"> 上一页 </a>", strUrl, iPageNo - 1);
                }

            }
            //中间队列
            if (pageCount <= iPage)
            {
                for (int i = 1; i <= pageCount; i++)
                {
                    if (i == iPageNo)
                    {
                        sb.AppendFormat("<span class=\"current\">{0}</span>", i);
                    }
                    else
                    {
                        if (i == 1)
                        {
                            sb.AppendFormat("<a href=\"{0}\">1</a>", strUrl);
                        }
                        else
                        {
                            sb.AppendFormat("<a href=\"{0}p{1}/\">{1}</a>", strUrl, i);
                        }
                    }
                }
            }
            else
            {
                var iEndPage = 0;
                if (iPageNo <= iPage / 2)
                {
                    iEndPage = (iPageNo + iPage) > pageCount ? pageCount : (iPageNo + iPage);
                    for (var i = 1; i <= iEndPage; i++)
                    {
                        if (i == iPageNo)
                        {
                            sb.AppendFormat("<span class=\"current\">{0}</span>", i);
                        }
                        else
                        {
                            sb.AppendFormat("<a href=\"{0}p{1}/\">{1}</a>", strUrl, i);
                        }
                    }
                }
                else
                {
                    iEndPage = (iPageNo - (iPage / 2) + iPage) > pageCount ? pageCount : (iPageNo - (iPage / 2) + iPage);
                    for (int i = iEndPage - iPage; i <= iEndPage; i++)
                    {
                        if (i == iPageNo)
                        {
                            sb.AppendFormat("<span class=\"current\">{0}</span>", i);
                        }
                        else
                        {
                            sb.AppendFormat("<a href=\"{0}p{1}/\">{1}</a>", strUrl, i);
                        }
                    }
                }
            }

            //下一页
            if (iPageNo >= pageCount)
            {
                sb.Append("<span class=\"disabled\"> 下一页 </span>");
            }
            else
            {
                sb.AppendFormat("<a href=\"{0}p{1}/\"> 下一页 </a>", strUrl, iPageNo + 1);
            }
            //尾页
            if (iPageNo >= pageCount)
            {
                sb.Append("<span class=\"disabled\"> 尾页 </span>");
            }
            else
            {
                sb.AppendFormat("<a href=\"{0}p{1}/\"> 尾页 </a> ", strUrl, pageCount);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 产品站内搜索使用
        /// </summary>
        /// <param name="strUrl"></param>
        /// <param name="iPage"></param>
        /// <param name="iTotal"></param>
        /// <param name="iPageSize"></param>
        /// <param name="iPageNo"></param>
        /// <returns></returns>
        public static string GetProSearchPageText(string strUrl, int iPage, int iTotal, int iPageSize, int iPageNo)
        {
            var sb = new StringBuilder();
            var pageCount = Convert.ToInt32(System.Math.Ceiling(Convert.ToDouble(iTotal) / Convert.ToDouble(iPageSize)));
            if (iPageNo <= 1)
            {
                sb.Append("<span class=\"disabled\"> 首页 </span>");
            }
            else
            {
                sb.AppendFormat("<a href=\"{0}\"> 首页 </a> ", strUrl);
            }
            //上一页
            if (iPageNo <= 1)
            {
                sb.Append("<span class=\"disabled\"> 上一页 </span>");
            }
            else
            {
                if (iPageNo == 2)
                {
                    sb.AppendFormat("<a href=\"{0}\"> 上一页 </a>", strUrl);
                }
                else
                {
                    sb.AppendFormat("<a href=\"{0}&page={1}/\"> 上一页 </a>", strUrl, (iPageNo - 1));
                }

            }
            //中间队列
            if (pageCount <= iPage)
            {
                for (int i = 1; i <= pageCount; i++)
                {
                    if (i == iPageNo)
                    {
                        sb.AppendFormat("<span class=\"current\">{0}</span>", i);
                    }
                    else
                    {
                        if (i == 1)
                        {
                            sb.AppendFormat("<a href=\"{0}\">1</a>", strUrl);
                        }
                        else
                        {
                            sb.AppendFormat("<a href=\"{0}&page={1}\">{1}</a>", strUrl, i);
                        }
                    }
                }
            }
            else
            {
                var iEndPage = 0;
                if (iPageNo <= iPage / 2)
                {
                    iEndPage = (iPageNo + iPage) > pageCount ? pageCount : (iPageNo + iPage);
                    for (var i = 1; i <= iEndPage; i++)
                    {
                        if (i == iPageNo)
                        {
                            sb.AppendFormat("<span class=\"current\">{0}</span>", i);
                        }
                        else
                        {
                            sb.AppendFormat("<a href=\"{0}&page={1}\">{1}</a>", strUrl, i);
                        }
                    }
                }
                else
                {
                    iEndPage = (iPageNo - (iPage / 2) + iPage) > pageCount ? pageCount : (iPageNo - (iPage / 2) + iPage);
                    for (int i = iEndPage - iPage; i <= iEndPage; i++)
                    {
                        if (i == iPageNo)
                        {
                            sb.AppendFormat("<span class=\"current\">{0}</span>", i.ToString());
                        }
                        else
                        {
                            sb.AppendFormat("<a href=\"{0}&page={1}\">{1}</a>", strUrl, i);
                        }
                    }
                }
            }

            //下一页
            if (iPageNo >= pageCount)
            {
                sb.Append("<span class=\"disabled\"> 下一页 </span>");
            }
            else
            {
                sb.AppendFormat("<a href=\"{0}&page={1}\"> 下一页 </a>", strUrl, iPageNo + 1);
            }
            //尾页
            if (iPageNo >= pageCount)
            {
                sb.Append("<span class=\"disabled\"> 尾页 </span>");
            }
            else
            {
                sb.AppendFormat("<a href=\"{0}&page={1}\"> 尾页 </a> ", strUrl, pageCount);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 通用翻页控件--Ajax--只有上一页 下一页
        /// </summary>
        /// <param name="prevFn"></param>
        /// <param name="nextFn"></param>
        /// <param name="iPage"></param>
        /// <param name="iTotal"></param>
        /// <param name="iPageSize"></param>
        /// <param name="iPageNo"></param>
        /// <returns></returns>
        public static string GetPageTextByAjax(string prevFn, string nextFn, int iTotal, int iPageSize, int iPageNo)
        {
            var sb = new StringBuilder();
            var pageCount = Convert.ToInt32(System.Math.Ceiling(Convert.ToDouble(iTotal) / Convert.ToDouble(iPageSize)));
            //<a href='javascript:' onclick='GetPrevPage(1)'  class='disabled'>上一页</a><span><font>1</font>/10</span><a href='javascript:' onclick='GetNextPage(1)'>下一页</a>
            //上一页
            if (iPageNo <= 1)
            {
                sb.Append("<a href='javascript:' class='disabled'>上一页</a>");
            }
            else
            {
                if (iPageNo == 2)
                {
                    sb.AppendFormat("<a href='javascript:' onclick='{0}(1)'>上一页</a>", prevFn);
                }
                else
                {
                    sb.AppendFormat("<a href='javascript:' onclick='{0}({1})'>上一页</a>", prevFn, iPageNo - 1);
                }

            }
            sb.AppendFormat("<span><font>{0}</font>/{1}</span>", iPageNo, pageCount);

            //下一页
            if (iPageNo >= pageCount)
            {
                sb.Append("<a href='javascript:' class='disabled'>下一页</a>");
            }
            else
            {
                sb.AppendFormat("<a href='javascript:' onclick='{0}({1})'>下一页</a>", nextFn, iPageNo + 1);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 通用翻页控件,手机站使用
        /// </summary>
        /// <param name="strUrl"></param>
        /// <param name="iTotal"></param>
        /// <param name="iPageSize"></param>
        /// <param name="iPageNo"></param>
        /// <returns></returns>
        public static string GetPageTextM(string strUrl, int iTotal, int iPageSize, int iPageNo)
        {
            //<p class="first"><a href="javascript:">首页</a></p>
            //<p class="prev"><a href="javascript:">上一页</a></p>
            //<p class="middle"><span><b>1</b>/139</span></p>
            //<p class="next"><a href="javascript:">下一页</a></p>
            //<p class="last"><a href="javascript:">末页</a></p>

            var sb = new StringBuilder();
            var pageCount = Convert.ToInt32(System.Math.Ceiling(Convert.ToDouble(iTotal) / Convert.ToDouble(iPageSize)));
            if (iPageNo <= 1)
            {
                sb.Append("<p class=\"first\"><a href=\"javascript:\">首页</a></p>");
            }
            else
            {
                sb.AppendFormat("<p class=\"first\"><a href=\"{0}\"> 首页 </a></p>", strUrl);
            }
            //上一页
            if (iPageNo <= 1)
            {
                sb.Append("<p class=\"prev\"><a href=\"javascript:\">上一页</a></p>");
            }
            else
            {
                if (iPageNo == 2)
                {
                    sb.AppendFormat("<p class=\"prev\"><a href=\"{0}\"> 上一页 </a></p>", strUrl);
                }
                else
                {
                    sb.AppendFormat("<p class=\"prev\"><a href=\"{0}p{1}\"> 上一页 </a></p>", strUrl, (iPageNo - 1));
                }

            }
            sb.AppendFormat("<p class=\"middle\"><span><b>{0}</b>/{1}</span></p>", iPageNo, pageCount);

            //下一页
            if (iPageNo >= pageCount)
            {
                sb.Append("<p class=\"next\"><a href=\"javascript:\">下一页</a></p>");
            }
            else
            {
                sb.AppendFormat("<p class=\"next\"><a href=\"{0}p{1}\"> 下一页 </a></p>", strUrl, iPageNo + 1);
            }
            //尾页
            if (iPageNo >= pageCount)
            {
                sb.Append("<p class=\"last\"><a href=\"javascript:\">尾页</a></span></p>");
            }
            else
            {
                sb.AppendFormat("<p class=\"last\"><a href=\"{0}p{1}\"> 尾页</a></p>", strUrl, pageCount);
            }
            return sb.ToString();
        }
    }
}
