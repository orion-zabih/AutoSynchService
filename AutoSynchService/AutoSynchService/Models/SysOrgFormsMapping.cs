using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class SysOrgFormsMapping
    {
        public int Id { get; set; }
        public int OrgId { get; set; }
        public int FormId { get; set; }
        public string? FormLabelDesc { get; set; }
        public bool? IsEnable { get; set; }
        public int DisplayOrder { get; set; }
    }
}
