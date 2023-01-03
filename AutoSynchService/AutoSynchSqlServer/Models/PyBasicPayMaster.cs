using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class PyBasicPayMaster
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
