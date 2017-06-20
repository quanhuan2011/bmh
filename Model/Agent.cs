using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Agent
    {

    }
    public class AgentForPie
    {
        public string name { get; set;}
        public string value { get; set; }

    }

    public class AgentDeductDetail
    {
        public string orderid { get; set; }
        public string clickcnt { get; set; }
        public string incomesum { get; set; }

    }

    public class AgentLinkUrlDetail
    {
        public string orderid { get; set; }
        public string linkurl { get; set; }
        public string clickcnt { get; set; }
        public string incomesum { get; set; }

    }
}
