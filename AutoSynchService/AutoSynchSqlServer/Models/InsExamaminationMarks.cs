using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsExamaminationMarks
    {
        public int Id { get; set; }
        public int ExaminationId { get; set; }
        public int ExaminationChildId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public decimal TotalTheoryMarks { get; set; }
        public decimal TotalPracticalMarks { get; set; }
        public decimal PassingTheoryMarks { get; set; }
        public decimal PassingPracticalMarks { get; set; }
        public decimal ObtainedTheoryMarks { get; set; }
        public decimal ObtainedPracticalMarks { get; set; }
        public decimal SubjectTotalMarks { get; set; }
        public decimal SubtectTotalObtainedMarks { get; set; }
        public decimal Percetage { get; set; }
        public bool? IsSubjectTheoretical { get; set; }
        public bool IsSubjectPractical { get; set; }
        public bool IsPresentInTheory { get; set; }
        public bool IsPresentInPractical { get; set; }
    }
}
