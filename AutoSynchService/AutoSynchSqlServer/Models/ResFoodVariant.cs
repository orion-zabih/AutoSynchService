using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class ResFoodVariant
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public string Variant { get; set; } = null!;
        public decimal? Price { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string? Code { get; set; }
        public int SectionId { get; set; }
        public bool? IsSalePriceOpen { get; set; }
    }
}
