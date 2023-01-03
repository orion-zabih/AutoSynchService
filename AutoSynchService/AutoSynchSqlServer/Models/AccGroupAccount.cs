using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class AccGroupAccount
    {
        public int Id { get; set; }
        public string? GroupName { get; set; }
        public int? HeadId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int BranchId { get; set; }
        public string? GroupCode { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
