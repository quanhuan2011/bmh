using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.pub;
using Model;
using Newtonsoft.Json;
using BLL.pub;
using StackExchange.Redis;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace BLL.redis
{
    public class RedisBase
    {
        System.Configuration.AppSettingsReader appReader = new System.Configuration.AppSettingsReader();

        public string strConnectType = "";
        //private BMH.Redis.Pub.RedisApi redisApi;

        private ConvertData cData = new ConvertData();
        //= System.Configuration.ConfigurationManager.AppSettings["ConnectType"];
        public ConnectionMultiplexer Init()
        {
            var config = new ConfigurationOptions();

            try
            {             
                config.EndPoints.Add("10.0.1.28:6379");
                config.Password = "baomihuaRDS1";                              
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(config);         
                return redis;
            }
            catch (Exception ex)
            {              
                LogApi.DebugInfo(ex);
                return null;
            }
        }

        /// <summary>
        /// 设置投放量上限
        /// </summary>
        /// <param name="adid"></param>
        /// <param name="putmax"></param>
        /// <param name="putmaxbyday"></param>
        public void SetPutMaxInfo(string adId, string putMax, string putMaxByDay, out bool boolStatus)
        {
            string strKey = string.Format("adinfo_{0}", adId);
            boolStatus = true;
            try
            {
                #region 取值
                string hadPutByDay = "0";
                string hadPutTotal = "0";
                string hadPutLastMin = "0";
                string hadPutLastHour = "0";
                string hadPutLastDay = "0";
                ConnectionMultiplexer redis = Init();
                if (redis == null)
                {
                    boolStatus = false;
                    return;
                }
                IDatabase db = redis.GetDatabase();
                string strVal = db.StringGet(strKey);
                LogApi.LogInfo("get_adinfo", string.Format("key:{0},val:{1}", strKey, strVal));
                if (!string.IsNullOrWhiteSpace(strVal))
                {
                    char[] s = { Convert.ToChar('"') };
                    strVal = strVal.TrimStart(s).TrimEnd(s).Replace("\\", "");
                    JArray json = (JArray)JsonConvert.DeserializeObject(strVal);
                    JObject obj = (JObject)json[0];
                    hadPutByDay = GetJObjectVal(obj, "hadputbyday", "0");
                    hadPutTotal = GetJObjectVal(obj, "hadputtotal", "0");
                    hadPutLastMin = GetJObjectVal(obj, "hadputlastmin", "0");
                    hadPutLastHour = GetJObjectVal(obj, "hadputlasthour", "0");
                    hadPutLastDay = GetJObjectVal(obj, "hadputlastday", "0");
                    if (int.Parse(GetJObjectVal(obj, "putmaxbyday", "0")) >= int.Parse(putMaxByDay))
                        boolStatus = false;
                }
                #endregion

                #region 赋值               
                string tempVal = new JArray(
                  new JObject(
                      new JProperty("putmax", putMax),
                      new JProperty("putmaxbyday", putMaxByDay),
                      new JProperty("hadputtotal", hadPutTotal),
                      new JProperty("hadputbyday", hadPutByDay),
                      new JProperty("hadputlastmin", hadPutLastMin),
                      new JProperty("hadputlasthour", hadPutLastHour),
                      new JProperty("hadputlastday", hadPutLastDay)
                  )
              ).ToString();
                tempVal = tempVal.Replace("\n", "");
                tempVal = tempVal.Replace("\r", "");
                tempVal = tempVal.Replace("\t", "");
                tempVal = tempVal.Replace(" ", "");

                LogApi.LogInfo("set_adinfo", string.Format("key:{0},val:{1}", strKey, tempVal));
                db.StringSet(strKey, tempVal);
                #endregion

            }
            catch (Exception ex)
            {
                boolStatus = false;
                LogApi.DebugInfo(ex);
            }

        }
        /// <summary>
        /// 推送广告投放信息到redis队列
        /// </summary>
        /// <param name="adId"></param>
        /// <param name="putMax"></param>
        /// <param name="putMaxByDay"></param>
        public void PushPutMaxInfo(string adId, string putMax, string putMaxByDay)
        {                        
            try
            {
                #region 定义
                string tempKey = string.Format("adinfopush_{0}", adId);
                ConnectionMultiplexer redis = Init();
                if (redis == null)
                {
                    return;
                }
                IDatabase db = redis.GetDatabase();
                #endregion

                #region 推送队列
                //投放上限和日投放上限
                string tempVal = string.Format("2|{0},{1}", putMax,putMaxByDay);
                db.ListRightPush(tempKey, tempVal);
                #endregion

            }
            catch (Exception ex)
            {
                LogApi.DebugInfo(ex);
            }
        }
        /// <summary>
        /// 设置广告主redis-日投放信息
        /// </summary>
        /// <param name="adUserId"></param>
        /// <param name="putMaxByDay"></param>
        /// <param name="boolStatus"></param>
        public void SetPutMaxInfoByAdU(string adUserId, string putMaxByDay, out bool boolStatus)
        {
            string strKey = string.Format("aduserinfo_{0}", adUserId);
            boolStatus = true;
            try
            {
                #region 取值
                string balance = "0";
                string balanceLastMin = "0";
                string balanceLastHour = "0";
                string balanceLastDay = "0";
                string hadPutByDay = "0";
                ConnectionMultiplexer redis = Init();
                if (redis == null)
                {
                    boolStatus = false;
                    return;
                }
                IDatabase db = redis.GetDatabase();
                string strVal = db.StringGet(strKey);
                LogApi.LogInfo("get_aduserinfo", string.Format("key:{0},val:{1}", strKey, strVal));
                if (!string.IsNullOrWhiteSpace(strVal))
                {
                    char[] s = { Convert.ToChar('"') };
                    strVal = strVal.TrimStart(s).TrimEnd(s).Replace("\\", "");
                    JArray json = (JArray)JsonConvert.DeserializeObject(strVal);
                    JObject obj = (JObject)json[0];
                    balance = GetJObjectVal(obj, "balance", "0");
                    balanceLastMin = GetJObjectVal(obj, "balancelastmin", "0");
                    balanceLastHour = GetJObjectVal(obj, "balancelasthour", "0");
                    balanceLastDay = GetJObjectVal(obj, "balancelastday", "0");
                    hadPutByDay = GetJObjectVal(obj, "hadputbyday", "0");
                    if (int.Parse(GetJObjectVal(obj, "putmaxbyday", "0")) >= int.Parse(putMaxByDay))
                        boolStatus = false;
                }
                #endregion

                #region 赋值               
                string tempVal = new JArray(
                  new JObject(
                      new JProperty("balance", balance),
                      new JProperty("balancelastmin", balanceLastMin),
                      new JProperty("balancelasthour", balanceLastHour),
                      new JProperty("balancelastday", balanceLastDay),
                       new JProperty("putmaxbyday", putMaxByDay),
                      new JProperty("hadputbyday", hadPutByDay)

                  )
              ).ToString();
                tempVal = tempVal.Replace("\n", "");
                tempVal = tempVal.Replace("\r", "");
                tempVal = tempVal.Replace("\t", "");
                tempVal = tempVal.Replace(" ", "");

                LogApi.LogInfo("set_aduserinfo", string.Format("key:{0},val:{1}", strKey, tempVal));
                db.StringSet(strKey, tempVal);
                #endregion

            }
            catch (Exception ex)
            {
                boolStatus = false;
                LogApi.DebugInfo(ex);
            }

        }
        /// <summary>
        /// 推送投放量上限信息到redis队列出处理
        /// </summary>
        /// <param name="adUserId"></param>
        /// <param name="putMaxByDay"></param>
        public void PushPutMaxInfoByAdu(string adUserId, string putMaxByDay)
        {                                
            try
            {
                #region 定义               
                string tempKey = string.Format("aduserinfopush_{0}", adUserId);
                ConnectionMultiplexer redis = Init();
                if (redis == null)
                {                  
                    return;
                }
                IDatabase db = redis.GetDatabase();
                #endregion

                #region 推送队列
                //广告主日总投放量信息
                string tempVal = string.Format("3|{0}", putMaxByDay);
                db.ListRightPush(tempKey, tempVal);
                #endregion

            }
            catch (Exception ex)
            {             
                LogApi.DebugInfo(ex);
            }
        }
        /// <summary>
        /// 设置广告主redis-余额相关
        /// </summary>
        /// <param name="adUserId"></param>
        /// <param name="money">充值金额</param>
        public void SetReChargeInfo(string adUserId, string money)
        {
            string strKey = string.Format("aduserinfo_{0}", adUserId);

            try
            {
                #region 取值
                string balance = "0";
                string balanceLastMin = "0";
                string balanceLastHour = "0";
                string balanceLastDay = "0";
                string putMaxByDay = "0";
                string hadPutByDay = "0";
                ConnectionMultiplexer redis = Init();
                if (redis == null)
                {
                    return;
                }
                IDatabase db = redis.GetDatabase();
                string strVal = db.StringGet(strKey);
                LogApi.LogInfo("get_aduserinfo", string.Format("key:{0},val:{1}", strKey, strVal));
                if (!string.IsNullOrWhiteSpace(strVal))
                {
                    char[] s = { Convert.ToChar('"') };
                    strVal = strVal.TrimStart(s).TrimEnd(s).Replace("\\", "");
                    JArray json = (JArray)JsonConvert.DeserializeObject(strVal);
                    JObject obj = (JObject)json[0];
                    balance = GetJObjectVal(obj, "balance", "0");
                    balanceLastMin = GetJObjectVal(obj, "balancelastmin", "0");
                    balanceLastHour = GetJObjectVal(obj, "balancelasthour", "0");
                    balanceLastDay = GetJObjectVal(obj, "balancelastday", "0");
                    putMaxByDay = GetJObjectVal(obj, "putmaxbyday", "0");
                    hadPutByDay = GetJObjectVal(obj, "hadputbyday", "0");
                }
                #endregion

                #region 金额计算
                balance = (float.Parse(balance) + float.Parse(money)).ToString();
                balanceLastMin = (float.Parse(balanceLastMin) + float.Parse(money)).ToString();
                balanceLastHour = (float.Parse(balanceLastHour) + float.Parse(money)).ToString();
                balanceLastDay = (float.Parse(balanceLastDay) + float.Parse(money)).ToString();
                #endregion

                #region 赋值               
                string tempVal = new JArray(
                  new JObject(
                      new JProperty("balance", balance),
                      new JProperty("balancelastmin", balanceLastMin),
                      new JProperty("balancelasthour", balanceLastHour),
                      new JProperty("balancelastday", balanceLastDay),
                      new JProperty("putmaxbyday", putMaxByDay),
                      new JProperty("hadputbyday", hadPutByDay)
                  )
              ).ToString();
                tempVal = tempVal.Replace("\n", "");
                tempVal = tempVal.Replace("\r", "");
                tempVal = tempVal.Replace("\t", "");
                tempVal = tempVal.Replace(" ", "");

                LogApi.LogInfo("set_aduserinfo", string.Format("key:{0},val:{1}", strKey, tempVal));
                db.StringSet(strKey, tempVal);
                #endregion

            }
            catch (Exception ex)
            {
                LogApi.DebugInfo(ex);
            }

        }
        /// <summary>
        /// 推送充值信息到redis队列
        /// </summary>
        /// <param name="adUserId"></param>
        /// <param name="money"></param>
        public void PushReChargeInfo(string adUserId, string money)
        {           
            try
            {
                #region 定义   
                string tempKey = string.Format("aduserinfopush_{0}", adUserId);              
                ConnectionMultiplexer redis = Init();
                if (redis == null)
                {
                    return;
                }
                IDatabase db = redis.GetDatabase();
                #endregion

                #region 推送队列
                //充值
                string tempVal1 = string.Format("2|{0}", money);
                db.ListRightPush(tempKey, tempVal1);
                ////上一分钟、小时和日余额
                //string tempVal2 = string.Format("5|2|{0}", money);
                //db.ListRightPush(tempKey, tempVal2);
                #endregion

            }
            catch (Exception ex)
            {
                LogApi.DebugInfo(ex);
            }
        }
        /// <summary>
        /// 修改价格
        /// </summary>
        /// <returns></returns>
        public bool UpdatePrice(string adId,string adUId,string price)
        {
            ConnectionMultiplexer redis = Init();
            try
            {
                //1号库           
                IDatabase db = redis.GetDatabase(1);
                string strKey = "BeePrice" + adId;
                string strVal = adUId + "_" + price;            
                //一个小时
                TimeSpan ts = new TimeSpan(0, 1, 0, 0);
                return db.StringSet(strKey, strVal, ts);
            }
            catch(Exception ex)
            {
                LogApi.DebugInfo(ex);
            }
            finally
            {
                if (null != redis)
                {
                    redis.Close();
                    redis.Dispose();
                    redis = null;
                }
            }

            return false;          
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
                result = (string)obj[key];
                if (string.IsNullOrWhiteSpace(result))
                    result = defaultVal;
            }
            catch
            {
                result = defaultVal;
            }
            return result;
        }
        private void test()
        {

        }

    }
}
