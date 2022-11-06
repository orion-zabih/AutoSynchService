using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchSqlServer.CustomModels
{
    public class Sequence
    {
        public decimal nextval { get; set; }
    }

    public class Timestamp
    {
        public DateTime current_timestamp { get; set; }
    }
}
