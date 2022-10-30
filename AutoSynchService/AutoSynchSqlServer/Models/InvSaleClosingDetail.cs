using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class InvSaleClosingDetail
    {
        public int Id { get; set; }
        public int MasterId { get; set; }
        public int PaymentTypeId { get; set; }
        public decimal Amount { get; set; }
    }
}
