using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMultiFlowWeb.Areas.Reprocessing.Models
{
    public class NonFlowedScriptsVersions
    {
        public int ID { get; set; }
        public string ScriptName { get; set; }
        public decimal ScriptVersion { get; set; }
        public DateTime DateOfTry { get; set; }
        public string ServerName { get; set; }
        public string ServerDB { get; set; }
    }
}