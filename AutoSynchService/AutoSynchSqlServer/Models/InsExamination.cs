using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsExamination
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public int BranchId { get; set; }
        public bool IsDeleted { get; set; }
        public int SessionId { get; set; }
        public DateTime? ExamStartDate { get; set; }
        public string? Remarks { get; set; }
        public decimal OverAllResultInPercent { get; set; }
        public bool IsFinalized { get; set; }
        public decimal HighestMarks { get; set; }
        public DateTime? ResultDeclarationDate { get; set; }
        public int AdditionalCriteriaId { get; set; }
        public string? DistributionType { get; set; }
    }
}
