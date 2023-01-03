using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class PyPayrollGenerationDetail
    {
        public int Id { get; set; }
        public int PayrollGenerationId { get; set; }
        public int ReferenceId { get; set; }
        public decimal Amount { get; set; }
    }
}
