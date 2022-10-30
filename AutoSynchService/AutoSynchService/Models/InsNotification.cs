using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class InsNotification
    {
        public int Id { get; set; }
        public int? No { get; set; }
        public string? Title { get; set; }
        public int? TypeId { get; set; }
        public DateTime? Date { get; set; }
        public int? ToOrgId { get; set; }
        public string? Remarks { get; set; }
        public int BranchId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsRead { get; set; }
    }
}
