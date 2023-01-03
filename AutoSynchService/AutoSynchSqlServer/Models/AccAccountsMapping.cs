using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class AccAccountsMapping
    {
        public int Id { get; set; }
        public int GroupAccountId { get; set; }
        public int AccountId { get; set; }
        public string? Description { get; set; }
        public string? DebitOrCredit { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int BranchId { get; set; }
        public string? MappingSource { get; set; }
        public bool? IsActive { get; set; }
        public int TypeId { get; set; }
        public int HeadId { get; set; }
        public string? MappingForm { get; set; }
        public int MappingSourceId { get; set; }
        public string? TransactionType { get; set; }
        public string? PartnerType { get; set; }
        public string? ItemBillType { get; set; }
    }
}
