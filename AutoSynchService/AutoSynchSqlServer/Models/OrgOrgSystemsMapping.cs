using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class OrgOrgSystemsMapping
    {
        public int Id { get; set; }
        public int OrgId { get; set; }
        public int SystemId { get; set; }
        public string? SystemLabelDesc { get; set; }
        public int DisplayOrder { get; set; }
    }
}
