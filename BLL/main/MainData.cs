using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL.pub;
using Model;
using BLL.pub;
using Common.pub;

namespace BLL.main
{
    /// <summary>
    /// 广告、广告位、物料 业务逻辑层
    /// by wangjc 20161014
    /// </summary>
    public class MainData
    {

        #region 变量定义
        Main main = new Main();
        ConvertData cdata = new ConvertData();
        #endregion

        /// <summary>
        /// 获取广告位列表数据
        /// </summary>
        public List<AdLocation> GetAdLocationList()
        {
            try
            {
                return cdata.FillModel<AdLocation>(main.GetAdLocationData());
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取物料列表数据
        /// </summary>
        public List<Model.Material> GetMaterialList()
        {
            try
            {
                return cdata.FillModel<Model.Material>(main.GetMaterialinfoData());
            }
            catch (Exception ex)
            {
                LogApi.DebugInfo(ex);
                return null;
            }
        }

        /// <summary>
        /// 获取物料列表数据
        /// </summary>
        public List<Advertisement> GetAdList()
        {
            try
            {
                return cdata.FillModel<Advertisement>(main.GetAdvertisementData());
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 根据用户获取广告列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Advertisement> GetAdList(string userId)
        {
            try
            {
                return cdata.FillModel<Advertisement>(main.GetAdvertisementData());
            }
            catch 
            {
                return null;
            }
        }


        

    }
}
