using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BLL.pub;
using DAL.database;
using Common.pub;

namespace BLL.users
{
    public  class AdUserUsers
    {
        ConvertData cData = new ConvertData();
        DBOperate dbOperate = new DBOperate();
        /// <summary>
        /// 获取充值记录列表返回DataTable
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable GetReChargeListDT(string adUserId, int pageSize, int pageNo, string sqlWhere, out int pageCount)
        {
            #region 定义变量
            DataTable dt = null;
            pageCount = 0;
            string sql = string.Format(@"select detailid, money, remark, createtime
                                              from bee.bee_rechargedetail s1
                                             where  s1.status = 1 and s1.aduserid={0}",adUserId);
            string orderBy = " order by s1.detailid desc";
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
        /// 获取广告主剩余金额
        /// </summary>
        /// <param name="aduserid"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable GetBalanceOfAduserDT(string adUserId)
        {
            string sql = string.Format(@"select nvl(p1.summoney, 0) - nvl(p2.deductmoney, 0) as balance
                                  from (select s1.aduserid, nvl(sum(s1.money),0) as summoney
                                          from bee_rechargedetail s1
                                         where  s1.aduserid={0}
                                         group by s1.aduserid) p1,
                                       (select s1.aduserid, nvl(sum(cnt * price),0) as deductmoney
                                          from bee_clickhourstatareaos@stat s1
                                         where s1.aduserid = {0}                                           
                                         group by aduserid) p2
                                 where p1.aduserid = p2.aduserid(+)", adUserId);
            DataTable dt = dbOperate.GetDataTable(sql);
            return dt;
        }
        /// <summary>
        /// 获取广告主点击量及支出
        /// </summary>
        /// <param name="aduserId"></param>
        /// <returns></returns>
        public DataTable GetADSumOfAduserDT(string adUserId,string startTime,string endTime)
        {
            string sql = string.Format(@" select nvl(sum(click_cnt), 0) as clickcnt,
                                           nvl(sum(income), 0) as deductsum
                                      from bee.bee_rpt_statbyadvd s1
                                     where s1.dateid <
                                           to_number(to_char(sysdate,
                                                             'yyyymmdd'))
                                       and s1.aduserid = {0}
                                       and s1.dateid between {1} and
                                           {2}", adUserId,startTime,endTime);
            DataTable dt = dbOperate.GetDataTable(sql);
            return dt;
        }
    }
}
