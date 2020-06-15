using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMultiFlowWeb.Areas.Processing.Models
{
    public class Flow
    {
        public string[] Scripts { get; set; }
        public string[] Servers { get; set; }
    }
}
