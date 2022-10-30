using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class AccVouchersLedger
    {
        public int Id { get; set; }
        public int VoucherMasterId { get; set; }
        public int VoucherDetailId { get; set; }
        public DateTime VoucherDate { get; set; }
        public int HeadId { get; set; }
        public int GroupId { get; set; }
        public int AccountId { get; set; }
        public string? AccountName { get; set; }
        public string? Description { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string VoucherType { get; set; } = null!;
        public int PartnerId { get; set; }
        public string? PartnerType { get; set; }
        public int ProductId { get; set; }
    }
}
