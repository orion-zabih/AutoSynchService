using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InvProductBatch
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? BatchNo { get; set; }
        public string? BatchBarcode { get; set; }
        public int? ReferenceId { get; set; }
        public string? Source { get; set; }
    }
}
