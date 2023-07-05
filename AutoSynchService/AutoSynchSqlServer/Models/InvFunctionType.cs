using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InvFunctionType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }
        public int BranchId { get; set; }
        public int DisplayOrder { get; set; }
        public string? Code { get; set; }
        public bool IsDeleted { get; set; }
    }
}
