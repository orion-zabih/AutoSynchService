using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class PayWageDetail
    {
        public int Id { get; set; }
        public int WageId { get; set; }
        public decimal Amount { get; set; }
        public int DesignationId { get; set; }
        public int RegionId { get; set; }
        public int Scale { get; set; }
        public string? RegionSelectionType { get; set; }
        public bool? IsFlate { get; set; }
        public bool IsCurrent { get; set; }
        public DateTime StartEntryDate { get; set; }
        public decimal Initial { get; set; }
        public decimal Increment { get; set; }
        public decimal Maximum { get; set; }
        public bool IsDeleted { get; set; }
        public int DependantWageId { get; set; }
        public string IncrementOn { get; set; } = null!;
        public int? Year { get; set; }
    }
}
