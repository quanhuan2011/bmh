using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using BLL.pub;
using DAL.database;
using System.Data;
using Common.pub;
using CVBUtility;

namespace BLL.manager
{
    /// <summary>
    /// 账户管理
    /// </summary>
    public class AccountManager
    {
        ConvertData cData = new ConvertData();
        DBOperate dbOperate = new DBOperate();
        /// <summary>
        /// 根据用户名返回帐号信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Account GetAccountInfo(string userName)
        {
            Account account = new Account();
            string sql = string.Format("select accountid,name,username,password,type,aduserid,remark,status,headimageurl from bee_accountinfo s1 where (s1.username='{0}') and status=1", userName);
            DataTable dt = dbOperate.GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                account = cData.FillModel<Account>(dt.Rows[0]);
            }
            return account;
        }
        /// <summary>
        /// 根据用户名密码返回帐号信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Account GetAccountInfo(string userName, string passWord)
        {
            Account account = new Account();
            string sql = string.Format("select accountid,name,username,password,type,aduserid,remark,status,headimageurl from bee_accountinfo s1 where (s1.username='{0}') and (s1.password='{1}' or s1.password ='{3}') and s1.type={2} and status=1", userName, passWord);
            DataTable dt = dbOperate.GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                account = cData.FillModel<Account>(dt.Rows[0]);
            }
            return account;
        }
        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <param name="accountType">账户类型</param>
        /// <param name="account">用户信息</param>
        /// <returns></returns>
        public bool CheckAccount(string userName, string passWord,out Account account)
        {
            //验证用户名有效性

            //验证密码有效性

            //密码加密匹配
            string md5PassWord = Utility.Md5Encrypt(passWord, EncryptType.Md5Code32);
            account = null;
            //验证用户
            try
            {
                string sql = string.Format("select count(1) from bee_accountinfo s1 where (s1.username='{0}') and (s1.password='{1}' or s1.password='{2}')  and s1.status=1", userName, passWord, md5PassWord);
                int accountCnt = dbOperate.GetCount(sql);
                if (accountCnt < 1)
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            //如果存在 返回用户信息
            try
            {
                string sql = string.Format("select accountid,name,username,password,type,aduserid,remark,status,headimageurl from bee_accountinfo s1 where (s1.username='{0}') and (s1.password='{1}' or s1.password ='{2}')  and status=1", userName, passWord, md5PassWord);
                DataTable dt = dbOperate.GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    account = cData.FillModel<Account>(dt.Rows[0]);
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <param name="accountType"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool UpdatePassWord(string accountId, string passWord)
        {
            //密码加密匹配
            passWord = Utility.Md5Encrypt(passWord, EncryptType.Md5Code32);
            //如果存在 返回用户信息
            try
            {
                string sql = string.Format("update bee_accountinfo u1 set u1.password ='{0}' ,u1.statustime=sysdate where u1.accountid={1}", passWord, accountId);
                int i = dbOperate.ExecuteStatement(sql);
                if (i < 1)
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="accountId">帐号id</param>
        /// <param name="passWord">帐号密码</param>
        /// <param name="operationId">操作人id</param>
        /// <returns></returns>
        public bool UpdatePassWord(string accountId, string passWord, string operationId)
        {
            //密码加密匹配
            passWord = Utility.Md5Encrypt(passWord, EncryptType.Md5Code32);
            //如果存在 返回用户信息
            try
            {
                string sql = string.Format("update bee_accountinfo u1 set u1.password ='{0}' ,u1.statustime=sysdate,u1.operationid={2} where u1.accountid={1}", passWord, accountId, operationId);
                int i = dbOperate.ExecuteStatement(sql);
                if (i < 1)
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// 记录登录日志
        /// </summary>
        public void LogInfo(Dictionary<string, string> dict)
        {
            string linkid = cData.GetDictionaryVal(dict, "accountid");
            string linktype = cData.GetDictionaryVal(dict, "accounttype");
            string type = cData.GetDictionaryVal(dict, "type");
            string ip = cData.GetDictionaryVal(dict, "ip");
            string ua = cData.GetDictionaryVal(dict, "ua");
            string country = string.Empty;
            string province = string.Empty;
            string city = string.Empty;
            string address = string.Empty;

            //匹配 
            DataTable dt1 = GetIpLib(ip);
            if (dt1 != null && dt1.Rows.Count > 0)
            {
                address = dt1.Rows[0]["area"].ToString();
            }
            DataTable dt2 = GetIpLibFormat(ip);
            if (dt2 != null && dt2.Rows.Count > 0)
            {
                country = dt2.Rows[0]["a1"].ToString();
                province = dt2.Rows[0]["a2"].ToString();
                city = dt2.Rows[0]["a3"].ToString();
            }
            string sql = string.Format(@"insert into bee_loginfo
                                          (logid,
                                           linkid,
                                           linktype,
                                           type,
                                           ip,
                                           createtime,
                                           statustime,
                                           status,
                                           ua,
                                           country,
                                           province,
                                           city,
                                           address)
                                        values
                                          (@logid,
                                           @linkid,
                                           @linktype,
                                           @type,
                                           @ip,
                                           sysdate,
                                           sysdate,
                                           @status,
                                           @ua,
                                           @country,
                                           @province,
                                           @city,
                                           @address)");

            try
            {
                //组装数据
                ParamCollections pc = new ParamCollections();
                pc.Add("@logid", dbOperate.GetSeqValue(CommonVariables.SEQ_BEE_LOGINFO_ID), OracleDataType.INT);
                pc.Add("@linkid", linkid, OracleDataType.INT);
                pc.Add("@linktype", linktype, OracleDataType.INT);
                pc.Add("@type", type, OracleDataType.INT);
                pc.Add("@ip", ip, OracleDataType.STRING);
                pc.Add("@status", "1", OracleDataType.INT);
                pc.Add("@ua", ua, OracleDataType.STRING);
                pc.Add("@country", country, OracleDataType.STRING);
                pc.Add("@province", province, OracleDataType.STRING);
                pc.Add("@city", city, OracleDataType.STRING);
                pc.Add("@address", address, OracleDataType.STRING);
                //执行sql
                dbOperate.ExecuteStatement(sql, pc.GetParams());

            }
            catch (Exception ex)
            {
                LogApi.DebugInfo(ex);
            }
        }
        /// <summary>
        /// 获取广告主id
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public string GetAdUserId(string accountId)
        {
            string adUserId = "";
            string sql = string.Format("select nvl(aduserid,-1) aduserid from bee_accountinfo s1 where s1.accountid={0} and s1.status=1", accountId);
            DataTable dt = dbOperate.GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                adUserId = dt.Rows[0]["aduserid"].ToString();
            }
            else
            {
                adUserId = "-1";
            }
            return adUserId;

        }
        /// <summary>
        /// 获取广告主信息
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public AdUser GetAdUserInfo(string accountId)
        {
            AdUser adUser = new AdUser();
            //  string adUserId = "";
            string sql = string.Format(@"select s1.aduserid,
                                               s1.name,
                                               s1.companyname,
                                               s1.contact,
                                               s1.sex,
                                               s1.age,
                                               s1.tel,
                                               s1.email,
                                               s1.address,
                                               s1.balance,
                                               s1.industryid
                                          from bee_aduserinfo s1, bee_accountinfo s2
                                         where s1.aduserid = s2.aduserid
                                           and s2.accountid = {0}
                                        ", accountId);
            DataTable dt = dbOperate.GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                adUser = cData.FillModel<AdUser>(dt.Rows[0]);
            }
            return adUser;

        }
        /// <summary>
        /// 获取ip匹配地址
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public DataTable GetIpLib(string ip)
        {
            string sql = string.Format(@"select *
                                          from bee.bee_ip_lib
                                         where startipnum < '{0}'
                                           and endipnum > '{0}'", ip);
            DataTable dt = dbOperate.GetDataTable(sql);
            return dt;
        }
        /// <summary>
        /// 获取ip匹配格式化地址 国家省市
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public DataTable GetIpLibFormat(string ip)
        {
            string sql = string.Format(@"select *
                                          from bee.bee_ip_lib_format
                                         where startipnum < '{0}'
                                           and endipnum > '{0}'", ip);
            DataTable dt = dbOperate.GetDataTable(sql);
            return dt;
        }
    }
}
