using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class ResSaleDetailTemp
    {
        public int Id { get; set; }
        public int BillId { get; set; }
        public int ItemId { get; set; }
        public decimal Qty { get; set; }
        public string Flage { get; set; } = null!;
    }
}
