using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class AccVoucherNumbringSeries
    {
        public int Id { get; set; }
        public int FiscalYearId { get; set; }
        public string VoucherType { get; set; } = null!;
        public int BranchId { get; set; }
        public string? Prefix { get; set; }
        public string? Suffix { get; set; }
        public int BodyLength { get; set; }
        public int StartNo { get; set; }
        public int CurrentNo { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
