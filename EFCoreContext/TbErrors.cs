using System;
using System.Collections.Generic;

namespace SQLMultiFlowWeb
{
    public partial class TbErrors
    {
        public int Id { get; set; }
        public DateTime DateAndTime { get; set; }
        public string ServerDb { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
