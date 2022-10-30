using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class InvSchemeDetail
    {
        public int Id { get; set; }
        public int SchemeIdMaster { get; set; }
        public int ProductId { get; set; }
    }
}
