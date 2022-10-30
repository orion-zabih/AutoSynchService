using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class ResFood
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int FoodCategoryId { get; set; }
        public int? KitchenId { get; set; }
        public bool IsOffer { get; set; }
        public decimal OfferRate { get; set; }
        public DateTime? OfferStartDate { get; set; }
        public DateTime? OfferEndDate { get; set; }
        public string? ImageName { get; set; }
        public bool? IsSpecial { get; set; }
        public string? Notes { get; set; }
        public string? Components { get; set; }
        public string? Description { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? DisplayOrder { get; set; }
        public bool? IsActive { get; set; }
        public int? BranchId { get; set; }
        public bool IsDeleted { get; set; }
        public decimal Vat { get; set; }
        public string? CookingTimeHours { get; set; }
        public string? CookingTimeMin { get; set; }
        public string? Type { get; set; }
    }
}
