using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class InvDemandNote
    {
        public int Id { get; set; }
        public string? Remarks { get; set; }
        public DateTime? DemandDate { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsDeleted { get; set; }
        public int BranchId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int FromWarehouseId { get; set; }
        public int ToWarehouseId { get; set; }
        public string? Status { get; set; }
        public int FiscalYearId { get; set; }
    }
}
