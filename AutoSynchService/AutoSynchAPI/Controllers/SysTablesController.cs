using AutoSynchService.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoSynchAPI.Controllers
{

    [Route("api/SysTables")]
    [ApiController]
    public class SysTablesController : ControllerBase
    {
        private readonly ILogger<SysTablesController> _logger;

        public SysTablesController(ILogger<SysTablesController> logger)
        {
            _logger = logger;
        }
        [Route("GetSysTables")]
        [HttpGet]
        public IActionResult GetSysTables(string branch_id)
        {

            Models.SysTablesResponse responseObj = new Models.SysTablesResponse();
            int _branchId = 0;
            if (!string.IsNullOrEmpty(branch_id))
            {
                if (int.TryParse(branch_id, out _branchId))
                {
                    try
                    {
                        using (Entities dbContext = new Entities())
                        {//branch id in appsettings
                         //org//all
                         //inv//all
                         //usr//all
                         //accfiscalyear, 
                            responseObj.sysControllesGroups = dbContext.SysControllesGroup.ToList();
                            responseObj.sysExecptionLoggings = dbContext.SysExecptionLogging.ToList();
                            responseObj.sysFeatures = dbContext.SysFeature.ToList();
                            responseObj.sysOrgFormsMappings = dbContext.SysOrgFormsMapping.ToList();
                            responseObj.sysForms = dbContext.SysForm.ToList();
                            responseObj.sysOrgModulesMappings = dbContext.SysOrgModulesMapping.ToList();
                            responseObj.sysLayouts = dbContext.SysLayout.ToList();
                            responseObj.sysModules = dbContext.SysModule.ToList();
                            responseObj.sysModuleFormsMappings = dbContext.SysModuleFormsMapping.ToList();
                            responseObj.syssystems = dbContext.SysSystem.ToList();
                            responseObj.sysWeekDays = dbContext.SysWeekDay.ToList();
                            responseObj.sysMonthNames = dbContext.SysMonthName.ToList();
                            responseObj.sysYears = dbContext.SysYear.ToList();
                            responseObj.sysLableContents = dbContext.SysLableContent.ToList();
                            responseObj.sysHtmls = dbContext.SysHtml.ToList();
                            responseObj.sysInvTypeWiseControlls = dbContext.SysInvTypeWiseControll.ToList();
                            //Inv
                            responseObj.invCategorys = dbContext.InvCategory.ToList();
                            responseObj.invCompanys = dbContext.InvCompany.ToList();
                            responseObj.invCustomers = dbContext.InvCustomer.ToList();
                            responseObj.invCustomerTypes = dbContext.InvCustomerType.ToList();
                            responseObj.invDeliveryChallanDetails = dbContext.InvDeliveryChallanDetail.ToList();
                            responseObj.invDeliveryChallanMasters = dbContext.InvDeliveryChallanMaster.ToList();
                            responseObj.invDemandNotes = dbContext.InvDemandNote.ToList();
                            responseObj.invDemandNoteDetails = dbContext.InvDemandNoteDetail.ToList();
                            responseObj.invGatePassInDetails = dbContext.InvGatePassInDetail.ToList();
                            responseObj.invGatePassInMasters = dbContext.InvGatePassInMaster.ToList();
                            responseObj.invJcMonthSettings = dbContext.InvJcMonthSetting.ToList();
                            responseObj.invLocations = dbContext.InvLocation.ToList();
                            responseObj.invPackageProductsMappings = dbContext.InvPackageProductsMapping.ToList();
                            responseObj.invPaymentTypes = dbContext.InvPaymentType.ToList();
                            responseObj.invProducts = dbContext.InvProduct.ToList();
                            responseObj.invProductBatchs = dbContext.InvProductBatch.ToList();
                            responseObj.invProductionDetails = dbContext.InvProductionDetail.ToList();
                            responseObj.invProductionMasters = dbContext.InvProductionMaster.ToList();
                            responseObj.invProductLedgers = dbContext.InvProductLedger.ToList();
                            responseObj.invPurchaseDetails = dbContext.InvPurchaseDetail.ToList();
                            responseObj.invPurchaseMasters = dbContext.InvPurchaseMaster.ToList();
                            responseObj.invPurchaseOrderDetails = dbContext.InvPurchaseOrderDetail.ToList();
                            responseObj.invPurchaseOrderMasters = dbContext.InvPurchaseOrderMaster.ToList();
                            responseObj.invQuatationDetails = dbContext.InvQuatationDetail.ToList();
                            responseObj.invQuatationMasters = dbContext.InvQuatationMaster.ToList();
                            responseObj.invSaleClosings = dbContext.InvSaleClosing.ToList();
                            responseObj.invSaleClosingDetails = dbContext.InvSaleClosingDetail.ToList();
                            responseObj.invSaleDetails = dbContext.InvSaleDetail.ToList();
                            responseObj.invSalemanToRoutsMappings = dbContext.InvSalemanToRoutsMapping.ToList();
                            responseObj.invSaleMasters = dbContext.InvSaleMaster.ToList();
                            responseObj.invSchemeDetails = dbContext.InvSchemeDetail.ToList();
                            responseObj.invSchemeMasters = dbContext.InvSchemeMaster.ToList();
                            responseObj.invShifts = dbContext.InvShift.ToList();
                            responseObj.invStockAdjustments = dbContext.InvStockAdjustment.ToList();
                            responseObj.invStockAdjustmentDetails = dbContext.InvStockAdjustmentDetail.ToList();
                            responseObj.invStockTransfers = dbContext.InvStockTransfer.ToList();
                            responseObj.invStockTransferDetails = dbContext.InvStockTransferDetail.ToList();
                            responseObj.invThirdPartyCustomers = dbContext.InvThirdPartyCustomer.ToList();
                            responseObj.invUnits = dbContext.InvUnit.ToList();
                            responseObj.invVehicles = dbContext.InvVehicle.ToList();
                            responseObj.invVendors = dbContext.InvVendor.ToList();
                            responseObj.invWarehouses = dbContext.InvWarehouse.ToList();
                            //Usr
                            responseObj.UsrSystemUsers = dbContext.UsrSystemUser.ToList();
                            responseObj.UsrUserBranchesMappings = dbContext.UsrUserBranchesMapping.ToList();
                            responseObj.UsrUserFormsMappings = dbContext.UsrUserFormsMapping.ToList();
                            responseObj.UsrUserParmsMappings = dbContext.UsrUserParmsMapping.ToList();
                            //Org
                            responseObj.OrgBranchs = dbContext.OrgBranch.ToList();
                            responseObj.OrgFeaturesMappings = dbContext.OrgFeaturesMapping.ToList();
                            responseObj.OrgOrganizations = dbContext.OrgOrganization.ToList();
                            responseObj.OrgOrgSystemsMappings = dbContext.OrgOrgSystemsMapping.ToList();
                            //AccFiscalYear
                            responseObj.AccFiscalYears = dbContext.AccFiscalYear.ToList();
                            if (responseObj.sysLayouts != null)
                            {
                                //responseObj.Response.Code = ApplicationResponse.SUCCESS_CODE;
                                //responseObj.Response.Message = ApplicationResponse.SUCCESS_MESSAGE;
                                return Ok(responseObj);
                            }
                            else
                            {
                                //responseObj.Response.Code = ApplicationResponse.NOT_EXISTS_CODE;
                                //responseObj.Response.Message = ApplicationResponse.NOT_EXISTS_MESSAGE;
                                return NotFound(responseObj);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //responseObj.Response.Code = ApplicationResponse.GENERIC_ERROR_CODE;
                        //responseObj.Response.Message = ApplicationResponse.GENERIC_ERROR_MESSAGE;
                        return BadRequest(responseObj);
                    }
                }
                else
                {
                    //responseObj.Response.Code = ApplicationResponse.GENERIC_ERROR_CODE;
                    //responseObj.Response.Message = ApplicationResponse.GENERIC_ERROR_MESSAGE;
                    return BadRequest(responseObj);
                }
            }
            else
            {
                //responseObj.Response.Code = ApplicationResponse.GENERIC_ERROR_CODE;
                //responseObj.Response.Message = ApplicationResponse.GENERIC_ERROR_MESSAGE;
                return BadRequest(responseObj);
            }
        }
    }
}
