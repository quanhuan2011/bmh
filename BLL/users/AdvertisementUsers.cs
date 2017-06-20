using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.pub;
using DAL.database;
using System.Data;
using Common.pub;

namespace BLL.users
{
   public  class AdvertisementUsers
   {
       ConvertData cData = new ConvertData();
       DBOperate dbOperate = new DBOperate();
       /// <summary>
       /// 获取广告消费列表返回DataTable
       /// </summary>
       /// <param name="pageSize"></param>
       /// <param name="pageNo"></param>
       /// <param name="sqlWhere"></param>
       /// <param name="pageCount"></param>
       /// <returns></returns>
       public DataTable GetAdDeductListDT(string adUserId, string startTime, string endTime, int pageSize, int pageNo, string sqlWhere, out int pageCount)
       {
           #region 定义变量
           DataTable dt = null;
           pageCount = 0;
           string sql = string.Format(@"select p1.adid, p1.adname, p2.clickcnt, p2.deductsum
  from (select s1.adid, s1.name as adname
          from bee.bee_adinfo s1
         where s1.aduserid = {0}) p1,
       (select s1.adid,
               nvl(sum(s1.click_cnt), 0) as clickcnt,
               nvl(sum(s1.income), 0) as deductsum
          from bee.bee_rpt_statbyadvd s1
         where s1.dateid < to_number(to_char(sysdate, 'yyyymmdd'))
           and s1.aduserid = {0}
           and s1.dateid between {1} and {2}
         group by s1.adid) p2
 where p1.adid = p2.adid", adUserId,startTime,endTime);
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
   }
}
