using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsStudentTestScore
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string? RollNo { get; set; }
        public decimal ObtainedScore { get; set; }
        public decimal TotalScore { get; set; }
        public int? Year { get; set; }
    }
}
