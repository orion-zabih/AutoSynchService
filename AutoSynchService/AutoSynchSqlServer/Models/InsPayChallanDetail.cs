using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsPayChallanDetail
    {
        public int Id { get; set; }
        public int ChallanMasterId { get; set; }
        public int FeePostedId { get; set; }
        public decimal NetAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public bool IsDeleted { get; set; }
        public string? PaidStatus { get; set; }
        public int FeeItemPostedId { get; set; }
        public string? ItemSource { get; set; }
        public string? ItemFeeDescription { get; set; }
    }
}
