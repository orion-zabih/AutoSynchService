using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class PyCorporateSalaryMaster
    {
        public int Id { get; set; }
        public int EmployeePostId { get; set; }
        public decimal? Salary { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int FiscalYearId { get; set; }
        public int BranchId { get; set; }
    }
}
