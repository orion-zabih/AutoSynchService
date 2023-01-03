using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsStudentAdmission
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int ClassId { get; set; }
        public int StudentId { get; set; }
        public string Status { get; set; } = null!;
        public int? RollNo { get; set; }
        public int? InstituteId { get; set; }
        public int? ExaminationCenterId { get; set; }
        public int HostelId { get; set; }
        public int TransportId { get; set; }
        public int ShiftId { get; set; }
        public int CategoryId { get; set; }
        public int SectionId { get; set; }
        public string? Remarks { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public int BranchId { get; set; }
        public int? FeeGroupId { get; set; }
        public int? FeeTermId { get; set; }
    }
}
