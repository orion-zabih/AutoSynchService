﻿using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InvUnit
    {
        public int Id { get; set; }
        public string? UnitName { get; set; }
        public decimal? UnitOfConversion { get; set; }
        public int ParentId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int DisplayOrder { get; set; }
        public bool? IsActive { get; set; }
        public int BranchId { get; set; }
        public bool IsDeleted { get; set; }
        public bool SetAsDefault { get; set; }
    }
}
