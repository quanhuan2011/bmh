using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 物料信息表
    /// </summary>
    public class Material
    {
        #region MyRegion
        ///// <summary>
        ///// 物料ID
        ///// </summary>
        //public string materialid { get; set; }
        ///// <summary>
        ///// 物料名称
        ///// </summary>
        //public string name { get; set; }
        ///// <summary>
        ///// 广告主ID
        ///// </summary>
        //public string aduserid { get; set; }
        //public string imgurl { get; set; }
        //public string linkurl { get; set; }
        //public string title { get; set; }
        //public string wide { get; set; }
        //public string high { get; set; }
        //public string size { get; set; }
        //public string format { get; set; }
        ///// <summary>
        ///// 展示方式  1新窗口 2原窗口
        ///// </summary>
        //public string display { get; set; }
        ///// <summary>
        ///// 权重类型
        ///// </summary>
        //public string weighttype { get; set; }
        ///// <summary>
        ///// 权重值
        ///// </summary>
        //public string weight { get; set; }
        //public DateTime createtime { get; set; }
        //public DateTime statustime { get; set; }
        //public string status { get; set; }
        ///// <summary>
        ///// 物料类型，1图片 2信息流
        ///// </summary>
        //public string materialtype { get; set; }
        ///// <summary>
        ///// 操作人ID
        ///// </summary>
        //public string operationid { get; set; } 
        #endregion

        private int _materialid = 0;
        /// <summary>
        /// 物料ID，序列：SEQ_BEE_MATERIALINFO_ID
        /// </summary>
        public int materialid
        {
            get { return this._materialid; }
            set
            {
                if (value.ToString() != string.Empty && value != 0)
                {
                    this._materialid = value;
                }
            }
        }
        /// <summary>
        /// 广告主ID
        /// </summary>
        public int aduserid
        {
            get;
            set;
        }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string name
        {
            get;
            set;
        }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string imageurl
        {
            get;
            set;
        }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string linkurl
        {
            get;
            set;
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string title
        {
            get;
            set;
        }
        /// <summary>
        /// 宽
        /// </summary>
        public int width
        {
            get;
            set;
        }
        /// <summary>
        /// 高
        /// </summary>
        public int height
        {
            get;
            set;
        }
        /// <summary>
        /// 大小
        /// </summary>
        public int sizes
        {
            get;
            set;
        }
        /// <summary>
        /// 格式
        /// </summary>
        public string format
        {
            get;
            set;
        }
        private int _display = 1;
        /// <summary>
        /// 呈现方式 1新 2原
        /// </summary>
        public int display
        {
            get { return this._display; }
            set
            {
                if (value.ToString() != string.Empty && value != 0)
                {
                    this._display = value;
                }

            }
        }

        /// <summary>
        /// 物料类型，1图片，2信息流
        /// </summary>
        public int materialtype
        {
            get;
            set;
        }
        private int _ismark = 1;
        /// <summary>
        /// 是否添加广告显示，1需要，0不需要
        /// </summary>
        public int ismark
        {
            get { return this._ismark; }
            set
            {
                if (value.ToString() != string.Empty)
                {
                    this._ismark = value;
                }
            }
        }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public int operationid
        {
            get;
            set;
        }
        /// <summary>
        /// 创建时间/上传时间
        /// </summary>
        public string createtime
        {
            get;
            set;
        }
        /// <summary>
        /// 弹窗时间
        /// </summary>
        public string showtime
        {
            get;
            set;
        }
        /// <summary>
        /// 确认文本
        /// </summary>
        public string confirmtext
        {
            get;
            set;
        }
        /// <summary>
        /// 取消文本
        /// </summary>
        public string canceltext
        {
            get;
            set;
        }
        /// <summary>
        /// 内容：类型为弹窗
        /// </summary>
        public string remark
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 物料数据-类型
    /// </summary>
    public class MaterialByType
    {
        /// <summary>
        /// 物料id
        /// </summary>
        public string materialid { get; set; }
        /// <summary>
        /// 广告主id
        /// </summary>
        public string aduserid { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string imageurl { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string linkurl { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        public string width { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        public string height { get; set; }
        /// <summary>
        /// 尺寸
        /// </summary>
        public string sizes { get; set; }
        /// <summary>
        /// 格式
        /// </summary>
        public string format { get; set; }
        /// <summary>
        /// 展现方式
        /// </summary>
        public string display { get; set; }
        /// <summary>
        /// 系统类型
        /// </summary>
        public string systemtype { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string createtime { get; set; }
        /// <summary>
        /// 物料类型
        /// </summary>
        public string materialtype { get; set; }
        /// <summary>
        /// 操作id
        /// </summary>
        public string operationid { get; set; }
        /// <summary>
        /// 操作名称
        /// </summary>
        public string operationname { get; set; }
        /// <summary>
        /// 物料类型名称
        /// </summary>
        public string materialtypename { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 弹出时间
        /// </summary>
        public string showtime { get; set; }
        /// <summary>
        /// 确认按钮文本
        /// </summary>
        public string confirmtext { get; set; }
        /// <summary>
        /// 取消按钮文本
        /// </summary>
        public string canceltext { get; set; }


    }





    //------------------------------报表------------------------------------//
    /// <summary>
    /// 报表数据-物料列表
    /// </summary>
    public class MaterialList
    {
        public string showcnt { get; set; }
        public string clickcnt { get; set; }
        public string ecpm { get; set; }
        public string cpc { get; set; }
        public string income { get; set; }

    }

    /// <summary>
    /// 报表数据-物料列表-日期
    /// </summary>
    public class MaterialListByDay : MaterialList
    {
        public string date { get; set; }
    }

    /// <summary>
    /// 报表数据-物料列表-时段
    /// </summary>
    public class MaterialListByHour : MaterialList
    {
        public string hour { get; set; }
    }

    /// <summary>
    /// 报表数据-物料列表-分类
    /// </summary>
    public class MaterialListByClass : MaterialList
    {
        public string classname { get; set; }
    }

}
