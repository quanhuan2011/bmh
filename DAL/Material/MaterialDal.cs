using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.database;
using System.Data;

namespace DAL.Material
{
    /// <summary>
    /// 物料管理、新增、修改
    /// by wangjc 20161018
    /// </summary>
    public class MaterialDal
    {
        DBOperate dbOperate = new DBOperate();

        /// <summary>
        /// 获取物料信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetMaterialinfoData(int maid)
        {
            string sql = string.Format(@"select * from bee_materialinfo a where a.status=1 and materialid={0}", maid);
            DataTable dt = dbOperate.GetDataTable(sql);
            return dt;
        }
    }
}
