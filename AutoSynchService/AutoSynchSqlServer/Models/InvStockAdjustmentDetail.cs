using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InvStockAdjustmentDetail
    {
        public int Id { get; set; }
        public int MasterId { get; set; }
        public int ProductId { get; set; }
        public int? ReasonId { get; set; }
        public decimal Cost { get; set; }
        public decimal SystemQty { get; set; }
        public decimal PhysicalQty { get; set; }
        public decimal QtyDifference { get; set; }
        public int UnitId { get; set; }
    }
}
