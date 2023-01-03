using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsStudentToSubjects
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public int ClassId { get; set; }
        public int SessionId { get; set; }
    }
}
