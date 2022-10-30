using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class ResOrderType
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public int BranchId { get; set; }
        public string? OrderTypeGroup { get; set; }
        public bool IsHideInSaleReports { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsDietation { get; set; }
        public string? ReceiptFormate { get; set; }
        public bool IsPrintReceipt { get; set; }
        public string? DefaultPaymentType { get; set; }
        public int? DefaultPaymentTypeId { get; set; }
        public string? CreditType { get; set; }
    }
}
