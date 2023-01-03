using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class SysModule
    {
        public int Id { get; set; }
        public int? SystemId { get; set; }
        public string? Module { get; set; }
        public string? DisplayName { get; set; }
        public int? DisplayOrder { get; set; }
        public string? Icon { get; set; }
        public int FormGroupId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
