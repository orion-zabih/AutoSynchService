using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsReligion
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public int BranchId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
