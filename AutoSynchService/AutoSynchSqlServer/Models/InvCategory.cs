using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InvCategory
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int DisplayOrder { get; set; }
        public bool? IsActive { get; set; }
        public int BranchId { get; set; }
        public bool IsDeleted { get; set; }
        public int ParentCategoryId { get; set; }
        public bool IsOffer { get; set; }
        public DateTime? OfferStartDate { get; set; }
        public DateTime? OfferEndDate { get; set; }
        public string? ImageName { get; set; }
        public string CategoryGroup { get; set; } = null!;
    }
}
