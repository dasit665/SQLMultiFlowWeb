using System;
using System.Collections.Generic;

namespace SQLMultiFlowWeb
{
    public partial class TbScriptsNames
    {
        public TbScriptsNames()
        {
            TbScriptVersion = new HashSet<TbScriptVersion>();
        }

        public int Id { get; set; }
        public string ScriptName { get; set; }

        public virtual ICollection<TbScriptVersion> TbScriptVersion { get; set; }
    }
}
