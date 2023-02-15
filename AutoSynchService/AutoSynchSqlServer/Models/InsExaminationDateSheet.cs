using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsExaminationDateSheet
    {
        public int Id { get; set; }
        public int ExaminationId { get; set; }
        public int SubjectId { get; set; }
        public int ClassId { get; set; }
        public DateTime? Date { get; set; }
        public int TimeSlotId { get; set; }
        public int CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
