using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class AccType
    {
        public int Id { get; set; }
        public string? TypeCode { get; set; }
        public string? TypeName { get; set; }
        public int DisplayOrder { get; set; }
        public bool? IsActive { get; set; }
    }
}
