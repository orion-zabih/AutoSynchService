using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InvShift
    {
        public int Id { get; set; }
        public string? Shift { get; set; }
        public TimeSpan? CutOffTime { get; set; }
        public int OrderNo { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int BranchId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public TimeSpan? StartTime { get; set; }
    }
}
