using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class InvSaleDetail
    {
        public int Id { get; set; }
        public int BillId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public decimal Qty { get; set; }
        public decimal Total { get; set; }
        public bool IsDeleted { get; set; }
        public decimal SaleValue { get; set; }
        public decimal TaxCharged { get; set; }
        public decimal TaxRate { get; set; }
        public string? Pctcode { get; set; }
        public decimal FurtherTax { get; set; }
        public decimal Discount { get; set; }
        public int InvoiceType { get; set; }
        public decimal PriceExclusiveTax { get; set; }
    }
}
