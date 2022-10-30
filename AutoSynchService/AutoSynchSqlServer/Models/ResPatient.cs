using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class ResPatient
    {
        public int Id { get; set; }
        public string? PatientMrNo { get; set; }
        public string? PatientName { get; set; }
        public int? WardId { get; set; }
        public int? BedId { get; set; }
        public bool? IsDischarge { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int BranchId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
