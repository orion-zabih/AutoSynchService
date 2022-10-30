using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class InsFeeItem
    {
        public int Id { get; set; }
        public string FeeName { get; set; } = null!;
        public string FeeType { get; set; } = null!;
        public int DisplayOrder { get; set; }
        public bool EducationTax { get; set; }
        public bool IsRefundable { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int BranchId { get; set; }
        public decimal DefaultValue { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
