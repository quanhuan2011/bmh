using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.pub;
using BLL.manager;
using System.Collections;

namespace BMH.EagleEye.api
{
    /// <summary>
    /// ManagerHandler 的摘要说明
    /// </summary>
    public class ManagerHandler : IHttpHandler
    {
        AdvertisementManager adManager;
        MaterialManager materialManager;
        DirectManager directManager;
        AdLocationManager adLManager;
        AdUserManager adUserManager;
        AreaManager areaManager;
        string _strAccountId = string.Empty;
        string _strAccountName = string.Empty;
        string _strAccountUserName = string.Empty;
        string _strAccountType = string.Empty;
        string _strHeadImageUrl = string.Empty;
        string _strAdUserId = string.Empty;
        public void ProcessRequest(HttpContext context)
        {
            #region 登录cookie信息
            BaseClass.POMOHOCookie cookies = new BaseClass.POMOHOCookie();
            _strAccountId = cookies.BeeAccountId;
            _strAccountName = cookies.BeeAccountName;
            _strAccountUserName = cookies.BeeAccountUserName;
            _strAccountType = cookies.BeeAccountType;
            _strHeadImageUrl = cookies.BeeHeadImageUrl;
            _strAdUserId = cookies.BeeAdUserId;
            #endregion

            context.Response.ContentType = "text/plain";

            string method = GetRequestVal("method");
            string result = "";
            switch (method)
            {
                case "UpdateAdData":
                    result = UpdateAdData();
                    break;
                case "UpdateAdDataByAdu":
                    result = UpdateAdDataByAdu();
                    break;
                case "InsertAdData":
                    result = InsertAdData();
                    break;
                case "InsertAdDataByAdu":
                    result = InsertAdDataByAdu();
                    break;
                case "GetMaterialListData":
                    result = GetMaterialListData();
                    break;
                case "UpdateAdStatus":
                    result = UpdateAdStatus();
                    break;
                case "GetDirectTypeData":
                    result = GetDirectTypeData();
                    break;
                case "InsertMaterialData":
                    result = InsertMaterialData();
                    break;
                case "GetCityInfoData":
                    result = GetCityInfoData();
                    break;
                case "GetCountryData":
                    result = GetCountryData();
                    break;
                case "GetAreaData":
                    result = GetAreaData();
                    break;
                case "GetProvinceData":
                    result = GetProvinceData();
                    break;
                case "GetCityData":
                    result = GetCityData();
                    break;
                case "GetAdLocationData":
                    result = GetAdLocationData();
                    break;
                case "GetAdLocationDataNew":
                    result = GetAdLocationDataNew();
                    break;
                case "GetSubAdTypeData":
                    result = GetSubAdTypeData();
                    break;
                case "GetSubAdTypeDataByAdu":
                    result = GetSubAdTypeDataByAdu();
                    break;
                case "GetPutMaxInfoByAdUData":
                    result = GetPutMaxInfoByAdUData();
                    break;
                case "SetPutMaxInfoByAdUData":
                    result = SetPutMaxInfoByAdUData();
                    break;
                case "GetBalanceData":
                    result = GetBalanceData();
                    break;
                case "InsertRechargeData":
                    result = InsertRechargeData();
                    break;
                case "InsertAdUserData":
                    result = InsertAdUserData();
                    break;
                case "UpdateAdUserData":
                    result = InsertAdUserData();
                    break;
                case "GetAdUserData":
                    result = GetAdUserData();
                    break;
                case "GetProvinceList":
                    result = GetProvinceList();
                    break;
                case "GetCityList":
                    result = GetCityList();
                    break;
                case "SaveAdUserBaseInfo":
                    result = SaveAdUserBaseInfo();
                    break;               
                case "GetLinkManList":
                    result = GetLinkManList();
                    break;
                case "InsertAdUserFile":
                    result = InsertAdUserFile();
                    break;
                case "UpdateAdUserFile":
                    result = UpdateAdUserFile();
                    break;
                case "CreateAduInfo":
                    result = CreateAduInfo();
                    break;
                case "SearchLastAcctInfo":
                    result = SearchLastAcctInfo();
                    break;
                default:
                    result = BLL.pub.Result.GetFailResult("错误的调用方式，此方法不存在");
                    break;
            }
            context.Response.Write(result);
        }
        /// <summary>
        /// 日志记录操作方法
        /// </summary>
        /// <param name="funcName"></param>
        /// <param name="data"></param>
        private void LogModify(string funcName,string data)
        {
            YYLog.ClassLibrary.Log.WriteLog(string.Format("YYLog.Modify:/api/ManagerHandler/{0}",funcName), "账户信息:账户id{0},账户名{1},ip{2};操作信息:{3}", _strAccountId, _strAccountName, pageclass.IP.GetIP(), data);
        }
        /// <summary>
        /// 更新广告数据
        /// </summary>
        /// <returns></returns>
        private string UpdateAdData()
        {            
            string data = GetRequestVal("data");
            //1一致，0不一致
            string flag = GetRequestVal("flag","0");
            LogModify("UpdateAdData", string.Format("data:{0},flag:{1}",data,flag));            
            adManager = new AdvertisementManager();
            return adManager.UpdateAdData(data,flag);
        }
        /// <summary>
        /// 代理商更新广告信息
        /// </summary>
        /// <returns></returns>
        private string UpdateAdDataByAdu()
        {        
            string data = GetRequestVal("data");
            //1一致，0不一致
            string flag = GetRequestVal("flag", "0");
            LogModify("UpdateAdDataByAdu", string.Format("data:{0},flag:{1}", data, flag));
            adManager = new AdvertisementManager();
            return adManager.UpdateAdDataByAdu(data,flag);
        }
        /// <summary>
        /// 新增广告信息
        /// </summary>
        /// <returns></returns>
        private string InsertAdData()
        {            
            string data = GetRequestVal("data");
            LogModify("InsertAdData", data);
            adManager = new AdvertisementManager();
            return adManager.InsertAdData(data);
        }
        /// <summary>
        /// 代理商新增广告信息
        /// </summary>
        /// <returns></returns>
        private string InsertAdDataByAdu()
        {            
            string data = GetRequestVal("data");
            LogModify("InsertAdDataByAdu", data);
            adManager = new AdvertisementManager();
            return adManager.InsertAdDataByAdu(data);
        }
        /// <summary>
        /// 获取物料列表-根据类型
        /// </summary>
        /// <returns></returns>
        private string GetMaterialListData()
        {
            string adtypeid = GetRequestVal("adtypeid");
            string aduserid = GetRequestVal("aduserid");
            materialManager = new MaterialManager();
            return materialManager.GetMaterialList(aduserid, adtypeid);

        }
        /// <summary>
        /// 更新广告状态
        /// </summary>
        /// <returns></returns>
        private string UpdateAdStatus()
        {
            string updatetype = GetRequestVal("updatetype");
            string data = GetRequestVal("data");
            LogModify("UpdateAdStatus", string.Format("updatetype:{0},data:{1}",updatetype,data));
            adManager = new AdvertisementManager();
            return adManager.UpdateAdStatus(updatetype, data);

        }
        /// <summary>
        /// 获取定向类型
        /// </summary>
        /// <returns></returns>
        private string GetDirectTypeData()
        {
            string userId = "";
            directManager = new DirectManager();
            return directManager.GetDirectTypeData(userId);

        }
        /// <summary>
        /// 新增物料
        /// </summary>
        /// <returns></returns>
        private string InsertMaterialData()
        {            
            string data = GetRequestVal("data");
            LogModify("InsertMaterialData", data);
            materialManager = new MaterialManager();
            return materialManager.InsertMaterialData(data);
        }
        /// <summary>
        /// 获取地域信息
        /// </summary>
        /// <returns></returns>
        private string GetCityInfoData()
        {            
            directManager = new DirectManager();
            return directManager.GetCityInfoData("");
        }
        /// <summary>
        /// 获取国家信息
        /// </summary>
        /// <returns></returns>
        private string GetCountryData()
        {            
            string data = GetRequestVal("data");
            materialManager = new MaterialManager();
            return materialManager.InsertMaterialData(data);
        }
        /// <summary>
        /// 获取区域信息
        /// </summary>
        /// <returns></returns>
        private string GetAreaData()
        {            
            string data = GetRequestVal("data");
            materialManager = new MaterialManager();
            return materialManager.InsertMaterialData(data);
        }
        /// <summary>
        /// 获取省份信息
        /// </summary>
        /// <returns></returns>
        private string GetProvinceData()
        {            
            string data = GetRequestVal("data");
            materialManager = new MaterialManager();
            return materialManager.InsertMaterialData(data);
        }
        /// <summary>
        /// 获取城市信息
        /// </summary>
        /// <returns></returns>
        private string GetCityData()
        {            
            string data = GetRequestVal("data");
            materialManager = new MaterialManager();
            return materialManager.InsertMaterialData(data);
        }
        /// <summary>
        /// 获取广告位列表
        /// </summary>
        /// <returns></returns>
        private string GetAdLocationData()
        {
            string pageId = GetRequestVal("pageid");
            adLManager = new AdLocationManager();
            return adLManager.GetAdLocationNameData(string.Format(" s1.pageid={0}", pageId));
        }
        /// <summary>
        /// 非竞价广告位
        /// </summary>
        /// <returns></returns>
        private string GetAdLocationDataNew()
        {
            string pageId = GetRequestVal("pageid");
            adLManager = new AdLocationManager();
            return adLManager.GetAdLocationNameData(string.Format(" s1.pageid={0}  and s1.isbid=0", pageId));
        }
        private string GetSubAdTypeData()
        {
            string adTypeId = GetRequestVal("adtypeid");
            adManager = new AdvertisementManager();
            return adManager.GetSubAdTypeData(string.Format(" s1.adtypeid={0} ", adTypeId));
        }
        private string GetSubAdTypeDataByAdu()
        {
            string adTypeId = GetRequestVal("adtypeid");
            adManager = new AdvertisementManager();
            return adManager.GetSubAdTypeDataByAdu(string.Format(" s1.adtypeid={0} ", adTypeId));
        }
        /// <summary>
        /// 获取广告主推送量上限信息
        /// </summary>
        /// <returns></returns>
        private string GetPutMaxInfoByAdUData()
        {
            string adUserId = GetRequestVal("aduid");
            adUserManager = new AdUserManager();
            return adUserManager.GetPutMaxInfoData(adUserId, "");
        }
        /// <summary>
        /// 设置广告主推送量上限信息
        /// </summary>
        /// <returns></returns>
        private string SetPutMaxInfoByAdUData()
        {
            string adUserId = GetRequestVal("aduid");
            string putMaxByDay = GetRequestVal("putmaxbyday");
            LogModify("SetPutMaxInfoByAdUData", string.Format("aduserid:{0},putmaxbyday:{1}", adUserId, putMaxByDay));
            adUserManager = new AdUserManager();
            return adUserManager.SetPutMaxInfoData(adUserId, putMaxByDay, "");
        }
        /// <summary>
        /// 获取广告主余额
        /// </summary>
        /// <returns></returns>
        private string GetBalanceData()
        {
            string adUserId = GetRequestVal("aduid");
            adUserManager = new AdUserManager();
            return adUserManager.GetBalanceData(adUserId, "");
        }
        /// <summary>
        /// 新增充值信息
        /// </summary>
        /// <returns></returns>
        private string InsertRechargeData()
        {
            string adUserId = GetRequestVal("aduid");
            string accountId = GetRequestVal("acctid");
            string money = GetRequestVal("money");
            LogModify("InsertRechargeData", string.Format("aduserid:{0},accountid:{1},money:{2}",adUserId,accountId,money));
            adUserManager = new AdUserManager();
            return adUserManager.InsertRechargeData(accountId, adUserId, money, "");
        }
        /// <summary>
        /// 新增代理商数据
        /// </summary>
        /// <returns></returns>
        private string InsertAdUserData()
        {
            string data = GetRequestVal("data");
            LogModify("InsertAdUserData", data);
            adUserManager = new AdUserManager();
            return adUserManager.InsertAdUserData(data);
        }
        /// <summary>
        /// 获取代理商资料
        /// </summary>
        /// <returns></returns>
        private string GetAdUserData()
        {
            string adUserId = GetRequestVal("aduserid");
            adUserManager = new AdUserManager();
            return adUserManager.GetAdUserInfo(adUserId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetProvinceList()
        {
            areaManager = new AreaManager();
            return areaManager.GetProvinceList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetCityList()
        {
            string provinceId = GetRequestVal("provinceid");
            areaManager = new AreaManager();
            return areaManager.GetCityList(provinceId);
        }
        //
        private string SaveAdUserBaseInfo()
        {
            string data = GetRequestVal("data");
            LogModify("SaveAdUserBaseInfo", data);
            adUserManager = new AdUserManager();
            return adUserManager.SaveAdUserBaseInfo(data);
        }

        private string GetLinkManList()
        {
            string adUserId = GetRequestVal("aduid");
            adUserManager = new AdUserManager();
            return adUserManager.GetLinkManList(adUserId);
        }
        private string InsertAdUserFile()
        {
            string data = GetRequestVal("data");
            LogModify("InsertAdUserFile", data);
            adUserManager = new AdUserManager();
            return adUserManager.InsertAdUserFile(data);
        }
        private string UpdateAdUserFile()
        {
            string data = GetRequestVal("data");
            LogModify("UpdateAdUserFile", data);
            adUserManager = new AdUserManager();
            return adUserManager.UpdateAdUserFile(data);
        }
        /// <summary>
        /// 创建广告主
        /// </summary>
        /// <returns></returns>
        private string CreateAduInfo()
        {
            string aduName = GetRequestVal("aduname");
            string aduCompName = GetRequestVal("aducompname");
            string aduContact = GetRequestVal("aducontact");
            string aduSex = GetRequestVal("adusex");
            string aduTel = GetRequestVal("adutel");
            LogModify("CreateAduInfo", string.Format("aduname:{0},aducompname:{1},aducontact:{2},adusex:{3},adutel:{4}",aduName,aduCompName,aduContact,aduSex,aduTel));
            adUserManager = new AdUserManager();
            return adUserManager.CreateAduInfo(aduName, aduCompName, aduContact, aduSex, aduTel);
        }
        /// <summary>
        /// 查找最近一个帐号（广告主）
        /// </summary>
        /// <returns></returns>
        private string SearchLastAcctInfo()
        {
            adUserManager = new AdUserManager();
            return adUserManager.SearchLastAcctInfo();
        }
        /// <summary>
        /// 获取请求键值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetRequestVal(string key,string defaultVal=null)
        {
            try
            {
                string _val = Convert.ToString(HttpContext.Current.Request[key]);
                if (string.IsNullOrEmpty(_val) && !string.IsNullOrEmpty(defaultVal))
                {
                    return defaultVal;
                }
                else
                {
                    return _val;
                }               
            }
            catch
            {
                return "";
            }

        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}