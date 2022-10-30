using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class PyEmployeeSalaryAllowanceMapping
    {
        public int Id { get; set; }
        public int EmployeeSalaryId { get; set; }
        public int AllowanceId { get; set; }
        public decimal Value { get; set; }
        public decimal? GrossAmount { get; set; }
    }
}
