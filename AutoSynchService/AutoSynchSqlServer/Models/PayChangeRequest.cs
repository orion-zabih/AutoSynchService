using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class PayChangeRequest
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int DesignationId { get; set; }
        public string? EmployeeName { get; set; }
        public int? Bps { get; set; }
        public int WageId { get; set; }
        public string? PostingType { get; set; }
        public string? Status { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string? Remarks { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastUpdatedById { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int LastPostingSalaryId { get; set; }
        public int CountPostings { get; set; }
        public decimal Amount { get; set; }
        public bool IsFlate { get; set; }
        public int BranchId { get; set; }
        public decimal Frequency { get; set; }
        public decimal Hours { get; set; }
        public bool IsActive { get; set; }
        public decimal TotalLoanAmount { get; set; }
        public int LoanInstallments { get; set; }
    }
}
