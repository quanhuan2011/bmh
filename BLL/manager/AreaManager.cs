using BLL.pub;
using Common.pub;
using DAL.database;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.manager
{
   public class AreaManager
    {
        ConvertData cData = new ConvertData();
        DBOperate dbOperate = new DBOperate();
        /// <summary>
        /// 获取省份列表
        /// </summary>
        /// <returns></returns>
        public string GetProvinceList()
        {
            List<ProvinceInfo> listAdLocation = new List<ProvinceInfo>();
            try
            {
                DataTable dt2 = GetProvinceDT();
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    listAdLocation = cData.FillModel<ProvinceInfo>(dt2);
                }
                Result.errCode = "1";
                Result.errMsg = "获取成功";
            }
            catch (Exception ex)
            {
                listAdLocation = null;
                Result.errCode = "-1";
                Result.errMsg = ex.Message;
                LogApi.DebugInfo(ex);
            }


            #region 返回数据
            return Result.GetResult(Result.errCode, Result.errMsg, listAdLocation);
            #endregion


        }
        public DataTable GetProvinceDT()
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format("select s1.cityid as provinceid,s1.cityname as provincename from bee.bee_cityinfo s1 where s1.citylevel=3 order by cityid asc ");
            #endregion           
            #region 获取数据
            try
            {

                dt = dbOperate.GetDataTable(sql);

            }
            catch (Exception ex)
            {
                dt = null;
                LogApi.DebugInfo(ex);
            }
            #endregion

            #region 返回数据
            return dt;
            #endregion
        }
        /// <summary>
        /// 获取城市列表
        /// </summary>
        /// <param name="provinceId"></param>
        /// <returns></returns>
        public string GetCityList(string provinceId)
        {
            List<CityInfo> listAdLocation = new List<CityInfo>();
            try
            {
                DataTable dt2 = GetCityDT(provinceId);
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    listAdLocation = cData.FillModel<CityInfo>(dt2);
                }
                Result.errCode = "1";
                Result.errMsg = "获取成功";
            }
            catch (Exception ex)
            {
                listAdLocation = null;
                Result.errCode = "-1";
                Result.errMsg = ex.Message;
                LogApi.DebugInfo(ex);
            }


            #region 返回数据
            return Result.GetResult(Result.errCode, Result.errMsg, listAdLocation);
            #endregion


        }
        public DataTable GetCityDT(string provinceId)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format("select s1.cityid as cityid,s1.cityname as cityname from bee.bee_cityinfo s1 where s1.citylevel=4 and s1.provinceid={0} order by cityid asc ",provinceId);
            #endregion           
            #region 获取数据
            try
            {

                dt = dbOperate.GetDataTable(sql);

            }
            catch (Exception ex)
            {
                dt = null;
                LogApi.DebugInfo(ex);
            }
            #endregion

            #region 返回数据
            return dt;
            #endregion
        }
    }
}
