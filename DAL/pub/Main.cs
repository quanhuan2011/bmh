using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL.database;

namespace DAL.pub
{
    /// <summary>
    /// 广告、广告位、物料 数据访问层
    /// by wangjc 20161014
    /// </summary>
    public class Main
    {
        DBOperate dbOperate = new DBOperate();
        /// <summary>
        /// 获取广告位信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetAdLocationData()
        {
            string sql = string.Format(@"select * from bee_adlocationinfo a where a.status=1 order by a.adlocationid asc");
            DataTable dt = dbOperate.GetDataTable(sql);
            return dt;
        }

        /// <summary>
        /// 获取物料信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetMaterialinfoData()
        {
            string sql = string.Format(@"select * from bee_materialinfo a where a.status=1 order by a.materialid asc");
            DataTable dt = dbOperate.GetDataTable(sql);
            return dt;
        }

        /// <summary>
        /// 获取广告信息
        /// </summary>
        public DataTable GetAdvertisementData()
        {
            string sql = string.Format(@"select * from bee_adinfo t where t.status=1 order by t.adid asc");
            DataTable dt = dbOperate.GetDataTable(sql);
            return dt;
        }
        

    
    }
}
