using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InvPurchaseDetail
    {
        public int Id { get; set; }
        public int MasterId { get; set; }
        public int ProductId { get; set; }
        public decimal Qty { get; set; }
        public int UnitId { get; set; }
        public decimal CostPrice { get; set; }
        public decimal RetailPrice { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public decimal Disc { get; set; }
        public decimal TaxAmount { get; set; }
        public bool IsBatchChange { get; set; }
        public decimal Scheme { get; set; }
        public decimal AverageCost { get; set; }
        public decimal CutQty { get; set; }
        public decimal AdditionalTaxAmount { get; set; }
        public decimal SaleTaxInPercent { get; set; }
    }
}
