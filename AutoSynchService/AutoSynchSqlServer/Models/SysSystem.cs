using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class SysSystem
    {
        public int Id { get; set; }
        public string? SystemName { get; set; }
        public string? DisplayName { get; set; }
        public int DisplayOrder { get; set; }
        public string? SysController { get; set; }
        public string? SysAction { get; set; }
        public string? Icon { get; set; }
        public bool IsActive { get; set; }
        public string? UserAuthGroupName { get; set; }
        public bool IsDeleted { get; set; }
        public string? Icon2 { get; set; }
    }
}
