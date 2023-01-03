using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsAttendanceMaster
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public int GroupId { get; set; }
        public int TimeSlotId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public int AttendanceTakenById { get; set; }
        public int AttendanceMarkedById { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public int LastUpdatedBy { get; set; }
        public int BranchId { get; set; }
        public bool IsDeleted { get; set; }
        public int SheetNo { get; set; }
        public int TeacherId { get; set; }
        public string? SheetStatus { get; set; }
        public string? Remarks { get; set; }
        public string? ClassActivity { get; set; }
    }
}
