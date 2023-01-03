using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class UsrUserFormsMapping
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FormId { get; set; }
        public bool? CanView { get; set; }
        public bool? CanCreate { get; set; }
        public bool? CanUpdate { get; set; }
        public bool? CanDelete { get; set; }
        public bool? CanApprove { get; set; }
    }
}
