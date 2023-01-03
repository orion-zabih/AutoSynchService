using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class PyDeductionMaster
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Type { get; set; }
        public decimal? Amount { get; set; }
        public bool? Rebatable { get; set; }
        public bool? IsAnnual { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int FiscalYearId { get; set; }
        public bool IsFlat { get; set; }
        public int BranchId { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int DeductionPostType { get; set; }
        public int FormulaId { get; set; }
        public int DisplayOrder { get; set; }
    }
}
