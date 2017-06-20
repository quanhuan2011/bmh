using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BLL.pub;
using DAL.database;
using Common.pub;
using CVBUtility;
using BLL.redis;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;
using System.Diagnostics;
using Model;

namespace BLL.manager
{
    public class AdUserManager
    {
        ConvertData cData = new ConvertData();
        DBOperate dbOperate = new DBOperate();

        /// <summary>
        /// 获取广告主(代理商)列表信息
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable GetAdUserListDT(int pageSize, int pageNo, string sqlWhere, out int pageCount)
        {
            #region 定义变量
            DataTable dt = null;
            pageCount = 0;
            string sql = string.Format(@"select s1.aduserid ,
      s1. aduserno,
       s1.name as adusername,
       s1.companyname,
       s1.contact,
       s1.tel,
       nvl(s2.cityname,'暂无') as cityname,      
       decode(nvl(s1.usertype, 0),
              1,
              '区域总代',
              2,
              '普通代理' || '(' || nvl(s3.name, '无') || ')',              
              3,
              '广告主',
              '其他') as aduserdesc,
          decode(nvl(s1.weight, 0),
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
              '其他') as weight
  from bee.bee_aduserinfo s1, bee.bee_cityinfo s2, bee.bee_aduserinfo s3
 where s1.regionid = s2.cityid(+)
   and s1.parentid = s3.aduserid(+)
and s1.status=1");
            string orderBy = " order by s1.aduserid desc";
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
        /// 获取广告主信息-基本信息-通用
        /// </summary>
        /// <param name="adUserId"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetAdUserInfoDT(string adUserId, string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"select * from bee_aduserinfo s1 where s1.aduserid={0}", adUserId);
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
        /// 获取广告主列表
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetAdUserNameDT(string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format("select distinct aduserid,name as adusername from bee_aduserinfo  where status=1");
            #endregion

            #region sql转换
            sql = cData.GetSqlBySqlWhere(sql, sqlWhere);
            #endregion

            #region 排序
            sql = string.Format("{0} order by 1 asc ", sql);
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
        /// 获取广告主统计信息
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable GetAdUserStatDetailDT(string startTime, string endTime, int pageSize, int pageNo, string sqlWhere, out int pageCount)
        {
            #region 定义变量
            DataTable dt = null;
            pageCount = 0;
            string sql = string.Format(@" select p1.aduserid,
       p1.adusername as statname,
       p2.clickcnt,
       round(p2.incomsum, 2) as incomsum,
       p1.balance
  from (select s1.aduserid, s1.name as adusername, s1.createtime, s1.balance
          from bee.bee_aduserinfo s1
         where s1.status = 1) p1,
       (select s1.aduserid,
               nvl(sum(s1.click_cnt), 0) as clickcnt,
               nvl(sum(s1.income), 0) as incomsum
          from bee.bee_rpt_statbyadvd s1
         where s1.dateid < to_number(to_char(sysdate, 'yyyymmdd'))
           and s1.dateid between {0} and {1}
         group by s1.aduserid) p2
 where p1.aduserid = p2.aduserid", startTime, endTime);
            string orderBy = " order by p1.aduserid asc ";
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
        public DataTable GetAdUserStatSumDT(string startTime, string endTime, string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"  select nvl(sum(p2.clickcnt), 0) as clicktotal,
        nvl(sum(round(p2.incomsum, 2)), 0) as incometotal
   from (select s1.aduserid,
                s1.name as adusername,
                s1.createtime,
                s1.balance
           from bee.bee_aduserinfo s1
          where s1.status = 1) p1,
        (select s1.aduserid,
                nvl(sum(s1.click_cnt), 0) as clickcnt,
                nvl(sum(s1.income), 0) as incomsum
           from bee.bee_rpt_statbyadvd s1
          where s1.dateid < to_number(to_char(sysdate, 'yyyymmdd'))
            and s1.dateid between {0} and {1}
          group by s1.aduserid) p2
  where p1.aduserid = p2.aduserid", startTime, endTime);
            // string orderBy = " order by p1.aduserid asc ";
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
        /// 获取广告主日投放量信息
        /// </summary>
        /// <param name="adUserId"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetPutMaxInfoDT(string adUserId, string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"select s1.putmaxbyday from bee_aduserinfo s1 where s1.aduserid={0}", adUserId);
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
        /// 获取广告主日投放量信息
        /// </summary>
        /// <param name="adUserId"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public string GetPutMaxInfoData(string adUserId, string sqlWhere)
        {
            DataTable dt = GetPutMaxInfoDT(adUserId, sqlWhere);
            if (dt != null && dt.Rows.Count > 0)
            {
                #region 组合数据
                var tempData = new
                {
                    putmaxbyday = dt.Rows[0]["putmaxbyday"].ToString()
                };
                #endregion

                #region 返回数据
                return Result.GetSuccessResult("获取成功", tempData);
                #endregion
            }
            else
            {
                #region 返回数据
                return Result.GetFailResult("获取失败");
                #endregion
            }

        }
        /// <summary>
        /// 设置投放量上限信息
        /// </summary>
        /// <param name="adUserId"></param>
        /// <param name="putMaxByDay"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public string SetPutMaxInfoData(string adUserId, string putMaxByDay, string sqlWhere)
        {
            #region 定义变量
            //  string sql = string.Format(@"update bee_aduserinfo u1 set u1.putmaxbyday = @putmaxbyday,u1.statustime=sysdate where u1.aduserid =@aduserid");
            string sql = string.Format(@"update bee_aduserinfo u1 set u1.putmaxbyday = {0},u1.statustime=sysdate where u1.aduserid ={1}", putMaxByDay, adUserId);
            #endregion

            #region 处理
            try
            {
                #region 组装数据
                //ParamCollections pc = new ParamCollections();
                //pc.Add("@putmaxbyday", putMaxByDay,OracleDataType.STRING);
                //pc.Add("@aduserid", adUserId, OracleDataType.INT);
                #endregion

                #region 执行sql

                //int intRet= dbOperate.ExecuteStatement(sql, pc.GetParams());
                int intRet = dbOperate.ExecuteStatement(sql);
                #endregion

                #region 处理redis
                if (intRet > 0)
                {
                    //更新redis
                    RedisBase redisBase = new RedisBase();
                    //bool boolStatus = false;
                    redisBase.PushPutMaxInfoByAdu(adUserId, putMaxByDay);
                    //如果需要更新广告状态                                       
                    UpdateAdStatusByAdU(adUserId, "1", "-6");


                }
                #endregion

                Result.errCode = Result.successCode;
                Result.errMsg = "操作成功";
            }
            catch (Exception ex)
            {
                LogApi.DebugInfo(ex);
                Result.errCode = Result.failCode;
                Result.errMsg = ex.Message;
            }
            #endregion

            #region 返回数据
            return Result.GetResult(Result.errCode, Result.errMsg, null);
            #endregion
        }
        /// <summary>
        /// 修改广告主下广告状态
        /// </summary>
        /// <param name="adId"></param>
        /// <param name="NewStatus"></param>
        /// <param name="oldStatus"></param>
        public void UpdateAdStatusByAdU(string adUserId, string newStatus, string oldStatus)
        {
            try
            {
                string sql = string.Format(@"
                        update bee_adinfo u1
                           set u1.status = @newstatus, u1.statustime = sysdate
                         where exists (select 1
                                  from bee_aduserinfo s1
                                 where s1.aduserid = u1.aduserid
                                   and s1.status = 1)
                           and u1.aduserid = @aduserid");
                ParamCollections pc = new ParamCollections();
                pc.Add("@newstatus", newStatus, OracleDataType.INT);
                pc.Add("@aduserid", adUserId, OracleDataType.INT);
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
        /// 获取余额
        /// </summary>
        /// <param name="adUserId"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public string GetBalanceData(string adUserId, string sqlWhere)
        {
            DataTable dt = GetAdUserInfoDT(adUserId, sqlWhere);
            if (dt != null && dt.Rows.Count > 0)
            {
                #region 组合数据
                var tempData = new
                {
                    balance = dt.Rows[0]["balance"].ToString()
                };
                #endregion

                #region 返回数据
                return Result.GetSuccessResult("获取成功", tempData);
                #endregion
            }
            else
            {
                #region 返回数据
                return Result.GetFailResult("获取失败");
                #endregion
            }

        }
        /// <summary>
        /// 新增充值信息
        /// </summary>
        /// <param name="adUserId">广告主id</param>
        /// <param name="money">充值金额</param>
        /// <param name="sqlWhere">条件</param>
        /// <returns></returns>
        public string InsertRechargeData(string accountId, string adUserId, string money, string sqlWhere)
        {
            #region 定义变量
            string sObjContent = "";
            string v_desc = "";
            string v_stamp = Utility.GetTimeStamp();//时间戳
            string o_status = "";
            string balance = "0";
            Result.errCode = Result.failCode;
            Result.errMsg = "操作失败";
            DateTime time = DateTime.Now;
            string dateId = time.ToString("yyyyMMdd");//time.Day.ToString();           
            string hourId = time.Hour.ToString();
            string timeNow = time.ToString();
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            #endregion

            #region 获取余额
            DataTable dt = GetAdUserInfoDT(adUserId, "");
            if (dt != null && dt.Rows.Count > 0)
            {
                balance = dt.Rows[0]["balance"].ToString();
            }
            #endregion

            #region 组装WirteMoneyLog数据
            //日期|小时|广告主|变动类型|变动金额|余额|变动前余额|操作者
            StringBuilder sb = new StringBuilder();
            sb.Append(dateId);
            sb.Append("|");
            sb.Append(hourId);
            sb.Append("|");
            sb.Append(adUserId);
            sb.Append("|");
            sb.Append("1");
            sb.Append("|");
            sb.Append(money);
            sb.Append("|");
            sb.Append((float.Parse(money) + float.Parse(balance)).ToString());
            sb.Append("|");
            sb.Append(balance);
            sb.Append("|");
            sb.Append(accountId);
            sObjContent = sb.ToString().Trim();
            sObjContent = sObjContent.Replace(" ", "");
            v_desc = string.Format("【鹰眼后台】广告主【{0}】于【{1}】进行一笔充值,充值金额为【{2}】,充值前余额为【{3}】,充值后余额为【{4}】", adUserId, timeNow, money, balance, (float.Parse(money) + float.Parse(balance)).ToString());
            v_stamp = Utility.GetTimeStamp();
            o_status = "-1";
            #endregion

            WirteMoneyLog(sObjContent, v_desc, v_stamp, out o_status);
            //if (o_status=="0")
            //{
            //    #region 组装AduserConsume数据
            //    //类型|日期|时段|广告主id|扣费量|扣费金额|充值金额|余额
            //    StringBuilder sb1 = new StringBuilder();
            //    sb1.Append("A");
            //    sb1.Append("|");
            //    sb1.Append(dateId);
            //    sb1.Append("|");
            //    sb1.Append(hourId);
            //    sb1.Append("|");
            //    sb1.Append(adUserId);
            //    sb1.Append("|");
            //    sb1.Append("0");
            //    sb1.Append("|");
            //    sb1.Append("0");
            //    sb1.Append("|");
            //    sb1.Append(money);
            //    sb1.Append("|");
            //    sb1.Append((float.Parse(money) + float.Parse(balance)).ToString());
            //    sObjContent = sb1.ToString().Trim();
            //    sObjContent = sObjContent.Replace(" ","");
            //    v_desc = string.Format("【鹰眼后台】广告主【{0}】于【{1}】进行一笔充值,充值金额为【{2}】,充值前余额为【{3}】,充值后余额为【{4}】", adUserId, timeNow, money, balance, (float.Parse(money) + float.Parse(balance)).ToString());
            //    v_stamp = Utility.GetTimeStamp();
            //    o_status = "-1";
            //    #endregion
            //    AduserConsume(sObjContent, v_desc, v_stamp, out o_status);                
            //    if (o_status == "0")
            //    {

            //        #region 更新redis
            //        RedisBase redisBase = new RedisBase();
            //        redisBase.PushReChargeInfo(adUserId, money);                    
            //        Result.errCode = Result.successCode;
            //        Result.errMsg = "操作成功";
            //        #endregion                                      
            //    }
            //}
            //sw.Stop();
            //TimeSpan ts1 = sw.Elapsed;
            //LogApi.LogInfo("time", string.Format("time1:'{0}'", ts1.TotalMilliseconds));
            #region 返回数据
            Result.errCode = Result.successCode;
            Result.errMsg = "操作成功";
            return Result.GetResult(Result.errCode, Result.errMsg, "");
            #endregion


        }
        /// <summary>
        /// 调用资金变动过程
        /// </summary>
        /// <param name="sObjContent">日期|小时|广告主|变动类型|变动金额|余额|变动前余额|操作者</param>
        /// <param name="v_desc">remark</param>
        /// <param name="v_stamp">时间戳</param>
        /// <param name="o_status">返回值，为0表示成功，否则失败</param>
        /// <returns></returns>
        public void WirteMoneyLog(string sObjContent, string v_desc, string v_stamp, out string o_status)
        {
            #region 定义变量
            o_status = "-1";
            // string execRet = "0";//过程执行情况
            string procedureName = "p_bee_Write_MoneyLog";
            #endregion

            #region 组合数据执行过程
            try
            {
                LogApi.LogInfo("p_bee_Write_MoneyLog", string.Format("sObjContent:'{0}',v_desc:'{1}',v_stamp:'{2}'", sObjContent, v_desc, v_stamp));
                ParamCollections paramItems = new ParamCollections();
                paramItems.Add("@sObjContent", sObjContent, OracleDataType.STRING);
                paramItems.Add("@v_desc", v_desc, OracleDataType.STRING);
                paramItems.Add("@v_stamp", v_stamp, OracleDataType.STRING);
                paramItems.Add("@o_status", o_status, OracleDataType.INT, InOutFlag.OUT);
                ArrayList arrayList = dbOperate.ExecProcedure(procedureName, paramItems);
                o_status = arrayList[0].ToString();
            }
            catch (Exception ex)
            {
                o_status = "-1";
                LogApi.DebugInfo(ex);
            }
            #endregion

            //  return execRet;
        }
        /// <summary>
        /// 广告主流水明细记录
        /// </summary>
        /// <param name="sObjContent">类型|日期|时段|广告主id|扣费量|扣费金额|充值金额|余额，其中类型为A，表示充值</param>
        /// <param name="v_desc">remark</param>
        /// <param name="v_stamp">时间戳</param>
        /// <param name="o_status">返回值，为0表示成功，否则失败</param>
        public void AduserConsume(string sObjContent, string v_desc, string v_stamp, out string o_status)
        {
            #region 定义变量
            o_status = "-1";
            string procedureName = "P_Bee_Aduser_Consume";
            #endregion

            #region 组合数据执行过程
            try
            {
                LogApi.LogInfo("P_Bee_Aduser_Consume", string.Format("sObjContent:'{0}',v_desc:'{1}',v_stamp:'{2}'", sObjContent, v_desc, v_stamp));
                ParamCollections paramItems = new ParamCollections();
                paramItems.Add("@sObjContent", sObjContent, OracleDataType.STRING);
                paramItems.Add("@v_desc", v_desc, OracleDataType.STRING);
                paramItems.Add("@v_stamp", v_stamp, OracleDataType.STRING);
                paramItems.Add("@o_status", o_status, OracleDataType.INT, InOutFlag.OUT);
                ArrayList arrayList = dbOperate.ExecProcedure(procedureName, paramItems);
                o_status = arrayList[0].ToString();
            }
            catch (Exception ex)
            {
                o_status = "-1";
                LogApi.DebugInfo(ex);
            }
            #endregion

            //  return execRet;
        }

        public string InsertAdUserData(string inJson)
        {
            #region 定义变量
            string procedureName = "p_insertaduserdata";
            #endregion

            #region 组合数据执行过程
            try
            {
                LogApi.LogInfo("p_insertaduserdata", inJson);
                ParamCollections paramItems = new ParamCollections();
                paramItems.Add("@injson", inJson, OracleDataType.STRING);
                paramItems.Add("@errcode", Result.errCode, OracleDataType.INT, InOutFlag.OUT);
                paramItems.Add("@errmsg", Result.errMsg, OracleDataType.STRING, InOutFlag.OUT);
                ArrayList arrayList = dbOperate.ExecProcedure(procedureName, paramItems);
                Result.errCode = arrayList[0].ToString();
                Result.errMsg = arrayList[1].ToString();               
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
        public DataTable GetAdUserDT(string adUserId, string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"select s1.aduserid,
       s3.username,
       s1.name,
       s1.companyname,
       s1.contact,
       s1.sex,
       s1.age,
       s1.tel,
       s1.email,
       s1.address,
s1.usertype,
       decode(s1.usertype,
              1,
              '区域总代',
              2,
              '普通代理',
              3,
              '广告主',
              '其他') as usertypename,
      s2.cityid as regionid,
       nvl(s2.cityname, '暂无') as regionname,
       s1.saler,
       s1.parentid,
       s1.idno,
       s1.aduserno
  from bee.bee_aduserinfo s1, bee.bee_cityinfo s2, bee.bee_accountinfo s3
 where s1.aduserid = {0}
   and s1.regionid = s2.cityid(+)
   and s1.aduserid = s3.aduserid(+)
", adUserId);
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
        /// <param name="adUserId"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetAdUserDocDT(string adUserId, string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"select filename,fileno,filetype,fileseq from bee.bee_aduserdoc s1 where s1.aduserid={0} order by s1.filetype,s1.fileseq", adUserId);
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
        public string GetAdUserInfo(string adUserId)
        {

            AdUserInfo adUserInfo = new AdUserInfo();
            List<AdUserDoc> listFileInfo = new List<AdUserDoc>();
            try
            {
                DataTable dt = GetAdUserDT(adUserId, "");
                if (dt != null && dt.Rows.Count > 0)
                {
                    adUserInfo = cData.FillModel<AdUserInfo>(dt.Rows[0]);
                }
                DataTable dt2 = GetAdUserDocDT(adUserId, "");
                if (dt2 != null && dt2.Rows.Count > 0)
                {
                    listFileInfo = cData.FillModel<AdUserDoc>(dt2);
                }
                Result.errCode = "1";
                Result.errMsg = "获取成功";
            }
            catch (Exception ex)
            {
                adUserInfo = null;
                Result.errCode = "-1";
                Result.errMsg = ex.Message;
                LogApi.DebugInfo(ex);
            }

            #region 组合数据
            var tempData = new
            {
                infodata = adUserInfo,
                filedata = listFileInfo
            };
            #endregion

            #region 返回数据
            return Result.GetResult(Result.errCode, Result.errMsg, tempData);
            #endregion


        }

        //SaveAdUserBaseInfo
        public string SaveAdUserBaseInfo(string data)
        {

            try
            {
                //修改保存数据
                JObject obj = (JObject)JsonConvert.DeserializeObject(data);
                string name = GetJObjectVal(obj, "name");
                string companyname = GetJObjectVal(obj, "cname");
                string contact = GetJObjectVal(obj, "contact");
                string tel = GetJObjectVal(obj, "tel");
                string email = GetJObjectVal(obj, "email");
                string address = GetJObjectVal(obj, "address");
                string regionid = GetJObjectVal(obj, "regionid");
                string idno = GetJObjectVal(obj, "idno");
                string aduserid = GetJObjectVal(obj, "aduid");
                string sql = string.Format(@"
                       update bee.bee_aduserinfo u1
   set u1.name        = nvl(@name, u1.name),
       u1.companyname = nvl(@companyname, u1.companyname),
       u1.contact     = nvl(@contact, u1.contact),
       u1.tel         = nvl(@tel, u1.tel),
       u1.email       = nvl(@email, u1.email),
       u1.address     = nvl(@address, u1.address),
       u1.regionid    = nvl(@regionid, u1.regionid),
       u1.idno        = nvl(@idno, u1.idno),
       u1.statustime  = sysdate
 where u1.aduserid =@aduserid");
                ParamCollections pc = new ParamCollections();
                pc.Add("@name", name, OracleDataType.STRING);
                pc.Add("@companyname", companyname, OracleDataType.STRING);
                pc.Add("@contact", contact, OracleDataType.STRING);
                pc.Add("@tel", tel, OracleDataType.STRING);
                pc.Add("@email", email, OracleDataType.STRING);
                pc.Add("@address", address, OracleDataType.STRING);
                pc.Add("@regionid", regionid, OracleDataType.INT);
                pc.Add("@idno", idno, OracleDataType.STRING);
                pc.Add("@aduserid", aduserid, OracleDataType.INT);


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
        private string GetJObjectVal(JObject obj, string key, string defaultVal = "")
        {
            string result = "";
            try
            {
                result = (string)obj[key];
                if (string.IsNullOrWhiteSpace(result))
                    result = defaultVal;
            }
            catch
            {
                result = defaultVal;
            }
            return result;
        }

        public DataTable GetLinkManDT(string adUserId, string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"select name, nvl(position,'暂无') as position, mobile, tel, nvl(email,'暂无') as email
              from bee_linkman s1
             where userid ={0}
               and usertype in (1, 2)
               and s1.status = 1
            order by s1.createtime asc 
            ", adUserId);
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

        public string GetLinkManList(string adUserId)
        {

            List<LinkManByAdu> listLinkManByAdu = new List<LinkManByAdu>();
            try
            {
                DataTable dt = GetLinkManDT(adUserId, "");
                if (dt != null && dt.Rows.Count > 0)
                {
                    listLinkManByAdu = cData.FillModel<LinkManByAdu>(dt);
                }

                Result.errCode = "1";
                Result.errMsg = "获取成功";
            }
            catch (Exception ex)
            {
                listLinkManByAdu = null;
                Result.errCode = "-1";
                Result.errMsg = ex.Message;
                LogApi.DebugInfo(ex);
            }

            #region 组合数据
            var tempData = new
            {
                infodata = listLinkManByAdu
            };
            #endregion

            #region 返回数据
            return Result.GetResult(Result.errCode, Result.errMsg, tempData);
            #endregion


        }

        public string InsertAdUserFile(string data)
        {

            try
            {
                //修改保存数据
                JObject obj = (JObject)JsonConvert.DeserializeObject(data);
                string fileno = GetJObjectVal(obj, "fno");
                string filetype = GetJObjectVal(obj, "ftype");
                string aduserid = GetJObjectVal(obj, "aduid");
                string usertype = "", fileseq = "";
                string sql1 = string.Format("select nvl(max(s1.fileseq),0)+1 as fileseq ,nvl(max(s1.usertype),2)as usertype    from bee.bee_aduserdoc s1 where s1.aduserid={0} and s1.filetype={1}", aduserid,filetype);
                DataTable dt = dbOperate.GetDataTable(sql1);
                if (dt != null && dt.Rows.Count > 0)
                {
                    usertype = dt.Rows[0]["usertype"].ToString();
                    fileseq = dt.Rows[0]["fileseq"].ToString();
                }
                else
                {
                    usertype = "2";
                    fileseq = "1";
                }

                string sql = string.Format(@"
                       insert into bee.bee_aduserdoc
  (id, aduserid,usertype, filename, fileno, fileseq, filetype)
values
  (s_aduserdoc.nextval, @aduserid,@usertype, 'FNO' ||
                                  to_char(sysdate, 'yyyymmddhh24miss') ||
                                  s_aduserdoc.currval, @fileno, @fileseq, @filetype)
");
                ParamCollections pc = new ParamCollections();
                pc.Add("@aduserid", aduserid, OracleDataType.INT);
                pc.Add("@usertype", usertype, OracleDataType.INT);
                pc.Add("@fileno", fileno, OracleDataType.STRING);
                pc.Add("@fileseq", fileseq, OracleDataType.INT);
                pc.Add("@filetype", filetype, OracleDataType.INT);


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
        public string UpdateAdUserFile(string data)
        {

            try
            {
                //修改保存数据
                JObject obj = (JObject)JsonConvert.DeserializeObject(data);
                string fileno = GetJObjectVal(obj, "fno");
                string oldfileno = GetJObjectVal(obj, "oldfno");
                string filetype = GetJObjectVal(obj, "ftype");
                string aduserid = GetJObjectVal(obj, "aduid");
                string sql = string.Format(@"
                       update bee.bee_aduserdoc u1
   set u1.fileno        = nvl(@fileno, u1.fileno),
       u1.statustime = sysdate
 where u1.aduserid =@aduserid and u1.filetype=@filetype and u1.fileno=@oldfileno");

                ParamCollections pc = new ParamCollections();
                pc.Add("@fileno", fileno, OracleDataType.STRING);
                pc.Add("@aduserid", aduserid, OracleDataType.INT);
                pc.Add("@filetype", filetype, OracleDataType.INT);
                pc.Add("@oldfileno", oldfileno, OracleDataType.STRING);
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
        public string UpdateAduWeight(string adUserId, string weight)
        {

            try
            {
                //修改保存数据              
                string sql = string.Format(@"
                       update bee.bee_aduserinfo u1
   set u1.weight = nvl(@weight, u1.weight), u1.statustime = sysdate
 where u1.aduserid = @aduserid");

                ParamCollections pc = new ParamCollections();
                pc.Add("@weight", weight, OracleDataType.INT);
                pc.Add("@aduserid", adUserId, OracleDataType.INT);

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
        /// <summary>
        /// 创建广告主
        /// </summary>
        /// <param name="adUserId"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public string CreateAduInfo(string aduName, string aduCompName, string aduContact, string aduSex, string aduTel)
        {

            try
            {
                string adUId = dbOperate.GetSeqValue("SEQ_BEE_ADUSERINFO_ID");

                //修改保存数据              
                string sql = string.Format(@"
                     insert into bee.bee_aduserinfo
                                      (aduserid,
                                       name,
                                       companyname,
                                       contact,
                                       sex,
                                       tel,
                                       balance,
                                       createtime,
                                       statustime,
                                       status,
                                       putmaxbyday,
                                       usertype,
                                       aduserno,
                                       weight)
                                    values
                                      (@aduserid,
                                       @name,
                                       @companyname,
                                       @contact,
                                       @sex,
                                       @tel,
                                       0,
                                       sysdate,
                                       sysdate,
                                       1,
                                       999999999,
                                       3,
                                       'FNO' || to_char(sysdate, 'yyyymmddhh24miss') ||
                                       @aduserno,
                                       1)");

                ParamCollections pc = new ParamCollections();
                pc.Add("@aduserid", adUId, OracleDataType.INT);
                pc.Add("@name", aduName, OracleDataType.STRING);
                pc.Add("@companyname", aduCompName, OracleDataType.STRING);
                pc.Add("@contact", aduContact, OracleDataType.STRING);
                pc.Add("@sex", aduSex, OracleDataType.STRING);
                pc.Add("@tel", aduTel, OracleDataType.STRING);
                pc.Add("@aduserno", adUId, OracleDataType.STRING);
                if (dbOperate.ExecuteStatement(sql, pc.GetParams()) == 1)
                {
                   
                    //创建帐号
                    string acctId = dbOperate.GetSeqValue("SEQ_BEE_ACCOUNTINFO_ID");
                    //用户名
                    ChineseCode cCode = new ChineseCode();                    
                    //前面两位
                    string strHead = string.Empty;
                    string strFoot = string.Empty;

                    //判断是否个人
                    if (aduName.Contains("个人"))
                    {
                        strHead = "gr_";
                        if (aduName.Contains("-"))
                            strFoot = cCode.GetSpell(aduName.Substring(3));
                        else
                            strFoot = cCode.GetSpell(aduName.Substring(2));
                    }
                    else
                    {
                        strHead = cCode.IndexCode(aduName.Substring(0, 2));
                        strFoot = cCode.GetSpell(aduName.Substring(2));
                    }
                    string acctUName = "yy_" + strHead + strFoot;

                    //判断帐号是否存在
                    string strSqlCnt = string.Format(@"select count(1) from bee.bee_accountinfo s1 where s1.username='{0}'", acctUName);
                    if (dbOperate.GetCount(strSqlCnt) > 0)
                    {
                        acctUName += cCode.IndexCode(aduContact);
                        //再次验证
                        strSqlCnt = string.Format(@"select count(1) from bee.bee_accountinfo s1 where s1.username='{0}'", acctUName);
                        if (dbOperate.GetCount(strSqlCnt) > 0)
                        {
                            Random rd = new Random();
                            acctUName += rd.Next(10);
                        }
                    }                    
                    string acctPwd = acctUName + "_123456";

                    //创建帐号
                    string strSql = string.Format(@"
                                            insert into bee.bee_accountinfo
                                              (accountid,
                                               name,
                                               username,
                                               password,
                                               type,
                                               aduserid,
                                               remark,
                                               createtime,
                                               statustime,
                                               status)
                                            values
                                              (@accountid,
                                               @name,
                                               @username,
                                               @password,
                                               1,
                                               @aduserid,
                                               @remark,
                                               sysdate,
                                               sysdate,
                                               1)
                                            ");
                    ParamCollections pc2 = new ParamCollections();
                    pc2.Add("@accountid", acctId, OracleDataType.INT);
                    pc2.Add("@name", aduName, OracleDataType.STRING);
                    pc2.Add("@username", acctUName, OracleDataType.STRING);
                    pc2.Add("@password", acctPwd, OracleDataType.STRING);
                    pc2.Add("@aduserid", adUId, OracleDataType.INT);
                    pc2.Add("@remark", aduContact, OracleDataType.STRING);
                    dbOperate.ExecuteStatement(strSql, pc2.GetParams());

                    Result.errCode = Result.successCode;
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
      /// <summary>
      /// 查找最近一个帐号密码
      /// </summary>
      /// <returns></returns>
        public string SearchLastAcctInfo()
        {
            string acctName=string.Empty, acctUName= string.Empty, acctPwd= string.Empty;
            try
            {          
                string sql = string.Format(@"select s1.name     as acctname,
                                   s1.username as acctuname,
                                   s1.password as acctpwd
                              from bee.bee_accountinfo s1,
                                   (select max(k1.accountid) as accountid
                                      from bee.bee_accountinfo k1
                                     where k1.type = 1) s2
                             where s1.accountid = s2.accountid
                            ");
                DataTable dt = dbOperate.GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    acctName = Convert.ToString(dt.Rows[0]["acctname"]);
                    acctUName = Convert.ToString(dt.Rows[0]["acctuname"]);
                    acctPwd = Convert.ToString(dt.Rows[0]["acctpwd"]);
                }
                Result.errCode = "1";
                Result.errMsg = "更新成功";
            }
            catch (Exception ex)
            {
                Result.errCode = "-1";
                Result.errMsg = ex.Message;
                LogApi.DebugInfo(ex);
            }
            var tempData = new {
                acctname= acctName,
                acctuname= acctUName,
                acctpwd= acctPwd
            };

            #region 返回数据
            return Result.GetResult(Result.errCode, Result.errMsg, tempData);
            #endregion
        }
    }
}

