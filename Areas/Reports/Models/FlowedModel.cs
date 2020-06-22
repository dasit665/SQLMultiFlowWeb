using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMultiFlowWeb.Areas.Reports.Models
{
    public class FlowedModel
    {
        public int Id { get; set; }
        public string ScriptName { get; set; }
        public decimal ScriptVersion { get; set; }
        public string ServerNameDB { get; set; }
        public DateTime DateOfTry { get; set; }
        public bool? VersionFlowed { get; set; }
    }
}