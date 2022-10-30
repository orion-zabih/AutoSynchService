using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class InvStockTransferDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal Qty { get; set; }
        public int UnitId { get; set; }
        public int TransferId { get; set; }
        public decimal Cost { get; set; }
        public decimal DemandedQty { get; set; }
        public decimal StockOnHand { get; set; }
    }
}
