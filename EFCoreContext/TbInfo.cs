using System;
using System.Collections.Generic;

namespace SQLMultiFlowWeb
{
    public partial class TbInfo
    {
        public int Id { get; set; }
        public DateTime DataTime { get; set; }
        public string ServerDb { get; set; }
        public string Script { get; set; }
        public string MessageInfo { get; set; }
    }
}
