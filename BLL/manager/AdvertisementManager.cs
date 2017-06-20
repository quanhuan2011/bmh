using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.pub;
using Model;
using DAL.database;
using Common.pub;
using System.Data;
using CVBUtility;
using System.Collections;
using BLL.redis;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace BLL.manager
{
    /// <summary>
    /// 广告管理
    /// </summary>
    public class AdvertisementManager
    {
        ConvertData cData = new ConvertData();
        DBOperate dbOperate = new DBOperate();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public string GetAdListHtml(int pageSize, int pageNo)

        {
            #region 定义变量
            int pageCount = 0;
            string strData = "";
            string sql = string.Format("select * from bee_adinfo where status=1 ");
            string orderBy = " order by statustime desc";
            #endregion

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
                        sb.Append("<li class='clearfix'>");
                        sb.Append("<div class='head1 name'>" + item.adlocationid + "</div>");
                        sb.Append("<div class='head2 pic'>");
                        sb.Append(item.name);
                        sb.Append("</div>");
                        sb.Append("<div class='head3 size'>" + item.tag + "</div>");
                        sb.Append("<div class='head5 operation edit'>");
                        sb.Append("<a href='../report/adlocation_data.aspx?adlocationid=" + item.adlocationid + "'>报告</a>");
                        sb.Append("</div>");
                        sb.Append("</li>");
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
        public DataTable GetAdListDT(int pageSize, int pageNo, string sqlWhere, out int pageCount)
        {
            #region 定义变量
            DataTable dt = null;
            pageCount = 0;
            string sql = string.Format(@"select p1.adid,
       p1.name as adname,
       p1.status,
       nvl(p2.statusname, '其他') as statusname,
       case
         when length((select listagg(k2.name || '(' || k1.adlocid || ')', ',') within group(order by k1.adlocid) as materialname
                       from bee_ad_adlocation k1, bee_adlocationinfo k2
                      where k1.adid = p1.adid
                        and k1.adlocid = k2.adlocationid)) > 100 then
          substr((select listagg(k2.name || '(' || k1.adlocid || ')', ',') within group(order by k1.adlocid) as materialname
                   from bee_ad_adlocation k1, bee_adlocationinfo k2
                  where k1.adid = p1.adid
                    and k1.adlocid = k2.adlocationid),
                 0,
                 100) || '...'
         else
          (select listagg(k2.name || '(' || k1.adlocid || ')', ',') within group(order by k1.adlocid) as materialname
             from bee_ad_adlocation k1, bee_adlocationinfo k2
            where k1.adid = p1.adid
              and k1.adlocid = k2.adlocationid)
       end as adlocationname,
       
       (select listagg(k1.name || '(' || k1.materialid || ')', ',') within group(order by k2.ad_materialid) as materialname
          from bee_materialinfo k1, bee_ad_materialinfo k2
         where k1.materialid = k2.materialid
           and k2.adid = p1.adid) as materialname,
       p1.price,
       to_char(p1.putstarttime, 'yyyy/mm/dd hh24:mi:ss') ||
       decode(nvl(to_char(p1.putendtime, 'yyyy'), 0),
              0,
              '',
              '至' || to_char(p1.putendtime, 'yyyy/mm/dd hh24:mi:ss')) as putrangetime
  from bee.bee_adinfo p1,
       (select * from bee.bee_dict_status s1 where s1.typeid = 1) p2
 where p1.status = p2.status(+) ");
            string orderBy = " order by p1.adid desc";
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
        public DataTable GetAdListDTByAdu(int pageSize, int pageNo, string sqlWhere, out int pageCount)
        {
            #region 定义变量
            DataTable dt = null;
            pageCount = 0;
            string sql = string.Format(@"select p1.adid,
       p1.name as adname,
       p1.status,
       decode(p1.status,
              1,
              '已启用',
              2,
              '暂停中',
              0,
              '未启用',
              3,
              '已投放',
              -1,
              '已失效',
              -2,
              '审核不通过',
              -4,
              '已达到每日上限',
              -5,
              '余额不足',
              -6,
              '广告主日投达到上限',
              -7,
              '广告达到上限',
              '其他') as statusname,
       (select listagg(substr(k1.linkurl, instr(k1.linkurl, '/', -1) + 1) || '(' ||
                       k1.materialid || ')',
                       ',') within
         group(
         order by k2.ad_materialid) as materialname
          from bee.bee_materialinfo k1, bee.bee_ad_materialinfo k2
         where k1.materialid = k2.materialid
           and k2.adid = p1.adid) as materialname,
       p1.price,
       to_char(p1.putstarttime, 'yyyy/mm/dd hh24:mi:ss') ||
       decode(nvl(to_char(p1.putendtime, 'yyyy'), 0),
              0,
              '',
              '至' || to_char(p1.putendtime, 'yyyy/mm/dd hh24:mi:ss')) as putrangetime
  from bee.bee_adinfo p1
 where 1 = 1
   and p1.isbid = 1");
            string orderBy = " order by p1.adid desc";
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
        ///  获取广告广告状态列表返回DataTable
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetAdStatusListDT(string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format("select s1.status,s1.statusname from bee.bee_dict_status s1 where s1.typeid=1  order by s1.rk");
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
        ///  获取广告涉及页面列表返回DataTable
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetAdPageListDT(string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format("select distinct p2.pageid ,p2.name as pagename from bee_adinfo p1, bee_pageinfo  p2 where p1.pageid=p2.pageid and p2.status=1  ");
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
        /// 获取广告类型
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetAdTypeDT(string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format("select distinct adtypeid,adtypename from bee.bee_adinfotype where status=1 ");
            #endregion

            #region sql转换
            sql = cData.GetSqlBySqlWhere(sql, sqlWhere);
            #endregion

            sql = sql + " order by adtypeid asc";
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
        public DataTable GetSubAdTypeDT(string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format("select distinct subadtypeid,subadtypename from bee.bee_adtype_config s1 where s1.status=1  ");
            #endregion

            #region sql转换
            sql = cData.GetSqlBySqlWhere(sql, sqlWhere);
            #endregion

            sql = sql + " order by subadtypeid asc";
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
        public DataTable GetSubAdTypeDTByAdu(string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format("select distinct s1.subadtypeid,s1.subadtypename,s1.width,s1.height from bee.bee_adtype_config s1 where s1.status=1  ");
            #endregion

            #region sql转换
            sql = cData.GetSqlBySqlWhere(sql, sqlWhere);
            #endregion

            sql = sql + " order by subadtypeid asc";
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
        /// 根据广告类型获取广告形式
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public string GetSubAdTypeData(string sqlWhere)
        {
            List<SubAdTypeByAdType> listSubAdType = new List<SubAdTypeByAdType>();
            try
            {
                DataTable dt2 = GetSubAdTypeDT(sqlWhere);
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    listSubAdType = cData.FillModel<SubAdTypeByAdType>(dt2);
                }
                Result.errCode = "1";
                Result.errMsg = "获取成功";
            }
            catch (Exception ex)
            {
                listSubAdType = null;
                Result.errCode = "-1";
                Result.errMsg = ex.Message;
                LogApi.DebugInfo(ex);
            }

            #region 返回数据
            return Result.GetResult(Result.errCode, Result.errMsg, listSubAdType);
            #endregion
        }
        /// <summary>
        /// 根据广告类型获取广告形式-代理商
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public string GetSubAdTypeDataByAdu(string sqlWhere)
        {
            List<SubAdTypeByAdTypeForAdu> listSubAdType = new List<SubAdTypeByAdTypeForAdu>();
            try
            {
                DataTable dt2 = GetSubAdTypeDTByAdu(sqlWhere);
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    listSubAdType = cData.FillModel<SubAdTypeByAdTypeForAdu>(dt2);
                }
                Result.errCode = "1";
                Result.errMsg = "获取成功";
            }
            catch (Exception ex)
            {
                listSubAdType = null;
                Result.errCode = "-1";
                Result.errMsg = ex.Message;
                LogApi.DebugInfo(ex);
            }

            #region 返回数据
            return Result.GetResult(Result.errCode, Result.errMsg, listSubAdType);
            #endregion
        }
        //
        /// <summary>
        ///  获取广告、页面以及广告位信息
        /// </summary>
        /// <param name="adId"></param>
        /// <returns></returns>
        public DataTable GetAdDT(string adId, string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"select p1.*,p2.name as pagename,p3.name as adlocationname
  from (select s1.adid,
               s1.name     as adname,
               s1.aduserid,
               s2.name     as adusername,
               s3.adlocid  as adlocationid,
               s3.pageid,
               s1.price,
               s1.putstarttime,
               s1.putendtime,
               s1.putmax,
               s1.putmaxbyday,
               s1.adtypeid,
               s4.adtypename,
               s1.subadtypeid,
               s5.subadtypename,
               s1.isbid,
               s1.isbottom,
               s1.billingtype,
               s1.termid                                             
          from bee.bee_adinfo         s1,
               bee_aduserinfo         s2,
               bee_ad_adlconfig_nobid s3,bee_adinfotype s4,bee_adtype_config s5
         where s1.adid = {0}
           and s1.aduserid = s2.aduserid
           and s1.adid = s3.adid(+)
           and s1.adtypeid=s4.adtypeid
           and s1.subadtypeid=s5.subadtypeid(+)
           ) p1,
       bee_pageinfo p2,
       bee_adlocationinfo p3
 where p1.pageid = p2.pageid(+)
   and p1.adlocationid = p3.adlocationid(+)", adId);
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
        /// 根据广告获取操作系统定向配置
        /// </summary>
        /// <param name="adId"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetSystemByAdDT(string adId, string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"select p3.systemid as directid, p3.systemname as directname
                                              from BEE_Directtype p1, bee_directdetail p2, bee_systeminfo p3
                                             where p1.directtypeid = p2.directtypeid
                                               and p2.directvalue = p3.systemid
                                               and p1.directtypeid = 1
                                               and p2.adid = {0}
                                               and p2.status = 1", adId);
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
        /// 根据广告获取分类定向配置
        /// </summary>
        /// <param name="adId"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetClassByAdDT(string adId, string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"select p3.classid as directid, p3.classname as directname
                                      from BEE_Directtype p1, bee_directdetail p2, bee_classinfo p3
                                     where p1.directtypeid = p2.directtypeid
                                       and p2.directvalue = p3.classid
                                       and p1.directtypeid = 2
                                       and p2.adid = {0}
                                       and p2.status = 1", adId);
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
        /// <param name="adId"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetMediaByAdDT(string adId, string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"select distinct p3.mediaid as directid, p3.name as directname
                                              from BEE_Directtype p1, bee_directdetail p2, bee.bee_mediainfo p3
                                             where p1.directtypeid = p2.directtypeid
                                               and p2.directvalue = p3.mediaid
                                               and p1.directtypeid = 7
                                               and p2.adid = {0}
                                               and p2.status = 1", adId);
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
        /// 根据广告获取物料信息
        /// </summary>
        /// <param name="adId"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetMaterialByAdDT(string adId, string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"
                              select s1.weighttype,
                                       s1.weight,
                                       s2.materialid,
                                       s2.name         as materialname,
                                       s2.imageurl,
                                       s2.title,
                                       s2.width,
                                       s2.height,
                                       s2.format,
                                       s2.display,
                                       s2.materialtype,
                                       s3.adtypename   as materialtypename,
                                       s2.remark,
                                       s2.confirmtext,
                                       s2.canceltext,
                                       s2.showtime
                                  from bee_ad_materialinfo s1, bee_materialinfo s2, bee_adinfotype s3
                                 where s1.materialid = s2.materialid
                                   and s2.materialtype = s3.adtypeid
                                   and s1.status = 1
                                   and s2.status = 1
                                   and s3.status = 1
                                   and s1.adid = {0}", adId);
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
        /// 根据广告获取其他定向类型
        /// </summary>
        /// <param name="adId"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetDirectTypeByAdDT(string adId, string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"
                             select s1.directtypeid, replace(s2.directtypename, '定向', '') as directtypename
                              from bee_directdetail s1, bee_directtype s2
                             where s1.directtypeid = s2.directtypeid
                               and s1.status = 1
                               and s2.status = 1
                               and s2.directtypeid in (1, 2,7)
                               and s1.adid = {0}
                             group by s1.directtypeid, s2.directtypename
                             order by s1.directtypeid", adId);
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
        /// 根据广告获取投放范围
        /// </summary>
        /// <param name="adId"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetPutRangeByAdDT(string adId)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"
                            select distinct pageid as pid from bee_ad_adlconfig_bid s1  where adid={0} order by pageid", adId);
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
        /// 获取广告相关数据
        /// </summary>
        /// <param name="adId"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public string GetAdInfo(string userId, string adId)
        {
            AdInfo adInfo = new AdInfo();
            DirectManager directManager = new DirectManager();
            List<MaterialInfoByAd> listMaterial = new List<MaterialInfoByAd>();
            //List<SystemInfoByAd> listSystem = new List<SystemInfoByAd>();
            //List<ClassInfoByAd> listClass = new List<ClassInfoByAd>();
            //定向全部数据
            List<DirectItem> listDirectItem = new List<DirectItem>();
            List<DirectData> listDirectData = new List<DirectData>();
            //根据广告匹配的定向数据
            List<DirectItem> listDirectItemByAd = new List<DirectItem>();
            List<DirectData> listDirectDataByAd = new List<DirectData>();
            //时间定向
            DirectDate directDate = new DirectDate();
            List<DirectHour> listDirectHour = new List<DirectHour>();
            //地域定向
            List<DirectAreaRegion> listDirectArea = new List<DirectAreaRegion>();
            //投放范围
            List<PutRangeByBid> listPutRange = new List<PutRangeByBid>();

            string sqlWhere = "";
            //  string directTypeId = "";
            try
            {
                DataTable dt = GetAdDT(adId, sqlWhere);
                if (dt != null && dt.Rows.Count > 0)
                {
                    adInfo = cData.FillModel<AdInfo>(dt.Rows[0]);
                }
                // DataTable dt1 = GetSystemByAdDT(adId, sqlWhere);
                // if (dt1 != null && dt1.Rows.Count > 0)
                // {
                //    listSystem = cData.FillModel<SystemInfoByAd>(dt1);
                // }
                DataTable dt2 = GetMaterialByAdDT(adId, sqlWhere);
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    listMaterial = cData.FillModel<MaterialInfoByAd>(dt2);
                }
                //其他设置
                //先取其他定向是否有记录 
                //根据广告匹配的定向数据
                DataTable dt3 = GetDirectTypeByAdDT(adId, sqlWhere);
                if (dt3 != null && dt3.Rows.Count > 0)
                {
                    for (int i = 0; i < dt3.Rows.Count; i++)
                    {
                        string directTypeId = dt3.Rows[i]["directtypeid"].ToString();
                        string directTypeName = dt3.Rows[i]["directtypename"].ToString();
                        DataTable dtTemp1 = new DataTable();
                        if (directTypeId == "1")
                        {
                            dtTemp1 = GetSystemByAdDT(adId, sqlWhere);
                            listDirectItemByAd = cData.FillModel<DirectItem>(dtTemp1);
                        }
                        else if (directTypeId == "2")
                        {
                            dtTemp1 = GetClassByAdDT(adId, sqlWhere);
                            listDirectItemByAd = cData.FillModel<DirectItem>(dtTemp1);
                        }
                        else if (directTypeId == "7")
                        {
                            dtTemp1 = GetMediaByAdDT(adId, sqlWhere);
                            listDirectItemByAd = cData.FillModel<DirectItem>(dtTemp1);
                        }
                        else
                        {
                            dtTemp1 = null;
                        }
                        DirectData directDataByAd = new DirectData();
                        directDataByAd.directtypeid = directTypeId;
                        directDataByAd.directtypename = directTypeName;
                        directDataByAd.directtypedata = listDirectItemByAd;
                        listDirectDataByAd.Add(directDataByAd);
                    }

                }

                //获取所有定向类型
                DataTable dt4 = directManager.GetDirectTypeDT(sqlWhere);
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
                            dtTemp1 = directManager.GetDirectSystemDT(sqlWhere);
                            listDirectItem = cData.FillModel<DirectItem>(dtTemp1);
                        }
                        //分类
                        else if (directTypeId == "2")
                        {
                            dtTemp1 = directManager.GetDirectClassDT(sqlWhere);
                            listDirectItem = cData.FillModel<DirectItem>(dtTemp1);
                        }
                        //媒体
                        else if (directTypeId == "7")
                        {
                            dtTemp1 = directManager.GetDirectMediaDT(sqlWhere);
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

                //时间定向
                DataTable dt5 = directManager.GetDirectDateDT(adId);
                string dateType = "1";
                if (dt5 != null && dt5.Rows.Count > 0)
                {
                    dateType = dt5.Rows[0]["datedirecttype"].ToString();
                    if (dateType == "2")
                    {
                        DataTable dtTemp1 = directManager.GetDirectHourDT(adId);
                        listDirectHour = cData.FillModel<DirectHour>(dtTemp1);
                    }
                }
                directDate.datetype = dateType;
                directDate.hourdata = listDirectHour;
                //地域定向
                DataTable dt6 = directManager.GetDirectAreaDT(adId);
                if (dt6 != null && dt6.Rows.Count > 0)
                {
                    listDirectArea = cData.FillModel<DirectAreaRegion>(dt6);
                }
                //投放范围 为竞价广告时
                if (!string.IsNullOrWhiteSpace(adInfo.isbid) && adInfo.isbid != "0")
                {
                    DataTable dtTemp1 = GetPutRangeByAdDT(adId);
                    if (dtTemp1 != null && dtTemp1.Rows.Count > 0)
                    {
                        listPutRange = cData.FillModel<PutRangeByBid>(dtTemp1);
                    }
                }


                Result.errCode = "1";
                Result.errMsg = "获取成功";
            }
            catch (Exception ex)
            {
                adInfo = null;
                Result.errCode = "-1";
                Result.errMsg = ex.Message;
                LogApi.DebugInfo(ex);
            }

            #region 组合数据
            var tempData = new
            {
                infodata = adInfo,
                materialdata = listMaterial,
                directdata = listDirectData,
                matchdata = listDirectDataByAd,
                directdatedata = directDate,
                directareadata = listDirectArea,
                putrange=listPutRange
            };
            #endregion

            #region 返回数据
            return Result.GetResult(Result.errCode, Result.errMsg, tempData);
            #endregion


        }
        /// <summary>
        /// 获取代理商广告
        /// </summary>
        /// <param name="adId"></param>
        /// <returns></returns>
        public string GetAdInfoByAdu(string adId)
        {
            AdInfo adInfo = new AdInfo();
            DirectManager directManager = new DirectManager();
            List<MaterialInfoByAd> listMaterial = new List<MaterialInfoByAd>();
            string sqlWhere = "";         
            try
            {
                DataTable dt = GetAdDT(adId, sqlWhere);
                if (dt != null && dt.Rows.Count > 0)
                {
                    adInfo = cData.FillModel<AdInfo>(dt.Rows[0]);
                }
               
                DataTable dt2 = GetMaterialByAdDT(adId, sqlWhere);
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    listMaterial = cData.FillModel<MaterialInfoByAd>(dt2);
                }                
                Result.errCode = "1";
                Result.errMsg = "获取成功";
            }
            catch (Exception ex)
            {
                adInfo = null;
                Result.errCode = "-1";
                Result.errMsg = ex.Message;
                LogApi.DebugInfo(ex);
            }

            #region 组合数据
            var tempData = new
            {
                infodata = adInfo,
                materialdata = listMaterial               
            };
            #endregion

            #region 返回数据
            return Result.GetResult(Result.errCode, Result.errMsg, tempData);
            #endregion
        }
        /// <summary>
        /// 更新广告信息
        /// </summary>
        /// <param name="inJson"></param>
        /// <returns></returns>
        public string UpdateAdData(string inJson,string inFlag)
        {
            #region 定义变量
            string procedureName = "p_updateaddata";
            #endregion

            #region 组合数据执行过程
            try
            {
                LogApi.LogInfo("p_updateaddata", inJson);
                ParamCollections paramItems = new ParamCollections();
                paramItems.Add("@injson", inJson, OracleDataType.STRING, InOutFlag.IN, 5000);
                paramItems.Add("@errcode", Result.errCode, OracleDataType.INT, InOutFlag.OUT);
                paramItems.Add("@errmsg", Result.errMsg, OracleDataType.STRING, InOutFlag.OUT);
                ArrayList arrayList = dbOperate.ExecProcedure(procedureName, paramItems);
                Result.errCode = arrayList[0].ToString();
                Result.errMsg = arrayList[1].ToString();
                if (Result.errCode != "1")
                {
                    LogApi.LogInfo("p_updateaddata-error", Result.errMsg);
                }
                else
                {
                    if (inFlag == "0")
                    {
                        JObject obj = (JObject)JsonConvert.DeserializeObject(inJson);
                        if (null != obj)
                        {
                            string adId = Convert.ToString(obj["infodata"]["adid"]);
                            string adUId = Convert.ToString(obj["infodata"]["aduserid"]);
                            string price = Convert.ToString(obj["infodata"]["price"]);
                            RedisBase redis = new BLL.redis.RedisBase();
                            if (!redis.UpdatePrice(adId, adUId, price))
                            {
                                Result.errCode = Result.failCode;
                                Result.errMsg = "更新价格失败，请重试";
                                LogApi.LogInfo("UpdateAdData-redis-fail", "更新价格失败");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Result.errCode = "-1";
                Result.errMsg = ex.Message;
                LogApi.DebugInfo(ex);
            }
            #endregion

            #region 返回数据
            return Result.GetResult(Result.errCode, Result.errMsg, "");
            #endregion
        }
        /// <summary>
        /// 代理商更新广告信息
        /// </summary>
        /// <param name="inJson"></param>
        /// <returns></returns>
        public string UpdateAdDataByAdu(string inJson,string inFlag)
        {
            #region 定义变量
            string procedureName = "p_updateaddatabyadu";
            #endregion

            #region 组合数据执行过程
            try
            {
                LogApi.LogInfo("p_updateaddatabyadu", inJson);
                ParamCollections paramItems = new ParamCollections();
                paramItems.Add("@injson", inJson, OracleDataType.STRING, InOutFlag.IN, 5000);
                paramItems.Add("@errcode", Result.errCode, OracleDataType.INT, InOutFlag.OUT);
                paramItems.Add("@errmsg", Result.errMsg, OracleDataType.STRING, InOutFlag.OUT);
                ArrayList arrayList = dbOperate.ExecProcedure(procedureName, paramItems);
                Result.errCode = arrayList[0].ToString();
                Result.errMsg = arrayList[1].ToString();
                if (Result.errCode != "1")
                {
                    LogApi.LogInfo("p_updateaddatabyadu-error", Result.errMsg);
                }
                else
                {
                    if (inFlag == "0")
                    {
                        JObject obj = (JObject)JsonConvert.DeserializeObject(inJson);
                        if (null != obj)
                        {
                            string adId = Convert.ToString(obj["infodata"]["adid"]);
                            DataTable dt = dbOperate.GetDataTable(string.Format("select s1.aduserid,s1.price from bee.bee_adinfo s1 where s1.adid={0}", adId));
                            if (null != dt && dt.Rows.Count > 0)
                            {
                                string adUId = Convert.ToString(dt.Rows[0]["aduserid"]);
                                string price = Convert.ToString(dt.Rows[0]["price"]);
                                RedisBase redis = new BLL.redis.RedisBase();
                                if (!redis.UpdatePrice(adId, adUId, price))
                                {
                                    Result.errCode = Result.failCode;
                                    Result.errMsg = "更新价格失败，请重试";
                                    LogApi.LogInfo("UpdateAdDataByAdu-redis-fail", "更新价格失败");
                                }
                            }                                                       
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Result.errCode = "-1";
                Result.errMsg = ex.Message;
                LogApi.DebugInfo(ex);
            }
            #endregion

            #region 返回数据
            return Result.GetResult(Result.errCode, Result.errMsg, "");
            #endregion
        }
        /// <summary>
        /// 新增广告信息
        /// </summary>
        /// <param name="inJson"></param>
        /// <returns></returns>
        public string InsertAdData(string inJson)
        {
            #region 定义变量
            string procedureName = "p_insertaddata";
            #endregion

            #region 组合数据执行过程
            try
            {
                LogApi.LogInfo("p_insertaddata", inJson);
                ParamCollections paramItems = new ParamCollections();
                paramItems.Add("@injson", inJson, OracleDataType.STRING,InOutFlag.IN,5000);
                paramItems.Add("@errcode", Result.errCode, OracleDataType.INT, InOutFlag.OUT);
                paramItems.Add("@errmsg", Result.errMsg, OracleDataType.STRING, InOutFlag.OUT);
                ArrayList arrayList = dbOperate.ExecProcedure(procedureName, paramItems);
                Result.errCode = arrayList[0].ToString();
                Result.errMsg = arrayList[1].ToString();
                if (Result.errCode != "1")
                {
                    LogApi.LogInfo("p_insertaddata-error", Result.errMsg);
                }
            }
            catch (Exception ex)
            {
                Result.errCode = "-1";
                Result.errMsg = ex.Message;
                LogApi.DebugInfo(ex);
            }
            #endregion

            #region 返回数据
            return Result.GetResult(Result.errCode, Result.errMsg, "");
            #endregion
        }
        /// <summary>
        /// 代理商新增广告信息
        /// </summary>
        /// <param name="inJson"></param>
        /// <returns></returns>
        public string InsertAdDataByAdu(string inJson)
        {
            #region 定义变量
            string procedureName = "p_insertaddatabyadu";
            #endregion

            #region 组合数据执行过程
            try
            {
                LogApi.LogInfo("p_insertaddatabyadu", inJson);
                ParamCollections paramItems = new ParamCollections();
                paramItems.Add("@injson", inJson, OracleDataType.STRING, InOutFlag.IN, 5000);
                paramItems.Add("@errcode", Result.errCode, OracleDataType.INT, InOutFlag.OUT);
                paramItems.Add("@errmsg", Result.errMsg, OracleDataType.STRING, InOutFlag.OUT);
                ArrayList arrayList = dbOperate.ExecProcedure(procedureName, paramItems);
                Result.errCode = arrayList[0].ToString();
                Result.errMsg = arrayList[1].ToString();
                if (Result.errCode != "1")
                {
                    LogApi.LogInfo("p_insertaddatabyadu-error", Result.errMsg);
                }
            }
            catch (Exception ex)
            {
                Result.errCode = "-1";
                Result.errMsg = ex.Message;
                LogApi.DebugInfo(ex);
            }
            #endregion

            #region 返回数据
            return Result.GetResult(Result.errCode, Result.errMsg, "");
            #endregion
        }
        /// <summary>
        /// 更新广告状态-广告管理列表
        /// </summary>
        /// <param name="updatetype"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string UpdateAdStatus(string updatetype, string data)
        {
            try
            {
                string status = "";
                //启用
                if (updatetype == "start")
                    status = "1";
                //暂停
                else if (updatetype == "stop")
                    status = "2";
                //审核不通过
                else if (updatetype == "unpass")
                    status = "-2";
                if (string.IsNullOrWhiteSpace(status))
                    throw new Exception("无效的更新类型");
                List<string> list = ConvertData.ConvertJsonToModel<List<string>>(data);
                for (int i = 0; i < list.Count; i++)
                {
                    int adId;
                    if (!int.TryParse(list[i], out adId))
                        throw new Exception("无效的广告id");
                    string sql = string.Format("update  bee_adinfo u1 set u1.status={0},u1.statustime=sysdate where u1.adid={1} and rownum=1", status, adId);
                    int result = dbOperate.ExecuteStatement(sql);
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
        /// <summary>
        /// 更新广告状态-通用-单个
        /// </summary>
        /// <param name="adId"></param>
        /// <param name="NewStatus"></param>
        /// <param name="oldStatus">可为空</param>
        /// <returns></returns>
        public void UpdateAdStatus(string adId, string NewStatus, string oldStatus)
        {
            try
            {
                string sql = string.Format(@"update  bee_adinfo u1 set u1.status=@newstatus,u1.statustime=sysdate where u1.adid=@adid and rownum=1 ");
                ParamCollections pc = new ParamCollections();
                pc.Add("@newstatus", NewStatus, OracleDataType.INT);
                pc.Add("@adid", adId, OracleDataType.INT);
                if (!string.IsNullOrWhiteSpace(oldStatus))
                {
                    sql = string.Format("{0} and u1.status=@oldstatus ", sql);
                    pc.Add("@oldstatus", oldStatus, OracleDataType.INT);
                }

                int resultCode = dbOperate.ExecuteStatement(sql, pc.GetParams());                
            }
            catch (Exception ex)
            {               
                LogApi.DebugInfo(ex);
            }           

        }
        /// <summary>
        /// 广告统计明细
        /// </summary>        
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable GetAdStatDetailDT(string startTime, string endTime, int pageSize, int pageNo, string sqlWhere, out int pageCount)
        {
            #region 定义变量
            DataTable dt = null;
            pageCount = 0;
            string sql = string.Format(@"select s1.name ||'的' || s2.name as statname,
       (select count(1)
          from bee_materialinfo k1, bee_ad_materialinfo k2
         where s2.adid = k2.adid
           and k1.materialid = k2.materialid
           and k1.status = 1
           and k2.status = 1) as statcnt,
       0 as clickcnt,
       0 as incomsum
  from bee_aduserinfo s1, bee_adinfo s2
 where s1.aduserid = s2.aduserid
   and s1.status = 1
   and s2.status in (1) ", startTime, endTime);
            string orderBy = " order by adid asc";
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
        /// 获取广告点击量及支出
        /// </summary>
        /// <param name="aduserId"></param>
        /// <returns></returns>
        public DataTable GetADStatSumDT()
        {
            string sql = string.Format(@"   select p1.dateid,
          p1.requestcnt,
          p2.incomesum,
          p2.clickcnt,
          decode(p1.requestcnt,
                 0,
                 0,
                 round((p2.clickcnt / p1.requestcnt) * 100, 2)) as clickrate
     from (select dateid, nvl(sum(ask_cnt), 0) as requestcnt
             from (select dateid, pageid, max(s1.ask_cnt) as ask_cnt
                     from bee.bee_rpt_statbyadld s1
                    where s1.dateid = to_number(to_char(sysdate - 1, 'yyyymmdd'))
                    group by dateid, pageid)
            group by dateid) p1,
          (select dateid,
                  nvl(sum(s1.income), 0) as incomesum,
                  nvl(sum(s1.click_cnt), 0) as clickcnt
             from bee.bee_rpt_statbyadvd s1
            where s1.dateid = to_number(to_char(sysdate - 1, 'yyyymmdd'))
            group by dateid) p2
    where p1.dateid = p2.dateid");
            DataTable dt = dbOperate.GetDataTable(sql);
            return dt;
        }

        public DataTable GetAdListByBiddingDT(int pageSize, int pageNo, string sqlWhere, out int pageCount)
        {
            #region 定义变量
            DataTable dt = null;
            pageCount = 0;
            string sql = string.Format(@"select s1.adid,
                                       s1.name as adname,
                                       s2.name as adusername,
                                       s1.price,
                                       decode(nvl(s1.isbid, 0), 1, '是', '否') as isbid
                                  from bee.bee_adinfo s1, bee.bee_aduserinfo s2
                                 where s1.status = 1
                                   and s1.isbid = 1
                                   and s1.isbottom=0
                                   and s1.aduserid=s2.aduserid");
            string orderBy = " order by s1.adid desc";
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

        public DataTable GetAdvListByNoBidDT()
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"select s1.adid,
       s1.name as adname,
       s2.name as adusername,
       s1.price,
       decode(nvl(s1.isbid, 0), 1, '是', '否') as isbid,
       s3.subadtypename
  from bee.bee_adinfo s1, bee.bee_aduserinfo s2, bee.bee_adtype_config s3
 where s1.status = 1
   and nvl(s1.isbid, 0) = 0
   and s1.aduserid = s2.aduserid
   and s1.isbottom=0
   and s1.subadtypeid = s3.subadtypeid order by s1.adid desc");
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

        public string GetAdvListByNoBid()
        {
            List<AdvListByBid> listAdvListByBid = new List<AdvListByBid>();
            try
            {
                DataTable dt = GetAdvListByNoBidDT();
                if (dt != null && dt.Rows.Count > 0)
                {
                    listAdvListByBid = cData.FillModel<AdvListByBid>(dt);
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
            return Result.GetResult(Result.errCode, Result.errMsg, listAdvListByBid);
            #endregion


        }
        /// <summary>
        /// 根据广告管理页面或者广告位筛选获取广告id
        /// </summary>
        /// <returns></returns>
        public DataTable GetAdIdByScreenDT(string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"select distinct s1.adid
  from bee.bee_ad_adlocation s1, bee_adlocationinfo s2
 where s1.adlocid=s2.adlocationid  ");
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

        public string UpdateIsBidByAdList(string inJson)
        {
            //解析
            JArray bidList = (JArray)JsonConvert.DeserializeObject(inJson);
            foreach (var jObject in bidList)
            {
                string adId = jObject["adid"].ToString();
                string pageIdStr = jObject["pageidstr"].ToString();
                string[] pageIdList = pageIdStr.Split(',');
                //先删除
                // string dSql1 = string.Format("delete from bee.bee_ad_adlconfig_bid d1 where d1.adid={0}", adId);
                string dSql2 = string.Format("delete from bee.bee_ad_adlconfig_nobid d1 where d1.adid={0}", adId);
                dbOperate.ExecuteStatement(dSql2);
                string uSql1 = string.Format("update bee_adinfo u1 set u1.isbid=1 ,u1.statustime=sysdate where u1.adid={0}", adId);
                dbOperate.ExecuteStatement(uSql1);
                //再插入
                for (int i = 0; i < pageIdList.Length; i++)
                {
                    string pageId = pageIdList[i].ToString();
                    string sql = string.Format(@" insert into bee_ad_adlconfig_bid
   (id, adlocid, adid, aduserid, pageid, subadtypeid, createtime)
   select s_ad_adlconfig_bid.nextval,
          s2.adlocationid,
          s1.adid,
          s1.aduserid,
          s2.pageid,
          s1.subadtypeid,
          sysdate
     from bee_adinfo s1, bee_adlocationinfo s2
    where s1.subadtypeid = s2.subadtypeid
      and s1.adid =@adid
      and s2.pageid =@pageid
      and s2.status = 1
");
                    ParamCollections pc = new ParamCollections();

                    pc.Add("@adid", adId, OracleDataType.INT);
                    pc.Add("@pageid", pageId, OracleDataType.INT);
                    int resultCode = dbOperate.ExecuteStatement(sql, pc.GetParams());
                    if (resultCode == 0)
                        throw new Exception(string.Format("执行广告id为:{0}时出现异常", adId));
                }

            }
            ////执行重算过程
            //#region 定义变量
            //string procedureName = "p_bee_adbid";
            //#endregion

            //#region 组合数据执行过程
            //try
            //{                                      
            //    dbOperate.ExecProcedure(procedureName, null);                              
            //}
            //catch (Exception ex)
            //{
            //    Result.errCode = "-1";
            //    Result.errMsg = ex.Message;
            //    LogApi.DebugInfo(ex);
            //}
            //#endregion

            #region 返回数据
            return Result.GetResult(Result.errCode, Result.errMsg, "");
            #endregion
        }
    }
}
