using System;
using System.Collections.Generic;

namespace SQLMultiFlowWeb
{
    public partial class TbErrors
    {
        public int Id { get; set; }
        public DateTime DateAndTime { get; set; }
        public int ScriptVersionId { get; set; }
        public int ServerDbid { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public virtual TbScriptVersion ScriptVersion { get; set; }
        public virtual TbServerList ServerDb { get; set; }
    }
}
