using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsQuotaSettings
    {
        public int Id { get; set; }
        public int DisciplineId { get; set; }
        public int CategoryId { get; set; }
        public int NoOfSeats { get; set; }
    }
}
