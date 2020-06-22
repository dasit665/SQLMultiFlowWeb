using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMultiFlowWeb.Areas.Reports.Models
{
    public class InfoModel
    {
        public int Id { get; set; }
        public DateTime DataTime { get; set; }
        public string ServerDb { get; set; }
        public string Script { get; set; }
        public string MessageInfo { get; set; }
    }
}
