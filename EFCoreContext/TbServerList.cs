using System;
using System.Collections.Generic;

namespace SQLMultiFlowWeb
{
    public partial class TbServerList
    {
        public TbServerList()
        {
            TbErrors = new HashSet<TbErrors>();
            TbFlowed = new HashSet<TbFlowed>();
            TbRelations = new HashSet<TbRelations>();
        }

        public int Id { get; set; }
        public string ServerDomainName { get; set; }
        public string DataBaseName { get; set; }
        public string ServerLogin { get; set; }
        public string ServerPassword { get; set; }

        public virtual ICollection<TbErrors> TbErrors { get; set; }
        public virtual ICollection<TbFlowed> TbFlowed { get; set; }
        public virtual ICollection<TbRelations> TbRelations { get; set; }
    }
}
