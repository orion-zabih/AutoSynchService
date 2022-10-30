using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class InsFeePostMaster
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public DateTime PostedDateTime { get; set; }
        public int PostedBy { get; set; }
        public int BranchId { get; set; }
        public int LastModifyBy { get; set; }
        public DateTime? LastModifyDate { get; set; }
        public string PostingMethod { get; set; } = null!;
        public string? PostingRemarks { get; set; }
        public int FiscalYearId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsRestrictDeletion { get; set; }
        public int ClassId { get; set; }
        public int ProgramId { get; set; }
    }
}
