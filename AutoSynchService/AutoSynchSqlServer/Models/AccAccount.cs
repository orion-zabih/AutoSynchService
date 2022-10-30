using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class AccAccount
    {
        public int Id { get; set; }
        public int? HeadId { get; set; }
        public int? GroupId { get; set; }
        public string? AccountName { get; set; }
        public decimal? OpeningBalanceDr { get; set; }
        public decimal? OpeningBalanceCr { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int BranchId { get; set; }
        public string? AccountCode { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
