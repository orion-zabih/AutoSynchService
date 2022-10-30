using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class PySalaryHead
    {
        public int Id { get; set; }
        public int Head { get; set; }
        public int DisplayOrder { get; set; }
        public int LedgerId { get; set; }
        public string? Operation { get; set; }
        public int BranchId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int CreatedById { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int UpdatedById { get; set; }
    }
}
