using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BLL.pub;
using DAL.database;
using Common.pub;
using Model;
using CVBUtility;
using System.Collections;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using YYLog.ClassLibrary;

namespace BLL.manager
{

    /// <summary>
    /// 物料管理
    /// </summary>
    public class MaterialManager
    {
        #region 定义变量
        ConvertData cData = new ConvertData();
        DBOperate dbOperate = new DBOperate();
        #endregion

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="funcName"></param>
        /// <param name="data"></param>
        /// <param name="logType"></param>
        private void LogInfo( string funcName, string data, LogType logType=LogType.Success)
        {
            switch (logType)
            {
                case LogType.Error:
                    Log.WriteErrorLog(string.Format("YYLog.Modify:BLL/manager/MaterialManager/{0}", funcName), data);
                    break;
                case LogType.Success:
                    Log.WriteLog(string.Format("YYLog.Modify:BLL/manager/MaterialManager/{0}", funcName), data);
                    break;
                default:break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="adTypeId"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetMaterialListDT(string adTypeId, string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"select s1.materialid,
                                               s1.aduserid,
                                               s1.name,
                                               s1.imageurl,
                                               s1.linkurl,
                                               s1.title,
                                               s1.width,
                                               s1.height,
                                               s1.sizes,
                                               s1.format,
                                               s1.display,
                                               s1.systemtype,
                                               to_char(s1.createtime, 'yyyy/mm/dd hh24:mi') as createtime,
                                               s1.class,
                                               s1.materialtype,
                                               s1.operationid,
                                               s2.adtypename as materialtypename,
                                               nvl(s1.remark, '无内容') as remark,
                                               s1.showtime,
                                               s1.confirmtext,
                                               s1.canceltext,
                                               s3.name as operationname
                                          from bee_materialinfo s1, bee_adinfotype s2, bee_accountinfo s3
                                         where s1.materialtype = {0}
                                           and s1.status = 1
                                           and s1.materialtype = s2.adtypeid
                                           and s2.status = 1
                                           and s1.operationid(+) = s3.accountid", adTypeId);
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
        public DataTable GetMaterialListDT(int pageSize, int pageNo, string sqlWhere, out int pageCount)
        {
            #region 定义变量
            DataTable dt = null;
            pageCount = 0;
            string sql = string.Format(@" select s1.materialid,
       s1.aduserid,
       s1.name as materialname,
       s1.imageurl,
       s1.linkurl,
       s1.title,
       s1.width || '*' || s1.height as sizepic,
       to_char(s1.createtime, 'yyyy-MM-dd') as createtime,
       s1.statustime,
       s1.materialtype,
       s1.remark,
       s1.confirmtext,
       s1.canceltext,
       s1.showtime,
       s2.name as username,
       s3.adtypename as materialtypename

  from bee_materialinfo s1, bee_accountinfo s2, bee_adinfotype s3
 where s1.operationid = s2.accountid(+)
   and s1.materialtype = s3.adtypeid(+)
 ");
            string orderBy = " order by s1.materialid desc";
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
        /// 物料分析-根据条件筛选符合条件的物料并去重
        /// </summary>        
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetMaterialIdByAnalyDT(string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format(@"select distinct p1.materialid
  from (select s1.materialid,
               s1.name as materialname,
               s1.createtime,
               s1.materialtype,
               s1.status,             
               s2.aduserid,
               s3.pageid,
               s4.adlocationid,
               s5.adid        
          from bee_materialinfo    s1,
               bee_aduserinfo      s2,
               bee_pageinfo        s3,
               bee_adlocationinfo  s4,
               bee_adinfo          s5,
               bee_ad_materialinfo s6
         where s1.materialid = s6.materialid
           and s1.aduserid = s2.aduserid
           and s3.pageid = s4.pageid
           and s4.adlocationid = s5.adlocationid
           and s5.adid = s6.adid) p1 where 1=1 ");
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
        /// 获取物料分析列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="sqlWhere"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable GetMaterialAnalyListDT(int pageSize, int pageNo, string sqlWhere, out int pageCount)
        {
            #region 定义变量
            DataTable dt = null;
            pageCount = 0;
            string sql = string.Format(@"select p1.*,
                                       nvl(p2.ecpm, 0) as ecpm,
                                       nvl(p2.avgclickrate, 0) as avgclickrate,
                                       nvl(p3.yestclickrate, 0) as yestclickrate
                                  from (select s1.materialid,
                                               s1.name as materialname,
                                               s1.createtime,
                                               s1.title,
                                               s1.imageurl,
                                               s1.remark,
                                               s1.confirmtext,
                                               s1.canceltext,
                                               s1.materialtype,
                                               decode(s1.status, '1', '有效', 0, '无效', '其他') as statusname
                                          from bee.bee_materialinfo s1) p1,
                                       (select s1.matid as materialid,
                                                 round((sum(nvl(s1.income, 0)) / decode(sum(nvl(s1.reveal_cnt, 0)),0, null, sum(nvl(s1.reveal_cnt, 0)))) * 1000,
                     2) as ecpm,
                                               round(avg(nvl(s1.click_rto, 0)), 2) as avgclickrate
                                          from bee.bee_rpt_statbyclassmatd s1
                                         group by s1.matid) p2,
                                       (select s1.matid as materialid,
                                               round(avg(nvl(s1.click_rto, 0)), 2) as yestclickrate
                                          from bee.bee_rpt_statbyclassmatd s1
                                         where s1.dateid = to_number(to_char(sysdate - 1, 'yyyymmdd'))
                                         group by s1.matid) p3
                                 where p1.materialid = p2.materialid(+)
                                   and p1.materialid = p3.materialid(+)");
            string orderBy = " order by p1.materialid desc";
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
        /// 获取物料列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="adId"></param>
        /// <returns></returns>
        public string GetMaterialList(string adUserId, string adTypeId)
        {
            List<MaterialByType> listMaterial = new List<MaterialByType>();
            string sqlWhere = string.Format(" s1.aduserid={0}", adUserId);
            try
            {
                DataTable dt = GetMaterialListDT(adTypeId, sqlWhere);
                if (dt != null && dt.Rows.Count > 0)
                {
                    listMaterial = cData.FillModel<MaterialByType>(dt);
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
            return Result.GetResult(Result.errCode, Result.errMsg, listMaterial);
            #endregion


        }
        /// <summary>
        /// 新增物料 --sql
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        public string InsertMaterialData(Model.Material material)
        {
            #region 定义变量
            string sql = string.Format(@"insert into bee_materialinfo
                                                      (name,
                                                       imageurl,
                                                       linkurl,
                                                       title,
                                                       width,
                                                       height,
                                                       sizes,
                                                       format,
                                                       display,
                                                       ismark,
                                                       materialtype,
                                                       operationid,
                                                       materialid,
                                                       aduserid,
                                                       remark,
                                                       showtime,
                                                       confirmtext,
                                                       canceltext)
                                                    values
                                                      (@name,
                                                       @imageurl,
                                                       @linkurl,
                                                       @title,
                                                       @width,
                                                       @height,
                                                       @sizes,
                                                       @format,
                                                       @display,
                                                       @ismark,
                                                       @materialtype,
                                                       @operationid,
                                                       @materialid,
                                                       @aduserid,
                                                       @remark,
                                                       @showtime,
                                                       @confirmtext,
                                                       @canceltext)");
            #endregion

            #region 组合数据执行过程
            try
            {
                ParamCollections pc = new ParamCollections();
                pc.Add("@name", material.name, OracleDataType.STRING);
                pc.Add("@imageurl", material.imageurl, OracleDataType.STRING);
                pc.Add("@linkurl", material.linkurl, OracleDataType.STRING);
                pc.Add("@title", material.title, OracleDataType.STRING);
                pc.Add("@width", material.width.ToString(), OracleDataType.STRING);
                pc.Add("@height", material.height.ToString(), OracleDataType.STRING);
                pc.Add("@sizes", material.sizes.ToString(), OracleDataType.STRING);
                pc.Add("@format", material.format, OracleDataType.STRING);
                pc.Add("@display", material.display.ToString(), OracleDataType.STRING);
                pc.Add("@ismark", material.ismark.ToString(), OracleDataType.INT);
                pc.Add("@materialtype", material.materialtype.ToString(), OracleDataType.INT);
                pc.Add("@operationid", material.operationid.ToString(), OracleDataType.INT);
                pc.Add("@materialid", material.materialid.ToString(), OracleDataType.INT);
                pc.Add("@aduserid", material.aduserid.ToString(), OracleDataType.INT);
                pc.Add("@remark", material.remark, OracleDataType.STRING);
                pc.Add("@showtime", material.showtime, OracleDataType.INT);
                pc.Add("@confirmtext", material.confirmtext, OracleDataType.STRING);
                pc.Add("@canceltext", material.canceltext, OracleDataType.STRING);
                //执行sql
                dbOperate.ExecuteStatement(sql, pc.GetParams());
                Result.errCode = Result.successCode;
                Result.errMsg = "新增成功";
            }
            catch (Exception ex)
            {
                Result.errCode = "-1";
                Result.errMsg = ex.Message;
                LogApi.DebugInfo(ex);
            }
            #endregion

            #region 返回数据
            return Result.GetResult(Result.errCode, Result.errMsg,"");
            #endregion
        }
        /// <summary>
        /// 新增物料-修改为普通方法
        /// </summary>
        /// <param name="inJson"></param>
        /// <returns></returns>
        public string InsertMaterialData(string inJson)
        {
            #region 定义变量
            CommonDBOperation db = new CommonDBOperation();          
            string materialid = "";
            #endregion

            #region 组合数据执行过程
            try
            {
                //获取物料id
                DataTable dt= db.GetTable(@"select seq_bee_materialinfo_id.nextval as materialid from dual ");
                if (null != dt && dt.Rows.Count > 0)
                {
                    materialid = Convert.ToString(dt.Rows[0]["materialid"]);
                }

                JObject obj = (JObject)JsonConvert.DeserializeObject(inJson);              
                string aduserid = GetJObjectVal(obj, "aduserid");
                string name = GetJObjectVal(obj, "name");
                string imageurl = GetJObjectVal(obj, "imageurl");
                string linkurl = GetJObjectVal(obj, "linkurl");
                string title = GetJObjectVal(obj, "title");
                string width = GetJObjectVal(obj, "width");
                string height = GetJObjectVal(obj, "height");
                string sizes = GetJObjectVal(obj, "sizes");
                string format = GetJObjectVal(obj, "format");
                string display = GetJObjectVal(obj, "display");
                string remark = GetJObjectVal(obj, "remark");
                string materialtype = GetJObjectVal(obj, "materialtype");
                string ismark = GetJObjectVal(obj, "ismark");
                string operationid = GetJObjectVal(obj, "operationid");
                string showtime = GetJObjectVal(obj, "showtime");
                string confirmtext = GetJObjectVal(obj, "confirmtext");
                string canceltext = GetJObjectVal(obj, "canceltext");

                string sql = @"insert into bee.bee_materialinfo(materialid,aduserid,name,imageurl,linkurl,title,width,height,sizes,format,display,remark,materialtype,ismark,operationid,showtime,confirmtext,canceltext,createtime,statustime,status) 
                           values(@materialid,@aduserid,@name,@imageurl,@linkurl,@title,@width,@height,@sizes,@format,@display,@remark,@materialtype,@ismark,@operationid,@showtime,@confirmtext,@canceltext,sysdate,sysdate,1)";

                ParamCollections paramItems = new ParamCollections();
                paramItems.Add("@materialid", materialid, OracleDataType.INT);
                paramItems.Add("@aduserid", aduserid, OracleDataType.INT);
                paramItems.Add("@name", name, OracleDataType.STRING);
                paramItems.Add("@imageurl", imageurl, OracleDataType.STRING);
                paramItems.Add("@linkurl", linkurl, OracleDataType.STRING);
                paramItems.Add("@title", title, OracleDataType.STRING);
                paramItems.Add("@width", width, OracleDataType.STRING);
                paramItems.Add("@height", height, OracleDataType.STRING);
                paramItems.Add("@sizes", sizes, OracleDataType.STRING);
                paramItems.Add("@format", format, OracleDataType.STRING);
                paramItems.Add("@display", display, OracleDataType.STRING);
                paramItems.Add("@remark", remark, OracleDataType.STRING);
                paramItems.Add("@materialtype", materialtype, OracleDataType.INT);
                paramItems.Add("@ismark", ismark, OracleDataType.INT);
                paramItems.Add("@operationid", operationid, OracleDataType.INT);
                paramItems.Add("@showtime", showtime, OracleDataType.INT);
                paramItems.Add("@confirmtext", confirmtext, OracleDataType.STRING);
                paramItems.Add("@canceltext", canceltext, OracleDataType.STRING);

                int retCode= db.ExecuteStatement(sql, paramItems.GetParams());
                if (retCode == 1)
                {
                    Result.errCode = Result.successCode;
                    Result.errMsg = "更新成功";
                }
                else
                {
                    Result.errCode = Result.failCode;
                    Result.errMsg = "更新失败";
                    LogInfo("InsertMaterialData", Result.errMsg, LogType.Error);
                }                       
            }
            catch (Exception ex)
            {
                Result.errCode = "-1";
                Result.errMsg = ex.Message;
                LogInfo("InsertMaterialData", string.Format("ex.message:'{0}',ex.stacktrace:'{1}'", ex.Message, ex.StackTrace),LogType.Error);
            }
            #endregion

            #region 返回数据
            return Result.GetResult(Result.errCode, Result.errMsg, materialid);
            #endregion
        }
        /// <summary>
        /// 获取物料状态列表
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetAdStatusListDT(string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format("select distinct status,decode(s1.status, '1', '有效', 0, '无效', '其他') as statusname  from bee_materialinfo s1   order by status desc  ");
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
        /// 获取值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="key"></param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        private string GetJObjectVal(JObject obj, string key, string defaultVal = "")
        {
            string result = "";
            try
            {
                result = Convert.ToString( obj[key]);
                if (string.IsNullOrWhiteSpace(result)&&!string.IsNullOrEmpty(defaultVal))
                    result = defaultVal;
            }
            catch
            {
                result = defaultVal;
            }
            return result;
        }
    }
}
