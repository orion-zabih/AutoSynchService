using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InvGatePassInMaster
    {
        public int Id { get; set; }
        public int GatePassNo { get; set; }
        public int PurchaseOrderId { get; set; }
        public string? DriverName { get; set; }
        public string? VehicleNo { get; set; }
        public DateTime? GatePassDateTime { get; set; }
        public bool IsCancel { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? DriverContact { get; set; }
        public int BranchId { get; set; }
        public int FiscalYearId { get; set; }
        public int VendorId { get; set; }
        public string? Status { get; set; }
        public string? Remarks { get; set; }
        public int WarehouseId { get; set; }
    }
}
