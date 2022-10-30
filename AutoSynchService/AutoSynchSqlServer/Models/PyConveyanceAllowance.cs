using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class PyConveyanceAllowance
    {
        public int Id { get; set; }
        public int FromBps { get; set; }
        public int ToBps { get; set; }
        public decimal Rate { get; set; }
        public bool? IsFlat { get; set; }
        public int BranchId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
