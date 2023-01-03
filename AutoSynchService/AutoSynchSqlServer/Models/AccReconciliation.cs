using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class AccReconciliation
    {
        public int Id { get; set; }
        public int ReconcileAccountId { get; set; }
        public DateTime StatmentEndingDate { get; set; }
        public decimal StatmentEndingBalance { get; set; }
        public DateTime BeginingDate { get; set; }
        public decimal BeginingBalance { get; set; }
        public int ReconcileBy { get; set; }
        public DateTime ReconcileOn { get; set; }
        public string? ClearVouchers { get; set; }
        public string? UnClearVouchers { get; set; }
        public bool Status { get; set; }
        public int BranchId { get; set; }
        public decimal Payments { get; set; }
        public decimal Deposits { get; set; }
    }
}
