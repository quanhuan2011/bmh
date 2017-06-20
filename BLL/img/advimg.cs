using BLL.pub;
using CVBUtility;
using DAL.database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.img
{
   public class AdvImg
    {
        public AdvImg()
        {
          //  System.Configuration.AppSettingsReader appReader = new System.Configuration.AppSettingsReader();
            DatabasePool.GetDatabaseConnectStr("bee", "chinavb234123489");
        }
        /// <summary>
        /// 根据广告获取中间下载页图片地址列表
        /// </summary>
        /// <param name="adId"></param>
        /// <returns></returns>
        public string GetImgByAdv(string adId)
        {
            //定义参数            
            CommonDBOperation dbOperate = new CommonDBOperation();
            List<string> listImg = new List<string>();
            try
            {
                string sql = string.Format(@"select imgurl from bee.bee_ad_imageurl s1 where s1.adid =@adid  and s1.status=1 order by rk");
                ParamCollections pc = new ParamCollections();
                pc.Add("@adid", adId, OracleDataType.INT);

                DataTable dt = dbOperate.GetTable(sql, pc.GetParams());
                if (null != dt && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        listImg.Add(Convert.ToString(dr["imgurl"]).Trim());
                    }
                }
                Result.errCode = Result.successCode;
                Result.errMsg = "获取成功";
            }
            catch (Exception ex)
            {
                Result.errCode = Result.failCode;
                Result.errMsg = Convert.ToString(ex.Message);
            }
            finally
            {
                if (null != dbOperate)
                {
                    dbOperate.Close();
                    dbOperate.Dispose();
                    dbOperate = null;
                }
            }
            //返回结果
            return Result.GetResult(Result.errCode, Result.errMsg, listImg);
        }
    }
}
