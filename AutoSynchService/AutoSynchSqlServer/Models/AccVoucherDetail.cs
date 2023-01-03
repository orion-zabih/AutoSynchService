using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class AccVoucherDetail
    {
        public int Id { get; set; }
        public int VoucherMasterId { get; set; }
        public int? HeadId { get; set; }
        public int? GroupId { get; set; }
        public int AccountId { get; set; }
        public string? Description { get; set; }
        public decimal AmountDebit { get; set; }
        public decimal AmountCredit { get; set; }
        public string Type { get; set; } = null!;
        public int PartnerId { get; set; }
        public string? PartnerType { get; set; }
        public int? ProductId { get; set; }
        public string? MappingSource { get; set; }
        public string? Folio { get; set; }
        public string? MappingForm { get; set; }
    }
}
