using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class HrmEmployee
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? EmployeeName { get; set; }
        public int? CategoryId { get; set; }
        public int? DepartmentId { get; set; }
        public int? DesignationId { get; set; }
        public string? Gender { get; set; }
        public string? MaritalStatus { get; set; }
        public int? ReligionId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? FatherName { get; set; }
        public string? Mobile { get; set; }
        public DateTime? DateOfJoin { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int? Weekholiday { get; set; }
        public int? Status { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int? QualificationId { get; set; }
        public string? Remarks { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? BranchId { get; set; }
        public int? LedgerId { get; set; }
        public string? PayrollType { get; set; }
        public int? BasicPayScale { get; set; }
        public string? Nic { get; set; }
        public int? CurrentStage { get; set; }
        public string? LinkCode { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsAvailForCreditLimit { get; set; }
        public decimal? FnsdLimitBalance { get; set; }
        public string? EmployeeGroup { get; set; }
        public string? Domicile { get; set; }
        public DateTime? RetirementDate { get; set; }
        public DateTime? PDate { get; set; }
        public DateTime? UDate { get; set; }
        public string? Qualification { get; set; }
        public string? StateS { get; set; }
        public DateTime? Ldate { get; set; }
        public string? BankAccountNo { get; set; }
        public string? BankName { get; set; }
        public string? Gpid { get; set; }
        public DateTime? DateOfMarriage { get; set; }
        public int NoOfDependents { get; set; }
        public int CityId { get; set; }
        public string? Nationality { get; set; }
        public string? ReasonForAction { get; set; }
        public int DomicileId { get; set; }
        public string? EmployeeGrade { get; set; }
        public string? CurrentGovt { get; set; }
        public DateTime? EntryDate { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? Pdate1 { get; set; }
        public string? Udate1 { get; set; }
        public string? Rdate { get; set; }
        public string? LDate1 { get; set; }
    }
}
