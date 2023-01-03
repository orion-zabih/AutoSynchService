using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class PyEmployeeTransaction
    {
        public int Id { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public decimal? NetAmount { get; set; }
        public string? Source { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ReferenceId { get; set; }
        public string? Vno { get; set; }
        public int? CreatedBy { get; set; }
        public int? EmployeeId { get; set; }
        public int? BranchId { get; set; }
        public int? SessionId { get; set; }
        public string PayrollType { get; set; } = null!;
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
