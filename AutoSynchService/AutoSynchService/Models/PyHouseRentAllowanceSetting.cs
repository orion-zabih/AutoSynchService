using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class PyHouseRentAllowanceSetting
    {
        public int Id { get; set; }
        public int Bps { get; set; }
        public decimal Min { get; set; }
    }
}
