using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class SysWeekDay
    {
        public int Id { get; set; }
        public int? Code { get; set; }
        public string? Day { get; set; }
    }
}
