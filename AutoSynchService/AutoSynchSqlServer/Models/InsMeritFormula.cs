using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsMeritFormula
    {
        public int Id { get; set; }
        public int DisciplineId { get; set; }
        public int QualificationId { get; set; }
        public decimal Perentage { get; set; }
    }
}
