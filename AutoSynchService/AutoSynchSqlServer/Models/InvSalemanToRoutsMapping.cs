using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class InvSalemanToRoutsMapping
    {
        public int Id { get; set; }
        public int SalemanId { get; set; }
        public int RouteId { get; set; }
    }
}
