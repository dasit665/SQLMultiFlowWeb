using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMultiFlowWeb.Areas.ChoiceVersion.Models
{
    public class ScriptNameIDVersions
    {
        public string ScriptName { get; set; }
        public int ScriptID { get; set; }
        public List<decimal> Versions { get; set; }
    }
}
