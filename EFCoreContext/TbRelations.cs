using System;
using System.Collections.Generic;

namespace SQLMultiFlowWeb
{
    public partial class TbRelations
    {
        public int Id { get; set; }
        public int ServerListId { get; set; }
        public int MarkerId { get; set; }

        public virtual TbMarkers Marker { get; set; }
        public virtual TbServerList ServerList { get; set; }
    }
}
