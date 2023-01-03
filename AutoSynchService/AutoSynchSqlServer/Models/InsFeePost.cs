using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsFeePost
    {
        public int Id { get; set; }
        public int PostMasterId { get; set; }
        public int AdmissionId { get; set; }
        public int StudentId { get; set; }
        public int FeeItemId { get; set; }
        public decimal Value { get; set; }
        public decimal TermValue { get; set; }
        public decimal AdditionalValue { get; set; }
        public decimal NetValue { get; set; }
        public int NewItemInsertedBy { get; set; }
        public bool IsNewInsertedItem { get; set; }
        public string? StatusChangeRemarks { get; set; }
        public int StatusChangedBy { get; set; }
        public string ApprovalStatus { get; set; } = null!;
    }
}
