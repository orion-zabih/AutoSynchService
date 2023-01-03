using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class PyCpFundCalculation
    {
        public int Id { get; set; }
        public int Bps { get; set; }
        public decimal MinimumAmount { get; set; }
        public decimal MaximumAmount { get; set; }
        public decimal PercentRateOnMean { get; set; }
        public decimal CalculatedRate { get; set; }
        public int BranchId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
