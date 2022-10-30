using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class InvDeliveryChallanMaster
    {
        public int Id { get; set; }
        public int DeliveryChallanNo { get; set; }
        public int QuatationId { get; set; }
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
        public int WarehouseId { get; set; }
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? Remarks { get; set; }
    }
}
