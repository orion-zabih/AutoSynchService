using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class ResCustomerTypeItemsMapping
    {
        public int Id { get; set; }
        public int CustomerTypeId { get; set; }
        public int VarientId { get; set; }
    }
}
