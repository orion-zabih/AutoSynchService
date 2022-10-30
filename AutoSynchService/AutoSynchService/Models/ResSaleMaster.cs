using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class ResSaleMaster
    {
        public int Id { get; set; }
        public int OrderNo { get; set; }
        public int? InvoiceNo { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? CustomerId { get; set; }
        public int? CustomerTypeId { get; set; }
        public int? PaymentTypeId { get; set; }
        public int? TableId { get; set; }
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
        public int WaiterId { get; set; }
        public bool IsDeleted { get; set; }
        public string? CookingTimeHours { get; set; }
        public int? ThirdPartyId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerContact { get; set; }
        public int? EmployeeId { get; set; }
        public string? StudentNo { get; set; }
        public int WardId { get; set; }
        public int? BedId { get; set; }
        public string? EmpCreditType { get; set; }
        public bool IsApproved { get; set; }
        public int? ApprovedBy { get; set; }
        public bool IsCashierClosed { get; set; }
        public string? StatusFromKitchen { get; set; }
        public string? Remarks { get; set; }
        public bool IsStar { get; set; }
        public string? CookingTimeMin { get; set; }
        public int PatientId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? CompletedBy { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int ShiftId { get; set; }
        public int Cover { get; set; }
        public bool CreditLimitFlage { get; set; }
        public int FiscalYearId { get; set; }
        public decimal DiscountCalculated { get; set; }
        public bool IsRunning { get; set; }
        public string? PaymentType { get; set; }
    }
}
