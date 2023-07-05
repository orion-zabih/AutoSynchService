using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class OrgOrganization
    {
        public int Id { get; set; }
        public string? OrgName { get; set; }
        public string? ShortAddress { get; set; }
        public string? LongAddress { get; set; }
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string? OrgLogo { get; set; }
        public string? Website { get; set; }
        public string? ReportsTitle { get; set; }
        public bool? IsActive { get; set; }
        public string? Edate { get; set; }
        public string? Eflage { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool? UseInfoForAllBranches { get; set; }
        public string? KpraNo { get; set; }
        public string? KpraKey { get; set; }
        public string? PayProCode { get; set; }
        public string? OrgCode { get; set; }
        public bool? IsSkipFromGoverHouse { get; set; }
        public string? FbrApiSandBox { get; set; }
        public string? FbrApiProduction { get; set; }
        public string DmInvCustType { get; set; } = null!;
        public string DmInvPayType { get; set; } = null!;
        public string DmCustomer { get; set; } = null!;
        public string DmVendor { get; set; } = null!;
        public string DmEmployee { get; set; } = null!;
        public string DmPatients { get; set; } = null!;
        public string DmProductCategory { get; set; } = null!;
        public string DmFnsdCategory { get; set; } = null!;
        public string DmFnsdFood { get; set; } = null!;
        public string DmWard { get; set; } = null!;
        public string DmInvShift { get; set; } = null!;
        public string DmResShift { get; set; } = null!;
        public string DmProduct { get; set; } = null!;
        public string DmDinTable { get; set; } = null!;
        public string DmAccType { get; set; } = null!;
        public string DmAccHead { get; set; } = null!;
        public string DmAccGroup { get; set; } = null!;
        public string DmAccFyear { get; set; } = null!;
        public string DmAccounts { get; set; } = null!;
        public string DmHrmCategory { get; set; } = null!;
        public string DmHrmDepartment { get; set; } = null!;
        public string DmHrmDesignation { get; set; } = null!;
        public string DmBatch { get; set; } = null!;
        public string DmCenter { get; set; } = null!;
        public string DmDivision { get; set; } = null!;
        public string DmExamination { get; set; } = null!;
        public string DmFeeItem { get; set; } = null!;
        public string DmFeeTerm { get; set; } = null!;
        public string DmFeeGroup { get; set; } = null!;
        public string DmInstitute { get; set; } = null!;
        public string DmProgram { get; set; } = null!;
        public string DmQualification { get; set; } = null!;
        public string DmReligion { get; set; } = null!;
        public string DmSession { get; set; } = null!;
        public string DmStudent { get; set; } = null!;
        public string DmStudentCatgory { get; set; } = null!;
        public string DmSubject { get; set; } = null!;
        public string DmCompany { get; set; } = null!;
        public string DmLocation { get; set; } = null!;
        public string DmUnit { get; set; } = null!;
        public string DmVehicle { get; set; } = null!;
        public string DmWarehouse { get; set; } = null!;
        public string DmThirdPartyCompany { get; set; } = null!;
        public string DmAccMapping { get; set; } = null!;
        public string AccountIntegration { get; set; } = null!;
        public string DmGenDomicile { get; set; } = null!;
        public string DmGenCity { get; set; } = null!;
        public int PayProBankAccountId { get; set; }
        public string? DmFnsdFoodAvailablity { get; set; }
        public decimal PayProCharges { get; set; }
        public string? OrgShortName { get; set; }
        public string? SmsApiUrl { get; set; }
        public string? SmsApiSender { get; set; }
        public string? SmsApiKey { get; set; }
        public string? DmInsDiscipline { get; set; }
        public string? IsOnlineFormSaveInSections { get; set; }
        public string? IsRequiredLoginForOnlineAdmin { get; set; }
        public string? IsStudentPhotoMandatory { get; set; }
        public string? TechnicalSupportContactNo { get; set; }
        public string? BankLogo { get; set; }
        public string? IsShowSingleReceiptInPrint { get; set; }
        public string? IsShowBankReceiptInPrint { get; set; }
        public string? IsShowUndertakenInPrint { get; set; }
        public string? IsShowPayProChallanNoOnBankRecipt { get; set; }
        public string? PaymentMode { get; set; }
        public string? BankAccountNo { get; set; }
        public string? DmInsStudentGroup { get; set; }
        public string? DmInsTimeSlot { get; set; }
        public string? DmPayWages { get; set; }
        public string? DmEmployeeGroup { get; set; }
        public string? DmProject { get; set; }
        public string? DmBankBranch { get; set; }
        public string? DmExamAdtCreteria { get; set; }
        public bool EnablePrivateKey { get; set; }
        public string? PrivateKey { get; set; }
        public string? GradingPolicy { get; set; }
        public string? DmBookingType { get; set; }
        public string? DmFunctionType { get; set; }
        public string? DmFunctionHall { get; set; }
    }
}
