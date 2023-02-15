using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsExamGradingC
    {
        public int Id { get; set; }
        public string? Grade { get; set; }
        public decimal Percentage { get; set; }
        public decimal Gpa { get; set; }
    }
}
