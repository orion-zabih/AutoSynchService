using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsStudentEmployments
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string? Department { get; set; }
        public string? PostBps { get; set; }
        public DateTime? PeriodFrom { get; set; }
        public DateTime? PeriodTo { get; set; }
    }
}
