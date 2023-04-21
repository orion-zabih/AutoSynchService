using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InvInspectionDetail
    {
        public int Id { get; set; }
        public int MasterId { get; set; }
        public int ProductId { get; set; }
        public decimal Qty { get; set; }
        public int UnitId { get; set; }
        public decimal Cost { get; set; }
        public string? Remarks { get; set; }
    }
}
