using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class SysHtml
    {
        public int Id { get; set; }
        public int OrgId { get; set; }
        public int BranchId { get; set; }
        public string? Section { get; set; }
        public string? HtmlFormate { get; set; }
    }
}
