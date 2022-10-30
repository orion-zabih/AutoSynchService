using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class SysFeature
    {
        public int Id { get; set; }
        public string? Feature { get; set; }
        public string? Details { get; set; }
        public string? DisplayName { get; set; }
        public int DisplayOrder { get; set; }
        public int ModuleId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
