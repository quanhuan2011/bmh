using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 广告主信息
    /// </summary>
   public  class AdUser
    {
        public string aduserid { get; set; }
        public string name { get; set; }
        public string companyname { get; set; }
        public string contact { get; set; }
        public string sex { get; set; }
        public string age { get; set; }
        public string tel { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string balance { get; set; }
        public string industryid { get; set; }     
    }

    public class AdUserInfo
    {
        public string aduserid { get; set; }
        public string username { get; set; }     
        public string name { get; set; }
        public string companyname { get; set; }
        public string contact { get; set; }      
        public string tel { get; set; }
        public string email { get; set; }
        public string address { get; set; }       
        public string usertype { get; set; }
        public string usertypename { get; set; }
        public string regionid { get; set; }
        public string regionname { get; set; }
        public string saler { get; set; }
        public string parentid { get; set; }
        public string idno { get; set; }
        public string aduserno { get; set; }


    }

    public class LinkManByAdu
    {
        public string name { get; set; }
        public string position { get; set; }
        public string tel { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }

    }
    public class AdUserDoc
    {
        public string filename { get; set; }
        public string fileno { get; set; }
        public string filetype { get; set; }
        public string fileseq { get; set; }
    }
    /// <summary>
    /// 广告主报表
    /// </summary>
    public class AdListOfAdUser
    {
        public string showcnt { get; set; }
        public string clickcnt { get; set; }
        public string ecpm { get; set; }
        public string cpc { get; set; }
        public string deductsum { get; set; }

    }
    /// <summary>
    /// 广告主报表-按日
    /// </summary>
   public class AdListOfAdUserByDay : AdListOfAdUser
    {
        public string date { get; set; }
    }
    /// <summary>
    /// 广告主报表-按小时
    /// </summary>
   public class AdListOfAdUserByHour : AdListOfAdUser
    {
        public string hour { get; set; }
    }
    /// <summary>
    /// 广告主报表-按分类
    /// </summary>
   public class AdListOfAdUserByClass : AdListOfAdUser
    {
        public string classname { get; set; }
    }
    /// <summary>
    /// 广告主报表-按地域
    /// </summary>
   public class AdListOfAdUserByArea : AdListOfAdUser
    {
        public string area { get; set; }
    }

}
