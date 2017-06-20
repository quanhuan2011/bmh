using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class AdLocation
    {
        /// <summary>
        /// 广告位id
        /// </summary>
        public int adlocationid { get; set; }
        /// <summary>
        /// 广告位名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 广告位标签
        /// </summary>
        public string tag { get; set; }
        /// <summary>
        /// 页面id
        /// </summary>
        public int pageid { get; set; }
        /// <summary>
        /// 系统
        /// </summary>
        public int system { get; set; }
        /// <summary>
        /// 轮播位置
        /// </summary>
        public int shufflocation { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createtime { get; set; }
        /// <summary>
        /// 状态时间
        /// </summary>
        public DateTime statustime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int status { get; set; }


    }

    public class AdLocationByPage
    {
        public string adlocationid { get; set; }
        public string adlocationname { get; set; }
    }

    //----------------------------竞价管理----------------------------------------------//
    /// <summary>
    /// 竞价管理-广告位管理竞价和非竞价列表
    /// </summary>
    public class AdlListByBid
    {
        public string adlocationid { get; set; }
        public string adlocationname { get; set; }
        public string bcode { get; set; }
        public string pagename { get; set; }
        public string subadtypename { get; set; }
        public string isbid { get; set; }
        public string alevel { get; set; }
    }


    //----------------------------报表数据----------------------------------------------//
    public class AdLocationList
   {
       public string requestcnt { get; set; }
       public string showcnt { get; set; }
       public string clickcnt { get; set; }
       public string ecpm { get; set; }
       public string cpc { get; set; }
       public string income { get; set; }
   }
   public class AdLocationListByDay : AdLocationList
    {
        public string date { get; set; }      
    }
   public class AdLocationListByHour : AdLocationList
   {
       public string hour { get; set; }
   }
   public class AdLocationListByClass : AdLocationList
   {
       public string classname { get; set; }
   }
   public class AdLocationListByArea : AdLocationList
   {
       public string area { get; set; }
   }
}
