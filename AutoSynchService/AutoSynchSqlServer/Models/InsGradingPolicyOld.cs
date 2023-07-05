using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsGradingPolicyOld
    {
        public int Id { get; set; }
        public decimal? MarksFrom { get; set; }
        public decimal? MarksTo { get; set; }
        public decimal? Gpa { get; set; }
        public string? Grade { get; set; }
    }
}
