using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.permission
{
    /// <summary>
    /// 验证权限
    /// </summary>
    public class VerifyPermission
    {
        /// <summary>
        /// 获取权限等级
        /// </summary>
        /// <returns></returns>
        public string GetPermissionLevel(string accountId)
        { 
            if (string.IsNullOrWhiteSpace(accountId))
                return "-1";
            try
            {
                //允许操作权限列表
                //读取配置文件
                System.Configuration.AppSettingsReader appReader = new System.Configuration.AppSettingsReader();
                string permissionList = appReader.GetValue("PermissionList", typeof(string)).ToString();
                if (string.IsNullOrWhiteSpace(permissionList))
                {
                    if (accountId == "2")
                        return "1";
                    else
                        return "-1";
                }
                else
                {
                    Array tempList = permissionList.Split(',');
                    foreach (var tempId in tempList)
                    {
                        if (accountId == tempId.ToString())
                            return "1";
                    }
                }
            }
            catch
            {
                return "-1";
            }
            return "-1";
        }

    }
}
