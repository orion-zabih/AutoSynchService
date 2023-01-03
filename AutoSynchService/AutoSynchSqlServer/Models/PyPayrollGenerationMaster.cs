using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class PyPayrollGenerationMaster
    {
        public int Id { get; set; }
        public int FiscalYearId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int BranchId { get; set; }
        public int SessionId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string Vno { get; set; } = null!;
        public string? Remarks { get; set; }
        public bool IsApproved { get; set; }
        public decimal PayrollTotalValue { get; set; }
        public string PayrollType { get; set; } = null!;
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
