using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
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
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? BranchId { get; set; }
        public int? LedgerId { get; set; }
        public string? PayrollType { get; set; }
        public int? BasicPayScale { get; set; }
        public string? Nic { get; set; }
        public int? CurrentStage { get; set; }
        public string? LinkCode { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAvailForCreditLimit { get; set; }
        public decimal FnsdLimitBalance { get; set; }
        public string? EmployeeGroup { get; set; }
        public int NoOfDependents { get; set; }
        public DateTime? DateOfMarriage { get; set; }
        public string? Nationality { get; set; }
        public int CityId { get; set; }
        public int DomicileId { get; set; }
        public string? ReasonForAction { get; set; }
        public string? EmployeeGrade { get; set; }
        public string? CurrentGovt { get; set; }
        public DateTime? EntryDate { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? EmployeeType { get; set; }
        public string? PhotoName { get; set; }
        public string? PermanentAddress { get; set; }
        public string? PostalAddress { get; set; }
        public int GroupId { get; set; }
        public int BankBranchId { get; set; }
        public string? SalaryTransferType { get; set; }
        public string? OrgLastEmployment { get; set; }
        public int ProjectId { get; set; }
        public string? IsGovtEmployee { get; set; }
        public int PreviousStage { get; set; }
        public DateTime? LastUpgradationDate { get; set; }
        public DateTime? LastPromotionDate { get; set; }
        public bool IsCreatedLogin { get; set; }
        public string? Ntn { get; set; }
        public string? TaxType { get; set; }
        public string? DisabilityDesc { get; set; }
        public string? IsDisabled { get; set; }
        public int InstituteId { get; set; }
        public string? FundType { get; set; }
        public decimal TaxRebateInPercent { get; set; }
    }
}
