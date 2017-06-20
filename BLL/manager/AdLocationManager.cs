using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using BLL.pub;
using DAL.database;
using Common.pub;
using System.Data;
using CVBUtility;

namespace BLL.manager
{
    /// <summary>
    /// 广告位管理
    /// </summary>
    public class AdLocationManager
    {
        ConvertData cData = new ConvertData();
        DBOperate dbOperate = new DBOperate();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public string GetAdLocationListHtml(int pageSize, int pageNo,string sqlWhere)
        {
            #region 定义变量
            int pageCount = 0;
            string strData = "";
            string sql = string.Format("select * from bee_adlocationinfo where status=1 ");
            string orderBy = " order by statustime desc";
            #endregion

            StringBuilder sbsql = new StringBuilder();
            sbsql.Append(sql);
            if (!string.IsNullOrWhiteSpace(sqlWhere))
            {
                sbsql.Append(" ");
                sbsql.Append("and");
                sbsql.Append(" ");
                sbsql.Append(sqlWhere);
            }
            sql = sbsql.ToString();
            try
            {
                #region 获取数据
                List<AdLocation> adLocationList = cData.FillModel<AdLocation>(dbOperate.GetPageData(sql, orderBy, pageSize, pageNo, out pageCount));
                #endregion

                #region 组合数据
                if (adLocationList != null && adLocationList.Count > 0)
                {
                    foreach (var item in adLocationList)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<tr>");
                        sb.Append("<td>" + item.adlocationid + "</td>");
                        sb.Append("<div class='head2 pic'>");
                        sb.Append("<td>" + item.name + "</td>");
                        sb.Append("<td>" + item.tag + "</td>");
                        sb.Append("<td class='editBox'><a href='../report/adlocation_data.aspx?adlocationid=" + item.adlocationid + "'>报告</a></td>");
                        sb.Append("</tr>");
                        strData += sb.ToString();
                    }
                }
                #endregion
                Result.errCode = "1";
                Result.errMsg = "获取成功";
            }
            catch (Exception ex)
            {
                Result.errCode = "-1";
                Result.errMsg = ex.Message;
                LogApi.DebugInfo(ex);
            }

            #region 转换数据
            var tempData = new
            {
                pagecount = pageCount,
                html = strData
            };
            #endregion

            #region 返回数据
            return Result.GetResult(Result.errCode, Result.errMsg, tempData);
            #endregion
        }
        /// <summary>
        /// 获取广告位列表返回DataTable
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable GetAdLocationListDT(int pageSize, int pageNo, string sqlWhere,out int pageCount)
        {
            #region 定义变量
            DataTable dt = null;
            pageCount = 0;
            string sql = string.Format("select adlocationid,name as adlocationname,bcode from bee_adlocationinfo s1 where status=1 ");
            string orderBy = " order by adlocationid desc";
            #endregion

            sql = cData.GetSqlBySqlWhere(sql, sqlWhere);

            #region 获取数据
            try
            {
                
                dt= dbOperate.GetPageData(sql, orderBy, pageSize, pageNo, out pageCount);
                             
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
        ///  获取广告位广告位类型列表返回DataTable
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetAdLocationTypeListDT(string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format("select distinct adlocationid,name as adlocationname from bee_adlocationinfo where status=1 order by adlocationid asc   ");
            #endregion

            sql = cData.GetSqlBySqlWhere(sql,sqlWhere);            
            try
            {
                #region 获取数据
                dt = dbOperate.GetDataTable(sql);
                #endregion
            }
            catch (Exception ex)
            {
                dt = null;
                LogApi.DebugInfo(ex);
            }

            #region 返回数据
            return dt;
            #endregion
        }
        /// <summary>
        /// 获取广告位列表名称及id
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetAdLocationNameDT(string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format("select distinct adlocationid,name as adlocationname from bee_adlocationinfo s1  where status=1");
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
        /// 获取广告位列表名称及id
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public string GetAdLocationNameData(string sqlWhere)
        {
            List<AdLocationByPage> listAdLocation = new List<AdLocationByPage>();
            try
            {
                DataTable dt2 = GetAdLocationNameDT(sqlWhere);
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    listAdLocation = cData.FillModel<AdLocationByPage>(dt2);
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
        /// <summary>
        /// 获取管理端广告位列表信息
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable GetAdLocationStatDetailDT(string startTime, string endTime, int pageSize, int pageNo, string sqlWhere, out int pageCount)
        {
            #region 定义变量
            DataTable dt = null;
            pageCount = 0;
            string sql = string.Format(@" select p1.adlocationid,
       p1.adlocationname as statname,
       p2.clickcnt,
       round(p2.incomsum, 2) as incomsum
  from (select s1.adlocationid, s1.name as adlocationname, s1.pageid
          from bee.bee_adlocationinfo s1) p1,
       (select s1.adlocid,
               nvl(sum(s1.click_cnt), 0) as clickcnt,
               nvl(sum(s1.income), 0) as incomsum
          from bee.bee_rpt_statbyadld s1
         where s1.dateid < to_number(to_char(sysdate, 'yyyymmdd'))
           and s1.dateid between {0} and {1}
         group by s1.adlocid) p2
 where p1.adlocationid = p2.adlocid", startTime, endTime);
            string orderBy = " order by p1.adlocationid asc ";
            #endregion

            #region sql转换
            sql = cData.GetSqlBySqlWhere(sql, sqlWhere);
            #endregion

            #region 获取数据
            try
            {

                dt = dbOperate.GetPageData(sql, orderBy, pageSize, pageNo, out pageCount);

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
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable GetAdLocationStatSumDT(string startTime, string endTime,string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;           
            string sql = string.Format(@" select nvl(sum(p2.clickcnt), 0) as clicktotal,
       nvl(sum(round(p2.incomsum, 2)), 0) as incometotal
  from (select s1.adlocationid, s1.name as adlocationname, s1.pageid
          from bee.bee_adlocationinfo s1
         where s1.status = 1) p1,
       (select s1.adlocid,
               nvl(sum(s1.click_cnt), 0) as clickcnt,
               nvl(sum(s1.income), 0) as incomsum
          from bee.bee_rpt_statbyadld s1
         where s1.dateid < to_number(to_char(sysdate, 'yyyymmdd'))
           and s1.dateid between {0} and {1}
         group by s1.adlocid) p2
 where p1.adlocationid = p2.adlocid", startTime, endTime);
            string orderBy = " order by p1.adlocationid asc ";
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
        /// 获取竞价管理中广告位列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable GetAdLocationListByBiddingDT(int pageSize, int pageNo, string sqlWhere, out int pageCount)
        {
            #region 定义变量
            DataTable dt = null;
            pageCount = 0;
            string sql = string.Format(@"select s1.adlocationid,
       s1.name as adlocationname,
       s1.bcode,
       s2.name as pagename,
       s3.subadtypename,
       decode(nvl(s1.isbid, 0), 1, '是', '否') as isbid,
       decode(nvl(s1.alevel, 0),
              1,
              'Ⅰ级',
              2,
              'Ⅱ级',
              3,
              'Ⅲ级',
              4,
              'Ⅳ级',
              5,
              'Ⅴ级',
              '其他') as alevel
  from bee.bee_adlocationinfo s1,
       bee.bee_pageinfo       s2,
       bee.bee_adtype_config  s3
 where s1.status = 1
   and s1.pageid = s2.pageid
   and s1.subadtypeid = s3.subadtypeid and s1.isbid=1");
            string orderBy = " order by s1.adlocationid desc";
            #endregion

            sql = cData.GetSqlBySqlWhere(sql, sqlWhere);

            #region 获取数据
            try
            {

                dt = dbOperate.GetPageData(sql, orderBy, pageSize, pageNo, out pageCount);

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

        public string UpdateAdlBid(string adLocationId, string isBid)
        {

            try
            {
                if (!string.IsNullOrWhiteSpace(isBid)&& !string.IsNullOrWhiteSpace(adLocationId))
                {   //先删除
                    if (isBid == "0")
                    {
                        string dSql2 = string.Format("delete from bee.bee_ad_adlconfig_bid d1 where d1.adlocid={0}", adLocationId);
                        dbOperate.ExecuteStatement(dSql2);
                    }
                    else
                    {
                        string dSql2 = string.Format("delete from bee.bee_ad_adlconfig_nobid d1 where d1.adlocid={0}", adLocationId);
                        dbOperate.ExecuteStatement(dSql2);
                    }
                    // string dSql1 = string.Format("delete from bee.bee_ad_adlconfig_bid d1 where d1.adid={0}", adId);
                    
                }
                //修改保存数据              
                string sql = string.Format(@"
                       update bee.bee_adlocationinfo u1
   set u1.isbid = nvl(@isbid, u1.isbid), u1.statustime = sysdate
 where u1.adlocationid = @adlocationid");

                ParamCollections pc = new ParamCollections();
                pc.Add("@isbid", isBid, OracleDataType.INT);
                pc.Add("@adlocationid", adLocationId, OracleDataType.INT);

                if (dbOperate.ExecuteStatement(sql, pc.GetParams()) == 1)
                {
                    Result.errCode = "1";
                    Result.errMsg = "更新成功";
                }
                else
                {
                    Result.errCode = Result.failCode;
                    Result.errMsg = "更新失败";
                }
            }
            catch (Exception ex)
            {
                Result.errCode = "-1";
                Result.errMsg = ex.Message;
                LogApi.DebugInfo(ex);
            }

            #region 返回数据
            return Result.GetResult(Result.errCode, Result.errMsg, "");
            #endregion
        }
        public DataTable GetAdlListByNoBidDT()
        {
            #region 定义变量
            DataTable dt = null;           
            string sql = string.Format(@"select s1.adlocationid,
       s1.name as adlocationname,
       s1.bcode,
       s2.name as pagename,
       s3.subadtypename,
       decode(nvl(s1.isbid, 0), 1, '是', '否') as isbid,
       decode(nvl(s1.alevel, 0),
              1,
              'Ⅰ级',
              2,
              'Ⅱ级',
              3,
              'Ⅲ级',
              4,
              'Ⅳ级',
              5,
              'Ⅴ级',
              '其他') as alevel
  from bee.bee_adlocationinfo s1,
       bee.bee_pageinfo       s2,
       bee.bee_adtype_config  s3
 where s1.status = 1
   and s1.pageid = s2.pageid
   and s1.subadtypeid = s3.subadtypeid and nvl(s1.isbid,0)=0  order by s1.adlocationid desc");         
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

        public string GetAdlListByNoBid()
        {
            List<AdlListByBid> listAdlListByBid = new List<AdlListByBid>();            
            try
            {
                DataTable dt = GetAdlListByNoBidDT();
                if (dt != null && dt.Rows.Count > 0)
                {
                    listAdlListByBid = cData.FillModel<AdlListByBid>(dt);
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

            #region 返回数据
            return Result.GetResult(Result.errCode, Result.errMsg, listAdlListByBid);
            #endregion


        }
        /// <summary>
        /// 更新广告位是否竞价
        /// </summary>
        /// <param name="updatetype"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string UpdateAdlIsBid(string updatetype, string data)
        {
            try
            {
                string isBid = "";
                if (updatetype == "y")
                    isBid = "1";
                else if (updatetype == "n")
                    isBid = "0";
                if (string.IsNullOrWhiteSpace(isBid))
                    throw new Exception("无效的更新类型");
                List<string> list = ConvertData.ConvertJsonToModel<List<string>>(data);
                for (int i = 0; i < list.Count; i++)
                {
                    int adlId;                    
                    if (!int.TryParse(list[i], out adlId))
                        throw new Exception("无效的广告位id");
                    //修改保存数据              
                    string sql = string.Format(@"update bee.bee_adlocationinfo u1
                                                   set u1.isbid = nvl(@isbid, u1.isbid), u1.statustime = sysdate
                                                 where u1.adlocationid = @adlocationid");

                    ParamCollections pc = new ParamCollections();
                    pc.Add("@isbid", isBid, OracleDataType.INT);
                    pc.Add("@adlocationid", adlId.ToString(), OracleDataType.INT);

                    dbOperate.ExecuteStatement(sql, pc.GetParams());
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

            #region 返回数据
            return Result.GetResult(Result.errCode, Result.errMsg, "");
            #endregion


        }
    }
}
