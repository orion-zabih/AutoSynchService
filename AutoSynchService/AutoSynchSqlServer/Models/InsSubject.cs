using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class InsSubject
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? ShortName { get; set; }
        public int PaperType { get; set; }
        public int SubjectType { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int DisplayOrder { get; set; }
        public bool? IsActive { get; set; }
        public int BranchId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
