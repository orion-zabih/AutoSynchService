using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsStudentGroup
    {
        public int Id { get; set; }
        public string? GroupName { get; set; }
        public string? Description { get; set; }
        public int BranchId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int LastUpdatedById { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}
