using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class OrgFeaturesMapping
    {
        public int Id { get; set; }
        public int FeatureId { get; set; }
        public int OrgId { get; set; }
    }
}
