using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class InvQuatationMaster
    {
        public int Id { get; set; }
        public int QuatationNo { get; set; }
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public DateTime? QuatationDateTime { get; set; }
        public bool IsCancel { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? ContactNo { get; set; }
        public int BranchId { get; set; }
        public int FiscalYearId { get; set; }
        public DateTime? DueDate { get; set; }
        public string? Status { get; set; }
        public string? Remarks { get; set; }
        public int WarehouseId { get; set; }
    }
}
