using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsClassFeeGroupsMapping
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int FeeGroupId { get; set; }
    }
}
