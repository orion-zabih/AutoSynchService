using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class InvPurchaseOrderDetail
    {
        public int Id { get; set; }
        public int MasterId { get; set; }
        public int ProductId { get; set; }
        public int LineNum { get; set; }
        public decimal? QtyOrdered { get; set; }
        public decimal UnitCost { get; set; }
        public decimal? QtyReceived { get; set; }
        public decimal? QtyDamaged { get; set; }
        public decimal? SalePrice { get; set; }
        public int UnitId { get; set; }
    }
}
