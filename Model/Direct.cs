using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 定向类
    /// </summary>

    public class Direct
    {

    }
    /// <summary>
    /// 定向数据
    /// </summary>
    public class DirectItem
    {
        public string directid { get; set; }
        public string directname { get; set; }
    }
    /// <summary>
    /// 其他定向
    /// </summary>
    public class DirectData
    {
        public string directtypeid { get; set; }
        public string directtypename { get; set; }
        public List<DirectItem> directtypedata { get; set; }
    }
    /// <summary>
    /// 定向地域信息-国家
    /// </summary>
    public class DirectCountry
    {
        public string cid { get; set; }
        public string cname { get; set; }                
    }
    /// <summary>
    /// 定向地域信息-区域
    /// </summary>
    public class DirectArea : DirectCountry
    {
        public string countryid { get; set; }
    }
    /// <summary>
    /// 定向地域信息-省份
    /// </summary>
    public class DirectProvince : DirectCountry
    {
        public string areaid { get; set; }
    }
    /// <summary>
    /// 定向地域信息-城市
    /// </summary>
    public class DirectCity : DirectCountry
    {
        public string provinceid { get; set; }
    }
    /// <summary>
    /// 时间定向
    /// </summary>
    public class DirectDate
    {
        public string datetype { get; set; }
        public List<DirectHour> hourdata { get; set; }
    }

    public class DirectHour
    {
        public string week { get; set; }
        public string starthour { get; set; }
        public string endhour { get; set; }
    }
    /// <summary>
    /// 地域定向
    /// </summary>
    public class DirectAreaRegion
    {
        public string cid { get; set; }
        public string clevel { get; set; }
        public string areaid { get; set; }
        public string provinceid { get; set; }
    }

}
