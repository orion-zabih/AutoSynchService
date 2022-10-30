using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class UsrUserBranchesMapping
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BranchId { get; set; }
        public bool IsDefault { get; set; }
    }
}
