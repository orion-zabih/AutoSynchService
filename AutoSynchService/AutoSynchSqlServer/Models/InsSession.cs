using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsSession
    {
        public int Id { get; set; }
        public string? SessionName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Status { get; set; }
        public int BranchId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string? IsAdmissionOpen { get; set; }
        public bool? IsActive { get; set; }
    }
}
