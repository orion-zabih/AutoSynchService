using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsMarksEvaluation
    {
        public int Id { get; set; }
        public int ClassToSubjectId { get; set; }
        public decimal MarksInPercent { get; set; }
        public int AdditionalCreteriaId { get; set; }
    }
}
