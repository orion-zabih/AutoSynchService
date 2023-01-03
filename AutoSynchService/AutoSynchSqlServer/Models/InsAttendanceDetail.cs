using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsAttendanceDetail
    {
        public int Id { get; set; }
        public int AttendanceMasterId { get; set; }
        public int StudentId { get; set; }
        public string? MarkStatus { get; set; }
        public string? CheckinTime { get; set; }
        public string? CheckoutTime { get; set; }
    }
}
