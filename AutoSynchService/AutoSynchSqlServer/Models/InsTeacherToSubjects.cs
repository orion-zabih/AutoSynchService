using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsTeacherToSubjects
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
    }
}
