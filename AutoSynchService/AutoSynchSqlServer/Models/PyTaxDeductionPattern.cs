using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class PyTaxDeductionPattern
    {
        public int Id { get; set; }
        public decimal LowerLimit { get; set; }
        public decimal UpperLimit { get; set; }
        public decimal IncomeTaxRatePer { get; set; }
        public decimal FixedTaxAmount { get; set; }
        public int FiscalYearId { get; set; }
        public int BranchId { get; set; }
        public int DisplayOrder { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
