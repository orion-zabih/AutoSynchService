using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsExamAdditionalCreteria
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Remarks { get; set; }
        public int? CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? LastUpdatedById { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string? DistributionType { get; set; }
        public int BranchId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
