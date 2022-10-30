using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class ResFoodAvailability
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public int? AvailableDay { get; set; }
        public string? FromTime { get; set; }
        public string? ToTime { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int BranchId { get; set; }
        public string? AvailableDays { get; set; }
        public string? FoodIds { get; set; }
    }
}
