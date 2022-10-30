using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class InsClassToSubjects
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
    }
}
