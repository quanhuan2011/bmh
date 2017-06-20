using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    class OracleEntity
    {

    }

    public class Infodata
    {
        /// <summary>
        /// 
        /// </summary>
        public int adid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int pageid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int adlocationid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int adtypeid { get; set; }
        /// <summary>
        /// 圣斗士APP信息流导量1new
        /// </summary>
        public string adname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string putstarttime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string putendtime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string putmax { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int putmaxbyday { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int aduserid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int accountid { get; set; }
    }

    public class MaterialdataItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int materialid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int weighttype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int weight { get; set; }
    }

    public class SystemdataItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int systemid { get; set; }
    }

    public class ClassdataItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int classid { get; set; }
    }
    /// <summary>
    /// 更新广告信息实体类
    /// </summary>
    public class UpdateAdData
    {
        /// <summary>
        /// 
        /// </summary>
        public Infodata infodata { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<MaterialdataItem> materialdata { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SystemdataItem> systemdata { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string areadata { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ClassdataItem> classdata { get; set; }
    }
}
