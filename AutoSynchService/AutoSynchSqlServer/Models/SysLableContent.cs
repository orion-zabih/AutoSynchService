using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class SysLableContent
    {
        public int Id { get; set; }
        public string? Language { get; set; }
        public int OrgId { get; set; }
        public string LableName { get; set; } = null!;
        public string LableContentStr { get; set; } = null!;
    }
}
