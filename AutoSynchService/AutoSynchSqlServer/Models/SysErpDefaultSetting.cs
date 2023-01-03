using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class SysErpDefaultSetting
    {
        public int Id { get; set; }
        public string IsUseMemoryCashe { get; set; } = null!;
        public string? SoftwareMode { get; set; }
    }
}
