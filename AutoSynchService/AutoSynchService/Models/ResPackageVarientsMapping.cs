using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class ResPackageVarientsMapping
    {
        public int Id { get; set; }
        public int PackageVarientId { get; set; }
        public int VarientId { get; set; }
        public decimal Qty { get; set; }
    }
}
