using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace AutoSynchService.Models
{
    public partial class Entities : DbContext
    {
        public Entities()
        {
        }

        public Entities(DbContextOptions<Entities> options)
            : base(options)
        {
        }

        public virtual DbSet<AccAccount> AccAccount { get; set; } = null!;
        public virtual DbSet<AccAccountHead> AccAccountHead { get; set; } = null!;
        public virtual DbSet<AccAccountsMapping> AccAccountsMapping { get; set; } = null!;
        public virtual DbSet<AccBudgetDetail> AccBudgetDetail { get; set; } = null!;
        public virtual DbSet<AccBudgetMaster> AccBudgetMaster { get; set; } = null!;
        public virtual DbSet<AccFiscalYear> AccFiscalYear { get; set; } = null!;
        public virtual DbSet<AccGroupAccount> AccGroupAccount { get; set; } = null!;
        public virtual DbSet<AccReconciliation> AccReconciliation { get; set; } = null!;
        public virtual DbSet<AccType> AccType { get; set; } = null!;
        public virtual DbSet<AccVoucherDetail> AccVoucherDetail { get; set; } = null!;
        public virtual DbSet<AccVoucherMaster> AccVoucherMaster { get; set; } = null!;
        public virtual DbSet<AccVoucherNumbringSeries> AccVoucherNumbringSeries { get; set; } = null!;
        public virtual DbSet<AccVouchersLedger> AccVouchersLedger { get; set; } = null!;
        public virtual DbSet<GenDomicile> GenDomicile { get; set; } = null!;
        public virtual DbSet<GenImages> GenImages { get; set; } = null!;
        public virtual DbSet<GenTermsAndConditions> GenTermsAndConditions { get; set; } = null!;
        public virtual DbSet<HrmCategory> HrmCategory { get; set; } = null!;
        public virtual DbSet<HrmDepartment> HrmDepartment { get; set; } = null!;
        public virtual DbSet<HrmDesignation> HrmDesignation { get; set; } = null!;
        public virtual DbSet<HrmEmployee> HrmEmployee { get; set; } = null!;
        public virtual DbSet<InsBatch> InsBatch { get; set; } = null!;
        public virtual DbSet<InsCenter> InsCenter { get; set; } = null!;
        public virtual DbSet<InsClass> InsClass { get; set; } = null!;
        public virtual DbSet<InsClassFeeGroupsMapping> InsClassFeeGroupsMapping { get; set; } = null!;
        public virtual DbSet<InsClassToSubjects> InsClassToSubjects { get; set; } = null!;
        public virtual DbSet<InsDiscipline> InsDiscipline { get; set; } = null!;
        public virtual DbSet<InsDivision> InsDivision { get; set; } = null!;
        public virtual DbSet<InsExamination> InsExamination { get; set; } = null!;
        public virtual DbSet<InsFeeGroup> InsFeeGroup { get; set; } = null!;
        public virtual DbSet<InsFeeGroupFeeItemsMapping> InsFeeGroupFeeItemsMapping { get; set; } = null!;
        public virtual DbSet<InsFeeItem> InsFeeItem { get; set; } = null!;
        public virtual DbSet<InsFeePost> InsFeePost { get; set; } = null!;
        public virtual DbSet<InsFeePostMaster> InsFeePostMaster { get; set; } = null!;
        public virtual DbSet<InsFeeTerm> InsFeeTerm { get; set; } = null!;
        public virtual DbSet<InsInstitute> InsInstitute { get; set; } = null!;
        public virtual DbSet<InsNotification> InsNotification { get; set; } = null!;
        public virtual DbSet<InsNotificationType> InsNotificationType { get; set; } = null!;
        public virtual DbSet<InsPayChallan> InsPayChallan { get; set; } = null!;
        public virtual DbSet<InsPayChallanDetail> InsPayChallanDetail { get; set; } = null!;
        public virtual DbSet<InsProgram> InsProgram { get; set; } = null!;
        public virtual DbSet<InsQualification> InsQualification { get; set; } = null!;
        public virtual DbSet<InsRegistrationNumbringSeries> InsRegistrationNumbringSeries { get; set; } = null!;
        public virtual DbSet<InsReligion> InsReligion { get; set; } = null!;
        public virtual DbSet<InsSection> InsSection { get; set; } = null!;
        public virtual DbSet<InsSession> InsSession { get; set; } = null!;
        public virtual DbSet<InsStudent> InsStudent { get; set; } = null!;
        public virtual DbSet<InsStudentAdmission> InsStudentAdmission { get; set; } = null!;
        public virtual DbSet<InsStudentCateogry> InsStudentCateogry { get; set; } = null!;
        public virtual DbSet<InsStudentEmployments> InsStudentEmployments { get; set; } = null!;
        public virtual DbSet<InsStudentFeeMapping> InsStudentFeeMapping { get; set; } = null!;
        public virtual DbSet<InsStudentQualifications> InsStudentQualifications { get; set; } = null!;
        public virtual DbSet<InsStudentSubjectMapping> InsStudentSubjectMapping { get; set; } = null!;
        public virtual DbSet<InsStudentTestScore> InsStudentTestScore { get; set; } = null!;
        public virtual DbSet<InsSubject> InsSubject { get; set; } = null!;
        public virtual DbSet<InsTermAppliedItemsMapping> InsTermAppliedItemsMapping { get; set; } = null!;
        public virtual DbSet<InvCategory> InvCategory { get; set; } = null!;
        public virtual DbSet<InvCompany> InvCompany { get; set; } = null!;
        public virtual DbSet<InvCustomer> InvCustomer { get; set; } = null!;
        public virtual DbSet<InvCustomerType> InvCustomerType { get; set; } = null!;
        public virtual DbSet<InvDeliveryChallanDetail> InvDeliveryChallanDetail { get; set; } = null!;
        public virtual DbSet<InvDeliveryChallanMaster> InvDeliveryChallanMaster { get; set; } = null!;
        public virtual DbSet<InvDemandNote> InvDemandNote { get; set; } = null!;
        public virtual DbSet<InvDemandNoteDetail> InvDemandNoteDetail { get; set; } = null!;
        public virtual DbSet<InvGatePassInDetail> InvGatePassInDetail { get; set; } = null!;
        public virtual DbSet<InvGatePassInMaster> InvGatePassInMaster { get; set; } = null!;
        public virtual DbSet<InvJcMonthSetting> InvJcMonthSetting { get; set; } = null!;
        public virtual DbSet<InvLocation> InvLocation { get; set; } = null!;
        public virtual DbSet<InvPackageProductsMapping> InvPackageProductsMapping { get; set; } = null!;
        public virtual DbSet<InvPaymentType> InvPaymentType { get; set; } = null!;
        public virtual DbSet<InvProduct> InvProduct { get; set; } = null!;
        public virtual DbSet<InvProductBatch> InvProductBatch { get; set; } = null!;
        public virtual DbSet<InvProductLedger> InvProductLedger { get; set; } = null!;
        public virtual DbSet<InvProductionDetail> InvProductionDetail { get; set; } = null!;
        public virtual DbSet<InvProductionMaster> InvProductionMaster { get; set; } = null!;
        public virtual DbSet<InvPurchaseDetail> InvPurchaseDetail { get; set; } = null!;
        public virtual DbSet<InvPurchaseMaster> InvPurchaseMaster { get; set; } = null!;
        public virtual DbSet<InvPurchaseOrderDetail> InvPurchaseOrderDetail { get; set; } = null!;
        public virtual DbSet<InvPurchaseOrderMaster> InvPurchaseOrderMaster { get; set; } = null!;
        public virtual DbSet<InvQuatationDetail> InvQuatationDetail { get; set; } = null!;
        public virtual DbSet<InvQuatationMaster> InvQuatationMaster { get; set; } = null!;
        public virtual DbSet<InvSaleClosing> InvSaleClosing { get; set; } = null!;
        public virtual DbSet<InvSaleClosingDetail> InvSaleClosingDetail { get; set; } = null!;
        public virtual DbSet<InvSaleDetail> InvSaleDetail { get; set; } = null!;
        public virtual DbSet<InvSaleMaster> InvSaleMaster { get; set; } = null!;
        public virtual DbSet<InvSalemanToRoutsMapping> InvSalemanToRoutsMapping { get; set; } = null!;
        public virtual DbSet<InvSchemeDetail> InvSchemeDetail { get; set; } = null!;
        public virtual DbSet<InvSchemeMaster> InvSchemeMaster { get; set; } = null!;
        public virtual DbSet<InvShift> InvShift { get; set; } = null!;
        public virtual DbSet<InvStockAdjustment> InvStockAdjustment { get; set; } = null!;
        public virtual DbSet<InvStockAdjustmentDetail> InvStockAdjustmentDetail { get; set; } = null!;
        public virtual DbSet<InvStockTransfer> InvStockTransfer { get; set; } = null!;
        public virtual DbSet<InvStockTransferDetail> InvStockTransferDetail { get; set; } = null!;
        public virtual DbSet<InvThirdPartyCustomer> InvThirdPartyCustomer { get; set; } = null!;
        public virtual DbSet<InvUnit> InvUnit { get; set; } = null!;
        public virtual DbSet<InvVehicle> InvVehicle { get; set; } = null!;
        public virtual DbSet<InvVendor> InvVendor { get; set; } = null!;
        public virtual DbSet<InvWarehouse> InvWarehouse { get; set; } = null!;
        public virtual DbSet<OrgBranch> OrgBranch { get; set; } = null!;
        public virtual DbSet<OrgFeaturesMapping> OrgFeaturesMapping { get; set; } = null!;
        public virtual DbSet<OrgOrgSystemsMapping> OrgOrgSystemsMapping { get; set; } = null!;
        public virtual DbSet<OrgOrganization> OrgOrganization { get; set; } = null!;
        public virtual DbSet<PyAdvanceLoanInfo> PyAdvanceLoanInfo { get; set; } = null!;
        public virtual DbSet<PyAllowanceMaster> PyAllowanceMaster { get; set; } = null!;
        public virtual DbSet<PyBasicPayDetail> PyBasicPayDetail { get; set; } = null!;
        public virtual DbSet<PyBasicPayMaster> PyBasicPayMaster { get; set; } = null!;
        public virtual DbSet<PyConveyanceAllowance> PyConveyanceAllowance { get; set; } = null!;
        public virtual DbSet<PyCorporateAllowanceMapping> PyCorporateAllowanceMapping { get; set; } = null!;
        public virtual DbSet<PyCorporateSalaryMaster> PyCorporateSalaryMaster { get; set; } = null!;
        public virtual DbSet<PyCpFundCalculation> PyCpFundCalculation { get; set; } = null!;
        public virtual DbSet<PyDeductionMaster> PyDeductionMaster { get; set; } = null!;
        public virtual DbSet<PyEmployeeDeductionMapping> PyEmployeeDeductionMapping { get; set; } = null!;
        public virtual DbSet<PyEmployeeDeductionMaster> PyEmployeeDeductionMaster { get; set; } = null!;
        public virtual DbSet<PyEmployeeSalaryAllowanceMapping> PyEmployeeSalaryAllowanceMapping { get; set; } = null!;
        public virtual DbSet<PyEmployeeSalaryMaster> PyEmployeeSalaryMaster { get; set; } = null!;
        public virtual DbSet<PyEmployeeTransaction> PyEmployeeTransaction { get; set; } = null!;
        public virtual DbSet<PyFormula> PyFormula { get; set; } = null!;
        public virtual DbSet<PyHouseRentAllowanceSetting> PyHouseRentAllowanceSetting { get; set; } = null!;
        public virtual DbSet<PyPaymentSlip> PyPaymentSlip { get; set; } = null!;
        public virtual DbSet<PyPayrollGeneration> PyPayrollGeneration { get; set; } = null!;
        public virtual DbSet<PyPayrollGenerationDetail> PyPayrollGenerationDetail { get; set; } = null!;
        public virtual DbSet<PyPayrollGenerationMaster> PyPayrollGenerationMaster { get; set; } = null!;
        public virtual DbSet<PySalaryHead> PySalaryHead { get; set; } = null!;
        public virtual DbSet<PyStaffAttendance> PyStaffAttendance { get; set; } = null!;
        public virtual DbSet<PyTaxDeductionPattern> PyTaxDeductionPattern { get; set; } = null!;
        public virtual DbSet<ResBed> ResBed { get; set; } = null!;
        public virtual DbSet<ResCurrencyNote> ResCurrencyNote { get; set; } = null!;
        public virtual DbSet<ResCustomerTypeItemsMapping> ResCustomerTypeItemsMapping { get; set; } = null!;
        public virtual DbSet<ResFnsdCreditResetingHostory> ResFnsdCreditResetingHostory { get; set; } = null!;
        public virtual DbSet<ResFood> ResFood { get; set; } = null!;
        public virtual DbSet<ResFoodAddons> ResFoodAddons { get; set; } = null!;
        public virtual DbSet<ResFoodAvailability> ResFoodAvailability { get; set; } = null!;
        public virtual DbSet<ResFoodCategory> ResFoodCategory { get; set; } = null!;
        public virtual DbSet<ResFoodToAddonsMapping> ResFoodToAddonsMapping { get; set; } = null!;
        public virtual DbSet<ResFoodVariant> ResFoodVariant { get; set; } = null!;
        public virtual DbSet<ResFoodVariantRecipe> ResFoodVariantRecipe { get; set; } = null!;
        public virtual DbSet<ResKipraHistory> ResKipraHistory { get; set; } = null!;
        public virtual DbSet<ResKitchen> ResKitchen { get; set; } = null!;
        public virtual DbSet<ResOrderType> ResOrderType { get; set; } = null!;
        public virtual DbSet<ResPackageVarientsMapping> ResPackageVarientsMapping { get; set; } = null!;
        public virtual DbSet<ResPatient> ResPatient { get; set; } = null!;
        public virtual DbSet<ResSaleClosing> ResSaleClosing { get; set; } = null!;
        public virtual DbSet<ResSaleClosingDetail> ResSaleClosingDetail { get; set; } = null!;
        public virtual DbSet<ResSaleDetail> ResSaleDetail { get; set; } = null!;
        public virtual DbSet<ResSaleDetailTemp> ResSaleDetailTemp { get; set; } = null!;
        public virtual DbSet<ResSaleMaster> ResSaleMaster { get; set; } = null!;
        public virtual DbSet<ResShift> ResShift { get; set; } = null!;
        public virtual DbSet<ResTable> ResTable { get; set; } = null!;
        public virtual DbSet<ResVarientPricingByCustType> ResVarientPricingByCustType { get; set; } = null!;
        public virtual DbSet<ResWard> ResWard { get; set; } = null!;
        public virtual DbSet<SysControllesGroup> SysControllesGroup { get; set; } = null!;
        public virtual DbSet<SysExecptionLogging> SysExecptionLogging { get; set; } = null!;
        public virtual DbSet<SysFeature> SysFeature { get; set; } = null!;
        public virtual DbSet<SysForm> SysForm { get; set; } = null!;
        public virtual DbSet<SysHtml> SysHtml { get; set; } = null!;
        public virtual DbSet<SysInvTypeWiseControll> SysInvTypeWiseControll { get; set; } = null!;
        public virtual DbSet<SysLableContent> SysLableContent { get; set; } = null!;
        public virtual DbSet<SysLayout> SysLayout { get; set; } = null!;
        public virtual DbSet<SysModule> SysModule { get; set; } = null!;
        public virtual DbSet<SysModuleFormsMapping> SysModuleFormsMapping { get; set; } = null!;
        public virtual DbSet<SysMonthName> SysMonthName { get; set; } = null!;
        public virtual DbSet<SysOrgFormsMapping> SysOrgFormsMapping { get; set; } = null!;
        public virtual DbSet<SysOrgModulesMapping> SysOrgModulesMapping { get; set; } = null!;
        public virtual DbSet<SysSystem> SysSystem { get; set; } = null!;
        public virtual DbSet<SysWeekDay> SysWeekDay { get; set; } = null!;
        public virtual DbSet<SysYear> SysYear { get; set; } = null!;
        public virtual DbSet<Table1> Table1 { get; set; } = null!;
        public virtual DbSet<UsrSystemUser> UsrSystemUser { get; set; } = null!;
        public virtual DbSet<UsrUserBranchesMapping> UsrUserBranchesMapping { get; set; } = null!;
        public virtual DbSet<UsrUserFormsMapping> UsrUserFormsMapping { get; set; } = null!;
        public virtual DbSet<UsrUserParmsMapping> UsrUserParmsMapping { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
              .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
              .AddJsonFile("appsettings.json")
              .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                //optionsBuilder.UseSqlServer("Data Source=ONE;User ID=sa;Password=Nadra@123;Database=cmsnet_inventory_db;Persist Security Info=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AccAccount>(entity =>
            {
                entity.Property(e => e.AccountCode).HasMaxLength(50);

                entity.Property(e => e.AccountName).HasMaxLength(200);

                entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((0))");

                entity.Property(e => e.GroupId).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.OpeningBalanceCr).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OpeningBalanceDr).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<AccAccountHead>(entity =>
            {
                entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((0))");

                entity.Property(e => e.HeadCode).HasMaxLength(10);

                entity.Property(e => e.HeadName).HasMaxLength(200);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<AccAccountsMapping>(entity =>
            {
                entity.Property(e => e.DebitOrCredit).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ItemBillType).HasMaxLength(20);

                entity.Property(e => e.MappingForm).HasMaxLength(100);

                entity.Property(e => e.MappingSource).HasMaxLength(50);

                entity.Property(e => e.PartnerType).HasMaxLength(20);

                entity.Property(e => e.TransactionType).HasMaxLength(20);
            });

            modelBuilder.Entity<AccBudgetDetail>(entity =>
            {
                entity.Property(e => e.ActualAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ApprovedAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.EstimatedAmount).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<AccBudgetMaster>(entity =>
            {
                entity.Property(e => e.BroughtForward).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.GovernmentFund).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.IncreaseInPercent).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LastUpdatedDate).HasColumnName("lastUpdatedDate");

                entity.Property(e => e.Other).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OwnResources).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.TotalBudget).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<AccFiscalYear>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");
            });

            modelBuilder.Entity<AccGroupAccount>(entity =>
            {
                entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((0))");

                entity.Property(e => e.GroupCode).HasMaxLength(10);

                entity.Property(e => e.GroupName).HasMaxLength(200);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<AccReconciliation>(entity =>
            {
                entity.Property(e => e.BeginingBalance).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.BeginingDate).HasColumnType("date");

                entity.Property(e => e.ClearVouchers).HasMaxLength(500);

                entity.Property(e => e.Deposits).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Payments).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.StatmentEndingBalance).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.StatmentEndingDate).HasColumnType("date");

                entity.Property(e => e.UnClearVouchers).HasMaxLength(500);
            });

            modelBuilder.Entity<AccType>(entity =>
            {
                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TypeCode).HasMaxLength(50);

                entity.Property(e => e.TypeGroup).HasMaxLength(20);

                entity.Property(e => e.TypeName).HasMaxLength(200);
            });

            modelBuilder.Entity<AccVoucherDetail>(entity =>
            {
                entity.Property(e => e.AmountCredit).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AmountDebit).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Folio).HasMaxLength(300);

                entity.Property(e => e.MappingForm).HasMaxLength(50);

                entity.Property(e => e.MappingSource).HasMaxLength(50);

                entity.Property(e => e.PartnerType).HasMaxLength(20);

                entity.Property(e => e.ProductId).HasDefaultValueSql("((0))");

                entity.Property(e => e.Type).HasMaxLength(10);
            });

            modelBuilder.Entity<AccVoucherMaster>(entity =>
            {
                entity.Property(e => e.BankAccountNo).HasMaxLength(200);

                entity.Property(e => e.BankName).HasMaxLength(300);

                entity.Property(e => e.ChequeDate).HasColumnType("date");

                entity.Property(e => e.ChequeNo).HasMaxLength(200);

                entity.Property(e => e.DebitableCreditable).HasMaxLength(500);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.JvFormType).HasMaxLength(10);

                entity.Property(e => e.PaidToReceivedFrom).HasMaxLength(500);

                entity.Property(e => e.VoucherNo).HasMaxLength(50);

                entity.Property(e => e.VoucherStatus).HasMaxLength(10);

                entity.Property(e => e.VoucherType).HasMaxLength(3);
            });

            modelBuilder.Entity<AccVoucherNumbringSeries>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.VoucherType).HasMaxLength(3);
            });

            modelBuilder.Entity<AccVouchersLedger>(entity =>
            {
                entity.Property(e => e.AccountName).HasMaxLength(300);

                entity.Property(e => e.Credit).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Debit).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.PartnerType).HasMaxLength(20);

                entity.Property(e => e.VoucherType).HasMaxLength(3);
            });

            modelBuilder.Entity<GenDomicile>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<GenImages>(entity =>
            {
                entity.Property(e => e.ImageName).HasMaxLength(500);

                entity.Property(e => e.ImagePath).HasMaxLength(500);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Source).HasMaxLength(50);
            });

            modelBuilder.Entity<GenTermsAndConditions>(entity =>
            {
                entity.Property(e => e.TextLine).HasMaxLength(500);

                entity.Property(e => e.Type).HasMaxLength(50);
            });

            modelBuilder.Entity<HrmCategory>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Remarks).IsUnicode(false);
            });

            modelBuilder.Entity<HrmDepartment>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<HrmDesignation>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ResFnsdAllowanceLimit).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<HrmEmployee>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.BankAccountNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BasicPayScale).HasDefaultValueSql("((-1))");

                entity.Property(e => e.BranchId).HasDefaultValueSql("((0))");

                entity.Property(e => e.CategoryId).HasDefaultValueSql("((0))");

                entity.Property(e => e.Code).HasMaxLength(100);

                entity.Property(e => e.Country).HasMaxLength(200);

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((0))");

                entity.Property(e => e.CurrentGovt).HasMaxLength(500);

                entity.Property(e => e.CurrentStage).HasDefaultValueSql("((0))");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.DateOfJoin).HasColumnType("date");

                entity.Property(e => e.DepartmentId).HasDefaultValueSql("((0))");

                entity.Property(e => e.DesignationId).HasDefaultValueSql("((0))");

                entity.Property(e => e.Domicile)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.EmployeeGrade).HasMaxLength(100);

                entity.Property(e => e.EmployeeGroup).HasMaxLength(50);

                entity.Property(e => e.EmployeeName).HasMaxLength(300);

                entity.Property(e => e.FatherName).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.FnsdLimitBalance)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.Gpid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("GPID");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.IsAvailForCreditLimit).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.LDate1)
                    .HasMaxLength(200)
                    .HasColumnName("L_Date");

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.Ldate)
                    .HasColumnType("date")
                    .HasColumnName("LDate");

                entity.Property(e => e.LedgerId).HasDefaultValueSql("((0))");

                entity.Property(e => e.LinkCode).HasMaxLength(50);

                entity.Property(e => e.MaritalStatus).HasMaxLength(10);

                entity.Property(e => e.Mobile).HasMaxLength(100);

                entity.Property(e => e.Nationality).HasMaxLength(100);

                entity.Property(e => e.Nic)
                    .HasMaxLength(50)
                    .HasColumnName("NIC");

                entity.Property(e => e.PDate)
                    .HasColumnType("date")
                    .HasColumnName("P_Date");

                entity.Property(e => e.PayrollType)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("(N'M')");

                entity.Property(e => e.Pdate1)
                    .HasMaxLength(200)
                    .HasColumnName("PDate");

                entity.Property(e => e.Phone).HasMaxLength(200);

                entity.Property(e => e.Qualification).HasMaxLength(50);

                entity.Property(e => e.Rdate)
                    .HasMaxLength(200)
                    .HasColumnName("RDate");

                entity.Property(e => e.ReligionId).HasDefaultValueSql("((0))");

                entity.Property(e => e.Remarks).HasMaxLength(500);

                entity.Property(e => e.RetirementDate).HasColumnType("date");

                entity.Property(e => e.StateS)
                    .HasMaxLength(10)
                    .HasColumnName("State_S")
                    .IsFixedLength();

                entity.Property(e => e.Status).HasDefaultValueSql("((0))");

                entity.Property(e => e.UDate)
                    .HasColumnType("date")
                    .HasColumnName("U_Date");

                entity.Property(e => e.Udate1)
                    .HasMaxLength(200)
                    .HasColumnName("UDate");

                entity.Property(e => e.UpdatedBy).HasDefaultValueSql("((0))");

                entity.Property(e => e.Weekholiday).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<InsBatch>(entity =>
            {
                entity.Property(e => e.BatchName).HasMaxLength(200);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.StartDate).HasColumnType("date");
            });

            modelBuilder.Entity<InsCenter>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<InsClass>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<InsDiscipline>(entity =>
            {
                entity.Property(e => e.AdmissionExpiryDate).HasColumnType("date");

                entity.Property(e => e.AdmissionFee).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.IsAdmissionOpen).HasMaxLength(10);

                entity.Property(e => e.IsGovtEmploymentOption).HasMaxLength(10);

                entity.Property(e => e.IsMultipeCategory).HasMaxLength(10);

                entity.Property(e => e.IsTestRequired)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Yes')");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.QualificationsRequired).HasMaxLength(100);

                entity.Property(e => e.TestFee).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<InsDivision>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PercentageFrom).HasColumnType("numeric(6, 2)");

                entity.Property(e => e.PercentageTo).HasColumnType("numeric(6, 2)");
            });

            modelBuilder.Entity<InsExamination>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<InsFeeGroup>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.GroupName).HasMaxLength(200);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsApplyChangesOnStudents)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<InsFeeGroupFeeItemsMapping>(entity =>
            {
                entity.Property(e => e.FeeRate).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<InsFeeItem>(entity =>
            {
                entity.Property(e => e.DefaultValue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FeeName).HasMaxLength(50);

                entity.Property(e => e.FeeType).HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<InsFeePost>(entity =>
            {
                entity.Property(e => e.AdditionalValue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ApprovalStatus)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('R')");

                entity.Property(e => e.NetValue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.StatusChangeRemarks).HasMaxLength(500);

                entity.Property(e => e.TermValue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<InsFeePostMaster>(entity =>
            {
                entity.Property(e => e.LastModifyBy).HasDefaultValueSql("((-1))");

                entity.Property(e => e.PostingMethod).HasMaxLength(20);

                entity.Property(e => e.PostingRemarks).HasMaxLength(500);
            });

            modelBuilder.Entity<InsFeeTerm>(entity =>
            {
                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Rate).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TermName).HasMaxLength(200);
            });

            modelBuilder.Entity<InsInstitute>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<InsNotification>(entity =>
            {
                entity.Property(e => e.Remarks).HasMaxLength(500);

                entity.Property(e => e.Title).HasMaxLength(200);
            });

            modelBuilder.Entity<InsNotificationType>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<InsPayChallan>(entity =>
            {
                entity.Property(e => e.AmountAfterDueDate)
                    .HasMaxLength(14)
                    .HasColumnName("Amount_After_DueDate");

                entity.Property(e => e.AmountPaid)
                    .HasMaxLength(12)
                    .HasColumnName("Amount_Paid");

                entity.Property(e => e.AmountWithinDueDate)
                    .HasMaxLength(14)
                    .HasColumnName("Amount_Within_DueDate");

                entity.Property(e => e.BankMnemonic)
                    .HasMaxLength(200)
                    .HasColumnName("Bank_Mnemonic");

                entity.Property(e => e.BillStatus)
                    .HasMaxLength(1)
                    .HasColumnName("Bill_Status");

                entity.Property(e => e.BillingMonth)
                    .HasMaxLength(4)
                    .HasColumnName("Billing_Month");

                entity.Property(e => e.ChallanId).HasColumnName("challan_Id");

                entity.Property(e => e.ChallanNo).HasMaxLength(20);

                entity.Property(e => e.ChallanSource).HasMaxLength(100);

                entity.Property(e => e.CustomerDetail)
                    .HasMaxLength(30)
                    .HasColumnName("Customer_Detail");

                entity.Property(e => e.DatePaid)
                    .HasMaxLength(8)
                    .HasColumnName("Date_Paid");

                entity.Property(e => e.DueDate)
                    .HasMaxLength(8)
                    .HasColumnName("Due_Date");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.NetAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PaidSource)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('System')");

                entity.Property(e => e.ReferenceChallanNo).HasMaxLength(50);

                entity.Property(e => e.Reserved).HasMaxLength(200);

                entity.Property(e => e.TranAuthId)
                    .HasMaxLength(6)
                    .HasColumnName("Tran_Auth_Id");

                entity.Property(e => e.TransDate)
                    .HasMaxLength(8)
                    .HasColumnName("Trans_Date");

                entity.Property(e => e.TransTime)
                    .HasMaxLength(6)
                    .HasColumnName("Trans_Time");

                entity.Property(e => e.TransactionAmount).HasMaxLength(14);
            });

            modelBuilder.Entity<InsPayChallanDetail>(entity =>
            {
                entity.Property(e => e.ItemFeeDescription).HasMaxLength(100);

                entity.Property(e => e.ItemSource).HasMaxLength(100);

                entity.Property(e => e.NetAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PaidAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PaidStatus)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('N')");
            });

            modelBuilder.Entity<InsProgram>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.FeePostingType).HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<InsQualification>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<InsRegistrationNumbringSeries>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<InsReligion>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<InsSection>(entity =>
            {
                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Section).HasMaxLength(200);
            });

            modelBuilder.Entity<InsSession>(entity =>
            {
                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.SessionName).HasMaxLength(200);

                entity.Property(e => e.StartDate).HasColumnType("date");
            });

            modelBuilder.Entity<InsStudent>(entity =>
            {
                entity.Property(e => e.BloodGroup).HasMaxLength(300);

                entity.Property(e => e.CategoryIds).HasMaxLength(500);

                entity.Property(e => e.Cnic)
                    .HasMaxLength(50)
                    .HasColumnName("CNIC");

                entity.Property(e => e.CommunicableDisease).HasMaxLength(300);

                entity.Property(e => e.DateOfJoining).HasColumnType("date");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.EmailAddress).HasMaxLength(200);

                entity.Property(e => e.FatherContact).HasMaxLength(50);

                entity.Property(e => e.FatherName).HasMaxLength(100);

                entity.Property(e => e.FatherProfession).HasMaxLength(50);

                entity.Property(e => e.FormNo).HasDefaultValueSql("((0))");

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.GuardianContact).HasMaxLength(50);

                entity.Property(e => e.GuardianName).HasMaxLength(100);

                entity.Property(e => e.InstitutionLastAttendant).HasMaxLength(300);

                entity.Property(e => e.IsAppearForTest)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('No')");

                entity.Property(e => e.IsGovtEmployee)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('No')");

                entity.Property(e => e.LastEnrolmentNo).HasMaxLength(100);

                entity.Property(e => e.Nationality).HasMaxLength(50);

                entity.Property(e => e.OnlineFormStepStatus).HasDefaultValueSql("((-1))");

                entity.Property(e => e.PermanentAddress).HasMaxLength(500);

                entity.Property(e => e.PlcNo).HasMaxLength(100);

                entity.Property(e => e.PostalAddress).HasMaxLength(300);

                entity.Property(e => e.PreviouslyEnrolledInOrg).HasMaxLength(10);

                entity.Property(e => e.ProgramIds).HasMaxLength(500);

                entity.Property(e => e.RegistrationNo).HasMaxLength(100);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.StudentContact).HasMaxLength(50);

                entity.Property(e => e.StudentName).HasMaxLength(100);
            });

            modelBuilder.Entity<InsStudentAdmission>(entity =>
            {
                entity.Property(e => e.ExaminationCenterId).HasColumnName("ExaminationCenterID");

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<InsStudentCateogry>(entity =>
            {
                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<InsStudentEmployments>(entity =>
            {
                entity.Property(e => e.Department).HasMaxLength(500);

                entity.Property(e => e.PeriodFrom).HasColumnType("date");

                entity.Property(e => e.PeriodTo).HasColumnType("date");

                entity.Property(e => e.PostBps)
                    .HasMaxLength(100)
                    .HasColumnName("PostBPS");
            });

            modelBuilder.Entity<InsStudentFeeMapping>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.AdditionalValue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.NetValue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TermValue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<InsStudentQualifications>(entity =>
            {
                entity.Property(e => e.BoardUniversity).HasMaxLength(500);

                entity.Property(e => e.Division).HasMaxLength(50);

                entity.Property(e => e.ObtMarks)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PercentageMarks)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RollNo).HasMaxLength(50);

                entity.Property(e => e.TotalMarks)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<InsStudentTestScore>(entity =>
            {
                entity.Property(e => e.ObtainedScore).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.RollNo).HasMaxLength(50);

                entity.Property(e => e.TotalScore).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<InsSubject>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.ShortName).HasMaxLength(50);
            });

            modelBuilder.Entity<InvCategory>(entity =>
            {
                entity.Property(e => e.CategoryGroup)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('Store')");

                entity.Property(e => e.ImageName).HasMaxLength(200);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.OfferEndDate).HasColumnType("date");

                entity.Property(e => e.OfferStartDate).HasColumnType("date");
            });

            modelBuilder.Entity<InvCompany>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<InvCustomer>(entity =>
            {
                entity.Property(e => e.AcctBalance).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AcctMaxBalance).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Address1).HasMaxLength(30);

                entity.Property(e => e.Address2).HasMaxLength(30);

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Company).HasMaxLength(30);

                entity.Property(e => e.Contact1).HasMaxLength(30);

                entity.Property(e => e.Contact2).HasMaxLength(30);

                entity.Property(e => e.DiscPercent).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FatherName).HasMaxLength(50);

                entity.Property(e => e.ImageName).HasMaxLength(255);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Mobile).HasMaxLength(15);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(15);

                entity.Property(e => e.Terms).HasMaxLength(15);
            });

            modelBuilder.Entity<InvCustomerType>(entity =>
            {
                entity.Property(e => e.DefaultPaymentType).HasMaxLength(20);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.OrderTypeGroup)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('General')");

                entity.Property(e => e.ReceiptFormate).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(200);
            });

            modelBuilder.Entity<InvDeliveryChallanDetail>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Qty).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<InvDeliveryChallanMaster>(entity =>
            {
                entity.Property(e => e.CustomerName).HasMaxLength(50);

                entity.Property(e => e.DriverContact).HasMaxLength(50);

                entity.Property(e => e.DriverName).HasMaxLength(200);

                entity.Property(e => e.Remarks).HasMaxLength(500);

                entity.Property(e => e.VehicleNo).HasMaxLength(200);
            });

            modelBuilder.Entity<InvDemandNote>(entity =>
            {
                entity.Property(e => e.Remarks).HasMaxLength(500);

                entity.Property(e => e.Status).HasMaxLength(20);
            });

            modelBuilder.Entity<InvDemandNoteDetail>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Cost).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Qty).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<InvGatePassInDetail>(entity =>
            {
                entity.Property(e => e.Cost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Qty).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<InvGatePassInMaster>(entity =>
            {
                entity.Property(e => e.DriverContact).HasMaxLength(50);

                entity.Property(e => e.DriverName).HasMaxLength(200);

                entity.Property(e => e.Remarks).HasMaxLength(500);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.VehicleNo).HasMaxLength(200);
            });

            modelBuilder.Entity<InvJcMonthSetting>(entity =>
            {
                entity.Property(e => e.FromDate).HasColumnType("date");

                entity.Property(e => e.ToDate).HasColumnType("date");

                entity.Property(e => e.WeekNosInJc).HasMaxLength(50);
            });

            modelBuilder.Entity<InvLocation>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<InvPackageProductsMapping>(entity =>
            {
                entity.Property(e => e.Qty).HasColumnType("decimal(18, 6)");
            });

            modelBuilder.Entity<InvPaymentType>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.PaymentType).HasMaxLength(100);

                entity.Property(e => e.TypeGroup)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('Other')");
            });

            modelBuilder.Entity<InvProduct>(entity =>
            {
                entity.Property(e => e.AverageCost).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.Barcode).HasMaxLength(50);

                entity.Property(e => e.BillType).HasMaxLength(50);

                entity.Property(e => e.ChargeMeterType)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CompDiscPercent)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CompExtraDiscPercent)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CompanyId).HasDefaultValueSql("((0))");

                entity.Property(e => e.Cost).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.CostIncTax).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.ImageName).HasMaxLength(300);

                entity.Property(e => e.MaterialSize)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MinimumLevel)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.OfferEndDate).HasColumnType("date");

                entity.Property(e => e.OfferRate).HasColumnType("numeric(18, 1)");

                entity.Property(e => e.OfferStartDate).HasColumnType("date");

                entity.Property(e => e.PurchaseUnitId).HasDefaultValueSql("((0))");

                entity.Property(e => e.ReorderQty).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.RetailPrice).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.SaleDiscount)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SaleTaxCalMethodInPur).HasMaxLength(20);

                entity.Property(e => e.SaleUnitId).HasDefaultValueSql("((0))");

                entity.Property(e => e.Tax).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TaxValue).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.Type).HasMaxLength(20);

                entity.Property(e => e.UnitWeight).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<InvProductBatch>(entity =>
            {
                entity.Property(e => e.BatchBarcode).HasMaxLength(200);

                entity.Property(e => e.BatchNo).HasMaxLength(100);

                entity.Property(e => e.Source).HasMaxLength(10);
            });

            modelBuilder.Entity<InvProductLedger>(entity =>
            {
                entity.Property(e => e.AverageCost).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.BatchBarcode).HasMaxLength(200);

                entity.Property(e => e.BatchNo).HasMaxLength(100);

                entity.Property(e => e.Cost).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.ExpiryDate).HasColumnType("date");

                entity.Property(e => e.QtyCut).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.QtyIn).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.QtyOut).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Remarks).HasMaxLength(500);

                entity.Property(e => e.RetailPrice).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.Source).HasMaxLength(50);

                entity.Property(e => e.SourceParty).HasMaxLength(300);
            });

            modelBuilder.Entity<InvProductionDetail>(entity =>
            {
                entity.Property(e => e.AverageCost).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.CunsumptionQty).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.NetQty).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.ProductionQty).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.Size)
                    .HasColumnType("decimal(18, 3)")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<InvProductionMaster>(entity =>
            {
                entity.Property(e => e.EndTime).HasMaxLength(50);

                entity.Property(e => e.GridMeterUnits).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.ProductionMeterUnits).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.ProductionPerUnitCost).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.StartTime).HasMaxLength(50);

                entity.Property(e => e.TotalMaterial).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.TotalProduction).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.TotalTimeInHours).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.TotalWasteMaterialCost).HasColumnType("decimal(18, 6)");
            });

            modelBuilder.Entity<InvPurchaseDetail>(entity =>
            {
                entity.Property(e => e.AdditionalTaxAmount).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.AverageCost).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.BatchNo).HasMaxLength(100);

                entity.Property(e => e.CostPrice).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.CutQty).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Disc).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.ExpiryDate).HasColumnType("date");

                entity.Property(e => e.Qty).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.RetailPrice).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.Scheme).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 6)");
            });

            modelBuilder.Entity<InvPurchaseMaster>(entity =>
            {
                entity.Property(e => e.AdvanceTaxAmount).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.BiltyDate).HasColumnType("date");

                entity.Property(e => e.BiltyNo).HasMaxLength(50);

                entity.Property(e => e.CancelRemarks).HasMaxLength(500);

                entity.Property(e => e.Commission).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.CurrencyRate).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DriverContactNo).HasMaxLength(50);

                entity.Property(e => e.DriverName).HasMaxLength(100);

                entity.Property(e => e.Frieght).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.GrandTotal).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.GrandTotalBeforeWhTax).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.InvoiceDisc).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.InvoiceTotal).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.LoadingCharges).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.OtherCharges).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.PaymentType).HasMaxLength(50);

                entity.Property(e => e.Remarks).HasMaxLength(500);

                entity.Property(e => e.Source).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(10);

                entity.Property(e => e.Tax).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.VehicleNo).HasMaxLength(50);

                entity.Property(e => e.WithholdingTaxInAmount).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.WithholdingTaxInPer).HasColumnType("decimal(18, 6)");
            });

            modelBuilder.Entity<InvPurchaseOrderDetail>(entity =>
            {
                entity.Property(e => e.QtyDamaged).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.QtyOrdered).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.QtyReceived).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.SalePrice).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.UnitCost).HasColumnType("decimal(18, 6)");
            });

            modelBuilder.Entity<InvPurchaseOrderMaster>(entity =>
            {
                entity.Property(e => e.Instructions).HasMaxLength(200);

                entity.Property(e => e.OrderingMode).HasMaxLength(50);

                entity.Property(e => e.Potype)
                    .HasMaxLength(50)
                    .HasColumnName("POType");

                entity.Property(e => e.Reference).HasMaxLength(200);

                entity.Property(e => e.ShipTo1).HasMaxLength(55);

                entity.Property(e => e.ShipTo2).HasMaxLength(55);

                entity.Property(e => e.ShipToDestination).HasMaxLength(10);

                entity.Property(e => e.ShipVia).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(10);

                entity.Property(e => e.Terms).HasMaxLength(15);

                entity.Property(e => e.TotalCharges).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalCost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalCostReceived).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UpdatedBy).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<InvQuatationDetail>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Qty).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<InvQuatationMaster>(entity =>
            {
                entity.Property(e => e.ContactNo).HasMaxLength(50);

                entity.Property(e => e.CustomerName).HasMaxLength(200);

                entity.Property(e => e.DueDate).HasColumnType("date");

                entity.Property(e => e.Remarks).HasMaxLength(500);

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<InvSaleClosing>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Remarks).HasMaxLength(500);

                entity.Property(e => e.TellAmount).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<InvSaleClosingDetail>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<InvSaleDetail>(entity =>
            {
                entity.Property(e => e.Discount).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.FurtherTax).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.Pctcode)
                    .HasMaxLength(100)
                    .HasColumnName("PCTCode");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PriceExclusiveTax).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.Qty).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SaleValue).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.TaxCharged).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.TaxRate).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<InvSaleMaster>(entity =>
            {
                entity.Property(e => e.BuyerCnic)
                    .HasMaxLength(50)
                    .HasColumnName("BuyerCNIC");

                entity.Property(e => e.BuyerNtn)
                    .HasMaxLength(50)
                    .HasColumnName("BuyerNTN");

                entity.Property(e => e.BuyerPhoneNumber).HasMaxLength(50);

                entity.Property(e => e.CancelledReason).HasMaxLength(300);

                entity.Property(e => e.Change).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CustomerContact).HasMaxLength(100);

                entity.Property(e => e.CustomerName).HasMaxLength(200);

                entity.Property(e => e.DiscountCalculated).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.DiscountPercent).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DiscountRemarks).HasMaxLength(200);

                entity.Property(e => e.EmpCreditType).HasMaxLength(50);

                entity.Property(e => e.FbrInvoiceNumber).HasMaxLength(50);

                entity.Property(e => e.FbrPosid).HasColumnName("FbrPOSID");

                entity.Property(e => e.FbrResponse).HasMaxLength(200);

                entity.Property(e => e.FbrResponseCode).HasMaxLength(10);

                entity.Property(e => e.FbrUsin)
                    .HasMaxLength(50)
                    .HasColumnName("FbrUSIN");

                entity.Property(e => e.FurtherTax).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.GrandTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.InvoiceTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.IsSentToFbr).HasDefaultValueSql("((-1))");

                entity.Property(e => e.NextServiceDate).HasColumnType("date");

                entity.Property(e => e.OrderStatus).HasMaxLength(50);

                entity.Property(e => e.PaymentReceived).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PaymentType).HasMaxLength(50);

                entity.Property(e => e.Remarks).HasMaxLength(200);

                entity.Property(e => e.ScaleNumber).HasMaxLength(100);

                entity.Property(e => e.ServiceChargesCalculated).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TaxCalculated).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalQuantity).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.VehicleNo).HasMaxLength(100);
            });

            modelBuilder.Entity<InvSchemeDetail>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<InvSchemeMaster>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.DiscLevel).HasMaxLength(1);

                entity.Property(e => e.FromDate).HasColumnType("date");

                entity.Property(e => e.Remarks).HasMaxLength(500);

                entity.Property(e => e.SchemeOn).HasMaxLength(1);

                entity.Property(e => e.ToDate).HasColumnType("date");

                entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<InvShift>(entity =>
            {
                entity.Property(e => e.Shift).HasMaxLength(100);
            });

            modelBuilder.Entity<InvStockAdjustment>(entity =>
            {
                entity.Property(e => e.Remarks).HasMaxLength(500);
            });

            modelBuilder.Entity<InvStockAdjustmentDetail>(entity =>
            {
                entity.Property(e => e.Cost).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.PhysicalQty).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.QtyDifference).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SystemQty).HasColumnType("decimal(18, 6)");
            });

            modelBuilder.Entity<InvStockTransfer>(entity =>
            {
                entity.Property(e => e.Cost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Remarks).HasMaxLength(500);
            });

            modelBuilder.Entity<InvStockTransferDetail>(entity =>
            {
                entity.Property(e => e.Cost).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.DemandedQty).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.Qty).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.StockOnHand).HasColumnType("decimal(18, 6)");
            });

            modelBuilder.Entity<InvThirdPartyCustomer>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.Commission).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<InvUnit>(entity =>
            {
                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ParentId).HasDefaultValueSql("((-1))");

                entity.Property(e => e.UnitName).HasMaxLength(50);

                entity.Property(e => e.UnitOfConversion).HasColumnType("decimal(18, 10)");
            });

            modelBuilder.Entity<InvVehicle>(entity =>
            {
                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Model).HasMaxLength(50);

                entity.Property(e => e.VehicleBrand)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.VehicleName).HasMaxLength(200);

                entity.Property(e => e.VehicleNo).HasMaxLength(50);
            });

            modelBuilder.Entity<InvVendor>(entity =>
            {
                entity.Property(e => e.Address1).HasMaxLength(500);

                entity.Property(e => e.Address2).HasMaxLength(500);

                entity.Property(e => e.City).HasMaxLength(20);

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Commission).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Company).HasMaxLength(500);

                entity.Property(e => e.Country).HasMaxLength(30);

                entity.Property(e => e.DefaultBillableDepartment).HasMaxLength(20);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FatherName).HasMaxLength(50);

                entity.Property(e => e.Fax).HasMaxLength(15);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MinimumOrder).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Mobile).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.NtnNo).HasMaxLength(200);

                entity.Property(e => e.Phone).HasMaxLength(15);

                entity.Property(e => e.SaleTaxNumber).HasMaxLength(200);

                entity.Property(e => e.TaxType).HasMaxLength(20);

                entity.Property(e => e.VendordTerms).HasMaxLength(15);

                entity.Property(e => e.Website).HasColumnType("ntext");

                entity.Property(e => e.ZipCode).HasMaxLength(10);
            });

            modelBuilder.Entity<InvWarehouse>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.StoreType).HasMaxLength(20);
            });

            modelBuilder.Entity<OrgBranch>(entity =>
            {
                entity.Property(e => e.AccAccountMethod).HasMaxLength(20);

                entity.Property(e => e.AccAccountPeriod).HasMaxLength(20);

                entity.Property(e => e.AccBussinessType).HasMaxLength(20);

                entity.Property(e => e.AccPostingMethod).HasMaxLength(20);

                entity.Property(e => e.AccVoucherAutoApproved)
                    .HasMaxLength(3)
                    .HasDefaultValueSql("('No')");

                entity.Property(e => e.BranchLogo).HasMaxLength(200);

                entity.Property(e => e.BranchLogoName).HasMaxLength(100);

                entity.Property(e => e.BranchName).HasMaxLength(100);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.InsFeeAccInteg)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('No')");

                entity.Property(e => e.InvBillAmountLimitForScharges)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("InvBillAmountLimitForSCharges");

                entity.Property(e => e.InvCreateJvInCaseOfQtsale)
                    .HasMaxLength(3)
                    .HasColumnName("InvCreateJvInCaseOfQTSale");

                entity.Property(e => e.InvDefaultPaymentType).HasMaxLength(50);

                entity.Property(e => e.InvIsItemComboLoadOnStart)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.InvIsSchargesFlatPos).HasColumnName("InvIsSChargesFlatPos");

                entity.Property(e => e.InvItemCodeReadOnly).HasMaxLength(10);

                entity.Property(e => e.InvMixSaleQtyCount).HasDefaultValueSql("((1))");

                entity.Property(e => e.InvProductLedgerTransactionFromJv).HasMaxLength(10);

                entity.Property(e => e.InvPurchaseAccInteg)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('No')");

                entity.Property(e => e.InvRowAddingStyle)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Append')");

                entity.Property(e => e.InvSaleAccInteg)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('No')");

                entity.Property(e => e.InvSaleInvoiceDate)
                    .HasMaxLength(3)
                    .HasDefaultValueSql("('No')");

                entity.Property(e => e.InvSchargesInPercent)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("InvSChargesInPercent");

                entity.Property(e => e.InvSkipSchargesIfBillIsQt)
                    .HasMaxLength(10)
                    .HasColumnName("InvSkipSChargesIfBillIsQT");

                entity.Property(e => e.InvTaxInPercent).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.InvUpdateLastPrices).HasMaxLength(5);

                entity.Property(e => e.InvUseFbrApi).HasMaxLength(1);

                entity.Property(e => e.InventoryType).HasMaxLength(30);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LongAddress).HasMaxLength(500);

                entity.Property(e => e.MobileNumber).HasMaxLength(50);

                entity.Property(e => e.NtnNo).HasMaxLength(200);

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.Property(e => e.ReportsTitle).HasMaxLength(200);

                entity.Property(e => e.ResBillAmountLimitForScharges)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("ResBillAmountLimitForSCharges");

                entity.Property(e => e.ResDefaultBillTypeInPayment)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('Takeaway')");

                entity.Property(e => e.ResIsItemAddtoSeparateLine).HasDefaultValueSql("((0))");

                entity.Property(e => e.ResIsPrintKotPlaceOrder)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ResIsShowIconicSearch)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ResIsTableSelection)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ResPrintOptionFromPos)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'KOT')");

                entity.Property(e => e.ResSchargesInPercent)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("ResSChargesInPercent");

                entity.Property(e => e.ResTaxInPercent).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ShortAddress).HasMaxLength(100);

                entity.Property(e => e.Strn)
                    .HasMaxLength(50)
                    .HasColumnName("STRN");

                entity.Property(e => e.TaxFormation).HasMaxLength(100);

                entity.Property(e => e.UseDataInMerging).HasMaxLength(3);

                entity.Property(e => e.Website).HasMaxLength(100);
            });

            modelBuilder.Entity<OrgFeaturesMapping>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<OrgOrgSystemsMapping>(entity =>
            {
                entity.Property(e => e.SystemLabelDesc).HasMaxLength(300);
            });

            modelBuilder.Entity<OrgOrganization>(entity =>
            {
                entity.Property(e => e.AccountIntegration)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('No')");

                entity.Property(e => e.BankLogo).HasMaxLength(100);

                entity.Property(e => e.DmAccFyear)
                    .HasMaxLength(10)
                    .HasColumnName("DmAccFYear")
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmAccGroup)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmAccHead)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmAccMapping)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmAccType)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmAccounts)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmBatch)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmCenter)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmCompany)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmCustomer)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmDinTable)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmDivision)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmEmployee)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmExamination)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmFeeGroup)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmFeeItem)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmFeeTerm)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmFnsdCategory)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmFnsdFood)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmFnsdFoodAvailablity).HasMaxLength(10);

                entity.Property(e => e.DmGenCity)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmGenDomicile)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmHrmCategory)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmHrmDepartment)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmHrmDesignation)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmInsDiscipline)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Merge')");

                entity.Property(e => e.DmInstitute)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmInvCustType)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmInvPayType)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmInvShift)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmLocation)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmPatients)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmProduct)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmProductCategory)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmProgram)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmQualification)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmReligion)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmResShift)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmSession)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmStudent)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmStudentCatgory)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmSubject)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmThirdPartyCompany)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmUnit)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmVehicle)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmVendor)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmWard)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.DmWarehouse)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Separate')");

                entity.Property(e => e.Edate).HasColumnName("EDate");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FbrApiProduction).HasMaxLength(100);

                entity.Property(e => e.FbrApiSandBox).HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsOnlineFormSaveInSections).HasMaxLength(10);

                entity.Property(e => e.IsRequiredLoginForOnlineAdmin).HasMaxLength(10);

                entity.Property(e => e.IsShowBankReceiptInPrint).HasMaxLength(10);

                entity.Property(e => e.IsShowSingleReceiptInPrint).HasMaxLength(10);

                entity.Property(e => e.IsShowUndertakenInPrint).HasMaxLength(10);

                entity.Property(e => e.IsSkipFromGoverHouse)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsStudentPhotoMandatory).HasMaxLength(10);

                entity.Property(e => e.KpraKey).HasMaxLength(100);

                entity.Property(e => e.KpraNo).HasMaxLength(100);

                entity.Property(e => e.LongAddress).HasMaxLength(500);

                entity.Property(e => e.MobileNumber).HasMaxLength(50);

                entity.Property(e => e.OrgCode).HasMaxLength(4);

                entity.Property(e => e.OrgLogo).HasMaxLength(100);

                entity.Property(e => e.OrgName).HasMaxLength(200);

                entity.Property(e => e.OrgShortName).HasMaxLength(100);

                entity.Property(e => e.PayProCharges).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PayProCode).HasMaxLength(4);

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.Property(e => e.ReportsTitle).HasMaxLength(200);

                entity.Property(e => e.ShortAddress).HasMaxLength(100);

                entity.Property(e => e.SmsApiKey).HasMaxLength(100);

                entity.Property(e => e.SmsApiSender).HasMaxLength(100);

                entity.Property(e => e.SmsApiUrl).HasMaxLength(100);

                entity.Property(e => e.TechnicalSupportContactNo).HasMaxLength(50);

                entity.Property(e => e.UseInfoForAllBranches)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Website).HasMaxLength(100);
            });

            modelBuilder.Entity<PyAdvanceLoanInfo>(entity =>
            {
                entity.Property(e => e.AdvanceMonthYearDay).HasColumnType("date");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.EffectiveFrom).HasColumnType("date");

                entity.Property(e => e.EmployeePayrollType).HasMaxLength(50);

                entity.Property(e => e.InstallmentAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OpenLoanPaidAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Source).HasMaxLength(50);
            });

            modelBuilder.Entity<PyAllowanceMaster>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Value).HasColumnType("decimal(16, 2)");
            });

            modelBuilder.Entity<PyBasicPayDetail>(entity =>
            {
                entity.Property(e => e.Increment).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Max).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Min).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Pbs).HasColumnName("PBS");
            });

            modelBuilder.Entity<PyConveyanceAllowance>(entity =>
            {
                entity.Property(e => e.FromBps).HasColumnName("FromBPS");

                entity.Property(e => e.IsFlat)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Rate).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ToBps).HasColumnName("ToBPS");
            });

            modelBuilder.Entity<PyCorporateAllowanceMapping>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<PyCorporateSalaryMaster>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Salary).HasColumnType("decimal(16, 4)");
            });

            modelBuilder.Entity<PyCpFundCalculation>(entity =>
            {
                entity.Property(e => e.Bps).HasColumnName("BPS");

                entity.Property(e => e.CalculatedRate).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MaximumAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MinimumAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PercentRateOnMean).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<PyDeductionMaster>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(16, 2)");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<PyEmployeeDeductionMapping>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Value).HasColumnType("decimal(18, 3)");
            });

            modelBuilder.Entity<PyEmployeeDeductionMaster>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<PyEmployeeSalaryAllowanceMapping>(entity =>
            {
                entity.Property(e => e.GrossAmount)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Value).HasColumnType("decimal(18, 3)");
            });

            modelBuilder.Entity<PyEmployeeSalaryMaster>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.BasicSalary).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<PyEmployeeTransaction>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FromDate).HasColumnType("date");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.NetAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PayrollType)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("(N'M')");

                entity.Property(e => e.Source)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ToDate).HasColumnType("date");

                entity.Property(e => e.Vno)
                    .IsUnicode(false)
                    .HasColumnName("VNo");
            });

            modelBuilder.Entity<PyFormula>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.FormulaCode).HasMaxLength(3);

                entity.Property(e => e.FormulaSection).HasMaxLength(10);
            });

            modelBuilder.Entity<PyHouseRentAllowanceSetting>(entity =>
            {
                entity.Property(e => e.Bps).HasColumnName("BPS");

                entity.Property(e => e.Min).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<PyPaymentSlip>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.NetAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PaymentMonthYearDay).HasColumnType("date");

                entity.Property(e => e.PaymentToDate).HasColumnType("date");

                entity.Property(e => e.PayrollType)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'M')");

                entity.Property(e => e.Remark).IsUnicode(false);

                entity.Property(e => e.SlipNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PyPayrollGeneration>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<PyPayrollGenerationDetail>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<PyPayrollGenerationMaster>(entity =>
            {
                entity.Property(e => e.FromDate).HasColumnType("date");

                entity.Property(e => e.PayrollTotalValue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PayrollType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ToDate).HasColumnType("date");

                entity.Property(e => e.Vno)
                    .HasColumnName("VNo")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<PySalaryHead>(entity =>
            {
                entity.Property(e => e.Operation)
                    .HasMaxLength(2)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PyStaffAttendance>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<PyTaxDeductionPattern>(entity =>
            {
                entity.Property(e => e.FixedTaxAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.IncomeTaxRatePer).HasColumnType("decimal(16, 2)");

                entity.Property(e => e.LowerLimit).HasColumnType("decimal(16, 2)");

                entity.Property(e => e.UpperLimit).HasColumnType("decimal(16, 2)");
            });

            modelBuilder.Entity<ResBed>(entity =>
            {
                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<ResFnsdCreditResetingHostory>(entity =>
            {
                entity.Property(e => e.CreditType).HasMaxLength(50);

                entity.Property(e => e.ResetingUpToDate).HasColumnType("date");
            });

            modelBuilder.Entity<ResFood>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CookingTimeHours).HasMaxLength(10);

                entity.Property(e => e.CookingTimeMin).HasMaxLength(10);

                entity.Property(e => e.ImageName).HasMaxLength(300);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsSpecial).HasDefaultValueSql("((0))");

                entity.Property(e => e.KitchenId).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.OfferEndDate).HasColumnType("date");

                entity.Property(e => e.OfferRate).HasColumnType("numeric(18, 1)");

                entity.Property(e => e.OfferStartDate).HasColumnType("date");

                entity.Property(e => e.Type)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("(N'Standard')");

                entity.Property(e => e.Vat).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<ResFoodAddons>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<ResFoodAvailability>(entity =>
            {
                entity.Property(e => e.AvailableDays).HasMaxLength(500);

                entity.Property(e => e.FoodIds).HasMaxLength(500);

                entity.Property(e => e.FromTime).HasMaxLength(50);

                entity.Property(e => e.ToTime).HasMaxLength(50);
            });

            modelBuilder.Entity<ResFoodCategory>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.ImageName).HasMaxLength(300);

                entity.Property(e => e.IsOffer).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.OfferEndDate).HasColumnType("date");

                entity.Property(e => e.OfferStartDate).HasColumnType("date");

                entity.Property(e => e.ParentCategoryId).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<ResFoodVariant>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsSalePriceOpen)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Variant).HasMaxLength(50);
            });

            modelBuilder.Entity<ResFoodVariantRecipe>(entity =>
            {
                entity.Property(e => e.Qty).HasColumnType("decimal(18, 6)");
            });

            modelBuilder.Entity<ResKipraHistory>(entity =>
            {
                entity.Property(e => e.InvoiceNo).HasMaxLength(50);

                entity.Property(e => e.KipraKey).HasMaxLength(50);

                entity.Property(e => e.Ntn)
                    .HasMaxLength(50)
                    .HasColumnName("NTN");

                entity.Property(e => e.Status).HasMaxLength(1);
            });

            modelBuilder.Entity<ResKitchen>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<ResOrderType>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.CreditType)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('None')");

                entity.Property(e => e.DefaultPaymentType).HasMaxLength(20);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.OrderTypeGroup).HasMaxLength(50);

                entity.Property(e => e.ReceiptFormate).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(200);
            });

            modelBuilder.Entity<ResPackageVarientsMapping>(entity =>
            {
                entity.Property(e => e.Qty).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<ResPatient>(entity =>
            {
                entity.Property(e => e.BedId).HasDefaultValueSql("((0))");

                entity.Property(e => e.PatientMrNo).HasMaxLength(50);

                entity.Property(e => e.PatientName).HasMaxLength(100);
            });

            modelBuilder.Entity<ResSaleClosing>(entity =>
            {
                entity.Property(e => e.Remarks).HasMaxLength(500);

                entity.Property(e => e.TellAmount).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<ResSaleClosingDetail>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PaymentType).HasMaxLength(20);
            });

            modelBuilder.Entity<ResSaleDetail>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Qty).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<ResSaleDetailTemp>(entity =>
            {
                entity.Property(e => e.Flage).HasMaxLength(50);

                entity.Property(e => e.Qty).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<ResSaleMaster>(entity =>
            {
                entity.Property(e => e.ApprovedBy).HasDefaultValueSql("((0))");

                entity.Property(e => e.BedId).HasDefaultValueSql("((0))");

                entity.Property(e => e.CancelledReason).HasMaxLength(300);

                entity.Property(e => e.Change).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CookingTimeHours).HasMaxLength(10);

                entity.Property(e => e.CookingTimeMin).HasMaxLength(10);

                entity.Property(e => e.CustomerContact).HasMaxLength(100);

                entity.Property(e => e.CustomerName).HasMaxLength(200);

                entity.Property(e => e.DiscountCalculated).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DiscountPercent).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DiscountRemarks).HasMaxLength(200);

                entity.Property(e => e.EmpCreditType).HasMaxLength(50);

                entity.Property(e => e.EmployeeId).HasDefaultValueSql("((0))");

                entity.Property(e => e.GrandTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.InvoiceTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OrderStatus).HasMaxLength(50);

                entity.Property(e => e.PaymentReceived).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PaymentType).HasMaxLength(20);

                entity.Property(e => e.Remarks).HasMaxLength(200);

                entity.Property(e => e.ServiceChargesCalculated).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.StatusFromKitchen)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("(N'No Response')");

                entity.Property(e => e.StudentNo).HasMaxLength(50);

                entity.Property(e => e.TaxCalculated).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.WaiterId).HasDefaultValueSql("((-1))");
            });

            modelBuilder.Entity<ResShift>(entity =>
            {
                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Shift).HasMaxLength(100);
            });

            modelBuilder.Entity<ResTable>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.ImagePath).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<ResVarientPricingByCustType>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<ResWard>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<SysControllesGroup>(entity =>
            {
                entity.Property(e => e.ActionLink).HasMaxLength(100);

                entity.Property(e => e.ControllGroupName).HasMaxLength(50);

                entity.Property(e => e.ControllerLink).HasMaxLength(100);

                entity.Property(e => e.Controlls).HasMaxLength(300);
            });

            modelBuilder.Entity<SysExecptionLogging>(entity =>
            {
                entity.Property(e => e.Action).HasMaxLength(100);

                entity.Property(e => e.Controller).HasMaxLength(100);

                entity.Property(e => e.Date).HasColumnName("date");
            });

            modelBuilder.Entity<SysFeature>(entity =>
            {
                entity.Property(e => e.Details).HasMaxLength(500);

                entity.Property(e => e.DisplayName).HasMaxLength(100);

                entity.Property(e => e.Feature).HasMaxLength(50);
            });

            modelBuilder.Entity<SysForm>(entity =>
            {
                entity.Property(e => e.FormName).HasMaxLength(100);

                entity.Property(e => e.FrmAction).HasMaxLength(100);

                entity.Property(e => e.FrmController).HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<SysHtml>(entity =>
            {
                entity.Property(e => e.Section).HasMaxLength(100);
            });

            modelBuilder.Entity<SysInvTypeWiseControll>(entity =>
            {
                entity.Property(e => e.InventoryType).HasMaxLength(50);
            });

            modelBuilder.Entity<SysLableContent>(entity =>
            {
                entity.Property(e => e.LableContentStr).HasMaxLength(100);

                entity.Property(e => e.LableName).HasMaxLength(50);

                entity.Property(e => e.Language).HasMaxLength(50);
            });

            modelBuilder.Entity<SysLayout>(entity =>
            {
                entity.Property(e => e.LayoutName).HasMaxLength(100);

                entity.Property(e => e.LayoutPath).HasMaxLength(500);
            });

            modelBuilder.Entity<SysModule>(entity =>
            {
                entity.Property(e => e.DisplayName).HasMaxLength(100);

                entity.Property(e => e.Icon).HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Module).HasMaxLength(100);
            });

            modelBuilder.Entity<SysMonthName>(entity =>
            {
                entity.Property(e => e.MonthName).HasMaxLength(50);
            });

            modelBuilder.Entity<SysOrgFormsMapping>(entity =>
            {
                entity.Property(e => e.FormLabelDesc).HasMaxLength(200);

                entity.Property(e => e.IsEnable)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<SysOrgModulesMapping>(entity =>
            {
                entity.Property(e => e.IsEnable)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModuleLabelDesc).HasMaxLength(200);
            });

            modelBuilder.Entity<SysSystem>(entity =>
            {
                entity.Property(e => e.DisplayName).HasMaxLength(100);

                entity.Property(e => e.Icon).HasMaxLength(100);

                entity.Property(e => e.Icon2).HasMaxLength(500);

                entity.Property(e => e.SysAction).HasMaxLength(100);

                entity.Property(e => e.SysController).HasMaxLength(100);

                entity.Property(e => e.SystemName).HasMaxLength(100);

                entity.Property(e => e.UserAuthGroupName).HasMaxLength(50);
            });

            modelBuilder.Entity<SysWeekDay>(entity =>
            {
                entity.Property(e => e.Day).HasMaxLength(50);
            });

            modelBuilder.Entity<Table1>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Table_1");

                entity.Property(e => e.Cetegory)
                    .HasMaxLength(100)
                    .HasColumnName("cetegory");

                entity.Property(e => e.Cost)
                    .HasColumnType("decimal(18, 6)")
                    .HasColumnName("cost");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");

                entity.Property(e => e.SalePrice)
                    .HasColumnType("decimal(18, 6)")
                    .HasColumnName("sale_price");

                entity.Property(e => e.Unit)
                    .HasMaxLength(50)
                    .HasColumnName("unit");
            });

            modelBuilder.Entity<UsrSystemUser>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.AuthorizationType)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('R')");

                entity.Property(e => e.DefBatchId).HasDefaultValueSql("((0))");

                entity.Property(e => e.DefClassId).HasDefaultValueSql("((0))");

                entity.Property(e => e.DefDateOfJoning)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DefDob)
                    .HasColumnType("date")
                    .HasColumnName("DefDOB")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DefGender)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("(N'Male')");

                entity.Property(e => e.DefInstitutionId).HasDefaultValueSql("((0))");

                entity.Property(e => e.DefJoiningSessionId).HasDefaultValueSql("((0))");

                entity.Property(e => e.DefNationality)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'Pakistani')");

                entity.Property(e => e.DefQualificationId).HasDefaultValueSql("((0))");

                entity.Property(e => e.DefReligionId).HasDefaultValueSql("((0))");

                entity.Property(e => e.DefSessionId).HasDefaultValueSql("((0))");

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.InvIsOpenTellLimitOpening)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsAllowForDiscount)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LoginType).HasMaxLength(20);

                entity.Property(e => e.UserRole).HasMaxLength(50);

                entity.Property(e => e.UserType).HasMaxLength(20);
            });

            modelBuilder.Entity<UsrUserFormsMapping>(entity =>
            {
                entity.Property(e => e.CanApprove).HasDefaultValueSql("((0))");

                entity.Property(e => e.CanCreate).HasDefaultValueSql("((0))");

                entity.Property(e => e.CanDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.CanUpdate).HasDefaultValueSql("((0))");

                entity.Property(e => e.CanView).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<UsrUserParmsMapping>(entity =>
            {
                entity.Property(e => e.ParmType).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
