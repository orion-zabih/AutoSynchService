using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsStudentMeritListing
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int DisciplineId { get; set; }
        public int ProgramId { get; set; }
        public bool IsCalledForInterview { get; set; }
        public bool IsAppearedForInterview { get; set; }
        public bool IsEligible { get; set; }
        public string? InterviewRemarks { get; set; }
        public string? Deficiencies { get; set; }
        public int MeritListId { get; set; }
        public int CategoryId { get; set; }
        public decimal MarksCaculated { get; set; }
        public string? MeritListStatus { get; set; }
        public DateTime? PaymentDueDate { get; set; }
        public string? InterviewStatus { get; set; }
        public int NoInMeritList { get; set; }
        public int SessionId { get; set; }
    }
}
