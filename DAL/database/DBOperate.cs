using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CVBUtility;
using System.Data;
using Common.pub;
using System.Collections;



namespace DAL.database
{
    /// <summary>
    /// 数据库操作类
    /// </summary>
    public class DBOperate
    {

        #region 变量定义
        CommonDBOperation dbOperation = null;
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        public DBOperate()
        {
            SetConnect();
        }

        /// <summary>
        /// 获取连接配置
        /// </summary>
        private void SetConnect()
        {
            System.Configuration.AppSettingsReader appReader = new System.Configuration.AppSettingsReader();
            DatabasePool.GetDatabaseConnectStr(appReader.GetValue("DBClient", typeof(string)).ToString(), "chinavb234123489");
        }

        /// <summary>
        /// 传入sql获取datatable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string sql)
        {
            DataTable dt;
            try
            {
                dbOperation = new CommonDBOperation();
                dt = dbOperation.GetTable(sql);
            }
            catch (Exception ex)
            {
                LogApi.DebugInfo(ex, sql);
                dt = null;
            }
            finally
            {
                if (dbOperation != null)
                {
                    dbOperation.Close();
                    dbOperation.Dispose();
                    dbOperation = null;
                }
            }
            return dt;
        }

        /// <summary>
        /// 数据修改、删除
        /// </summary>
        /// <param name="str">sql语句</param>
        /// <returns>影响的行数</returns>
        public int ExecuteStatement(string str)
        {
            try
            {
                dbOperation = new CommonDBOperation();                
                return dbOperation.ExecuteStatement(str);
            }
            catch (Exception ex)
            {
                LogApi.DebugInfo(ex, str);
                return 0;
            }
            finally
            {
                if (dbOperation != null)
                {
                    dbOperation.Close();
                    dbOperation.Dispose();
                    dbOperation = null;
                }
            }

        }
        /// <summary>
        /// 数据更新
        /// </summary>
        /// <param name="str"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int ExecuteStatement(string str,List<ParamItem> param)
        {
            try
            {
                dbOperation = new CommonDBOperation();
                return dbOperation.ExecuteStatement(str,param);
            }
            catch (Exception ex)
            {
                LogApi.DebugInfo(ex, str);
                return 0;
            }
            finally
            {
                if (dbOperation != null)
                {
                    dbOperation.Close();
                    dbOperation.Dispose();
                    dbOperation = null;
                }
            }

        }
        /// <summary>
        /// 获取序列下一值
        /// </summary>
        /// <param name="seqName">序列名称</param>
        /// <returns>下一值</returns>
        public string GetSeqValue(string seqName)
        {
            string seqValue = string.Empty;
            string sqlSeq = string.Format("select {0}.Nextval as seqValue from dual", seqName);
            try
            {
                DataTable dt = GetDataTable(sqlSeq);
                if (dt != null && dt.Rows.Count > 0)
                {
                    seqValue = dt.Rows[0]["seqValue"].ToString();
                }
            }
            catch (Exception ex)
            {
                LogApi.DebugInfo(ex, sqlSeq);
            }
            finally
            {
                if (dbOperation != null)
                {
                    dbOperation.Close();
                    dbOperation.Dispose();
                    dbOperation = null;
                }
            }
            return seqValue;
        }
        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int GetCount(string sql)
        {           
            try
            {
                dbOperation = new CommonDBOperation();
                return dbOperation.GetCount(sql);
            }
            catch (Exception ex)
            {
                LogApi.DebugInfo(ex, sql);
                return 0;
            }
            finally
            {
                if (dbOperation != null)
                {
                    dbOperation.Close();
                    dbOperation.Dispose();
                    dbOperation = null;
                }
            }
        }
        /// <summary>
        /// 获取数据总量，用于分页
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>总量</returns>
        public int GetDataCount(string tableName, string whereStr)
        {
            string sqlStr = string.Format("select count(*) from {0} where 1=1 {1}", tableName, whereStr);
            try
            {
                dbOperation = new CommonDBOperation();
                return dbOperation.GetCount(sqlStr);
            }
            catch (Exception ex)
            {
                LogApi.DebugInfo(ex, sqlStr);
                return 0;
            }
            finally
            {
                if (dbOperation != null)
                {
                    dbOperation.Close();
                    dbOperation.Dispose();
                    dbOperation = null;
                }
            }
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="orderby"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <param name="resultCount"></param>
        /// <returns></returns>
        public DataTable GetPageData(string sql, string orderby, int pageSize, int currentPage, out int resultCount)
        {
            DataTable dt = null;
            try
            {

                dbOperation = new CommonDBOperation();
                //dbOperation.GetCount(sql);
                string commandText = string.Format("select count(*) from ({0}) T", sql);
                resultCount = dbOperation.GetCount(commandText);

                int startRow = pageSize * (currentPage - 1);
                int endRow = startRow + pageSize;

                StringBuilder sb = new StringBuilder();
                sb.Append("select * from ( select row_limit.*, rownum rownum_ from (");
                sb.Append(sql);
                if (!string.IsNullOrWhiteSpace(orderby))
                {
                    sb.Append(" ");
                    sb.Append(orderby);
                }
                sb.Append(" ) row_limit where rownum <= ");
                sb.Append(endRow);
                sb.Append(" ) where rownum_ >");
                sb.Append(startRow);
                string endSql = sb.ToString();
                dt = dbOperation.GetTable(endSql);

            }
            catch (Exception ex)
            {
                LogApi.DebugInfo(ex, sql);
                resultCount = 0;
                dt = null;
            }
            finally
            {
                if (dbOperation != null)
                {
                    dbOperation.Close();
                    dbOperation.Dispose();
                    dbOperation = null;
                }
            }
            return dt;
        }
   

        /// <summary>  
        /// 获取分页数据
        /// </summary>
        /// <param name="tableName">表名字</param>  
        /// <param name="whereStr">条件字符 必须前加 and</param>  
        /// <param name="orderExpression">排序 例如 Order by ID asc</param>  
        /// <param name="pageIdex">当前索引页</param>  
        /// <param name="pageSize">每页记录数</param>  
        /// <returns></returns>  
        public DataTable GetPagerData(string tableName, string whereStr, string orderExpression, int pageIdex, int pageSize, out int dataCount)
        {
            string sqlStr = string.Empty;
            int rows = 0;
            try
            {
                sqlStr = string.Format("select * from(select rownum as rowIndex, a.* from (select * from {0} where 1=1 {1} ", tableName, whereStr);
                if (!string.IsNullOrEmpty(orderExpression))
                {
                    sqlStr += string.Format(" {0}", orderExpression);
                }

                sqlStr += string.Format(")a) where rowIndex between {0} and {1}", (pageIdex - 1) * pageSize + 1, pageIdex * pageSize);

                DataTable dt = GetDataTable(sqlStr);
                rows = GetDataCount(tableName, whereStr);
                dataCount = rows;

                return dt;
            }
            catch (Exception ex)
            {
                LogApi.DebugInfo(ex, sqlStr);
                dataCount = rows;
                return null;
            }
            finally
            {
                if (dbOperation != null)
                {
                    dbOperation.Close();
                    dbOperation.Dispose();
                    dbOperation = null;
                }
            }
        }

        /// <summary>
        /// 执行存储过程返回列表
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="paramItems"></param>
        /// <returns></returns>
        public ArrayList ExecProcedure(string procedureName, ParamCollections paramItems)
        {           
            ArrayList arrayList = new ArrayList();         
            try
            {               
                 dbOperation = new CommonDBOperation();
                 arrayList = dbOperation.ExecProcedure(procedureName, paramItems.GetParams());
            }
            catch (Exception ex)
            {
                arrayList = null;
                LogApi.DebugInfo(ex, procedureName);              
            }
            finally
            {
                if (dbOperation != null)
                {
                    dbOperation.Close();
                    dbOperation.Dispose();
                    dbOperation = null;
                }
            }
            return arrayList;
        }
    }
}
