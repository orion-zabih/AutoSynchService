using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class PyEmployeeDeductionMapping
    {
        public int Id { get; set; }
        public int DeductionId { get; set; }
        public int EmployeeDeductionId { get; set; }
        public decimal Value { get; set; }
    }
}
