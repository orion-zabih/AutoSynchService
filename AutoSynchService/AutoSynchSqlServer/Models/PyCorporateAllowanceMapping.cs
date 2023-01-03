using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class PyCorporateAllowanceMapping
    {
        public int Id { get; set; }
        public int AllowanceId { get; set; }
        public int CorporateId { get; set; }
    }
}
