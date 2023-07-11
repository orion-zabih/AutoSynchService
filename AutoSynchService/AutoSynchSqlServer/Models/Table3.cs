using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class Table3
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Stock { get; set; }
        public decimal? AverageCost { get; set; }
        public decimal? Total { get; set; }
        public int? BranchId { get; set; }
        public int? WarehouseId { get; set; }
        public int? FiscalYearId { get; set; }
    }
}
