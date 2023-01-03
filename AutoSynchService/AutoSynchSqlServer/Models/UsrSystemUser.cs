using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class UsrSystemUser
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string LoginName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool? IsActive { get; set; }
        public int AuthorizationGroupId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int OrgId { get; set; }
        public string AuthorizationType { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public string? UserType { get; set; }
        public string? LastLoginIp { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int? DefSessionId { get; set; }
        public int? DefInstitutionId { get; set; }
        public int? DefClassId { get; set; }
        public int? DefQualificationId { get; set; }
        public int? DefJoiningSessionId { get; set; }
        public int? DefBatchId { get; set; }
        public int? DefReligionId { get; set; }
        public string? DefGender { get; set; }
        public DateTime? DefDob { get; set; }
        public DateTime? DefDateOfJoning { get; set; }
        public string? DefNationality { get; set; }
        public bool IsAllowFinancial { get; set; }
        public string? UserRole { get; set; }
        public bool? IsAllowForDiscount { get; set; }
        public bool? InvIsOpenTellLimitOpening { get; set; }
        public int UserDepartmentId { get; set; }
        public string? LoginType { get; set; }
    }
}
