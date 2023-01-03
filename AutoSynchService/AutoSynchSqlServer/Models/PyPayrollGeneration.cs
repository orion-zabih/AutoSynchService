using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class PyPayrollGeneration
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int SalaryHeadId { get; set; }
        public decimal Amount { get; set; }
        public int PayrollMasterId { get; set; }
    }
}
