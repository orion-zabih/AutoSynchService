﻿using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class SysForm
    {
        public int Id { get; set; }
        public string? FormName { get; set; }
        public int? DisplayOrder { get; set; }
        public string? FrmController { get; set; }
        public string? FrmAction { get; set; }
        public bool? IsActive { get; set; }
        public int FormGroupId { get; set; }
        public bool IsDeleted { get; set; }
        public string? ReportParms { get; set; }
        public bool IsTeachProtalFrm { get; set; }
        public bool IsEmpProtalFrm { get; set; }
        public bool IsStuProtalFrm { get; set; }
    }
}
