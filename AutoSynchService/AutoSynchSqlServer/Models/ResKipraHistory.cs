using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class ResKipraHistory
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public string InvoiceNo { get; set; } = null!;
        public int OrderId { get; set; }
        public DateTime InvoiceDateTime { get; set; }
        public double InvoiceTotal { get; set; }
        public double TaxCalculated { get; set; }
        public string Ntn { get; set; } = null!;
        public string KipraKey { get; set; } = null!;
        public string Status { get; set; } = null!;
        public int FiscalYearId { get; set; }
    }
}
