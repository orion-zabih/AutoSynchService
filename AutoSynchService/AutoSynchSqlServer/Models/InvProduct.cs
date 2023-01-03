using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InvProduct
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Barcode { get; set; }
        public int CategoryId { get; set; }
        public bool IsOffer { get; set; }
        public decimal OfferRate { get; set; }
        public DateTime? OfferStartDate { get; set; }
        public DateTime? OfferEndDate { get; set; }
        public string? ImageName { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public int BranchId { get; set; }
        public bool IsDeleted { get; set; }
        public decimal Tax { get; set; }
        public string Type { get; set; } = null!;
        public decimal Cost { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal? MinimumLevel { get; set; }
        public decimal? ReorderQty { get; set; }
        public int? DefaultVendorId { get; set; }
        public int UnitId { get; set; }
        public int? PurchaseUnitId { get; set; }
        public int? SaleUnitId { get; set; }
        public int? CompanyId { get; set; }
        public decimal? CompDiscPercent { get; set; }
        public decimal? CompExtraDiscPercent { get; set; }
        public bool IsExpiry { get; set; }
        public decimal? SaleDiscount { get; set; }
        public bool IsSaleDiscountFlate { get; set; }
        public decimal UnitWeight { get; set; }
        public decimal AverageCost { get; set; }
        public decimal TaxValue { get; set; }
        public bool IsRetailPriceInclusiveTax { get; set; }
        public string? BillType { get; set; }
        public bool IsSalePriceOpen { get; set; }
        public string? CategoryName { get; set; }
        public string? Gst { get; set; }
        public int MixtureId { get; set; }
        public decimal MaterialSize { get; set; }
        public bool IsProduction { get; set; }
        public string? ChargeMeterType { get; set; }
        public decimal Rate { get; set; }
        public string? SaleTaxCalMethodInPur { get; set; }
        public decimal CostIncTax { get; set; }
    }
}
