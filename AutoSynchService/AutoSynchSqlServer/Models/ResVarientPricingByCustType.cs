using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class ResVarientPricingByCustType
    {
        public int Id { get; set; }
        public int CustomerTypeId { get; set; }
        public int VarientId { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
    }
}
