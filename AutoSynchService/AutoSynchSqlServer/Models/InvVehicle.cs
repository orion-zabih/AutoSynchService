using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InvVehicle
    {
        public int Id { get; set; }
        public string? VehicleNo { get; set; }
        public string? Model { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int BranchId { get; set; }
        public int CustomerId { get; set; }
        public string? VehicleBrand { get; set; }
        public int ReadingPerDayInKm { get; set; }
        public int ServiceAfterReadingInKm { get; set; }
        public string? VehicleName { get; set; }
        public DateTime? RcvDate { get; set; }
        public string? VehicleRegNo { get; set; }
        public string? JobCard { get; set; }
        public string? FrameNo { get; set; }
    }
}
