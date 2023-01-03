using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class SysMonthName
    {
        public int Id { get; set; }
        public int MonthNo { get; set; }
        public string MonthName { get; set; } = null!;
    }
}
