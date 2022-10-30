using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class AccBudgetDetail
    {
        public int Id { get; set; }
        public int? BudgetMasterId { get; set; }
        public int? AccountId { get; set; }
        public decimal? EstimatedAmount { get; set; }
        public decimal? ApprovedAmount { get; set; }
        public decimal? ActualAmount { get; set; }
    }
}
