using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class GenCountry
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int UpdatedById { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int BranchId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
