using AutoSynchSqlServer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchSqlServer.CustomModels
{

    public class InvPurchaseMasterUnmapped
    {
        [NotMapped]
        public string Flage { get; set; }
        [NotMapped]
        public string VendorName { get; set; }
        [NotMapped]
        public List<InvPurchaseDetail> PurchaseDetails = new List<InvPurchaseDetail>();
        [NotMapped]
        public int ProductId { get; set; }
        [NotMapped]
        public decimal Qty { get; set; }
        [NotMapped]
        public decimal CostPrice { get; set; }
        [NotMapped]
        public decimal RetailPrice { get; set; }
        [NotMapped]
        public decimal ProductTotal { get; set; }
        [NotMapped]
        public int UnitId { get; set; }
        [NotMapped]
        public string WarehouseName { get; set; }
        [NotMapped]
        public decimal CutQty { get; set; }
        [NotMapped]
        public int AccountId { get; set; }
        [NotMapped]
        public decimal DebitAmount { get; set; }
        [NotMapped]
        public decimal CreditAmount { get; set; }
        [NotMapped]
        public string Description { get; set; }
        [NotMapped]
        public decimal GrossAmountTotal { get; set; }
        [NotMapped]
        public decimal ItemWiseDiscTotal { get; set; }
        [NotMapped]
        public decimal ItemWiseSaleTaxTotal { get; set; }
        [NotMapped]
        public decimal ItemWiseExtraTaxTotal { get; set; }
        [NotMapped]
        public decimal Disc { get; set; }
        [NotMapped]
        public decimal TaxAmount { get; set; }
        [NotMapped]
        public decimal AdditionalTaxAmount { get; set; }
        [NotMapped]
        public string ProductName { get; set; }
        [NotMapped]
        public decimal ItemCostValue { get; set; }
        [NotMapped]
        public decimal ItemCutValue { get; set; }
        [NotMapped]
        public decimal ItemNetAmount { get; set; }
        [NotMapped]
        public int JvFlage { get; set; }
        [NotMapped]
        public string VendorAddress { get; set; }
        [NotMapped]
        public decimal ItemDisc { get; set; }
        [NotMapped]
        public decimal CostIncTax { get; set; }
        [NotMapped]
        public string BranchName { get; set; }
        [NotMapped]
        public string CategoryGroup { get; set; }
        [NotMapped]
        public string VendorNTN { get; set; }
        [NotMapped]
        public string VendorSTRN { get; set; }
        [NotMapped]
        public int ItemId { get; set; }
        public InvVendor VendorInfo = new InvVendor();
        [NotMapped]
        public decimal SaleTaxInPercent { get; set; }
    }
    public class InvPurchaseMasterDup
    {
        [NotMapped]
        public int counts { get; set; }
        [NotMapped]
        public string? FbrInvoiceNumber { get; set; }
        
        [NotMapped]
        public int InvoiceNo { get; set; }

        [NotMapped]
        public int BranchId { get; set; }

        [NotMapped]
        public int FiscalYearId { get; set; }

    }
    public class InvPurchaseMasterMin
    {
        [NotMapped]
        public int OrderNo { get; set; }
        //[NotMapped]
        //public string? FbrInvoiceNumber { get; set; }

    }

}
