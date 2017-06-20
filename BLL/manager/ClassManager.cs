using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BLL.pub;
using DAL.database;
using Common.pub;

namespace BLL.manager
{
    /// <summary>
    /// 分类管理
    /// </summary>
    public  class ClassManager
    {
        #region 全局参数定义
        ConvertData cData = new ConvertData();
        DBOperate dbOperate = new DBOperate();
        #endregion

        public DataTable GetClassNameDT(string sqlWhere)
        {
            #region 定义变量
            DataTable dt = null;
            string sql = string.Format("select distinct classid,classname from bee_classinfo s1  where status=1");
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
    }
}
