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
    /// 页面管理
    /// </summary>
    public class PageManager
    {
        ConvertData cData = new ConvertData();
        DBOperate dbOperate = new DBOperate();
        /// <summary>
        ///  获取广告广告状态列表返回DataTable
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetPageNameDT(string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format("select distinct pageid,name as pagename from bee_pageinfo where status=1 ");
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

        public DataTable GetPageListByBidDT(string adId)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"select s1.pageid,
       s1.name as pagename,
       s1.termid,
       decode(nvl(s1.termid, 0), 1, 'H5', 2, 'APP', 3, '矩阵APP', '其他') as termname
  from bee.bee_pageinfo s1, bee.bee_adinfo s2, bee.bee_adlocationinfo s3
 where s1.status = 1
   and s2.adid = {0}
   and s2.subadtypeid = s3.subadtypeid
   and s1.pageid = s3.pageid
   and s3.isbid=1
 group by s1.pageid,
          s1.name,
          s1.termid,
          decode(nvl(s1.termid, 0), 1, 'H5', 2, 'APP', 3, '矩阵APP', '其他'),
          s1.subtermid
 order by s1.termid, s1.subtermid, s1.pageid
",adId);
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

        public string GetPageListByBid(string adId)
        {
            List<PageListByBid> listByBid = new List<PageListByBid>();
            try
            {
                DataTable dt = GetPageListByBidDT(adId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    listByBid = cData.FillModel<PageListByBid>(dt);
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
            return Result.GetResult(Result.errCode, Result.errMsg, listByBid);
            #endregion


        }
        //GetPageListBySubAdType

        public DataTable GetPageListBySubAdTypeDT(string subAdTypeId)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"select s1.pageid,
       s1.name as pagename,
       s1.termid,
       decode(nvl(s1.termid, 0), 1, 'H5', 2, 'APP', 3, '矩阵APP', '其他') as termname
  from bee.bee_pageinfo s1, bee.bee_adlocationinfo s2
 where s1.status = 1      
   and s2.subadtypeid = {0}
   and s1.pageid = s2.pageid
   and s2.isbid=1
 group by s1.pageid,
          s1.name,
          s1.termid,
          decode(nvl(s1.termid, 0), 1, 'H5', 2, 'APP', 3, '矩阵APP', '其他'),
          s1.subtermid
 order by s1.termid, s1.subtermid, s1.pageid

", subAdTypeId);
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

        public string GetPageListBySubAdType(string subAdTypeId)
        {
            List<PageListByBid> listByBid = new List<PageListByBid>();
            try
            {
                DataTable dt = GetPageListBySubAdTypeDT(subAdTypeId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    listByBid = cData.FillModel<PageListByBid>(dt);
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
            return Result.GetResult(Result.errCode, Result.errMsg, listByBid);
            #endregion


        }
    }
}
