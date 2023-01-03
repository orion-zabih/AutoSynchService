using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InvPurchaseMaster
    {
        public int Id { get; set; }
        public int InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public int? VendorId { get; set; }
        public decimal? InvoiceDisc { get; set; }
        public decimal? Frieght { get; set; }
        public bool IsReturn { get; set; }
        public int PaymentTypeId { get; set; }
        public decimal InvoiceTotal { get; set; }
        public decimal GrandTotal { get; set; }
        public int? ReferenceId { get; set; }
        public string? Source { get; set; }
        public int WarehouseId { get; set; }
        public int BranchId { get; set; }
        public bool IsCancel { get; set; }
        public string? Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int CurrencyId { get; set; }
        public decimal CurrencyRate { get; set; }
        public int? GatePassNo { get; set; }
        public string? BiltyNo { get; set; }
        public DateTime? BiltyDate { get; set; }
        public string? VehicleNo { get; set; }
        public string? DriverName { get; set; }
        public string? DriverContactNo { get; set; }
        public decimal Commission { get; set; }
        public decimal Tax { get; set; }
        public string? Remarks { get; set; }
        public int FiscalYearId { get; set; }
        public decimal LoadingCharges { get; set; }
        public decimal OtherCharges { get; set; }
        public int GatePassId { get; set; }
        public decimal GrandTotalBeforeWhTax { get; set; }
        public decimal WithholdingTaxInAmount { get; set; }
        public decimal WithholdingTaxInPer { get; set; }
        public string? PaymentType { get; set; }
        public decimal AdvanceTaxAmount { get; set; }
        public string? CancelRemarks { get; set; }
    }
}
