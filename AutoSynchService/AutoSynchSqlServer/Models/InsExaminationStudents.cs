using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsExaminationStudents
    {
        public int Id { get; set; }
        public int ExaminationId { get; set; }
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public int ProgramId { get; set; }
        public decimal TotalMarks { get; set; }
        public decimal ObtainedMarks { get; set; }
        public string? Result { get; set; }
        public int DivisionId { get; set; }
        public string? Division { get; set; }
        public decimal Percentage { get; set; }
        public int Rank { get; set; }
        public int ExamRollNo { get; set; }
        public int ExamCenterId { get; set; }
        public int HallId { get; set; }
        public int HallRowNo { get; set; }
        public bool? IsAppear { get; set; }
        public string? Remarks { get; set; }
    }
}
