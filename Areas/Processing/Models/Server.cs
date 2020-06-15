using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMultiFlowWeb.Areas.Processing.Models
{
    public class Server
    {
        public string ServerDomainName { get; set; }
        public string DataBaseName { get; set; }
        public string ServerLogin { get; set; }
        public string ServerPassword { get; set; }
    }
}