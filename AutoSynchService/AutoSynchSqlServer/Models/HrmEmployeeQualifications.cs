using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class HrmEmployeeQualifications
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int QualificationId { get; set; }
        public string? BoardUniversity { get; set; }
        public int? PassingYear { get; set; }
        public string? RollNo { get; set; }
        public decimal? TotalMarks { get; set; }
        public decimal? ObtMarks { get; set; }
        public string? Division { get; set; }
        public decimal? PercentageMarks { get; set; }
    }
}
