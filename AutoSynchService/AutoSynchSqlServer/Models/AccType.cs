using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class AccType
    {
        public int Id { get; set; }
        public string? TypeName { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int BranchId { get; set; }
        public int DisplayOrder { get; set; }
        public string? TypeCode { get; set; }
        public string? TypeGroup { get; set; }
    }
}
