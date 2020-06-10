using System;
using System.Collections.Generic;

namespace SQLMultiFlowWeb
{
    public partial class TbMarkers
    {
        public TbMarkers()
        {
            TbRelations = new HashSet<TbRelations>();
        }

        public int Id { get; set; }
        public string MarkerName { get; set; }

        public virtual ICollection<TbRelations> TbRelations { get; set; }
    }
}
