using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMultiFlowWeb.Areas.Processing.Models
{
    public class ScriptsServers
    {
        public List<string> ScriptsNames { get; set; }
        public List<string> Markers { get; set; }
        public List<Server> Servers { get; set; }
    }
}
