using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class GenTermsAndConditions
    {
        public int Id { get; set; }
        public int? OrgId { get; set; }
        public string? TextLine { get; set; }
        public int? DisplayOrder { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? Type { get; set; }
    }
}
