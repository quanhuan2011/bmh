using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.pub;
using System.Data;
using BLL.pub;
using Common.pub;
using Model;

namespace BLL.report
{
   public class AdUserReport
    {
        /// <summary>
        /// 获取广告主报表中总数量数据
        /// </summary>
        /// <param name="aduserId"></param>
        /// <returns></returns>
        public string GetAdSumOfAdUser(string adUserId)
        {

            #region 定义数据
            Report report = new Report();
            float balanceSum = 0.00f;
            int clickSum = 0;
            float deductSum = 0.00f;
            #endregion

            #region 获取数据
            try
            {
                DataTable dt = report.GetBalanceOfAduserDT(adUserId);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        balanceSum = float.Parse(dt.Rows[0]["balance"].ToString());
                    }
                }
                DataTable dt1 = report.GetADSumOfAduserDT(adUserId);
                if (dt1 != null)
                {
                    if (dt1.Rows.Count > 0)
                    {
                        clickSum = int.Parse(dt1.Rows[0]["clickcnt"].ToString());
                        deductSum = float.Parse(dt1.Rows[0]["deductsum"].ToString());
                    }
                }
                Result.errCode = Result.successCode;
                Result.errMsg = "获取成功";
            }
            catch (Exception ex)
            {
                Result.errCode = Result.failCode;
                Result.errMsg = ex.Message;
                LogApi.DebugInfo(ex);
            }
            #endregion

            #region 转换数据
            var temp = new { balancesum = balanceSum, clicksum = clickSum, deductsum = deductSum };
            #endregion

            #region 返回数据
            return Result.GetResult(Result.errCode, Result.errMsg, temp);
            #endregion
        }
        /// <summary>
        /// 获取广告主报表中总数量数据-按时间
        /// </summary>
        /// <param name="adUserId"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public string GetAdSumOfAdUserByTime(string adUserId, string startTime, string endTime)
        {

            #region 定义数据
            Report report = new Report();
            int clickSum = 0;
            float deductSum = 0.00f;
            #endregion

            #region 获取数据
            try
            {
                DataTable dt1 = report.GetADSumOfAduserByTimeDT(adUserId, startTime, endTime);
                if (dt1 != null)
                {
                    if (dt1.Rows.Count > 0)
                    {
                        clickSum = int.Parse(dt1.Rows[0]["clickcnt"].ToString());
                        deductSum = float.Parse(dt1.Rows[0]["deductsum"].ToString());
                    }
                }
                Result.errCode = Result.successCode;
                Result.errMsg = "获取成功";
            }
            catch (Exception ex)
            {
                Result.errCode = Result.failCode;
                Result.errMsg = ex.Message;
                LogApi.DebugInfo(ex);
            }
            #endregion

            #region 转换数据
            var temp = new { clicksum = clickSum, deductsum = deductSum };
            #endregion

            #region 返回数据
            return Result.GetResult(Result.errCode, Result.errMsg, temp);
            #endregion
        }
        /// <summary>
        /// 获取当前广告位中广告详细数据
        /// </summary>
        /// <param name="adLocationId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="dimensionType"></param>
        /// <param name="showList"></param>
        /// <returns></returns>
        public string GetAdListOfAdUser(string adUserId, string startTime, string endTime, string dimensionType)
        {
            #region 定义数据
            ConvertData cData = new ConvertData();
            List<AdListOfAdUser> listAdList = new List<AdListOfAdUser>();
            #endregion

            #region 组合数据
            try
            {
                switch (dimensionType)
                {
                    case "day":
                        listAdList = GetAdListOfAdUserByDay(adUserId, startTime, endTime);
                        break;
                    //case "area":
                    //    listAdList = GetAdListOfAdUserByArea(adUserId, startTime, endTime);
                    //    break;

                    //case "hour":
                    //    listAdList = GetAdListOfAdUserByHour(adUserId, startTime, endTime);
                    //    break;

                    //case "class":
                    //    listAdList = GetAdListOfAdUserByClass(adUserId, startTime, endTime);
                    //    break;
                    default: listAdList = null; break;

                }
                Result.errCode = "0";
                Result.errMsg = "获取成功";
            }
            catch (Exception ex)
            {
                Result.errCode = "-1";
                Result.errMsg = ex.Message;
                LogApi.DebugInfo(ex);
            }
            #endregion

            #region 返回数据
            return Result.GetResult(Result.errCode, Result.errMsg, listAdList);
            #endregion
        }
        /// <summary>
        /// 获取广告明细数据返回List-按日期
        /// </summary>
        /// <param name="adId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<AdListOfAdUser> GetAdListOfAdUserByDay(string adUserId, string startTime, string endTime)
        {
            Report report = new Report();
            DataTable dt = report.GetAdListOfAdUserByDayDT(adUserId, startTime, endTime);
            List<AdListOfAdUser> listAdList = new List<AdListOfAdUser>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AdListOfAdUserByDay adListItem = new AdListOfAdUserByDay();
                    adListItem.date = dr["dateid"].ToString();
                  //  adListItem.showcnt = dr["showcnt"].ToString();
                    adListItem.clickcnt = dr["clickcnt"].ToString();
                    adListItem.deductsum = dr["deductsum"].ToString();
                   // adListItem.income = (int.Parse(dr["clickcnt"].ToString()) * 0.2).ToString();
                   // adListItem.ecpm = dr["showcnt"].ToString() == "0" ? "0" : (((int.Parse(dr["clickcnt"].ToString()) * 0.2) / (int.Parse(dr["showcnt"].ToString()))) * 1000).ToString();
                    //adListItem.cpc = dr["clickcnt"].ToString() == "0" ? "0" : ((int.Parse(dr["clickcnt"].ToString()) * 0.2) / (int.Parse(dr["clickcnt"].ToString()))).ToString();
                    listAdList.Add(adListItem);
                }
            }
            return listAdList;
        }
        /// <summary>
        /// 获取广告明细数据返回List-按时段
        /// </summary>
        /// <param name="adId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        //public List<AdListOfAdUser> GetAdListOfAdUserByHour(string adUserId, string startTime, string endTime)
        //{
        //    Report report = new Report();
        //    DataTable dt = report.GetAdListOfAdUserByHourDT(adUserId, startTime, endTime);
        //    List<AdListOfAdUser> listAdList = new List<AdListOfAdUser>();
        //    if (dt != null)
        //    {
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            AdListOfAdUserByHour adListItem = new AdListOfAdUserByHour();
        //            adListItem.hour = dr["hourid"].ToString();
        //            adListItem.showcnt = dr["showcnt"].ToString();
        //            adListItem.clickcnt = dr["clickcnt"].ToString();
        //          //  adListItem.income = (int.Parse(dr["clickcnt"].ToString()) * 0.2).ToString();
        //            adListItem.ecpm = dr["showcnt"].ToString() == "0" ? "0" : (((int.Parse(dr["clickcnt"].ToString()) * 0.2) / (int.Parse(dr["showcnt"].ToString()))) * 1000).ToString();
        //            adListItem.cpc = dr["clickcnt"].ToString() == "0" ? "0" : ((int.Parse(dr["clickcnt"].ToString()) * 0.2) / (int.Parse(dr["clickcnt"].ToString()))).ToString();
        //            listAdList.Add(adListItem);
        //        }
        //    }
        //    return listAdList;
        //}
        /// <summary>
        /// 获取广告明细数据返回List-按分类
        /// </summary>
        /// <param name="adId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        //public List<AdListOfAdUser> GetAdListOfAdUserByClass(string adUserId, string startTime, string endTime)
        //{
        //    Report report = new Report();
        //    DataTable dt = report.GetAdListOfAdUserByClassDT(adUserId, startTime, endTime);
        //    List<AdListOfAdUser> listAdList = new List<AdListOfAdUser>();
        //    if (dt != null)
        //    {
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            AdListOfAdUserByClass adListItem = new AdListOfAdUserByClass();
        //            adListItem.classname = dr["classname"].ToString();
        //            adListItem.showcnt = dr["showcnt"].ToString();
        //            adListItem.clickcnt = dr["clickcnt"].ToString();
        //           // adListItem.income = (int.Parse(dr["clickcnt"].ToString()) * 0.2).ToString();
        //            adListItem.ecpm = dr["showcnt"].ToString() == "0" ? "0" : (((int.Parse(dr["clickcnt"].ToString()) * 0.2) / (int.Parse(dr["showcnt"].ToString()))) * 1000).ToString();
        //            adListItem.cpc = dr["clickcnt"].ToString() == "0" ? "0" : ((int.Parse(dr["clickcnt"].ToString()) * 0.2) / (int.Parse(dr["clickcnt"].ToString()))).ToString();
        //            listAdList.Add(adListItem);
        //        }
        //    }
        //    return listAdList;
        //}
        /// <summary>
        /// 获取广告明细数据返回List-按地域
        /// </summary>
        /// <param name="adId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        //public List<AdListOfAdUser> GetAdListOfAdUserByArea(string adUserId, string startTime, string endTime)
        //{
        //    Report report = new Report();
        //    DataTable dt = report.GetAdListOfAdUserByAreaDT(adUserId, startTime, endTime);
        //    List<AdListOfAdUser> listAdList = new List<AdListOfAdUser>();
        //    if (dt != null)
        //    {
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            AdListOfAdUserByArea adListItem = new AdListOfAdUserByArea();
        //            adListItem.area = dr["areaname"].ToString();
        //            adListItem.showcnt = dr["showcnt"].ToString();
        //            adListItem.clickcnt = dr["clickcnt"].ToString();
        //           // adListItem.income = (int.Parse(dr["clickcnt"].ToString()) * 0.2).ToString();
        //            adListItem.ecpm = dr["showcnt"].ToString() == "0" ? "0" : (((int.Parse(dr["clickcnt"].ToString()) * 0.2) / (int.Parse(dr["showcnt"].ToString()))) * 1000).ToString();
        //            adListItem.cpc = dr["clickcnt"].ToString() == "0" ? "0" : ((int.Parse(dr["clickcnt"].ToString()) * 0.2) / (int.Parse(dr["clickcnt"].ToString()))).ToString();
        //            listAdList.Add(adListItem);
        //        }
        //    }
        //    return listAdList;
        //}
    }
}
