using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.pub;
using DAL.database;
using System.Data;
using Common.pub;
using Model;

namespace BLL.manager
{
    /// <summary>
    /// 定向管理
    /// </summary>
   public  class DirectManager
    {
        ConvertData cData = new ConvertData();
        DBOperate dbOperate = new DBOperate();
        /// <summary>
        ///  获取广告广告状态列表返回DataTable
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetDirectTypeDT(string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"select s1.directtypeid,
                                           replace(s1.directtypename, '定向', '') as directtypename
                                      from bee_directtype s1
                                     where s1.directtypeid in (1, 2,7)
                                       and s1.status = 1");
            #endregion

            #region sql转换
            sql = cData.GetSqlBySqlWhere(sql, sqlWhere);
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
        /// 获取定向所有值-分类列表
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetDirectClassDT(string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"select s1.classid as directid, s1.classname as directname
                                          from bee_classinfo s1
                                         where s1.status = 1
                                         order by s1.ordernum asc ");
            #endregion

            #region sql转换
            sql = cData.GetSqlBySqlWhere(sql, sqlWhere);
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
        /// 
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetDirectMediaDT(string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"select distinct s1.mediaid as directid, s1.name as directname
                                          from bee.bee_mediainfo s1
                                         where s1.status = 1
                                         order by s1.mediaid asc ");
            #endregion

            #region sql转换
            sql = cData.GetSqlBySqlWhere(sql, sqlWhere);
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
        ///  获取定向所有值-分类列表
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetDirectSystemDT(string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@" 
                                    select s1.systemid as directid, s1.systemname as directname
                                    from bee_systeminfo s1
                                    where s1.status = 1
                                    order by s1.ordernum asc");
            #endregion

            #region sql转换
            sql = cData.GetSqlBySqlWhere(sql, sqlWhere);
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
       /// 获取其他定向类型所对应的列表数据
       /// </summary>
       /// <param name="userId"></param>
       /// <returns></returns>
        public string GetDirectTypeData(string userId)
        {
            
            //定向全部数据
            List<DirectItem> listDirectItem = new List<DirectItem>();
            List<DirectData> listDirectData = new List<DirectData>();
            string sqlWhere = "";
            //  string directTypeId = "";
            try
            {            
                //获取所有定向类型
                DataTable dt4 = GetDirectTypeDT(sqlWhere);
                if (dt4 != null && dt4.Rows.Count > 0)
                {
                    for (int i = 0; i < dt4.Rows.Count; i++)
                    {
                        string directTypeId = dt4.Rows[i]["directtypeid"].ToString();
                        string directTypeName = dt4.Rows[i]["directtypename"].ToString();
                        DataTable dtTemp1 = new DataTable();
                        //操作系统
                        if (directTypeId == "1")
                        {
                            dtTemp1 =GetDirectSystemDT(sqlWhere);
                            listDirectItem = cData.FillModel<DirectItem>(dtTemp1);
                        }
                        //分类
                        else if (directTypeId == "2")
                        {
                            dtTemp1 = GetDirectClassDT(sqlWhere);
                            listDirectItem = cData.FillModel<DirectItem>(dtTemp1);
                        }
                        //媒体
                        else if (directTypeId == "7")
                        {
                            dtTemp1 = GetDirectMediaDT(sqlWhere);
                            listDirectItem = cData.FillModel<DirectItem>(dtTemp1);
                        }
                        else
                        {
                            dtTemp1 = null;
                        }
                        DirectData directData = new DirectData();
                        directData.directtypeid = directTypeId;
                        directData.directtypename = directTypeName;
                        directData.directtypedata = listDirectItem;
                        listDirectData.Add(directData);
                    }

                }

                Result.errCode = "1";
                Result.errMsg = "获取成功";
            }
            catch (Exception ex)
            {
               
                Result.errCode = "-1";
                Result.errMsg = ex.Message;
                LogApi.DebugInfo(ex);
            }

            #region 组合数据
            var tempData = new
            {
                directdata = listDirectData              
            };
            #endregion

            #region 返回数据
            return Result.GetResult(Result.errCode, Result.errMsg, tempData);
            #endregion


        }
       /// <summary>
       /// 获取国家信息
       /// </summary>
       /// <param name="sqlWhere"></param>
       /// <returns></returns>
        public DataTable GetCountryDT(string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"select s1.cityid as cid, s1.cityname as cname, s1.citylevel
                                          from bee_cityinfo s1
                                         where s1.citylevel = 1
                                           and s1.status = 1 ");
            #endregion

            #region sql转换
            sql = cData.GetSqlBySqlWhere(sql, sqlWhere);
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
        /// 获取区域信息
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetAreaDT(string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@" 
                                    select s1.cityid as cid, s1.cityname as cname, s1.citylevel, s1.countryid 
                                          from bee_cityinfo s1
                                         where s1.citylevel = 2
                                           and s1.status = 1
                                        ");
            #endregion

            #region sql转换
            sql = cData.GetSqlBySqlWhere(sql, sqlWhere);
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
        /// 获取省份信息
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetProvinceDT(string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@" 
                                    select s1.cityid as cid, s1.cityname as cname, s1.citylevel, s1.areaid 
                                          from bee_cityinfo s1
                                         where s1.citylevel = 3
                                           and s1.status = 1
                                         ");
            #endregion

            #region sql转换
            sql = cData.GetSqlBySqlWhere(sql, sqlWhere);
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
        /// 获取城市信息
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetCityDT(string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@" 
                                    select s1.cityid as cid, s1.cityname as cname, s1.citylevel, s1.provinceid 
                                          from bee_cityinfo s1
                                         where s1.citylevel = 4
                                           and s1.status = 1
                                        ");
            #endregion

            #region sql转换
            sql = cData.GetSqlBySqlWhere(sql, sqlWhere);
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
       /// 获取地域信息
       /// </summary>
       /// <returns></returns>
        public string GetCityInfoData(string sqlWhere)
        {            
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("{");
                sb.Append("\"countrydata\":" + cData.DataTableToJsonList(GetCountryDT("")));
                sb.Append(",");
                sb.Append("\"areadata\":" + cData.DataTableToJsonList(GetAreaDT("")));
                sb.Append(",");
                sb.Append("\"provincedata\":" + cData.DataTableToJsonList(GetProvinceDT("")));
                sb.Append(",");
                sb.Append("\"citydata\":" + cData.DataTableToJsonList(GetCityDT("")));
                sb.Append("}");
                Result.errCode = Result.successCode;
                Result.errMsg = "获取成功";
            }
            catch (Exception ex)
            {
                Result.errCode = Result.failCode;
                Result.errMsg = "获取失败" + ex.ToString();
                LogApi.DebugInfo(ex);
            }
            return Result.GetResultString(Result.errCode, Result.errMsg, sb.ToString());

        }
       /// <summary>
       /// 获取地域信息
       /// </summary>
       /// <param name="userId"></param>
       /// <returns></returns>
        public string GetRegionData(string userId)
        {
            List<DirectCountry> listCountry = new List<DirectCountry>();
            List<DirectArea> listArea = new List<DirectArea>();
            List<DirectProvince> listProvince = new List<DirectProvince>();
            List<DirectCity> listCity = new List<DirectCity>();
            List<DirectCountry> listOthers = new List<DirectCountry>();

            string sqlWhere = "";            
            try
            {
                DataTable dt1 = GetCountryDT("");
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    DataTable tempAll = dt1.Clone();
                    DataTable tempOthers = dt1.Clone();
                    foreach (DataRow dr in dt1.Rows)
                    {
                        if (dr["cid"].ToString() == "0")
                            tempAll.ImportRow(dr);
                        else
                            tempOthers.ImportRow(dr);
                    }                    
                    listCountry = cData.FillModel<DirectCountry>(tempAll);
                    listOthers = cData.FillModel<DirectCountry>(tempOthers);
                }
                DataTable dt2 = GetAreaDT("");
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    listArea = cData.FillModel<DirectArea>(dt2);
                }
                DataTable dt3 = GetProvinceDT("");
                if (dt3 != null && dt3.Rows.Count > 0)
                {
                    listProvince = cData.FillModel<DirectProvince>(dt3);
                }
                DataTable dt4 = GetCityDT(sqlWhere);
                if (dt4 != null && dt4.Rows.Count > 0)
                {
                    listCity = cData.FillModel<DirectCity>(dt4);
                }

             
                Result.errCode = "1";
                Result.errMsg = "获取成功";
            }
            catch (Exception ex)
            {                
                Result.errCode = "-1";
                Result.errMsg = ex.Message;
                LogApi.DebugInfo(ex);
            }

            #region 组合数据
            var tempData = new
            {
                countrydata = listCountry,
                areadata = listArea,
                provincedata = listProvince,
                citydata=listCity,
                othersdata=listOthers
            };
            #endregion

            #region 返回数据
            return Result.GetResult(Result.errCode, Result.errMsg, tempData);
            #endregion
           
        }
       /// <summary>
       /// 获取地域定向-根据省份获取城市信息
       /// </summary>
       /// <param name="userId"></param>
       /// <param name="provinceId"></param>
       /// <returns></returns>
        public string GetRegionOfCityData(string userId, string provinceId)
        {
            List<DirectCity> listCity = new List<DirectCity>();

            string sqlWhere = string.Format("  s1.provinceid={0} order by s1.ordernum asc ", provinceId);
            try
            {
                DataTable dt1 = GetCityDT(sqlWhere);
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    listCity = cData.FillModel<DirectCity>(dt1);
                }


                Result.errCode = "1";
                Result.errMsg = "获取成功";
            }
            catch (Exception ex)
            {
                Result.errCode = "-1";
                Result.errMsg = ex.Message;
                LogApi.DebugInfo(ex);
            }

            #region 组合数据
            var tempData = new
            {
                citydata = listCity
            };
            #endregion

            #region 返回数据
            return Result.GetResult(Result.errCode, Result.errMsg, tempData);
            #endregion

        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="adId"></param>
       /// <returns></returns>
        public DataTable GetDirectDateDT(string adId)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"select  s1.datedirecttype from bee_directdatedetail s1 where s1.adid ={0} ", adId);
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
       /// 
       /// </summary>
       /// <param name="adId"></param>
       /// <returns></returns>
        public DataTable GetDirectHourDT(string adId)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"select s1.week,s1.starthour,s1.endhour from bee_directhourdetail s1 where s1.adid ={0}  order by s1.directid asc ", adId);
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
        /// 获取地域定向
        /// </summary>
        /// <param name="adId"></param>
        /// <returns></returns>
        public DataTable GetDirectAreaDT(string adId)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"select s1.cityid as cid, s1.citylevel as clevel, s1.areaid, s1.provinceid
                                          from bee_cityinfo s1, bee_directdetail s2
                                         where s1.cityid = s2.directvalue
                                           and s2.directtypeid = 3
                                           and s2.adid = {0}
                                          order by s2.directid asc ", adId);
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
