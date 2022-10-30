using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class PyBasicPayDetail
    {
        public int Id { get; set; }
        public int MasterId { get; set; }
        public int Pbs { get; set; }
        public decimal Min { get; set; }
        public decimal Increment { get; set; }
        public decimal Max { get; set; }
    }
}
