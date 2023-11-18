
using AutoSynchClientEngine.Classes;
using AutoSynchSqlServer.Models;

namespace AutoSynchClientEngine.Classes
{
    public class SysTablesResponse
    {
        public List<SysControllesGroup> sysControllesGroups { get; set; }
        public List<SysExecptionLogging> sysExecptionLoggings { get; set; }
        public List<SysFeature> sysFeatures { get; set; }
        public List<SysOrgFormsMapping> sysOrgFormsMappings { get; set; }
        public List<SysForm> sysForms { get; set; }
        public List<SysOrgModulesMapping> sysOrgModulesMappings { get; set; }
        public List<SysLayout> sysLayouts { get; set; }
        public List<SysModule> sysModules { get; set; }
        public List<SysModuleFormsMapping> sysModuleFormsMappings { get; set; }
        public List<SysSystem> syssystems { get; set; }
        public List<SysWeekDay> sysWeekDays { get; set; }
        public List<SysMonthName> sysMonthNames { get; set; }
        public List<SysYear> sysYears { get; set; }
        public List<SysLableContent> sysLableContents { get; set; }
        public List<SysHtml> sysHtmls { get; set; }
        public List<SysInvTypeWiseControll> sysInvTypeWiseControlls { get; set; }
        //Inv
        public List<InvCategory> invCategorys { get; set; }
        public List<InvCompany> invCompanys { get; set; }
        public List<InvCustomer> invCustomers { get; set; }
        public List<InvCustomerType> invCustomerTypes { get; set; }
        //public List<InvDeliveryChallanDetail> invDeliveryChallanDetails { get; set; }
        //public List<InvDeliveryChallanMaster> invDeliveryChallanMasters { get; set; }
        public List<InvDemandNote> invDemandNotes { get; set; }
        public List<InvDemandNoteDetail> invDemandNoteDetails { get; set; }
        public List<InvGatePassInDetail> invGatePassInDetails { get; set; }
        public List<InvGatePassInMaster> invGatePassInMasters { get; set; }
        public List<InvJcMonthSetting> invJcMonthSettings { get; set; }
        public List<InvLocation> invLocations { get; set; }
        public List<InvPackageProductsMapping> invPackageProductsMappings { get; set; }
        public List<InvPaymentType> invPaymentTypes { get; set; }
        public List<InvProductBatch> invProductBatchs { get; set; }
        public List<InvProductionDetail> invProductionDetails { get; set; }
        public List<InvProductionMaster> invProductionMasters { get; set; }
        public List<InvPurchaseDetail> invPurchaseDetails { get; set; }
        public List<InvPurchaseMaster> invPurchaseMasters { get; set; }
        public List<InvPurchaseOrderDetail> invPurchaseOrderDetails { get; set; }
        public List<InvPurchaseOrderMaster> invPurchaseOrderMasters { get; set; }
        public List<InvQuatationDetail> invQuatationDetails { get; set; }
        public List<InvQuatationMaster> invQuatationMasters { get; set; }
        public List<InvSaleClosing> invSaleClosings { get; set; }
        public List<InvSaleClosingDetail> invSaleClosingDetails { get; set; }
        public List<InvSaleDetail> invSaleDetails { get; set; }
        public List<InvSalemanToRoutsMapping> invSalemanToRoutsMappings { get; set; }
        public List<InvSaleMaster> invSaleMasters { get; set; }
        public List<InvSchemeDetail> invSchemeDetails { get; set; }
        public List<InvSchemeMaster> invSchemeMasters { get; set; }
        public List<InvShift> invShifts { get; set; }
        public List<InvStockAdjustment> invStockAdjustments { get; set; }
        public List<InvStockAdjustmentDetail> invStockAdjustmentDetails { get; set; }
        public List<InvStockTransfer> invStockTransfers { get; set; }
        public List<InvStockTransferDetail> invStockTransferDetails { get; set; }
        public List<InvThirdPartyCustomer> invThirdPartyCustomers { get; set; }
        public List<InvUnit> invUnits { get; set; }
        public List<InvVehicle> invVehicles { get; set; }
        public List<InvVendor> invVendors { get; set; }
        public List<InvWarehouse> invWarehouses { get; set; }

        //Usr
        public List<UsrSystemUser> UsrSystemUsers { get; set; }
        public List<UsrUserBranchesMapping> UsrUserBranchesMappings { get; set; }
        public List<UsrUserFormsMapping> UsrUserFormsMappings { get; set; }
        public List<UsrUserParmsMapping> UsrUserParmsMappings { get; set; }
        //Org
        public List<OrgBranch> OrgBranchs { get; set; }
        public List<OrgFeaturesMapping> OrgFeaturesMappings { get; set; }
        public List<OrgOrganization> OrgOrganizations { get; set; }
        public List<OrgOrgSystemsMapping> OrgOrgSystemsMappings { get; set; }
        //AccFiscalYear
        public List<AccFiscalYear> AccFiscalYears { get; set; }
        public SysTablesResponse()
        {
            sysControllesGroups = new List<SysControllesGroup>();
            sysExecptionLoggings = new List<SysExecptionLogging>();
            sysFeatures = new List<SysFeature>();
            sysOrgFormsMappings = new List<SysOrgFormsMapping>();
            sysForms = new List<SysForm>();
            sysOrgModulesMappings = new List<SysOrgModulesMapping>();
            sysLayouts = new List<SysLayout>();
            sysModules = new List<SysModule>();
            sysModuleFormsMappings = new List<SysModuleFormsMapping>();
            syssystems = new List<SysSystem>();
            sysWeekDays = new List<SysWeekDay>();
            sysMonthNames = new List<SysMonthName>();
            sysYears = new List<SysYear>();
            sysLableContents = new List<SysLableContent>();
            sysHtmls = new List<SysHtml>();
            sysInvTypeWiseControlls = new List<SysInvTypeWiseControll>();
            //inv
            invCategorys = new List<InvCategory>();
            invCompanys = new List<InvCompany>();
            invCustomers = new List<InvCustomer>();
            invCustomerTypes = new List<InvCustomerType>();
            //invDeliveryChallanDetails = new List<InvDeliveryChallanDetail>();
            //invDeliveryChallanMasters = new List<InvDeliveryChallanMaster>();
            invDemandNotes = new List<InvDemandNote>();
            invDemandNoteDetails = new List<InvDemandNoteDetail>();
            invGatePassInDetails = new List<InvGatePassInDetail>();
            invGatePassInMasters = new List<InvGatePassInMaster>();
            invJcMonthSettings = new List<InvJcMonthSetting>();
            invLocations = new List<InvLocation>();
            invPackageProductsMappings = new List<InvPackageProductsMapping>();
            invPaymentTypes = new List<InvPaymentType>();
            invProductBatchs = new List<InvProductBatch>();
            invProductionDetails = new List<InvProductionDetail>();
            invProductionMasters = new List<InvProductionMaster>();
            invPurchaseDetails = new List<InvPurchaseDetail>();
            invPurchaseMasters = new List<InvPurchaseMaster>();
            invPurchaseOrderDetails = new List<InvPurchaseOrderDetail>();
            invPurchaseOrderMasters = new List<InvPurchaseOrderMaster>();
            invQuatationDetails = new List<InvQuatationDetail>();
            invQuatationMasters = new List<InvQuatationMaster>();
            invSaleClosings = new List<InvSaleClosing>();
            invSaleClosingDetails = new List<InvSaleClosingDetail>();
            invSaleDetails = new List<InvSaleDetail>();
            invSalemanToRoutsMappings = new List<InvSalemanToRoutsMapping>();
            invSaleMasters = new List<InvSaleMaster>();
            invSchemeDetails = new List<InvSchemeDetail>();
            invSchemeMasters = new List<InvSchemeMaster>();
            invShifts = new List<InvShift>();
            invStockAdjustments = new List<InvStockAdjustment>();
            invStockAdjustmentDetails = new List<InvStockAdjustmentDetail>();
            invStockTransfers = new List<InvStockTransfer>();
            invStockTransferDetails = new List<InvStockTransferDetail>();
            invThirdPartyCustomers = new List<InvThirdPartyCustomer>();
            invUnits = new List<InvUnit>();
            invVehicles = new List<InvVehicle>();
            invVendors = new List<InvVendor>();
            invWarehouses = new List<InvWarehouse>();

            //Usr
            UsrSystemUsers = new List<UsrSystemUser>();
            UsrUserBranchesMappings = new List<UsrUserBranchesMapping>();
            UsrUserFormsMappings = new List<UsrUserFormsMapping>();
            UsrUserParmsMappings = new List<UsrUserParmsMapping>();

            //Org
            OrgBranchs = new List<OrgBranch>();
            OrgFeaturesMappings = new List<OrgFeaturesMapping>();
            OrgOrganizations = new List<OrgOrganization>();
            OrgOrgSystemsMappings = new List<OrgOrgSystemsMapping>();

            //AccFiscalYear
            AccFiscalYears = new List<AccFiscalYear>();
        }
    }
    public class InvProductsResponse
    {
        public ApiResponse Response { get; set; }
        public List<InvProduct> invProducts { get; set; }
        public List<InvVendor> invVendors { get; set; }
        public InvProductsResponse()
        {
            Response = new ApiResponse();
            invProducts = new List<InvProduct>();
            invVendors=new List<InvVendor>();
        }
    }
}
