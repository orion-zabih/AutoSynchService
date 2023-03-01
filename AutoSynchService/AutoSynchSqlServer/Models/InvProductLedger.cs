using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InvProductLedger
    {
        public int Id { get; set; }
        public DateTime? TransDate { get; set; }
        public int ProductId { get; set; }
        public string? Remarks { get; set; }
        public int ReferenceId { get; set; }
        public string Source { get; set; } = null!;
        public decimal QtyIn { get; set; }
        public decimal QtyOut { get; set; }
        public int BranchId { get; set; }
        public bool IsCancel { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int UnitId { get; set; }
        public int WarehouseId { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? BatchBarcode { get; set; }
        public string? SourceParty { get; set; }
        public decimal Cost { get; set; }
        public decimal AverageCost { get; set; }
        public decimal RetailPrice { get; set; }
        public int FiscalYearId { get; set; }
        public int PackageId { get; set; }
        public int MaterialId { get; set; }
        public decimal QtyCut { get; set; }
    }
}
