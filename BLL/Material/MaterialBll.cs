using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Material;
using System.Data;
using DAL.database;
using Common.pub;
using BLL.pub;

namespace BLL.Material
{
    public class MaterialBll
    {
        DBOperate dbOperate = new DBOperate();
        ConvertData cdata = new ConvertData();
        MaterialDal materialDal = new MaterialDal();
        /// <summary>  
        /// 物料管理-获取分页数据
        /// </summary>
        /// <param name="whereStr">条件字符 必须前加 and</param>  
        /// <param name="orderExpression">排序 例如 Order by ID asc</param>  
        /// <param name="pageIdex">当前索引页</param>  
        /// <param name="pageSize">每页记录数</param>  
        /// <returns></returns>  
        public DataTable GetMaterialinfoData(string whereStr, int pageIdex, int pageSize, out int dataCount)
        {
            return dbOperate.GetPagerData(CommonVariables.V_BEE_MATERIALINFO, whereStr, " order by statustime desc", pageIdex, pageSize, out dataCount);
        }

        /// <summary>
        /// 根据id获取物料信息
        /// </summary>
        public Model.Material GetMaterialList(int maid)
        {
            try
            {
                return cdata.FillModel<Model.Material>(materialDal.GetMaterialinfoData(maid)).First();
            }
            catch (Exception ex)
            {
                LogApi.DebugInfo(ex);
                return null;
            }
        }

    }
}
