using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class UsrUserParmsMapping
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ParmId { get; set; }
        public string ParmType { get; set; } = null!;
        public int BranchId { get; set; }
    }
}
