using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class ResFnsdCreditResetingHostory
    {
        public int Id { get; set; }
        public DateTime ResetedDateTime { get; set; }
        public int ResetedBy { get; set; }
        public DateTime ResetingUpToDate { get; set; }
        public string CreditType { get; set; } = null!;
        public string ResetedOrderIds { get; set; } = null!;
    }
}
