using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.database;
using System.Data;

namespace BLL.pub
{
    /// <summary>
    /// 通用工具类，业务逻辑
    /// by wangjc 20161017
    /// </summary>
    public class ToolClass
    {
        DBOperate dbOperate = new DBOperate();

        /// <summary>
        /// 获取序列下一值
        /// </summary>
        /// <param name="seqName">序列名称</param>
        /// <returns>下一值</returns>
        public int GetSeqValue(string seqName) 
        {
            return int.Parse(dbOperate.GetSeqValue(seqName));
        }

        /// <summary>
        /// 数据修改、删除
        /// </summary>
        /// <param name="str">sql语句</param>
        /// <returns>影响的行数</returns>
        public int ExecuteStatement(string str)
        {
            return dbOperate.ExecuteStatement(str);
        }

    }
}
