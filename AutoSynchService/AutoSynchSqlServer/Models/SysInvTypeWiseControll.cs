using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class SysInvTypeWiseControll
    {
        public int Id { get; set; }
        public string? InventoryType { get; set; }
        public string? Controlles { get; set; }
    }
}
