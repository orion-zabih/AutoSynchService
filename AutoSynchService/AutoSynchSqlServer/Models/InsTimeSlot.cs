using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsTimeSlot
    {
        public int Id { get; set; }
        public string? SlotDescription { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int BranchId { get; set; }
        public int CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int LastUpdatedById { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string? PartOfDay { get; set; }
        public string? ModuleName { get; set; }
    }
}
