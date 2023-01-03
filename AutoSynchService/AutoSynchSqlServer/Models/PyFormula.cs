using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class PyFormula
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string FormulaCode { get; set; } = null!;
        public bool IsActive { get; set; }
        public string FormulaSection { get; set; } = null!;
    }
}
