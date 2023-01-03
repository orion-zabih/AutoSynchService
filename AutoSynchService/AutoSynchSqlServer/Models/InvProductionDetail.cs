using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InvProductionDetail
    {
        public int Id { get; set; }
        public int MasterId { get; set; }
        public int ProductId { get; set; }
        public decimal ProductionQty { get; set; }
        public decimal CunsumptionQty { get; set; }
        public decimal NetQty { get; set; }
        public decimal Size { get; set; }
        public decimal AverageCost { get; set; }
    }
}
