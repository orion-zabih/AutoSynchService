using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class SysExecptionLogging
    {
        public int Id { get; set; }
        public string? Controller { get; set; }
        public string? Action { get; set; }
        public string? Exception { get; set; }
        public DateTime? Date { get; set; }
        public int UserId { get; set; }
    }
}
