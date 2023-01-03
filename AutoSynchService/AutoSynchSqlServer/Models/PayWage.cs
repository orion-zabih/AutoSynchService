using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class PayWage
    {
        public int Id { get; set; }
        public string? WageName { get; set; }
        public string? Code { get; set; }
        public string? Type { get; set; }
        public string? Operation { get; set; }
        public DateTime? StartYear { get; set; }
        public bool? IsFreez { get; set; }
        public DateTime? FreezDate { get; set; }
        public string? ShortName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsFlate { get; set; }
        public decimal Value { get; set; }
        public int DependantWageId { get; set; }
        public bool IsInclusive { get; set; }
        public string IncrementOn { get; set; } = null!;
        public int CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int LastUpdatedById { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public int DisplayOrder { get; set; }
        public int BranchId { get; set; }
        public bool IsDeleted { get; set; }
        public string? Remarks { get; set; }
        public bool IsSystemDefined { get; set; }
    }
}
