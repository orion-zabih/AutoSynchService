using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class SysControllesGroup
    {
        public int Id { get; set; }
        public string? ControllGroupName { get; set; }
        public string? Controlls { get; set; }
        public string? ControllerLink { get; set; }
        public string? ActionLink { get; set; }
    }
}
