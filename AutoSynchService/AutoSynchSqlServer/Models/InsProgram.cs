using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsProgram
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? FeePostingType { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int DisplayOrder { get; set; }
        public bool? IsActive { get; set; }
        public int BranchId { get; set; }
        public bool IsDeleted { get; set; }
        public int DisciplineId { get; set; }
        public int RegNoBodyLength { get; set; }
        public int RegStartNo { get; set; }
        public int RegCurrentNo { get; set; }
        public string? RegNoPrefix { get; set; }
        public string? RegNoSuffix { get; set; }
        public string? EnrNoPrefix { get; set; }
        public string? EnrNoSuffix { get; set; }
        public int EnrNoBodyLength { get; set; }
        public int EnrStartNo { get; set; }
        public int EnrCurrentNo { get; set; }
        public int DepartmentId { get; set; }
        public string? ShortName { get; set; }
    }
}
