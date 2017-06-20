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
    public class AdLocationReport
    {        
        /// <summary>
        /// 获取当前广告位中广告位总量数据，包括显示量、点击量、收入数据
        /// </summary>
        /// <param name="adLocationId">广告位id</param>
        /// <returns></returns>
        public string GetAdLocationSum(string adLocationId, string startTime, string endTime)
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
                DataTable dt = report.GetAdLocationSumDT(adLocationId, startTime, endTime);
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
        /// 获取当前广告位中广告位详细数据
        /// </summary>
        /// <param name="adLocationId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="dimensionType"></param>
        /// <param name="showList"></param>
        /// <returns></returns>
        public string GetAdLocationList(string adLocationId, string startTime, string endTime, string dimensionType)
        {
            #region 定义数据
            ConvertData cData = new ConvertData();
            List<AdLocationList> listAdLocationList = new List<AdLocationList>();
            #endregion

            #region 组合数据
            try
            {
                switch (dimensionType)
                {
                    case "day":
                        listAdLocationList = GetAdLocationListByDay(adLocationId, startTime, endTime);
                        break;
                    case "area":
                        listAdLocationList = GetAdLocationListByArea(adLocationId, startTime, endTime);
                        break;

                    case "hour":
                        listAdLocationList = GetAdLocationListByHour(adLocationId, startTime, endTime);
                        break;

                    case "class":
                        listAdLocationList = GetAdLocationListByClass(adLocationId, startTime, endTime);
                        break;
                    default: listAdLocationList = null; break;

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
            return Result.GetResult(Result.errCode, Result.errMsg, listAdLocationList);
            #endregion

        }
        /// <summary>
        /// 获取广告位明细数据返回List-按日期
        /// </summary>
        /// <param name="adLocationId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<AdLocationList> GetAdLocationListByDay(string adLocationId, string startTime, string endTime)
        {
            Report report = new Report();
            DataTable dt= report.GetAdLocationListByDayDT(adLocationId, startTime, endTime);
            List<AdLocationList> listAdLocationList = new List<AdLocationList>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AdLocationListByDay listAdLocationListItem = new AdLocationListByDay();
                    listAdLocationListItem.date = dr["dateid"].ToString();
                    listAdLocationListItem.showcnt = dr["showcnt"].ToString();
                    listAdLocationListItem.requestcnt = dr["requestcnt"].ToString();
                    listAdLocationListItem.clickcnt = dr["clickcnt"].ToString();
                    listAdLocationListItem.income = float.Parse(dr["incomesum"].ToString()).ToString();
                    listAdLocationListItem.ecpm = dr["showcnt"].ToString() == "0" ? "0" : ((float.Parse(dr["incomesum"].ToString()) / (int.Parse(dr["showcnt"].ToString()))) * 1000).ToString();
                    listAdLocationListItem.cpc = dr["clickcnt"].ToString() == "0" ? "0" : (float.Parse(dr["incomesum"].ToString()) / (int.Parse(dr["clickcnt"].ToString()))).ToString();
                    listAdLocationList.Add(listAdLocationListItem);
                }
            }
            return listAdLocationList;
        }
        /// <summary>
        /// 获取广告位明细数据返回List-按时段
        /// </summary>
        /// <param name="adLocationId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<AdLocationList> GetAdLocationListByHour(string adLocationId, string startTime, string endTime)
        {
            Report report = new Report();
            DataTable dt = report.GetAdLocationListByHourDT(adLocationId, startTime, endTime);
            List<AdLocationList> listAdLocationList = new List<AdLocationList>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AdLocationListByHour listAdLocationListItem = new AdLocationListByHour();
                    listAdLocationListItem.hour = dr["hourid"].ToString();
                    listAdLocationListItem.showcnt = dr["showcnt"].ToString();
                    listAdLocationListItem.requestcnt = dr["requestcnt"].ToString();
                    listAdLocationListItem.clickcnt = dr["clickcnt"].ToString();
                    listAdLocationListItem.income = float.Parse(dr["incomesum"].ToString()).ToString();
                    listAdLocationListItem.ecpm = dr["showcnt"].ToString() == "0" ? "0" : ((float.Parse(dr["incomesum"].ToString()) / (int.Parse(dr["showcnt"].ToString()))) * 1000).ToString();
                    listAdLocationListItem.cpc = dr["clickcnt"].ToString() == "0" ? "0" : (float.Parse(dr["incomesum"].ToString()) / (int.Parse(dr["clickcnt"].ToString()))).ToString();
                    listAdLocationList.Add(listAdLocationListItem);
                }
            }
            return listAdLocationList;
        }
        /// <summary>
        /// 获取广告位明细数据返回List-按分类
        /// </summary>
        /// <param name="adLocationId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<AdLocationList> GetAdLocationListByClass(string adLocationId, string startTime, string endTime)
        {
            Report report = new Report();
            DataTable dt = report.GetAdLocationListByClassDT(adLocationId, startTime, endTime);
            List<AdLocationList> listAdLocationList = new List<AdLocationList>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AdLocationListByClass listAdLocationListItem = new AdLocationListByClass();
                    listAdLocationListItem.classname = dr["classname"].ToString();
                    listAdLocationListItem.showcnt = dr["showcnt"].ToString();
                    listAdLocationListItem.requestcnt = dr["requestcnt"].ToString();
                    listAdLocationListItem.clickcnt = dr["clickcnt"].ToString();
                    listAdLocationListItem.income = float.Parse(dr["incomesum"].ToString()).ToString();
                    listAdLocationListItem.ecpm = dr["showcnt"].ToString() == "0" ? "0" : ((float.Parse(dr["incomesum"].ToString()) / (int.Parse(dr["showcnt"].ToString()))) * 1000).ToString();
                    listAdLocationListItem.cpc = dr["clickcnt"].ToString() == "0" ? "0" : (float.Parse(dr["incomesum"].ToString()) / (int.Parse(dr["clickcnt"].ToString()))).ToString();
                    listAdLocationList.Add(listAdLocationListItem);
                }
            }
            return listAdLocationList;
        }
        /// <summary>
        /// 获取广告位明细数据返回List-地域
        /// </summary>
        /// <param name="adLocationId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<AdLocationList> GetAdLocationListByArea(string adLocationId, string startTime, string endTime)
        {
            Report report = new Report();
            DataTable dt = report.GetAdLocationListByAreaDT(adLocationId, startTime, endTime);
            List<AdLocationList> listAdLocationList = new List<AdLocationList>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AdLocationListByArea listAdLocationListItem = new AdLocationListByArea();
                    listAdLocationListItem.area = dr["areaname"].ToString();
                    listAdLocationListItem.showcnt = dr["showcnt"].ToString();
                    listAdLocationListItem.requestcnt = dr["requestcnt"].ToString();
                    listAdLocationListItem.clickcnt = dr["clickcnt"].ToString();
                    listAdLocationListItem.income = float.Parse(dr["incomesum"].ToString()).ToString();
                    listAdLocationListItem.ecpm = dr["showcnt"].ToString() == "0" ? "0" : ((float.Parse(dr["incomesum"].ToString()) / (int.Parse(dr["showcnt"].ToString()))) * 1000).ToString();
                    listAdLocationListItem.cpc = dr["clickcnt"].ToString() == "0" ? "0" : (float.Parse(dr["incomesum"].ToString()) / (int.Parse(dr["clickcnt"].ToString()))).ToString();
                    listAdLocationList.Add(listAdLocationListItem);
                }
            }
            return listAdLocationList;
        }
        /// <summary>
        /// 获取广告位名称
        /// </summary>
        /// <param name="adLocationId"></param>
        /// <returns></returns>
        public string GetAdLocationName(string adLocationId)
        {   string tempName="";
            Report report = new Report();
            DataTable dt= report.GetAdLocationDT(adLocationId);
            if (dt != null && dt.Rows.Count > 0)
            {
                tempName = dt.Rows[0]["name"].ToString();
            }
            return tempName;
        }
     
    }
}
