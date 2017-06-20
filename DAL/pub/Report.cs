using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL.database;

namespace DAL.pub
{
   public  class Report
    {
       DBOperate dbOperate = new DBOperate();

       /// <summary>
       /// 获取广告位总量信息
       /// </summary>
       /// <param name="adLocationId"></param>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       public DataTable GetAdLocationSumDT(string adLocationId, string startTime, string endTime)
       {
            string sql=string.Format(@"       
                             select nvl(sum(s1.reveal_cnt),0) as showcnt,
                                    nvl(sum(s1.click_cnt),0) as clickcnt,
                                    nvl(sum(income),0) as incomesum
                               from bee.BEE_Rpt_StatByAdlD s1
                              where s1.adlocid = {0}
                                and s1.dateid between {1} and {2}", adLocationId, startTime, endTime);          
           DataTable dt = dbOperate.GetDataTable(sql);
           return dt;
       }
       /// <summary>
       /// 获取广告总量信息，含展示量和点击量
       /// </summary>
       /// <param name="adId"></param>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       public DataTable GetAdSumDT(string adId, string startTime, string endTime)
       {
           string sql = string.Format(@"select nvl(sum(s1.reveal_cnt), 0) as showcnt,
                                     nvl(sum(s1.click_cnt), 0) as clickcnt,
                                     nvl(sum(income), 0) as incomesum
                                from bee.BEE_Rpt_StatByAdvD s1
                               where s1.adid = {0}
                                 and s1.dateid between {1} and
                                     {2}", adId, startTime, endTime);
           DataTable dt = dbOperate.GetDataTable(sql);
           return dt;
       }

       
       /// <summary>
       /// 获取物料总量信息
       /// </summary>
       /// <param name="materialId"></param>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       public DataTable GetMaterialSumDT(string materialId, string startTime, string endTime)
       {
           string sql = string.Format(@"  
                                     select nvl(sum(s1.reveal_cnt), 0) as showcnt,
                                     nvl(sum(s1.click_cnt), 0) as clickcnt,
                                     nvl(sum(income), 0) as incomesum
                                from bee.BEE_Rpt_StatBymatD s1
                               where s1.matid = {0}
                                 and s1.dateid between {1} and
                                     {2}", materialId, startTime, endTime);
           DataTable dt = dbOperate.GetDataTable(sql);
           return dt;
       }
       

       /// <summary>
       /// 获取广告位明细数据-按日期
       /// </summary>
       /// <param name="adLocationId"></param>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       public DataTable GetAdLocationListByDayDT(string adLocationId, string startTime, string endTime)
       {
           string sql = string.Format(@"
                                    select s1.dateid,
                                    s1.reveal_cnt as showcnt,
                                    s1.click_cnt as clickcnt,
                                    s1.income as incomesum,
                                    s1.ask_cnt as requestcnt
                               from bee.BEE_Rpt_StatByAdlD s1
                              where s1.adlocid = {0}
                                and s1.dateid between {1} and {2}
                              order by s1.dateid asc", adLocationId, startTime, endTime);
           DataTable dt = dbOperate.GetDataTable(sql);
           return dt;
       }
       /// <summary>
       /// 获取广告位明细数据-按时段
       /// </summary>
       /// <param name="adLocationId"></param>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       public DataTable GetAdLocationListByHourDT(string adLocationId, string startTime, string endTime)
       {
           string sql = string.Format(@"select s1.hourid,
                                     nvl(sum(s1.reveal_cnt), 0) as showcnt,
                                     nvl(sum(s1.click_cnt), 0) as clickcnt,
                                     nvl(sum(s1.income), 0) as incomesum,
                                     nvl(sum(s1.ask_cnt), 0) as requestcnt
                                from bee.BEE_Rpt_StatByAdlh s1
                               where s1.adlocid ={0}
                                 and s1.dateid between {1} and
                                     {2}
                               group by s1.hourid
                               order by s1.hourid asc", adLocationId, startTime, endTime);
           DataTable dt = dbOperate.GetDataTable(sql);
           return dt;
       }
       /// <summary>
       /// 获取广告位明细数据-按分类
       /// </summary>
       /// <param name="adLocationId"></param>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       public DataTable GetAdLocationListByClassDT(string adLocationId, string startTime, string endTime)
       {
           string sql = string.Format(@" select s2.classname,
                                     nvl(sum(s1.reveal_cnt), 0) as showcnt,
                                     nvl(sum(s1.click_cnt), 0) as clickcnt,
                                     nvl(sum(s1.income), 0) as incomesum,
                                     nvl(sum(s1.ask_cnt), 0) as requestcnt
                                from bee.BEE_Rpt_StatByClassAdlD s1,
                                     bee.bee_classinfo           s2
                               where s1.adlocid ={0}
                                 and s1.dateid between {1} and
                                     {2}
                                 and s1.class1 = s2.classid
                               group by s2.classname
                               order by s2.classname asc", adLocationId, startTime, endTime);
           DataTable dt = dbOperate.GetDataTable(sql);
           return dt;
       }
       /// <summary>
       /// 获取广告位明细数据-按地域
       /// </summary>
       /// <param name="adLocationId"></param>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       public DataTable GetAdLocationListByAreaDT(string adLocationId, string startTime, string endTime)
       {
           string sql = string.Format(@"   select s1.area as areaname,
                                      nvl(sum(s1.reveal_cnt), 0) as showcnt,
                                      nvl(sum(s1.click_cnt), 0) as clickcnt,
                                      nvl(sum(s1.income), 0) as incomesum,
                                      nvl(sum(s1.ask_cnt), 0) as requestcnt
                                 from bee.BEE_Rpt_StatByareaAdlD s1
                                where s1.adlocid ={0}
                                 and s1.dateid between {1} and
                                     {2}                             
                                group by s1.area
                                order by s1.area asc", adLocationId, startTime, endTime);
           DataTable dt = dbOperate.GetDataTable(sql);
           return dt;
       }

       /// <summary>
       /// 获取广告明细数据-按日期
       /// </summary>
       /// <param name="adLocationId"></param>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       public DataTable GetAdListByDayDT(string adId, string startTime, string endTime)
       {
           string sql = string.Format(@"select s1.dateid,
                                    s1.reveal_cnt as showcnt,
                                    s1.click_cnt as clickcnt,
                                    s1.income as incomesum
                               from bee.BEE_Rpt_StatByAdvD s1
                              where s1.adid = {0}
                                and s1.dateid between {1} and {2}
                              order by s1.dateid asc", adId, startTime, endTime);
           DataTable dt = dbOperate.GetDataTable(sql);
           return dt;
       }
       /// <summary>
       /// 获取广告明细数据-按小时
       /// </summary>
       /// <param name="materialId"></param>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       public DataTable GetAdListByHourDT(string adId, string startTime, string endTime)
       {
           string sql = string.Format(@" select s1.hourid,
                                      nvl(sum(s1.reveal_cnt), 0) as showcnt,
                                      nvl(sum(s1.click_cnt), 0) as clickcnt,
                                      nvl(sum(s1.income), 0) as incomesum
                                 from bee.BEE_Rpt_StatByAdvh s1
                                where s1.adid = {0}
                                and s1.dateid between {1} and {2}
                                group by s1.hourid
                                order by s1.hourid asc", adId, startTime, endTime);
           DataTable dt = dbOperate.GetDataTable(sql);
           return dt;
       }
       /// <summary>
       /// 获取广告明细数据-按分类
       /// </summary>
       /// <param name="materialId"></param>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       public DataTable GetAdListByClassDT(string adId, string startTime, string endTime)
       {
           string sql = string.Format(@"select s2.classname,
                                     nvl(sum(s1.reveal_cnt), 0) as showcnt,
                                     nvl(sum(s1.click_cnt), 0) as clickcnt,
                                     nvl(sum(s1.income), 0) as incomesum
                                from bee.BEE_Rpt_StatByClassAdvD s1,
                                     bee.bee_classinfo           s2
                               where s1.adid = {0}
                                and s1.dateid between {1} and {2}
                                 and s1.class1 = s2.classid
                               group by s2.classname
                               order by s2.classname asc", adId, startTime, endTime);
           DataTable dt = dbOperate.GetDataTable(sql);
           return dt;
       }
       /// <summary>
       /// 获取广告明细数据-按地域
       /// </summary>
       /// <param name="adId"></param>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       public DataTable GetAdListByAreaDT(string adId, string startTime, string endTime)
       {
           string sql = string.Format(@"select s1.area as areaname,
                                      nvl(sum(s1.reveal_cnt), 0) as showcnt,
                                      nvl(sum(s1.click_cnt), 0) as clickcnt,
                                      nvl(sum(s1.income), 0) as incomesum
                                 from bee.BEE_Rpt_StatByareaAdvD s1
                                where s1.adid = {0}
                                and s1.dateid between {1} and {2}      
                                group by s1.area
                                order by s1.area asc", adId, startTime, endTime);
           DataTable dt = dbOperate.GetDataTable(sql);
           return dt;
       }
       /// <summary>
       /// 获取物料明细数据-按日期
       /// </summary>
       /// <param name="adLocationId"></param>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       public DataTable GetMaterialListByDayDT(string materialId, string startTime, string endTime)
       {
           string sql = string.Format(@"select s1.dateid,
                                    s1.reveal_cnt as showcnt,
                                    s1.click_cnt as clickcnt,
                                    s1.income as incomesum
                               from bee.BEE_Rpt_StatBymatD s1
                              where s1.matid = {0}
                                and s1.dateid between {1} and {2}
                              order by s1.dateid asc", materialId, startTime, endTime);
           DataTable dt = dbOperate.GetDataTable(sql);
           return dt;
       }
       /// <summary>
       /// 获取物料明细数据-按小时
       /// </summary>
       /// <param name="materialId"></param>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       public DataTable GetMaterialListByHourDT(string materialId, string startTime, string endTime)
       {
           string sql = string.Format(@"select s1.hourid,
                                      nvl(sum(s1.reveal_cnt), 0) as showcnt,
                                      nvl(sum(s1.click_cnt), 0) as clickcnt,
                                      nvl(sum(s1.income), 0) as incomesum
                                 from bee.BEE_Rpt_StatBymath s1
                                where s1.matid = {0}
                                and s1.dateid between {1} and {2}
                                group by s1.hourid
                                order by s1.hourid asc", materialId, startTime, endTime);
           DataTable dt = dbOperate.GetDataTable(sql);
           return dt;
       }
       /// <summary>
       /// 获取物料明细数据-按分类
       /// </summary>
       /// <param name="materialId"></param>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       public DataTable GetMaterialListByClassDT(string materialId, string startTime, string endTime)
       {
           string sql = string.Format(@"  select s2.classname,
                                     nvl(sum(s1.reveal_cnt), 0) as showcnt,
                                     nvl(sum(s1.click_cnt), 0) as clickcnt,
                                     nvl(sum(s1.income), 0) as incomesum
                                from bee.BEE_Rpt_StatByClassmatD s1,
                                     bee.bee_classinfo           s2
                               where s1.matid = {0}
                                 and s1.dateid between {1} and {2}
                                 and s1.class1 = s2.classid
                               group by s2.classname
                               order by s2.classname asc", materialId, startTime, endTime);
           DataTable dt = dbOperate.GetDataTable(sql);
           return dt;
       }
       /// <summary>
       /// 获取指定广告位详情
       /// </summary>
       /// <param name="adLocationId"></param>
       /// <returns></returns>
       public DataTable GetAdLocationDT(string adLocationId)
       {
           string sql = string.Format(@"  select * from bee_adlocationinfo where adlocationid={0}", adLocationId);
           DataTable dt = dbOperate.GetDataTable(sql);
           return dt;
       }
       /// <summary>
       /// 获取指定广告详情
       /// </summary>
       /// <param name="adId"></param>
       /// <returns></returns>
       public DataTable GetAdDT(string adId)
       {
           string sql = string.Format(@"  select * from bee_adinfo where adid={0}", adId);
           DataTable dt = dbOperate.GetDataTable(sql);
           return dt;
       }
       /// <summary>
       /// 获取指定物料详情
       /// </summary>
       /// <param name="materialId"></param>
       /// <returns></returns>
       public DataTable GetMaterialDT(string materialId)
       {
           string sql = string.Format(@"  select * from bee_materialinfo where materialid={0}", materialId);
           DataTable dt = dbOperate.GetDataTable(sql);
           return dt;
       }
       //---------------------------广告主报表数据------------------------------------//
       /// <summary>
       /// 获取广告主剩余金额 截至到昨天
       /// </summary>
       /// <param name="aduserid"></param>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       public DataTable GetBalanceOfAduserDT(string adUserId)
       {
           string sql = string.Format(@"select s1.balance from bee_aduserinfo s1 where s1.aduserid={0}", adUserId);
           DataTable dt = dbOperate.GetDataTable(sql);
           return dt;
       }
       
       /// <summary>
       /// 获取广告主点击量及支出
       /// </summary>
       /// <param name="aduserId"></param>
       /// <returns></returns>
       public DataTable GetADSumOfAduserDT(string adUserId)
       {
           string sql = string.Format(@"select nvl(sum(s1.click_cnt), 0) as clickcnt,
       nvl(sum(s1.income), 0) as deductsum
  from bee.BEE_Rpt_StatByAdvD s1
 where s1.dateid = to_number(to_char(sysdate - 1, 'yyyymmdd'))
   and s1.aduserid = {0}", adUserId);
           DataTable dt = dbOperate.GetDataTable(sql);
           return dt;
       }
       /// <summary>
       /// 获取广告主点击量及支出-按时间
       /// </summary>
       /// <param name="aduserId"></param>
       /// <returns></returns>
       public DataTable GetADSumOfAduserByTimeDT(string adUserId,string startTime,string endTime)
       {
           string sql = string.Format(@"  select nvl(sum(s1.click_cnt), 0) as clickcnt,
       nvl(sum(s1.income), 0) as deductsum
  from bee.BEE_Rpt_StatByAdvD s1
 where s1.dateid < to_number(to_char(sysdate, 'yyyymmdd'))
 and s1.aduserid = {0} 
   and s1.dateid between {1} and {2}
  ", adUserId, startTime, endTime);
           DataTable dt = dbOperate.GetDataTable(sql);
           return dt;
       }
       /// <summary>
       /// 获取广告主报表明细数据-按日期
       /// </summary>
       /// <param name="adUserId"></param>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       public DataTable GetAdListOfAdUserByDayDT(string adUserId, string startTime, string endTime)
       {
           string sql = string.Format(@"select s1.dateid,
       s1.aduserid,
       nvl(sum(s1.click_cnt), 0) as clickcnt,
       nvl(sum(s1.income), 0) as deductsum
  from bee.BEE_Rpt_StatByAdvD s1
 where s1.dateid < to_number(to_char(sysdate, 'yyyymmdd'))
   and s1.aduserid = {0}
   and s1.dateid between {1} and {2}
 group by s1.dateid, aduserid
 order by s1.dateid asc", adUserId, startTime, endTime);
           DataTable dt = dbOperate.GetDataTable(sql);
           return dt;
       }
       /// <summary>
       /// 获取广告主报表明细数据-按小时
       /// </summary>
       /// <param name="materialId"></param>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       //public DataTable GetAdListOfAdUserByHourDT(string adUserId, string startTime, string endTime)
       //{
       //    string sql = string.Format(@"  select *
       //                               from (select 
       //                                            hourid,
       //                                            nvl(showcnt, 0) as showcnt,
       //                                            nvl(clickcnt, 0) as clickcnt
       //                                       from (select  hourid, actionid, sum(summary) as summary
       //                                               from bee_channelhourstat@stat s1,
       //                                                    bee_aduserinfo          s2,
       //                                                    bee_adinfo              s3
       //                                              where s1.advertid = s3.adid
       //                                                and s2.aduserid = s3.aduserid
       //                                                and s2.aduserid = {0}
       //                                                and s1.dateid between {1} and {2}
       //                                              group by  hourid, actionid) pivot(max(summary) for actionid in(2 as
       //                                                                                                                    showcnt,
       //                                                                                                                    3 as
       //                                                                                                                    clickcnt)))
       //                                         order by  hourid asc", adUserId, startTime, endTime);
       //    DataTable dt = dbOperate.GetDataTable(sql);
       //    return dt;
       //}
       /// <summary>
       /// 获取广告主报表明细数据-按分类
       /// </summary>
       /// <param name="materialId"></param>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       //public DataTable GetAdListOfAdUserByClassDT(string adUserId, string startTime, string endTime)
       //{
       //    string sql = string.Format(@" select *
       //                               from (select classname,
       //                                            nvl(showcnt, 0) as showcnt,
       //                                            nvl(clickcnt, 0) as clickcnt
       //                                       from (select s2.classname, actionid, sum(summary) as summary
       //                                               from bee_channelhourstat@stat s1,
       //                                                    pmh_videoclassconfig    s2,
       //                                                    bee_aduserinfo          s3,
       //                                                    bee_adinfo              s4
       //                                              where s1.advertid = s4.adid
       //                                                and s3.aduserid = s4.aduserid
       //                                                and s3.aduserid = {0}
       //                                                and s1.dateid between {1} and {2}
       //                                                and s1.channelid(+) = s2.classid
       //                                              group by channelid, classname, actionid) pivot(max(summary) for actionid in(2 as
       //                                                                                                                          showcnt,
       //                                                                                                                          3 as
       //                                                                                                                          clickcnt)))
       //                              order by classname asc", adUserId, startTime, endTime);
       //    DataTable dt = dbOperate.GetDataTable(sql);
       //    return dt;
       //}
       /// <summary>
       /// 获取广告主报表明细数据-按地域
       /// </summary>
       /// <param name="adId"></param>
       /// <param name="startTime"></param>
       /// <param name="endTime"></param>
       /// <returns></returns>
       //public DataTable GetAdListOfAdUserByAreaDT(string adUserId, string startTime, string endTime)
       //{
       //    string sql = string.Format(@"   select p2.province as areaname,       
       //                            nvl(showcnt, 0) as showcnt,
       //                            nvl(clickcnt, 0) as clickcnt
       //                       from (select *
       //                               from (select province, actionid, sum(summary) as summary
       //                                       from (select dateid, advertid, actionid, province, summary
       //                                               from (select dateid,
                                       
       //                                                            advertid,
       //                                                            actionid,
       //                                                            sum(XZ) as XZ,
       //                                                            sum(TJ) as TJ,
       //                                                            sum(TW) as TW,
       //                                                            sum(SC) as SC,
       //                                                            sum(SH) as SH,
       //                                                            sum(SN) as SN,
       //                                                            sum(SX) as SX,
       //                                                            sum(SD) as SD,
       //                                                            sum(QH) as QH,
       //                                                            sum(NX) as NX,
       //                                                            sum(NM) as NM,
       //                                                            sum(LN) as LN,
       //                                                            sum(JX) as JX,
       //                                                            sum(JS) as JS,
       //                                                            sum(JL) as JL,
       //                                                            sum(HN) as HN,
       //                                                            sum(HB) as HB,
       //                                                            sum(HL) as HL,
       //                                                            sum(HA) as HA,
       //                                                            sum(HE) as HE,
       //                                                            sum(HI) as HI,
       //                                                            sum(GZ) as GZ,
       //                                                            sum(GX) as GX,
       //                                                            sum(GD) as GD,
       //                                                            sum(GS) as GS,
       //                                                            sum(FJ) as FJ,
       //                                                            sum(BJ) as BJ,
       //                                                            sum(MO) as MO,
       //                                                            sum(AH) as AH,
       //                                                            sum(HK) as HK,
       //                                                            sum(XJ) as XJ,
       //                                                            sum(YN) as YN,
       //                                                            sum(ZJ) as ZJ,
       //                                                            sum(ZH) as ZH,
       //                                                            sum(CQ) as CQ,
       //                                                            sum(QT) as QT
       //                                                       from bee_adverthourstat@stat s1,bee_aduserinfo s2,bee_adinfo s3
       //                                                      where s1.actionid in (2, 3)
       //                                                            and s1.advertid=s3.adid
       //                                                            and s2.aduserid=s3.aduserid
       //                                                            and s2.aduserid={0}
       //                                                            and s1. dateid between {1} and {2}
       //                                                      group by s1.dateid, s1.advertid, s1.actionid) unpivot(summary for province in (XZ, TJ, TW, SC, SH, SN, SX, SD, QH, NX, NM, LN, JX, JS, JL, HN, HB, HL, HA, HE, HI, GZ, GX, GD, GS, FJ, BJ, MO, AH, HK, XJ, YN, ZJ, ZH, CQ, QT)))                                              
       //                                      group by province, actionid) pivot(max(summary) for actionid in (2 as showcnt, 3 as clickcnt))) p1,
       //                            bee_province_shortcut p2
       //                      where p1.province = p2.shortcut
       //                      order by p2.province asc", adUserId, startTime, endTime);
       //    DataTable dt = dbOperate.GetDataTable(sql);
       //    return dt;
       //}

    }
}
