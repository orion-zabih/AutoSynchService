using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InvStockTransfer
    {
        public int Id { get; set; }
        public string? Remarks { get; set; }
        public int FromWarehouseId { get; set; }
        public int ToWarehouseId { get; set; }
        public DateTime? TransferDate { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsDeleted { get; set; }
        public int BranchId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public decimal Cost { get; set; }
        public int DemandNoteId { get; set; }
        public bool IsReturn { get; set; }
        public int FiscalYearId { get; set; }
    }
}
