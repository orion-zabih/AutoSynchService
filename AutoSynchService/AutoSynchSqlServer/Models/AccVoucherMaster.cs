using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class AccVoucherMaster
    {
        public int Id { get; set; }
        public string VoucherNo { get; set; } = null!;
        public DateTime VoucherDate { get; set; }
        public string VoucherType { get; set; } = null!;
        public string? Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public int BranchId { get; set; }
        public bool IsCancelled { get; set; }
        public bool IsDeleted { get; set; }
        public int MainAccountId { get; set; }
        public int? HeadId { get; set; }
        public int? GroupId { get; set; }
        public int FiscalYearId { get; set; }
        public int ReferenceId { get; set; }
        public string? JvFormType { get; set; }
        public string? DebitableCreditable { get; set; }
        public string? PaidToReceivedFrom { get; set; }
        public int SerialNumber { get; set; }
        public string? BankAccountNo { get; set; }
        public string? ChequeNo { get; set; }
        public string? BankName { get; set; }
        public string? CancelReason { get; set; }
        public string? VoucherStatus { get; set; }
        public int StatusChangedById { get; set; }
        public DateTime? StatusChangedDate { get; set; }
        public DateTime? ChequeDate { get; set; }
    }
}
