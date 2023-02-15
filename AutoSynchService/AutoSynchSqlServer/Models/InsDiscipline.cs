using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InsDiscipline
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public decimal AdmissionFee { get; set; }
        public int BranchId { get; set; }
        public bool IsDeleted { get; set; }
        public int MaxAgeLimit { get; set; }
        public string? IsGovtEmploymentOption { get; set; }
        public DateTime? AdmissionExpiryDate { get; set; }
        public string? IsAdmissionOpen { get; set; }
        public int MaxProgramsApply { get; set; }
        public string? IsTestRequired { get; set; }
        public decimal TestFee { get; set; }
        public int ApproximatelyAge { get; set; }
        public int MaxNoOfQualifications { get; set; }
        public string? IsMultipeCategory { get; set; }
        public string? QualificationsRequired { get; set; }
        public string? IsQualiPercentageOnTotal { get; set; }
        public decimal? TestMarksPercentage { get; set; }
        public string? ResultSystem { get; set; }
        public string? ResultSheetFt { get; set; }
        public decimal InternalPercentage { get; set; }
        public decimal FinalTermPercentage { get; set; }
        public decimal MidTermPercentage { get; set; }
    }
}
