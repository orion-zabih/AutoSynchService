using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class PayPayrollMaster
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int SessionId { get; set; }
        public int FiscalYearId { get; set; }
        public string? Remarks { get; set; }
        public decimal TotalValue { get; set; }
        public string? CancelledRemarks { get; set; }
        public int CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int LastUpdatedById { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public bool? IsActive { get; set; }
        public string? Status { get; set; }
        public int PayCode { get; set; }
        public bool IsCancelled { get; set; }
        public int BranchId { get; set; }
        public decimal TotalApprovedValue { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? ApprovedDate { get; set; }
    }
}
