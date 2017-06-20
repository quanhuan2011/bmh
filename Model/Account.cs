using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public  class Account
    {
        /// <summary>
        /// 账户id
        /// </summary>
        public string accountid { get; set; }
        /// <summary>
        /// 账户名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 账户类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 广告主id
        /// </summary>
        public string aduserid { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        //public string createtime { get; set; }
        //public string statustime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string headimageurl { get; set; }

    }
}
