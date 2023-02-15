using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsExamGradingB
    {
        public int Id { get; set; }
        public decimal MarksInPercentage { get; set; }
        public decimal GradePoint { get; set; }
        public string? Grade { get; set; }
    }
}
