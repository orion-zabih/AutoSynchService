using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InvInspectionMaster
    {
        public int Id { get; set; }
        public int GatePassInId { get; set; }
        public DateTime? InspectionDateTime { get; set; }
        public bool IsCancel { get; set; }
        public int? CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int UpdatedById { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int BranchId { get; set; }
        public int FiscalYearId { get; set; }
        public int VendorId { get; set; }
        public string? InspectedByIds { get; set; }
        public string? Remarks { get; set; }
        public int WarehouseId { get; set; }
        public bool IsApproved { get; set; }
        public int GatePassNo { get; set; }
    }
}
