using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMultiFlowWeb.Areas.Reports.Models
{
    public class ErrorModel
    {
        public int Id { get; set; }
        public DateTime DateAndTime { get; set; }
        public string ScriptName { get; set; }
        public decimal ScriptVersion { get; set; }
        public string ServerDb { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
