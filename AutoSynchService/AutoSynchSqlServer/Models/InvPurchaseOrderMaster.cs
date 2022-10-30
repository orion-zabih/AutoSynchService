using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class InvPurchaseOrderMaster
    {
        public int Id { get; set; }
        public DateTime? OrderDateTime { get; set; }
        public string? Reference { get; set; }
        public int? VendorId { get; set; }
        public decimal? TotalCost { get; set; }
        public decimal? TotalCostReceived { get; set; }
        public string? Terms { get; set; }
        public DateTime? DueDate { get; set; }
        public string? ShipVia { get; set; }
        public string? ShipTo1 { get; set; }
        public string? ShipTo2 { get; set; }
        public string? Instructions { get; set; }
        public string? Status { get; set; }
        public string? ShipToDestination { get; set; }
        public string? OrderingMode { get; set; }
        public DateTime? CancelDate { get; set; }
        public decimal? TotalCharges { get; set; }
        public string? Potype { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsCancelled { get; set; }
        public bool IsDeleted { get; set; }
        public int BranchId { get; set; }
        public int WarehouseId { get; set; }
        public int FiscalYearId { get; set; }
    }
}
