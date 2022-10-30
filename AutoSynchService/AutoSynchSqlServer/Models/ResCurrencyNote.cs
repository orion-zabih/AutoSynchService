using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class ResCurrencyNote
    {
        public int Id { get; set; }
        public int SaleClosingId { get; set; }
        public int CurrencyNote { get; set; }
        public int Qty { get; set; }
    }
}
