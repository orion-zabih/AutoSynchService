using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InvJcMonthSetting
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Quarter { get; set; }
        public int MonthJcNo { get; set; }
        public string? WeekNosInJc { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int OrgId { get; set; }
    }
}
