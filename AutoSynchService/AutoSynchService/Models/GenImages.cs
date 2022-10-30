using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class GenImages
    {
        public int Id { get; set; }
        public int SourceId { get; set; }
        public string? ImageName { get; set; }
        public string? ImagePath { get; set; }
        public string? Source { get; set; }
        public bool? IsActive { get; set; }
        public int UploadedBy { get; set; }
        public DateTime? UploadedDate { get; set; }
        public int DisplayOrder { get; set; }
        public int BranchId { get; set; }
    }
}
