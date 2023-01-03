using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class PyAdvanceLoanInfo
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string? Source { get; set; }
        public DateTime? Date { get; set; }
        public int ReferenceId { get; set; }
        public string? EmployeePayrollType { get; set; }
        public decimal Amount { get; set; }
        public DateTime? EffectiveFrom { get; set; }
        public int TotalInstallments { get; set; }
        public decimal InstallmentAmount { get; set; }
        public int? BranchId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? AdvanceMonthYearDay { get; set; }
        public int PayrollReferenceId { get; set; }
        public int PaymentFromAccount { get; set; }
        public decimal OpenLoanPaidAmount { get; set; }
    }
}
