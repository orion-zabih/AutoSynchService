using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class InvQuatationDetail
    {
        public int Id { get; set; }
        public int MasterId { get; set; }
        public int ProductId { get; set; }
        public decimal Qty { get; set; }
        public decimal Price { get; set; }
        public int UnitId { get; set; }
    }
}
