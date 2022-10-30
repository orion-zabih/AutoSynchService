using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class PyStaffAttendance
    {
        public int Id { get; set; }
        public int StaffId { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; } = null!;
        public int AcademicYearId { get; set; }
    }
}
