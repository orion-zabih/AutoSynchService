using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsDivision
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public decimal? PercentageFrom { get; set; }
        public decimal? PercentageTo { get; set; }
        public int? DisplayOrder { get; set; }
        public int BranchId { get; set; }
        public int ProgramId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
