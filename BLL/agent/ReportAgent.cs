using BLL.pub;
using Common.pub;
using DAL.database;
using DAL.pub;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.agent
{
    /// <summary>
    /// 代理商数据
    /// </summary>
    public class ReportAgent
    {
        ConvertData cData = new ConvertData();
        DBOperate dbOperate = new DBOperate();
        public float GetAdSumOfAdUser(string adUserId)
        {

            #region 定义数据
            Report report = new Report();
            float balanceSum = 0.00f;
            #endregion

            #region 获取数据
            try
            {
                DataTable dt = report.GetBalanceOfAduserDT(adUserId);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        balanceSum = float.Parse(dt.Rows[0]["balance"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {

                LogApi.DebugInfo(ex);
            }
            #endregion

            return balanceSum;
        }
        public float GetRebateSumb(string adUserId)
        {

            #region 定义数据
            Report report = new Report();
            float rebateSum = 0.00f;
            #endregion

            #region 获取数据
            try
            {
                DataTable dt = GetRebateSumbDT(adUserId);
                if (dt != null && dt.Rows.Count > 0)
                {

                    rebateSum = float.Parse(dt.Rows[0]["rebatesum"].ToString());

                }
            }
            catch (Exception ex)
            {

                LogApi.DebugInfo(ex);
            }
            #endregion

            return rebateSum;
        }
        /// <summary>
        /// 获取本月返点和上月返点
        /// </summary>
        /// <param name="adUserId"></param>
        /// <returns></returns>
        public DataTable GetRebateSumbDT(string adUserId)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"select (p2.maxrbtamt - p1.maxrbtamt) as rbtamt,
       (p1.maxrbtamt - p0.maxrbtamt) as lastrbtamt
  from (select nvl(max(s1.rbtamt), 0) as maxrbtamt
          from bee_rebate_money s1
         where substr(s1.dateid, 0, 6) =
               to_char(add_months(trunc(sysdate), -2), 'yyyymm')
           and s1.aduserid = {0}) p0,
       (select nvl(max(s1.rbtamt), 0) as maxrbtamt
          from bee_rebate_money s1
         where substr(s1.dateid, 0, 6) =
               to_char(add_months(trunc(sysdate), -1), 'yyyymm')
           and s1.aduserid = {0}) p1,
       (select nvl(max(s1.rbtamt), 0) as maxrbtamt
          from bee_rebate_money s1
         where substr(s1.dateid, 0, 6) =
               to_char(add_months(trunc(sysdate), 0), 'yyyymm')
           and s1.aduserid = {0}) p2
", adUserId);
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
        /// 获取本月消费和上月消费
        /// </summary>
        /// <param name="adUserId"></param>
        /// <returns></returns>
        public DataTable GetDeductSumbDT(string adUserId)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"select s1.aduserid, nvl(sum(s1.income), 0) as incomesum
  from bee.bee_rpt_statbyadvh s1
 where substr(s1.dateid, 0, 6)= to_char(add_months(trunc(sysdate), 0), 'yyyymm')
   and s1.aduserid = {0}
 group by aduserid
", adUserId);
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
        public DataTable GetLastDeductSumbDT(string adUserId)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"select s1.aduserid, nvl(sum(s1.income), 0) as incomesum
  from bee.bee_rpt_statbyadvh s1
 where substr(s1.dateid, 0, 6)= to_char(add_months(trunc(sysdate), -1), 'yyyymm')
   and s1.aduserid = {0}
 group by aduserid
", adUserId);
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
        /// 获取链接top10数据
        /// </summary>
        /// <param name="adUserId"></param>
        /// <returns></returns>
        public DataTable GetLinkUrlTop10DT(string adUserId)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"
select p1.linkurl,
       p1.clickcnt,
      decode(p2.clickcntsum,0,0,round(p1.clickcnt / p2.clickcntsum, 4)*100)||'%'  as clickrto
  from (select k1.*, rownum as rowno
          from (select s1.aduserid,
                       s1.linkurl,
                       nvl(sum(s1.click_cnt), 0) as clickcnt
                  from bee.bee_rpt_statbylkurlh s1
                 where s1.dateid = to_char(trunc(sysdate) - 1, 'yyyymmdd')
                   and s1.aduserid ={0}
                 group by s1.aduserid, s1.linkurl
                 order by nvl(sum(s1.click_cnt), 0) desc) k1) p1,
       (select s1.aduserid, nvl(sum(s1.click_cnt), 0) as clickcntsum
          from bee.bee_rpt_statbylkurlh s1
         where s1.dateid = to_char(trunc(sysdate) - 1, 'yyyymmdd')
           and s1.aduserid = {0}
         group by s1.aduserid) p2
 where p1.aduserid = p2.aduserid
   and p1.rowno < 11
 order by    decode(p2.clickcntsum,0,0,round(p1.clickcnt / p2.clickcntsum, 4)) desc
", adUserId);
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
        /// 获取明细数据按日
        /// </summary>
        /// <returns></returns>
        public DataTable GetDetailDataByDay(string adUserId, string dateId)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"select dateid, hourid, sum(click_cnt) as clickcnt, sum(income) as incomesum
                                          from bee.bee_rpt_statbyadvh s1
                                         where s1.aduserid = {0}
                                           and dateid ={1}
                                         group by dateid, hourid
                                         order by hourid", adUserId, dateId);
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

        public string GetDetailData(string adUserId)
        {

            //  List<AdLocationByPage> listAdLocation = new List<AdLocationByPage>();          
            List<string> tClicks = new List<string>();
            List<string> tIncomes = new List<string>();
            List<string> yClicks = new List<string>();
            List<string> yIncomes = new List<string>();
            decimal yIncome = 0;
            int yClick = 0;
            decimal tIncome = 0;
            int tClick = 0;
            decimal eIncome = 0;
            int eClick = 0;
            try
            {
                DateTime time = DateTime.Now;
                //昨日数据
                DataTable dt1 = GetDetailDataByDay(adUserId, time.AddDays(-1).ToString("yyyyMMdd"));
                //今日数据
                DataTable dt2 = GetDetailDataByDay(adUserId, time.ToString("yyyyMMdd"));
                //昨日列表数据
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        for (int j = 0; j < (i == 0 ? int.Parse(dt1.Rows[i]["hourid"].ToString()) : int.Parse(dt1.Rows[i]["hourid"].ToString()) - int.Parse(dt1.Rows[i - 1]["hourid"].ToString()) - 1); j++)
                        {
                            yClicks.Add("");
                            yIncomes.Add("");
                        }
                        yClicks.Add(dt1.Rows[i]["clickcnt"].ToString());
                        yIncomes.Add(dt1.Rows[i]["incomesum"].ToString());
                        yClick += int.Parse(dt1.Rows[i]["clickcnt"].ToString());
                        yIncome += decimal.Parse(dt1.Rows[i]["incomesum"].ToString());
                    }

                }               
                //今日列表数据
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        for (int j = 0; j < (i == 0 ? int.Parse(dt2.Rows[i]["hourid"].ToString()) : int.Parse(dt2.Rows[i]["hourid"].ToString()) - int.Parse(dt2.Rows[i - 1]["hourid"].ToString()) - 1); j++)
                        {
                            tClicks.Add("");
                            tIncomes.Add("");
                        }
                        tClicks.Add(dt2.Rows[i]["clickcnt"].ToString());
                        tIncomes.Add(dt2.Rows[i]["incomesum"].ToString());
                        tClick += int.Parse(dt2.Rows[i]["clickcnt"].ToString());
                        tIncome += decimal.Parse(dt2.Rows[i]["incomesum"].ToString());
                    }
                }             
                //计算预计点击数据
                //如果昨日和今日都不为空
                if (yClicks.Count > 0 && tClicks.Count > 0)
                {
                    //昨日与今日能匹配上的小时总数
                    int ySumClick = 0, tSumClick = 0, eSumClick = 0, yHours = 0, tHours = 0, tAllClick = 0;
                    for (int i = 0; i < ((tClicks.Count< yClicks.Count)? tClicks.Count: yClicks.Count); i++)
                    {
                        //昨日与今日小时数据都有的情况下
                        if (!string.IsNullOrWhiteSpace(tClicks[i].ToString()))
                        {
                            if (!string.IsNullOrWhiteSpace(yClicks[i].ToString()))
                            {
                                ySumClick += int.Parse(yClicks[i].ToString());
                                tSumClick += int.Parse(tClicks[i].ToString());
                            }
                            //今日小时有数据的小时数
                            tHours++;
                            tAllClick += int.Parse(tClicks[i].ToString());
                        }
                    }
                    //今日较昨日求出剩下小时数的数据
                    if (ySumClick > 0 && tSumClick > 0)
                    {
                        for (int i = tClicks.Count; i < yClicks.Count; i++)
                        {
                            if (!string.IsNullOrWhiteSpace(yClicks[i].ToString()))
                            {
                                eSumClick += int.Parse(yClicks[i].ToString());
                            }
                            else
                            {
                                yHours++;
                            }
                        }
                        //如果昨日还有小时投放数据 则今日有交叉小时按比例计算
                        if (eSumClick > 0)
                        {
                            eSumClick = (int)(eSumClick * ((decimal)tSumClick / (decimal)ySumClick));
                        }
                    }
                    if (yHours > 0)
                    {
                        //今日每个小时的平均*昨日没有数据小时数
                        eSumClick += (int)((decimal)tAllClick / (decimal)tHours) * yHours;
                    }
                    //现有的+预计的数据
                    eClick = tClick + eSumClick;
                }
                //计算预计消费数据 规则与点击一致
                //如果昨日和今日都不为空
                if (yIncomes.Count > 0 && tIncomes.Count > 0)
                {
                    //昨日与今日能匹配上的小时总数
                    decimal ySumIncome = 0, tSumIncome = 0, eSumIncome = 0, yHours = 0, tHours = 0, tAllIncome = 0;

                    for (int i = 0; i < ((tIncomes.Count < yIncomes.Count) ? tIncomes.Count : yIncomes.Count); i++)
                    {
                        //昨日与今日小时数据都有的情况下
                        if (!string.IsNullOrWhiteSpace(tIncomes[i].ToString()))
                        {
                            if (!string.IsNullOrWhiteSpace(yIncomes[i].ToString()))
                            {
                                ySumIncome += decimal.Parse(yIncomes[i].ToString());
                                tSumIncome += decimal.Parse(tIncomes[i].ToString());
                            }
                            //今日小时有数据的小时数
                            tHours++;
                            tAllIncome += decimal.Parse(tIncomes[i].ToString());
                        }


                    }                
                    //今日较昨日求出剩下小时数的数据
                    if (ySumIncome > 0 && tSumIncome > 0)
                    {
                        for (int i = tIncomes.Count; i < yIncomes.Count; i++)
                        {
                            if (!string.IsNullOrWhiteSpace(yIncomes[i].ToString()))
                            {
                                eSumIncome += decimal.Parse(yIncomes[i].ToString());
                            }
                            else
                            {
                                yHours++;
                            }
                        }
                        //如果昨日还有小时投放数据 则今日有交叉小时按比例计算
                        if (eSumIncome > 0)
                        {
                            eSumIncome = eSumIncome * (tSumIncome / ySumIncome);
                        }
                    }
                    if (yHours > 0)
                    {
                        //今日每个小时的平均*昨日没有数据小时数
                        eSumIncome += (tAllIncome / tHours) * yHours;
                    }
                    //现有的+预计的数据
                    eIncome = tIncome + eSumIncome;
                }
                Result.errCode = "1";
                Result.errMsg = "获取成功";
            }
            catch (Exception ex)
            {
                // listAdLocation = null;
                Result.errCode = "-1";
                Result.errMsg = ex.Message;
                LogApi.DebugInfo(ex);
            }

            #region 组装数据
            var today = new
            {
                click = tClicks,
                income = tIncomes
            };
            var yesterday = new
            {
                click = yClicks,
                income = yIncomes
            };
            var tempData = new
            {
                yclick = yClick,
                yincome = yIncome,
                tclick = tClick,
                tincome = tIncome,
                eclick = Convert.ToInt32(eClick),
                eincome = Convert.ToDouble(eIncome).ToString("f2"),
                today = today,
                yesterday = yesterday
            };
            #endregion

            #region 返回数据
            return Result.GetResult(Result.errCode, Result.errMsg, tempData);
            #endregion


        }
        /// <summary>
        /// 获取终端设备数据
        /// </summary>
        /// <param name="adUserId"></param>
        /// <returns></returns>
        public DataTable GetTermDetailData(string adUserId)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"select decode(termid, '1', 'H5', '2', 'APP', '其他') as name,
       sum(click_cnt) as value
  from bee.bee_rpt_statbytermh s1
 where s1.aduserid = {0} and 
s1.dateid = to_number(to_char(sysdate - 2, 'yyyymmdd'))
 group by termid
having(sum(click_cnt)) > 0
 order by sum(click_cnt) desc", adUserId);
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
        public DataTable GetOsDetailData(string adUserId)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"select ostype as name, sum(click_cnt) as value
                                      from bee.bee_rpt_statbyosh s1
                                     where s1.aduserid = {0} and 
                                     s1.dateid = to_number(to_char(sysdate - 2, 'yyyymmdd'))
                                     group by ostype
                                    having(sum(click_cnt)) > 0
                                     order by sum(click_cnt) desc", adUserId);
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
        public DataTable GetLinkUrlDetailData(string adUserId)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"select linkurl as name, sum(click_cnt) as value
  from bee.bee_rpt_statbylkurlh s1
 where s1.aduserid = {0}
   and s1.dateid = to_number(to_char(sysdate - 2, 'yyyymmdd'))
 group by linkurl
having(sum(click_cnt)) > 0
 order by sum(click_cnt) desc
", adUserId);
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
        /// 获取饼图数据
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public string GetDetailForPieData(string adUserId)
        {
            //  List<AdLocationByPage> listAdLocation = new List<AdLocationByPage>();
            List<AgentForPie> listOs = new List<AgentForPie>();
            List<AgentForPie> listTerm = new List<AgentForPie>();
            List<AgentForPie> listLinkUrl = new List<AgentForPie>();
            try
            {
                DataTable dt2 = GetOsDetailData(adUserId);
                DataTable dt1 = GetTermDetailData(adUserId);
                DataTable dt3 = GetLinkUrlDetailData(adUserId);
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    listTerm = cData.FillModel<AgentForPie>(dt1);
                }
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    listOs = cData.FillModel<AgentForPie>(dt2);
                }
                if (dt3 != null && dt3.Rows.Count > 0)
                {
                    listLinkUrl = cData.FillModel<AgentForPie>(dt3);
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
            #region 组装数据
            var tempData = new
            {
                osdata = listOs,
                termdata = listTerm,
                linkurldata = listLinkUrl
            };
            #endregion

            #region 返回数据
            return Result.GetResult(Result.errCode, Result.errMsg, tempData);
            #endregion


        }
        //public DataTable GetTermDetailData(string adUserId, string dateId)
        //{
        //    #region 定义变量
        //    DataTable dt = null;
        //    string sql = string.Format(@"select dateid, hourid, sum(click_cnt) as clickcnt, sum(income) as incomesum
        //                                  from bee.bee_rpt_statbyadvh s1
        //                                 where s1.aduserid = {0}
        //                                   and dateid ={1}
        //                                 group by dateid, hourid
        //                                 order by hourid", adUserId, dateId);
        //    #endregion

        //    #region 获取数据
        //    try
        //    {
        //        dt = dbOperate.GetDataTable(sql);
        //    }
        //    catch (Exception ex)
        //    {
        //        dt = null;
        //        LogApi.DebugInfo(ex);
        //    }
        //    #endregion

        //    #region 返回数据
        //    return dt;
        //    #endregion
        //}
        /// <summary>
        /// 获取消费明细-列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="adUserId"></param>
        /// <param name="sTime"></param>
        /// <param name="eTime"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable GetDeductListDT(int pageSize, int pageNo, string adUserId, string sTime, string eTime, out int pageCount)
        {
            #region 定义变量
            DataTable dt = null;
            pageCount = 0;
            string sql = "", orderBy = "";
            DateTime time = DateTime.Now;
            //查询类型
            int sType = 0;//0一天，1为多天
            if (string.IsNullOrWhiteSpace(sTime) || string.IsNullOrWhiteSpace(eTime))
                sTime = time.ToString("yyyyMMdd");
            else
            {
                if (int.Parse(eTime) > int.Parse(sTime))
                {
                    sType = 1;
                }
            }
            //查询类型
            if (sType == 0)
            {
                sql = string.Format(@"select hourid||'h' as orderid, sum(click_cnt) as clickcnt,sum(income) as incomesum
                      from bee.bee_rpt_statbyadvh s1
                     where s1.dateid = {0}
                       and s1.aduserid = {1}
                     group by hourid
                    ", sTime, adUserId);
                orderBy = " order by s1.hourid asc";
            }
            else
            {
                sql = string.Format(@" select dateid as orderid, sum(click_cnt) as clickcnt,sum(income) as incomesum
   from bee.bee_rpt_statbyadvh s1
  where s1.dateid between {0} and {1}
    and s1.aduserid = {2}
  group by dateid
                    ", sTime, eTime, adUserId);
                orderBy = " order by s1.dateid asc";
            }

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
        /// 获取消费明细-折线图
        /// </summary>
        /// <param name="adUserId"></param>
        /// <param name="sTime"></param>
        /// <param name="eTime"></param>
        /// <returns></returns>
        public DataTable GetDeductDetailDT(string adUserId, string sTime, string eTime, out int sType)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = "";
            //查询类型
            sType = 0;//0一天，1为多天
            if (string.IsNullOrWhiteSpace(sTime) || string.IsNullOrWhiteSpace(eTime))
                sTime = DateTime.Now.ToString("yyyyMMdd");
            else
            {
                if (int.Parse(eTime) > int.Parse(sTime))
                {
                    sType = 1;
                }
            }
            //查询类型
            if (sType == 0)
            {
                sql = string.Format(@"select hourid as orderid, sum(click_cnt) as clickcnt,sum(income) as incomesum
                      from bee.bee_rpt_statbyadvh s1
                     where s1.dateid = {0}
                       and s1.aduserid = {1}
                     group by hourid   order by s1.hourid asc
                    ", sTime, adUserId);

            }
            else
            {
                sql = string.Format(@" select dateid as orderid, sum(click_cnt) as clickcnt,sum(income) as incomesum
                           from bee.bee_rpt_statbyadvh s1
                          where s1.dateid between {0} and {1}
                            and s1.aduserid = {2}
                          group by dateid  order by s1.dateid asc", sTime, eTime, adUserId);
            }

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
        /// 获取消费详细-折线图
        /// </summary>
        /// <param name="adUserId"></param>
        /// <param name="sTime"></param>
        /// <param name="eTime"></param>
        /// <returns></returns>
        public string GetDeductDetail(string adUserId, string sTime, string eTime)
        {
            List<AgentDeductDetail> listDeduct = new List<AgentDeductDetail>();
            List<string> regionList = new List<string>();
            List<string> incomeList = new List<string>();
            int sType = 0;
            try
            {

                DataTable dt1 = GetDeductDetailDT(adUserId, sTime, eTime, out sType);
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    if (sType == 0)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            for (int j = 0; j < (i == 0 ? int.Parse(dt1.Rows[i]["orderid"].ToString()) : int.Parse(dt1.Rows[i]["orderid"].ToString()) - int.Parse(dt1.Rows[i - 1]["orderid"].ToString()) - 1); j++)
                            {
                                incomeList.Add("");
                            }
                            incomeList.Add(dt1.Rows[i]["incomesum"].ToString());
                        }
                    }
                    //多天
                    else
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            string tempTime = dt1.Rows[i]["orderid"].ToString();
                            string tempTime2 = i == 0 ? sTime : dt1.Rows[i - 1]["orderid"].ToString();
                            DateTime time1 = DateTime.Parse(tempTime.Substring(0, 4) + "-" + tempTime.Substring(4, 2) + "-" + tempTime.Substring(6, 2));
                            DateTime time2;
                            //起始则包含，起始往前一天
                            if (i == 0) 
                                time2 = DateTime.Parse(tempTime2.Substring(0, 4) + "-" + tempTime2.Substring(4, 2) + "-" + tempTime2.Substring(6, 2)).AddDays(-1);
                            else
                                time2 = DateTime.Parse(tempTime2.Substring(0, 4) + "-" + tempTime2.Substring(4, 2) + "-" + tempTime2.Substring(6, 2));
                            TimeSpan ts = time1 - time2;
                            if (ts.Days - 1 > 0)
                            {
                                for (int j = 0; j < ts.Days - 1; j++)
                                {
                                    incomeList.Add("");
                                }
                            }
                            incomeList.Add(dt1.Rows[i]["incomesum"].ToString());
                        }

                    }
                    if (sType == 0)
                    {
                        for (int i = 0; i < 24; i++)
                        {
                            regionList.Add(i.ToString() + "h");
                        }
                    }
                    else
                    {
                        DateTime time2 = DateTime.Parse(sTime.Substring(0, 4) + "-" + sTime.Substring(4, 2) + "-" + sTime.Substring(6, 2));
                        DateTime time1 = DateTime.Parse(eTime.Substring(0, 4) + "-" + eTime.Substring(4, 2) + "-" + eTime.Substring(6, 2));
                        TimeSpan ts = time1 - time2;
                        if (ts.Days > 0)
                        {
                            for (int i = 0; i <= ts.Days; i++)
                            {
                                regionList.Add(time2.AddDays(i).ToString("yyyyMMdd"));
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 24; i++)
                            {
                                regionList.Add(i.ToString() + "h");
                            }
                        }
                    }
                    //listDeduct = cData.FillModel<AgentDeductDetail>(dt1);
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

            var tempData = new
            {

                incomedata = incomeList,
                regiondata = regionList
            };

            #region 返回数据
            return Result.GetResult(Result.errCode, Result.errMsg, tempData);
            #endregion


        }


        public DataTable GetLinkUrlListDT(int pageSize, int pageNo, string adUserId, string sTime, string eTime, out int pageCount)
        {
            #region 定义变量
            DataTable dt = null;
            pageCount = 0;
            string sql = "", orderBy = "";
            DateTime time = DateTime.Now;
            //查询类型
            int sType = 0;//0一天，1为多天
            if (string.IsNullOrWhiteSpace(sTime) || string.IsNullOrWhiteSpace(eTime))
                sTime = time.ToString("yyyyMMdd");
            else
            {
                if (int.Parse(eTime) > int.Parse(sTime))
                {
                    sType = 1;
                }
            }
            //查询类型
            if (sType == 0)
            {
                sql = string.Format(@"select linkurl, sum(click_cnt) as clickcnt, sum(income) as incomesum
  from bee.bee_rpt_statbylkurlh s1
 where s1.dateid = {0}
   and s1.aduserid = {1}
 group by linkurl
                    ", sTime, adUserId);
                orderBy = " order by s1.linkurl asc";
            }
            else
            {
                sql = string.Format(@" select linkurl, sum(click_cnt) as clickcnt, sum(income) as incomesum
  from bee.bee_rpt_statbylkurlh s1
 where s1.dateid between {0} and {1}
   and s1.aduserid = {2}
 group by linkurl
                    ", sTime, eTime, adUserId);
                orderBy = " order by s1.linkurl asc";
            }

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
        public DataTable GetLinkUrlDetailListDT(string adUserId, string sTime, string eTime, out int sType)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = "";
            //查询类型
            sType = 0;//0一天，1为多天
            if (string.IsNullOrWhiteSpace(sTime) || string.IsNullOrWhiteSpace(eTime))
                sTime = DateTime.Now.ToString("yyyyMMdd");
            else
            {
                if (int.Parse(eTime) > int.Parse(sTime))
                {
                    sType = 1;
                }
            }
            //查询类型
            if (sType == 0)
            {
                sql = string.Format(@"select distinct linkurl
  from bee.bee_rpt_statbylkurlh s1
 where s1.dateid = {0}
   and s1.aduserid = {1} ", sTime, adUserId);

            }
            else
            {
                sql = string.Format(@" select distinct linkurl
                      from bee.bee_rpt_statbylkurlh s1
                     where s1.dateid between {0} and {1}
                       and s1.aduserid = {2}
                     ", sTime, eTime, adUserId);
            }

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
        public DataTable GetLinkUrlDetailDT(string adUserId, string sTime, string eTime, string linkUrl)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = "";
            //查询类型
            int sType = 0;//0一天，1为多天
            if (string.IsNullOrWhiteSpace(sTime) || string.IsNullOrWhiteSpace(eTime))
                sTime = DateTime.Now.ToString("yyyyMMdd");
            else
            {
                if (int.Parse(eTime) > int.Parse(sTime))
                {
                    sType = 1;
                }
            }
            //查询类型
            if (sType == 0)
            {
                sql = string.Format(@"select hourid as orderid,                           
                           sum(click_cnt) as clickcnt,
                           sum(income) as incomesum
                      from bee.bee_rpt_statbylkurlh s1
                     where s1.dateid = {0}
                       and s1.aduserid = {1}
                       and s1.linkurl='{2}'
                     group by  hourid
                     order by hourid ", sTime, adUserId, linkUrl);

            }
            else
            {
                sql = string.Format(@" select dateid as orderid,                           
                           sum(click_cnt) as clickcnt,
                           sum(income) as incomesum
                      from bee.bee_rpt_statbylkurlh s1
                     where s1.dateid between {0} and {1}
                       and s1.aduserid = {2}
 and s1.linkurl='{3}'
                     group by  dateid
                     order by dateid", sTime, eTime, adUserId, linkUrl);
            }

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
        /// 获取到达链接详细-折线图
        /// </summary>
        /// <param name="adUserId"></param>
        /// <param name="sTime"></param>
        /// <param name="eTime"></param>
        /// <returns></returns>
        public string GetLinkUrlDetail(string adUserId, string sTime, string eTime)
        {
            List<AgentDeductDetail> listDeduct = new List<AgentDeductDetail>();
            //获取总的链接数
            int sType;
            DataTable dt1 = GetLinkUrlDetailListDT(adUserId, sTime, eTime, out sType);
            List<string> linkUrlList = new List<string>();// ["0", "1", "2", "3"];
            List<List<string>> clickAllList = new List<List<string>>();
            List<string> regionList = new List<string>();//坐标维度
            try
            {

                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt1.Rows)
                    {
                        // if()
                        DataTable tempDT = new DataTable();
                        tempDT = GetLinkUrlDetailDT(adUserId, sTime, eTime, dr["linkurl"].ToString());
                        linkUrlList.Add(dr["linkurl"].ToString());
                        List<string> tempList = new List<string>();
                        //一天
                        if (sType == 0)
                        {
                            for (int i = 0; i < tempDT.Rows.Count; i++)
                            {
                                for (int j = 0; j < (i == 0 ? int.Parse(tempDT.Rows[i]["orderid"].ToString()) : int.Parse(tempDT.Rows[i]["orderid"].ToString()) - int.Parse(tempDT.Rows[i - 1]["orderid"].ToString()) - 1); j++)
                                {
                                    tempList.Add("");
                                }
                                tempList.Add(tempDT.Rows[i]["clickcnt"].ToString());
                            }
                        }
                        //多天
                        else
                        {
                            for (int i = 0; i < tempDT.Rows.Count; i++)
                            {
                                string tempTime = tempDT.Rows[i]["orderid"].ToString();
                                string tempTime2 = i == 0 ? sTime : tempDT.Rows[i - 1]["orderid"].ToString();
                                DateTime time1 = DateTime.Parse(tempTime.Substring(0, 4) + "-" + tempTime.Substring(4, 2) + "-" + tempTime.Substring(6, 2));
                                DateTime time2;
                                //起始则包含，起始往前一天
                                if (i == 0)
                                    time2 = DateTime.Parse(tempTime2.Substring(0, 4) + "-" + tempTime2.Substring(4, 2) + "-" + tempTime2.Substring(6, 2)).AddDays(-1);
                                else
                                    time2 = DateTime.Parse(tempTime2.Substring(0, 4) + "-" + tempTime2.Substring(4, 2) + "-" + tempTime2.Substring(6, 2));
                                TimeSpan ts = time1 - time2;
                                if (ts.Days - 1 > 0)
                                {
                                    for (int j = 0; j < ts.Days - 1; j++)
                                    {
                                        tempList.Add("");
                                    }
                                }
                                tempList.Add(tempDT.Rows[i]["clickcnt"].ToString());
                            }

                        }
                        //var tempAllItem = new { clickdata = tempList };
                        clickAllList.Add(tempList);
                    }
                    if (sType == 0)
                    {
                        for (int i = 0; i < 24; i++)
                        {
                            regionList.Add(i.ToString() + "h");
                        }
                    }
                    else
                    {
                        DateTime time2 = DateTime.Parse(sTime.Substring(0, 4) + "-" + sTime.Substring(4, 2) + "-" + sTime.Substring(6, 2));
                        DateTime time1 = DateTime.Parse(eTime.Substring(0, 4) + "-" + eTime.Substring(4, 2) + "-" + eTime.Substring(6, 2));
                        TimeSpan ts = time1 - time2;
                        if (ts.Days > 0)
                        {
                            for (int i = 0; i <= ts.Days; i++)
                            {
                                regionList.Add(time2.AddDays(i).ToString("yyyyMMdd"));
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 24; i++)
                            {
                                regionList.Add(i.ToString() + "h");
                            }
                        }
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
            #region 组装数据
            var tempData = new
            {
                linkurldata = linkUrlList,
                clickdata = clickAllList,
                regiondata = regionList
            };
            #endregion

            #region 返回数据
            return Result.GetResult(Result.errCode, Result.errMsg, tempData);
            #endregion


        }

        /// <summary>
        /// 获取链接明细
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="adUserId"></param>
        /// <param name="sTime"></param>
        /// <param name="eTime"></param>
        /// <param name="linkUrl"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable GetLinkDetailListDT(int pageSize, int pageNo, string adUserId, string sTime, string eTime, string linkUrl, out int pageCount)
        {
            #region 定义变量
            DataTable dt = null;
            pageCount = 0;
            string sql = "", orderBy = "";
            DateTime time = DateTime.Now;
            //查询类型
            int sType = 0;//0一天，1为多天
            if (string.IsNullOrWhiteSpace(sTime) || string.IsNullOrWhiteSpace(eTime))
                sTime = time.ToString("yyyyMMdd");
            else
            {
                if (int.Parse(eTime) > int.Parse(sTime))
                {
                    sType = 1;
                }
            }
            //查询类型
            if (sType == 0)
            {
                sql = string.Format(@"select hourid||'h' as orderid, sum(click_cnt) as clickcnt,sum(income) as incomesum
                      from bee.bee_rpt_statbylkurlh s1
                     where s1.dateid = {0}
                       and s1.aduserid = {1}
                       and s1.linkurl='{2}'
                     group by hourid
                    ", sTime, adUserId, linkUrl);
                orderBy = " order by s1.hourid asc";
            }
            else
            {
                sql = string.Format(@" select dateid as orderid, sum(click_cnt) as clickcnt,sum(income) as incomesum
   from bee.bee_rpt_statbylkurlh s1
  where s1.dateid between {0} and {1}
    and s1.aduserid = {2}
and s1.linkurl='{3}'
  group by dateid
                    ", sTime, eTime, adUserId, linkUrl);
                orderBy = " order by s1.dateid asc";
            }

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

        public DataTable GetLinkUrlListByAgentDT(string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format("select distinct trim(s1.linkurl) as linkurl from bee.bee_materialinfo s1 where s1.status=1 ");
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
        /// 根据链接地址获取链接消费明细
        /// </summary>
        /// <param name="adUserId"></param>
        /// <param name="sTime"></param>
        /// <param name="eTime"></param>
        /// <param name="lUrl"></param>
        /// <param name="sType"></param>
        /// <returns></returns>
        public DataTable GetLinkUrlDetailByLinkDT(string adUserId, string sTime, string eTime,string lUrl, out int sType)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = "";
            //查询类型
            sType = 0;//0一天，1为多天
            if (string.IsNullOrWhiteSpace(sTime) || string.IsNullOrWhiteSpace(eTime))
                sTime = DateTime.Now.ToString("yyyyMMdd");
            else
            {
                if (int.Parse(eTime) > int.Parse(sTime))
                {
                    sType = 1;
                }
            }
            //查询类型
            if (sType == 0)
            {
                sql = string.Format(@"select hourid as orderid, sum(click_cnt) as clickcnt,sum(income) as incomesum
                      from bee.bee_rpt_statbylkurlh s1
                     where s1.dateid = {0}
                       and s1.aduserid = {1}
                       and s1.linkurl='{2}'
                     group by hourid   order by s1.hourid asc
                    ", sTime, adUserId,lUrl);

            }
            else
            {
                sql = string.Format(@" select dateid as orderid, sum(click_cnt) as clickcnt,sum(income) as incomesum
                           from bee.bee_rpt_statbylkurlh s1
                          where s1.dateid between {0} and {1}
                            and s1.aduserid = {2}
                            and s1.linkurl='{3}'
                          group by dateid  order by s1.dateid asc", sTime, eTime, adUserId,lUrl);
            }

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
        /// 根据链接地址获取链接消费明细-折线图
        /// </summary>
        /// <param name="adUserId"></param>
        /// <param name="sTime"></param>
        /// <param name="eTime"></param>
        /// <returns></returns>
        public string GetLinkUrlDetailByLink(string adUserId, string sTime, string eTime,string lUrl)
        {
            List<AgentDeductDetail> listDeduct = new List<AgentDeductDetail>();
            List<string> regionList = new List<string>();
            List<string> incomeList = new List<string>();
            List<string> clickList = new List<string>();
            int sType = 0;
            try
            {

                DataTable dt1 = GetLinkUrlDetailByLinkDT(adUserId, sTime, eTime,lUrl, out sType);
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    if (sType == 0)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            for (int j = 0; j < (i == 0 ? int.Parse(dt1.Rows[i]["orderid"].ToString()) : int.Parse(dt1.Rows[i]["orderid"].ToString()) - int.Parse(dt1.Rows[i - 1]["orderid"].ToString()) - 1); j++)
                            {
                                incomeList.Add("");
                                clickList.Add("");
                            }
                            incomeList.Add(dt1.Rows[i]["incomesum"].ToString());
                            clickList.Add(dt1.Rows[i]["clickcnt"].ToString());
                        }
                    }
                    //多天
                    else
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            string tempTime = dt1.Rows[i]["orderid"].ToString();
                            string tempTime2 = i == 0 ? sTime : dt1.Rows[i - 1]["orderid"].ToString();
                            DateTime time1 = DateTime.Parse(tempTime.Substring(0, 4) + "-" + tempTime.Substring(4, 2) + "-" + tempTime.Substring(6, 2));
                            DateTime time2;
                            //起始则包含，起始往前一天
                            if (i == 0)
                                time2 = DateTime.Parse(tempTime2.Substring(0, 4) + "-" + tempTime2.Substring(4, 2) + "-" + tempTime2.Substring(6, 2)).AddDays(-1);
                            else
                                time2 = DateTime.Parse(tempTime2.Substring(0, 4) + "-" + tempTime2.Substring(4, 2) + "-" + tempTime2.Substring(6, 2));
                            TimeSpan ts = time1 - time2;
                            if (ts.Days - 1 > 0)
                            {
                                for (int j = 0; j < ts.Days - 1; j++)
                                {
                                    incomeList.Add("");
                                    clickList.Add("");
                                }
                            }
                            incomeList.Add(dt1.Rows[i]["incomesum"].ToString());
                            clickList.Add(dt1.Rows[i]["clickcnt"].ToString());
                        }

                    }
                    if (sType == 0)
                    {
                        for (int i = 0; i < 24; i++)
                        {
                            regionList.Add(i.ToString() + "h");
                        }
                    }
                    else
                    {
                        DateTime time2 = DateTime.Parse(sTime.Substring(0, 4) + "-" + sTime.Substring(4, 2) + "-" + sTime.Substring(6, 2));
                        DateTime time1 = DateTime.Parse(eTime.Substring(0, 4) + "-" + eTime.Substring(4, 2) + "-" + eTime.Substring(6, 2));
                        TimeSpan ts = time1 - time2;
                        if (ts.Days > 0)
                        {
                            for (int i = 0; i <= ts.Days; i++)
                            {
                                regionList.Add(time2.AddDays(i).ToString("yyyyMMdd"));
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 24; i++)
                            {
                                regionList.Add(i.ToString() + "h");
                            }
                        }
                    }
                    //listDeduct = cData.FillModel<AgentDeductDetail>(dt1);
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

            var tempData = new
            {

                incomedata = incomeList,
                regiondata = regionList,
                clickdata=clickList
            };

            #region 返回数据
            return Result.GetResult(Result.errCode, Result.errMsg, tempData);
            #endregion


        }
    }
}
