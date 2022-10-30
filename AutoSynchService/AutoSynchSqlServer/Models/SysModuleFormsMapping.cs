using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class SysModuleFormsMapping
    {
        public int Id { get; set; }
        public int? ModuleId { get; set; }
        public int? FormId { get; set; }
        public bool IsCheckedForUserGroup { get; set; }
        public int? FrmLayoutId { get; set; }
    }
}
