using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class HrmDepartment
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public int BranchId { get; set; }
        public int DisplayOrder { get; set; }
        public string? Code { get; set; }
        public bool IsDeleted { get; set; }
    }
}
