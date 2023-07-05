using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class PayPayrollDetail
    {
        public int Id { get; set; }
        public int MasterId { get; set; }
        public int ChildId { get; set; }
        public int EmployeeId { get; set; }
        public int WageId { get; set; }
        public int WageDetailId { get; set; }
        public decimal Amount { get; set; }
        public string WageType { get; set; } = null!;
        public string Operation { get; set; } = null!;
        public decimal RebatedAmount { get; set; }
        public decimal CalculatedAmount { get; set; }
        public int ChangeRequestId { get; set; }
    }
}
