using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class ResSaleClosingDetail
    {
        public int Id { get; set; }
        public int MasterId { get; set; }
        public int PaymentTypeId { get; set; }
        public decimal Amount { get; set; }
        public string? PaymentType { get; set; }
    }
}
