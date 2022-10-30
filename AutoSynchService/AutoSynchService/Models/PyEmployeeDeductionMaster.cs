using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class PyEmployeeDeductionMaster
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int FiscalYearId { get; set; }
        public int BranchId { get; set; }
    }
}
