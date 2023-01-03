using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class PayStagesScale
    {
        public int Id { get; set; }
        public int? Year { get; set; }
        public int? Scale { get; set; }
        public decimal? Initial { get; set; }
        public decimal? Increment { get; set; }
        public decimal? Maximum { get; set; }
        public bool IsCurrent { get; set; }
    }
}
