using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsSection
    {
        public int Id { get; set; }
        public string? Section { get; set; }
        public int ClassId { get; set; }
        public int? DisplayOrder { get; set; }
        public bool? IsActive { get; set; }
        public int? StrengthCapicity { get; set; }
        public bool IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
