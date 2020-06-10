using System;
using System.Collections.Generic;

namespace SQLMultiFlowWeb
{
    public partial class TbFlowed
    {
        public int Id { get; set; }
        public int ScriptVersionId { get; set; }
        public int ServerDbid { get; set; }
        public bool? VersionFlowed { get; set; }

        public virtual TbScriptVersion ScriptVersion { get; set; }
        public virtual TbServerList ServerDb { get; set; }
    }
}
