using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsClassToSubjects
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public decimal TotalTheoryMarks { get; set; }
        public decimal TotalPracticalMarks { get; set; }
        public decimal PassingTheoryMarks { get; set; }
        public decimal PassingPracticalMarks { get; set; }
        public string? Remarks { get; set; }
    }
}
