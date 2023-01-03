using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InvStockAdjustment
    {
        public int Id { get; set; }
        public int WarehouseId { get; set; }
        public int BranchId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? Remarks { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime? AdjustmentDateTime { get; set; }
        public int FiscalYearId { get; set; }
    }
}
