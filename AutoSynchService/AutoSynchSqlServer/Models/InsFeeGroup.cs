using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class InsFeeGroup
    {
        public int Id { get; set; }
        public string GroupName { get; set; } = null!;
        public string? Description { get; set; }
        public int DisplayOrder { get; set; }
        public int BranchId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsApplyChangesOnStudents { get; set; }
        public int CategoryId { get; set; }
    }
}
