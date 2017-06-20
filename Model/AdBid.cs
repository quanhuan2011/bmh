using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class AdBid
    {
    }
    /// <summary>
    /// 竞价管理投放范围
    /// </summary>
    public class PageListByBid
    {
        public string pageid { get; set; }
        public string pagename { get; set; }
        public string termid { get; set; }
        public string termname { get; set; }

    }
    /// <summary>
    /// 投放范围
    /// </summary>
    public class PutRangeByBid
    {
        public string pid { get; set; }
    }
}
