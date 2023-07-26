using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsStudent
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int ClassId { get; set; }
        public string? RegistrationNo { get; set; }
        public int? BatchId { get; set; }
        public string StudentName { get; set; } = null!;
        public string? FatherName { get; set; }
        public string? PermanentAddress { get; set; }
        public DateTime? Dob { get; set; }
        public string? Nationality { get; set; }
        public int ReligionId { get; set; }
        public int QualificationId { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public int InstitutionId { get; set; }
        public string? Gender { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? PhotoName { get; set; }
        public string? StudentContact { get; set; }
        public string? FatherContact { get; set; }
        public string? Cnic { get; set; }
        public string? EmailAddress { get; set; }
        public string? PostalAddress { get; set; }
        public string? BloodGroup { get; set; }
        public string? FatherProfession { get; set; }
        public string? GuardianContact { get; set; }
        public int AdmissionId { get; set; }
        public int BranchId { get; set; }
        public string Status { get; set; } = null!;
        public string? PlcNo { get; set; }
        public string? Domicile { get; set; }
        public int JoiningSessionId { get; set; }
        public string? GuardianName { get; set; }
        public int? FeeGroupId { get; set; }
        public int? FeeTermId { get; set; }
        public int? CategoryId { get; set; }
        public int? HostelId { get; set; }
        public int? ShiftId { get; set; }
        public int? TransportId { get; set; }
        public int SectionId { get; set; }
        public int? FamilyId { get; set; }
        public bool IsDeleted { get; set; }
        public int CityId { get; set; }
        public int OnlineFormStepStatus { get; set; }
        public string? OrgLastEmployment { get; set; }
        public int LoginId { get; set; }
        public string? IsGovtEmployee { get; set; }
        public string? IsAppearForTest { get; set; }
        public string? InstitutionLastAttendant { get; set; }
        public bool IsOnlineApplied { get; set; }
        public string? CategoryIds { get; set; }
        public string? ProgramIds { get; set; }
        public int DisciplineId { get; set; }
        public int ProgramId { get; set; }
        public string? LastEnrolmentNo { get; set; }
        public int? FormNo { get; set; }
        public string? PreviouslyEnrolledInOrg { get; set; }
        public string? CommunicableDisease { get; set; }
        public int GroupId { get; set; }
        public string? OutSource { get; set; }
        public string? InSource { get; set; }
        public DateTime? AdmittedDate { get; set; }
        public string? EnrollmentNo { get; set; }
        public bool IsCreatedLogin { get; set; }
        public string? FatherCnic { get; set; }
    }
}
