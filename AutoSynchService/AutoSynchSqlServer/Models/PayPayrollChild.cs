using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class PayPayrollChild
    {
        public int Id { get; set; }
        public int MasterId { get; set; }
        public int EmployeeId { get; set; }
        public int DesignationId { get; set; }
        public int DepartmentId { get; set; }
        public int BasicPayScale { get; set; }
        public int CategoryId { get; set; }
        public string? SalaryTransferType { get; set; }
        public int CurrentStage { get; set; }
        public string Status { get; set; } = null!;
        public int BranchId { get; set; }
    }
}
