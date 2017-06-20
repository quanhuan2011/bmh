using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.pub;
using DAL.pub;
using System.Data;
using Model;
using Common.pub;

namespace BLL.report
{
    public class MaterialReport
    {
        /// <summary>
        /// 获取广告表中显示量、点击量、收入数据
        /// </summary>
        /// <param name="AdLocationgId"></param>
        /// <returns></returns>
        public string GetMaterialSum(string materialId, string startTime, string endTime)
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
                DataTable dt = report.GetMaterialSumDT(materialId, startTime, endTime);
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
        /// 获取物料列表
       /// </summary>
       /// <param name="materialId"></param>
       /// <param name="startTime">开始时间</param>
       /// <param name="endTime">结束时间</param>
       /// <param name="dimensionType">维度</param>
       /// <param name="showList">逗号分隔</param>
       /// <returns></returns>
        public string GetMaterialList(string materialId, string startTime, string endTime, string dimensionType)
        {
            #region 定义数据
            ConvertData cData = new ConvertData();
            List<MaterialList> listMaterialList = new List<MaterialList>();
            #endregion

            #region 组合数据
            try
            {
                switch (dimensionType)
                {
                    case "day":
                        listMaterialList = GetMaterialListByDay(materialId, startTime, endTime);
                        break;
                    case "hour":
                        listMaterialList = GetMaterialListByHour(materialId, startTime, endTime);
                        break;

                    case "class":
                        listMaterialList = GetMaterialListByClass(materialId, startTime, endTime);
                        break;
                    default: listMaterialList = null; break;

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
            return Result.GetResult(Result.errCode, Result.errMsg, listMaterialList);
            #endregion

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="materialId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<MaterialList> GetMaterialListByDay(string materialId, string startTime, string endTime)
        {
            Report report = new Report();
            DataTable dt= report.GetMaterialListByDayDT(materialId, startTime, endTime);
            List<MaterialList> listMaterialList = new List<MaterialList>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    MaterialListByDay materialListItem = new MaterialListByDay();
                    materialListItem.date = dr["dateid"].ToString();
                    materialListItem.showcnt = dr["showcnt"].ToString();
                    materialListItem.clickcnt = dr["clickcnt"].ToString();
                    materialListItem.income = float.Parse(dr["incomesum"].ToString()).ToString();
                    materialListItem.ecpm = dr["showcnt"].ToString() == "0" ? "0" : ((float.Parse(dr["incomesum"].ToString()) / (int.Parse(dr["showcnt"].ToString()))) * 1000).ToString();
                    materialListItem.cpc = dr["clickcnt"].ToString() == "0" ? "0" : (float.Parse(dr["incomesum"].ToString()) / (int.Parse(dr["clickcnt"].ToString()))).ToString();
                    listMaterialList.Add(materialListItem);
                }
            }
            return listMaterialList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="materialId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<MaterialList> GetMaterialListByHour(string materialId, string startTime, string endTime)
        {
            Report report = new Report();
            DataTable dt = report.GetMaterialListByHourDT(materialId, startTime, endTime);
            List<MaterialList> listMaterialList = new List<MaterialList>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    MaterialListByHour materialListItem = new MaterialListByHour();
                    materialListItem.hour = dr["hourid"].ToString();
                    materialListItem.showcnt = dr["showcnt"].ToString();
                    materialListItem.clickcnt = dr["clickcnt"].ToString();
                    materialListItem.income = float.Parse(dr["incomesum"].ToString()).ToString();
                    materialListItem.ecpm = dr["showcnt"].ToString() == "0" ? "0" : ((float.Parse(dr["incomesum"].ToString()) / (int.Parse(dr["showcnt"].ToString()))) * 1000).ToString();
                    materialListItem.cpc = dr["clickcnt"].ToString() == "0" ? "0" : (float.Parse(dr["incomesum"].ToString()) / (int.Parse(dr["clickcnt"].ToString()))).ToString();
                    listMaterialList.Add(materialListItem);
                }
            }
            return listMaterialList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="materialId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<MaterialList> GetMaterialListByClass(string materialId, string startTime, string endTime)
        {
            Report report = new Report();
            DataTable dt = report.GetMaterialListByClassDT(materialId, startTime, endTime);
            List<MaterialList> listMaterialList = new List<MaterialList>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    MaterialListByClass materialListItem = new MaterialListByClass();
                    materialListItem.classname = dr["classname"].ToString();
                    materialListItem.showcnt = dr["showcnt"].ToString();
                    materialListItem.clickcnt = dr["clickcnt"].ToString();
                    materialListItem.income = float.Parse(dr["incomesum"].ToString()).ToString();
                    materialListItem.ecpm = dr["showcnt"].ToString() == "0" ? "0" : ((float.Parse(dr["incomesum"].ToString()) / (int.Parse(dr["showcnt"].ToString()))) * 1000).ToString();
                    materialListItem.cpc = dr["clickcnt"].ToString() == "0" ? "0" : (float.Parse(dr["incomesum"].ToString()) / (int.Parse(dr["clickcnt"].ToString()))).ToString();
                    listMaterialList.Add(materialListItem);
                }
            }
            return listMaterialList;
        }
        /// <summary>
        /// 获取物料名称
        /// </summary>
        /// <param name="adLocationId"></param>
        /// <returns></returns>
        public string GetMaterialName(string materialId)
        {
            string tempName = "";
            Report report = new Report();
            DataTable dt = report.GetMaterialDT(materialId);
            if (dt != null&&dt.Rows.Count>0)
            {
                tempName=dt.Rows[0]["name"].ToString();
            }
            return tempName;
        }
    }
}
