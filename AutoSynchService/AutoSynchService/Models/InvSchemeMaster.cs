using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class InvSchemeMaster
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public string? Remarks { get; set; }
        public string? SchemeOn { get; set; }
        public string? DiscLevel { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public decimal Value { get; set; }
        public bool IsFlat { get; set; }
        public int? NearByExpiryDays { get; set; }
        public int? ProductsCount { get; set; }
        public int? FreeProductsCount { get; set; }
        public bool? Status { get; set; }
        public bool? IsDefault { get; set; }
    }
}
