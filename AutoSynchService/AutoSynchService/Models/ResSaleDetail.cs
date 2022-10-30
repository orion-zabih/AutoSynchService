using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class ResSaleDetail
    {
        public int Id { get; set; }
        public int BillId { get; set; }
        public int FoodVariantId { get; set; }
        public decimal Price { get; set; }
        public decimal Qty { get; set; }
        public decimal Total { get; set; }
        public bool IsDeleted { get; set; }
    }
}
