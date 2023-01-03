using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class ResCurrencyNote
    {
        public int Id { get; set; }
        public int SaleClosingId { get; set; }
        public int CurrencyNote { get; set; }
        public int Qty { get; set; }
    }
}
