using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class SysLayout
    {
        public int Id { get; set; }
        public string? LayoutName { get; set; }
        public string? LayoutPath { get; set; }
    }
}
