using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class SysOrgModulesMapping
    {
        public int Id { get; set; }
        public int OrgId { get; set; }
        public int ModuleId { get; set; }
        public string? ModuleLabelDesc { get; set; }
        public bool? IsEnable { get; set; }
        public int DisplayOrder { get; set; }
    }
}
