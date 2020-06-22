using System;
using System.Collections.Generic;

namespace SQLMultiFlowWeb
{
    public partial class TbScriptVersion
    {
        public TbScriptVersion()
        {
            TbErrors = new HashSet<TbErrors>();
            TbFlowed = new HashSet<TbFlowed>();
        }

        public int Id { get; set; }
        public int ScriptId { get; set; }
        public DateTime Touch { get; set; }
        public decimal Virsion { get; set; }
        public string ScriptContent { get; set; }

        public virtual TbScriptsNames Script { get; set; }
        public virtual ICollection<TbErrors> TbErrors { get; set; }
        public virtual ICollection<TbFlowed> TbFlowed { get; set; }
    }
}
