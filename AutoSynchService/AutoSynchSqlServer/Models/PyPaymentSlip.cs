using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class PyPaymentSlip
    {
        public int Id { get; set; }
        public string SlipNo { get; set; } = null!;
        public int EmployeeId { get; set; }
        public decimal NetAmount { get; set; }
        public int LedgerId { get; set; }
        public int CreatedById { get; set; }
        public string? Remark { get; set; }
        public int BranchId { get; set; }
        public DateTime PaymentMonthYearDay { get; set; }
        public DateTime PaymentToDate { get; set; }
        public DateTime CreateDate { get; set; }
        public string PayrollType { get; set; } = null!;
    }
}
