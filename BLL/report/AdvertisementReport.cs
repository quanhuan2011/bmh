using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.pub;
using System.Data;
using DAL.pub;
using Model;
using Common.pub;

namespace BLL.report
{
    /// <summary>
    /// 广告相关数据
    /// </summary>
    public class AdvertisementReport
    {
        /// <summary>
        /// 获取当前广告位中广告总量数据，包括显示量、点击量、收入数据
        /// </summary>
        /// <param name="adLocationId">广告位id</param>
        /// <returns></returns>
        public string GetAdSum(string adId, string startTime, string endTime)
        {

            #region 定义数据
            Report report = new Report();
            int clickSum = 0;
            int showSum = 0;
            float incomeSum = 0.00f;
            #endregion

            #region 获取数据
            try
            {
                DataTable dt = report.GetAdSumDT(adId, startTime, endTime);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        showSum = int.Parse(dt.Rows[0]["showcnt"].ToString());
                        clickSum = int.Parse(dt.Rows[0]["clickcnt"].ToString());
                        incomeSum = float.Parse(dt.Rows[0]["incomesum"].ToString());
                    }
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

            #region 转换数据
            var temp = new { clicksum = clickSum, showsum = showSum, incomesum = incomeSum };
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
        public string GetAdList(string adId, string startTime, string endTime, string dimensionType)
        {
            #region 定义数据
            ConvertData cData = new ConvertData();
            List<AdList> listAdList = new List<AdList>();
            #endregion

            #region 组合数据
            try
            {
                switch (dimensionType)
                {
                    case "day":
                        listAdList = GetAdListByDay(adId, startTime, endTime);
                        break;
                    case "area":
                        listAdList = GetAdListByArea(adId, startTime, endTime);
                        break;
                    case "hour":
                        listAdList = GetAdListByHour(adId, startTime, endTime);
                        break;
                    case "class":
                        listAdList = GetAdListByClass(adId, startTime, endTime);
                        break;
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
        public List<AdList> GetAdListByDay(string adId, string startTime, string endTime)
        {
            Report report = new Report();
            DataTable dt=report.GetAdListByDayDT(adId, startTime, endTime);
            List<AdList> listAdList = new List<AdList>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AdListByDay adListItem = new AdListByDay();
                    adListItem.date = dr["dateid"].ToString();
                    adListItem.showcnt = dr["showcnt"].ToString();
                    adListItem.clickcnt = dr["clickcnt"].ToString();
                    adListItem.income = float.Parse(dr["incomesum"].ToString()).ToString();
                    adListItem.ecpm = dr["showcnt"].ToString() == "0" ? "0" : ((float.Parse(dr["incomesum"].ToString()) / (int.Parse(dr["showcnt"].ToString()))) * 1000).ToString();
                    adListItem.cpc = dr["clickcnt"].ToString() == "0" ? "0" : (float.Parse(dr["incomesum"].ToString()) / (int.Parse(dr["clickcnt"].ToString()))).ToString();
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
        public List<AdList> GetAdListByHour(string adId, string startTime, string endTime)
        {
            Report report = new Report();
            DataTable dt = report.GetAdListByHourDT(adId, startTime, endTime);
            List<AdList> listAdList = new List<AdList>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AdListByHour adListItem = new AdListByHour();
                    adListItem.hour = dr["hourid"].ToString();
                    adListItem.showcnt = dr["showcnt"].ToString();
                    adListItem.clickcnt = dr["clickcnt"].ToString();
                    adListItem.income = float.Parse(dr["incomesum"].ToString()).ToString();
                    adListItem.ecpm = dr["showcnt"].ToString() == "0" ? "0" : ((float.Parse(dr["incomesum"].ToString()) / (int.Parse(dr["showcnt"].ToString()))) * 1000).ToString();
                    adListItem.cpc = dr["clickcnt"].ToString() == "0" ? "0" : (float.Parse(dr["incomesum"].ToString()) / (int.Parse(dr["clickcnt"].ToString()))).ToString();
                    listAdList.Add(adListItem);
                }
            }
            return listAdList;
        }
        /// <summary>
        /// 获取广告明细数据返回List-按分类
        /// </summary>
        /// <param name="adId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<AdList> GetAdListByClass(string adId, string startTime, string endTime)
        {
            Report report = new Report();
            DataTable dt = report.GetAdListByClassDT(adId, startTime, endTime);
            List<AdList> listAdList = new List<AdList>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AdListByClass adListItem = new AdListByClass();
                    adListItem.classname = dr["classname"].ToString();
                    adListItem.showcnt = dr["showcnt"].ToString();
                    adListItem.clickcnt = dr["clickcnt"].ToString();
                    adListItem.income = float.Parse(dr["incomesum"].ToString()).ToString();
                    adListItem.ecpm = dr["showcnt"].ToString() == "0" ? "0" : ((float.Parse(dr["incomesum"].ToString()) / (int.Parse(dr["showcnt"].ToString()))) * 1000).ToString();
                    adListItem.cpc = dr["clickcnt"].ToString() == "0" ? "0" : (float.Parse(dr["incomesum"].ToString()) / (int.Parse(dr["clickcnt"].ToString()))).ToString();
                    listAdList.Add(adListItem);
                }
            }
            return listAdList;
        }
        /// <summary>
        /// 获取广告明细数据返回List-按地域
        /// </summary>
        /// <param name="adId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<AdList> GetAdListByArea(string adId, string startTime, string endTime)
        {
            Report report = new Report();
            DataTable dt = report.GetAdListByAreaDT(adId, startTime, endTime);
            List<AdList> listAdList = new List<AdList>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AdListByArea adListItem = new AdListByArea();
                    adListItem.area = dr["areaname"].ToString();
                    adListItem.showcnt = dr["showcnt"].ToString();
                    adListItem.clickcnt = dr["clickcnt"].ToString();
                    adListItem.income = float.Parse(dr["incomesum"].ToString()).ToString();
                    adListItem.ecpm = dr["showcnt"].ToString() == "0" ? "0" : ((float.Parse(dr["incomesum"].ToString()) / (int.Parse(dr["showcnt"].ToString()))) * 1000).ToString();
                    adListItem.cpc = dr["clickcnt"].ToString() == "0" ? "0" : (float.Parse(dr["incomesum"].ToString()) / (int.Parse(dr["clickcnt"].ToString()))).ToString();
                    listAdList.Add(adListItem);
                }
            }
            return listAdList;
        }
        /// <summary>
        /// 获取广告名称
        /// </summary>
        /// <param name="adId"></param>
        /// <returns></returns>
        public string GetAdName(string adId)
        {
            string tempName = "";
            Report report = new Report();
            DataTable dt = report.GetAdDT(adId);
            if (dt != null && dt.Rows.Count > 0)
            {
                tempName = dt.Rows[0]["name"].ToString();
            }
            return tempName;
        }

        public DataTable GetAdInfoDT(string adId)
        {

            Report report = new Report();
            return report.GetAdDT(adId);
        }

    }
}
