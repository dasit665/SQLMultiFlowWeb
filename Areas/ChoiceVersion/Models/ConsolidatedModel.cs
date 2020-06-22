using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMultiFlowWeb.Areas.ChoiceVersion.Models
{
    public class ConsolidatedModel
    {
        public List<ScriptNameIDVersions> Scripts { get; set; }
        public List<ServersIDNameDB> Servers { get; set; }
        public List<MarkerIDName> Markers { get; set; }
    }
}
