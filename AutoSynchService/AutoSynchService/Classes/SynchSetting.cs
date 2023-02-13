using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchService.Classes
{
    public partial class SynchSetting
    {
        
        public int setting_id { get; set; }
        public string? synch_method { get; set; }
        public string? synch_type { get; set; }
        public string? table_names { get; set; }
        public string? status { get; set; }
        public DateTime? sync_timestamp { get; set; }
        public DateTime? update_timestamp { get; set; }
        public DateTime? insertion_timestamp{ get; set; }

    }
}
