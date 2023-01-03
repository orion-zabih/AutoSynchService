using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsStudentQualifications
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int DegreeId { get; set; }
        public string? BoardUniversity { get; set; }
        public int? PassingYear { get; set; }
        public string? RollNo { get; set; }
        public decimal? TotalMarks { get; set; }
        public decimal? ObtMarks { get; set; }
        public string? Division { get; set; }
        public decimal? PercentageMarks { get; set; }
    }
}
