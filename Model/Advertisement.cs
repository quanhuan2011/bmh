using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Advertisement
    {
        /// <summary>
        /// 广告Id
        /// </summary>
        public int adid { get; set; }
        /// <summary>
        /// 广告名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 广告主id
        /// </summary>
        public int aduserid { get; set; }
        /// <summary>
        /// 页面id
        /// </summary>
        public int pageid { get; set; }
        /// <summary>
        /// 推送量
        /// </summary>
        public string putcount { get; set; }
        /// <summary>
        /// 推送开始时间
        /// </summary>
        public DateTime putstarttime { get; set; }
        /// <summary>
        /// 推送结束时间
        /// </summary>
        public DateTime putendtime { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public int price { get; set; }
        /// <summary>
        /// 剩余金额
        /// </summary>
        public string balance { get; set; }
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
        /// <summary>
        /// 最大推送量
        /// </summary>
        public int putmax { get; set; }
        /// <summary>
        /// 单日最大推送量
        /// </summary>
        public int putmaxbyday { get; set; }
    }

    public class SubAdTypeByAdType
    {
        public string subadtypeid { get; set; }
        public string subadtypename { get; set; }
    }
    public class SubAdTypeByAdTypeForAdu
    {
        public string subadtypeid { get; set; }
        public string subadtypename { get; set; }
        public string width { get; set; }
        public string height { get; set; }
    }

    //----------------------竞价--------------------------//
    public class AdvListByBid
    {
        public string adid { get; set; }
        public string adname { get; set; }
        public string adusername { get; set; }
        public string price { get; set; }
        public string isbid { get; set; }
        public string subadtypename { get; set; }

    }


    //------------------------报表---------------------------//
    /// <summary>
    /// 广告报表
    /// </summary>
    public class AdList
    {
        public string showcnt { get; set; }
        public string clickcnt { get; set; }
        public string ecpm { get; set; }
        public string cpc { get; set; }
        public string income { get; set; }

    }
    /// <summary>
    /// 广告报表-按日
    /// </summary>
    public class AdListByDay : AdList
    {
        public string date { get; set; }
    }
    /// <summary>
    /// 广告报表-按小时
    /// </summary>
    public class AdListByHour : AdList
    {
        public string hour { get; set; }
    }
    /// <summary>
    /// 广告报表-按分类
    /// </summary>
    public class AdListByClass : AdList
    {
        public string classname { get; set; }
    }
    /// <summary>
    /// 广告报表-按地域
    /// </summary>
    public class AdListByArea : AdList
    {
        public string area { get; set; }
    }












    /// <summary>
    /// 广告信息-广告管理新建和编辑
    /// </summary>
    public class AdInfo
    {
        /// <summary>
        /// 广告主id
        /// </summary>
        public string aduserid { get; set; }
        /// <summary>
        /// 广告主名称
        /// </summary>
        public string adusername { get; set; }
        /// <summary>
        /// 广告类型
        /// </summary>
        public string adtypeid { get; set; }
        /// <summary>
        /// 广告类型名称
        /// </summary>
        public string adtypename { get; set; }
        /// <summary>
        /// 广告位id
        /// </summary>
        public string adlocationid { get; set; }
        /// <summary>
        /// 广告位名称
        /// </summary>
        public string adlocationname { get; set; }
        /// <summary>
        /// 页面id
        /// </summary>
        public string pageid { get; set; }
        /// <summary>
        /// 页面名称
        /// </summary>
        public string pagename { get; set; }
        /// <summary>
        /// 广告id
        /// </summary>
        public string adid { get; set; }
        /// <summary>
        /// 广告名称
        /// </summary>
        public string adname { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public string price { get; set; }
        /// <summary>
        /// 推送开始时间
        /// </summary>
        public string putstarttime { get; set; }
        /// <summary>
        /// 推送结束时间
        /// </summary>
        public string putendtime { get; set; }
        /// <summary>
        /// 推送最大量
        /// </summary>
        public string putmax { get; set; }
        /// <summary>
        /// 每日推送最大量
        /// </summary>
        public string putmaxbyday { get; set; }
        /// <summary>
        /// 广告类型
        /// </summary>
        public string subadtypeid { get; set; }
        /// <summary>
        /// 广告形式
        /// </summary>
        public string subadtypename { get; set; }
        /// <summary>
        /// 是否竞价
        /// </summary>
        public string isbid { get; set; }
        /// <summary>
        /// 是否打底
        /// </summary>
        public string isbottom { get; set; }
        /// <summary>
        /// 计费方式
        /// </summary>
        public string billingtype { get; set; }
        /// <summary>
        /// 投放终端
        /// </summary>
        public string termid { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class MaterialInfoByAd
    {
        /// <summary>
        /// 权重类型
        /// </summary>
        public string weighttype { get; set; }
        /// <summary>
        /// 权重
        /// </summary>
        public string weight { get; set; }
        /// <summary>
        /// 物料id
        /// </summary>
        public string materialid { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string materialname { get; set; }
        /// <summary>
        /// 物料类型
        /// </summary>
        public string materialtype { get; set; }
        /// <summary>
        /// 物料类型名称
        /// </summary>
        public string materialtypename { get; set; }
        /// <summary>
        /// 物料图片地址
        /// </summary>
        public string imageurl { get; set; }
        /// <summary>
        /// 物料标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 物料宽度
        /// </summary>
        public string width { get; set; }
        /// <summary>
        /// 物料高度
        /// </summary>
        public string height { get; set; }
        /// <summary>
        /// 物料格式
        /// </summary>
        public string format { get; set; }
        /// <summary>
        /// 物料展示方式
        /// </summary>
        public string display { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 确定按键文本
        /// </summary>
        public string confirmtext { get; set; }
        /// <summary>
        /// 取消按键文本
        /// </summary>
        public string canceltext { get; set; }
        /// <summary>
        /// 弹出时间
        /// </summary>
        public string showtime { get; set; }
    }



    /// <summary>
    /// 广告定向类--根据当前广告
    /// </summary>
    public class DirectDataByAd
    {
        public string directid { get; set; }
        public string directname { get; set; }

    }
    public class DirectByAd
    {
        public string directtypeid { get; set; }
        public string directtypename { get; set; }
        public List<DirectDataByAd> directdata { get; set; }
    }

    /// <summary>
    /// 操作系统-定向类
    /// </summary>    
    public class SystemInfoByAd
    {
        public string systemid { get; set; }
        public string systemname { get; set; }
    }
    /// <summary>
    /// 分类-定向类
    /// </summary>
    public class ClassInfoByAd
    {
        public string classid { get; set; }
        public string classname { get; set; }
    }
    ///// <summary>
    ///// 分类-定向类
    ///// </summary>
    //public class OsSysterm
    //{
    //    public string systemid { get; set; }
    //    public string systemname { get; set; }
    //}

    //-----------------------------redis-----------------------//
    /// <summary>
    /// 广告推送量信息
    /// </summary>
    //public class AdPutInfoByRedis
    //{
    //    public List<AdPutInfoByRedisItem> data { get; set; }
    //}
    public class AdPutInfoByRedisItem
    {
        public string putmax { get; set; }
        public string putmaxbyday { get; set; }
        public string hadputtotal { get; set; }
        public string hadputbyday { get; set; }
    }
}
