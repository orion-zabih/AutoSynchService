using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsExamGradingA
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? Grade { get; set; }
        public decimal GradePointFrom { get; set; }
        public decimal GradePointTo { get; set; }
        public decimal PercentageFrom { get; set; }
        public decimal PercentageTo { get; set; }
    }
}
