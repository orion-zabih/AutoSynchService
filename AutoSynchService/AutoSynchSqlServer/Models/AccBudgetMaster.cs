using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class AccBudgetMaster
    {
        public int Id { get; set; }
        public string? BudgetTitle { get; set; }
        public int FiscalYearId { get; set; }
        public decimal TotalBudget { get; set; }
        public decimal IncreaseInPercent { get; set; }
        public decimal BroughtForward { get; set; }
        public decimal GovernmentFund { get; set; }
        public decimal OwnResources { get; set; }
        public decimal Other { get; set; }
        public string Status { get; set; } = null!;
        public int BranchId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsCancelled { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}
