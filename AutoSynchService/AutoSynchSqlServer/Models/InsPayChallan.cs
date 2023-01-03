using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsPayChallan
    {
        public int Id { get; set; }
        public string? ChallanNo { get; set; }
        public string? CustomerDetail { get; set; }
        public string? BillStatus { get; set; }
        public string? DueDate { get; set; }
        public string? AmountWithinDueDate { get; set; }
        public string? AmountAfterDueDate { get; set; }
        public string? BillingMonth { get; set; }
        public string? DatePaid { get; set; }
        public string? AmountPaid { get; set; }
        public string? TranAuthId { get; set; }
        public string? Reserved { get; set; }
        public string? TransactionAmount { get; set; }
        public string? TransDate { get; set; }
        public string? TransTime { get; set; }
        public bool? IsActive { get; set; }
        public string? BankMnemonic { get; set; }
        public int AdmissionId { get; set; }
        public decimal NetAmount { get; set; }
        public int ChallanId { get; set; }
        public int BranchId { get; set; }
        public int OrgId { get; set; }
        public int FiscalYearId { get; set; }
        public string PaidSource { get; set; } = null!;
        public int FeePostedId { get; set; }
        public int ReceivedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? PaidDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int DeletedBy { get; set; }
        public string? CreatedFromChallanNo { get; set; }
        public DateTime? BlockedDate { get; set; }
        public int BlockedBy { get; set; }
        public string? BlockedReason { get; set; }
        public int FormNo { get; set; }
        public string? ChallanSource { get; set; }
        public int StudentMeritListId { get; set; }
        public int SessionId { get; set; }
        public string? CustomerType { get; set; }
        public int CustomerId { get; set; }
    }
}
