using System;
using System.Collections.Generic;

namespace SQLMultiFlowWeb
{
    public partial class TbLogins
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string HashPasswors { get; set; }
    }
}
