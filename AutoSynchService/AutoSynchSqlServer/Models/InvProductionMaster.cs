﻿using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InvProductionMaster
    {
        public int Id { get; set; }
        public int HeatNo { get; set; }
        public int LineNumber { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public int? SupperWiser { get; set; }
        public int Shift { get; set; }
        public DateTime? ProductionDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsCancelled { get; set; }
        public int FiscalYearId { get; set; }
        public int BranchId { get; set; }
        public decimal TotalProduction { get; set; }
        public decimal Wastage { get; set; }
        public decimal ProductionPerUnitCost { get; set; }
        public string? TotalTime { get; set; }
        public decimal ElectricityUnitConsume { get; set; }
    }
}
