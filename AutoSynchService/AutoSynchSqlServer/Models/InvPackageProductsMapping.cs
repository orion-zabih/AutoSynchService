using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InvPackageProductsMapping
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        public int ProductId { get; set; }
        public decimal Qty { get; set; }
        public int UnitId { get; set; }
    }
}
