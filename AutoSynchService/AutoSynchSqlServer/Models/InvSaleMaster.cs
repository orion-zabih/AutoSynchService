using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InvSaleMaster
    {
        public int Id { get; set; }
        public int OrderNo { get; set; }
        public int? InvoiceNo { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? CustomerId { get; set; }
        public int? CustomerTypeId { get; set; }
        public int? PaymentTypeId { get; set; }
        public decimal? InvoiceTotal { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? TaxCalculated { get; set; }
        public decimal? ServiceChargesCalculated { get; set; }
        public decimal? GrandTotal { get; set; }
        public decimal? PaymentReceived { get; set; }
        public decimal Change { get; set; }
        public bool IsCanceled { get; set; }
        public string? CancelledReason { get; set; }
        public string OrderStatus { get; set; } = null!;
        public string? DiscountRemarks { get; set; }
        public int BranchId { get; set; }
        public int SessionId { get; set; }
        public bool IsDeleted { get; set; }
        public int? ThirdPartyId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerContact { get; set; }
        public int? EmployeeId { get; set; }
        public int WardId { get; set; }
        public int? BedId { get; set; }
        public string? EmpCreditType { get; set; }
        public bool IsApproved { get; set; }
        public int? ApprovedBy { get; set; }
        public bool IsCashierClosed { get; set; }
        public string? Remarks { get; set; }
        public bool IsStar { get; set; }
        public int PatientId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? CompletedBy { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int ShiftId { get; set; }
        public bool CreditLimitFlage { get; set; }
        public int StoreId { get; set; }
        public bool IsReturn { get; set; }
        public int FiscalYearId { get; set; }
        public int VehicleId { get; set; }
        public int CurrentReading { get; set; }
        public int NextServiceAfterKm { get; set; }
        public int ExpectReadingOnNext { get; set; }
        public int ReadingPerDay { get; set; }
        public DateTime? NextServiceDate { get; set; }
        public string? FbrInvoiceNumber { get; set; }
        public int FbrPosid { get; set; }
        public string? FbrUsin { get; set; }
        public string? BuyerNtn { get; set; }
        public string? BuyerCnic { get; set; }
        public string? BuyerPhoneNumber { get; set; }
        public int FbrPaymentModeCode { get; set; }
        public decimal? DiscountCalculated { get; set; }
        public decimal? TotalQuantity { get; set; }
        public decimal? FurtherTax { get; set; }
        public int FbrInvoiceTypeCode { get; set; }
        public int IsSentToFbr { get; set; }
        public string? FbrResponseCode { get; set; }
        public string? FbrResponse { get; set; }
        public string? PaymentType { get; set; }
        public string? VehicleNo { get; set; }
        public string? ScaleNumber { get; set; }
        public int QuatationId { get; set; }
    }
}
