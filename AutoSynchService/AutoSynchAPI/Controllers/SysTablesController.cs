using AutoSynchAPI.Classes;
using AutoSynchAPI.Models;
using AutoSynchSqlServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        [Route("GetTableData")]
        [HttpGet]
        public IActionResult GetTableData(string branch_id, string synch_type, string table_list)
        {
            SynchTypes synchType = SynchTypes.full;
            Enum.TryParse(synch_type, out synchType);

            Models.SysTablesResponse responseObj = new Models.SysTablesResponse();
            int _branchId = 0;
            if (!string.IsNullOrEmpty(branch_id))
            {
                if (int.TryParse(branch_id, out _branchId))
                {
                    try
                    {
                        using (Entities dbContext = new Entities())
                        {
                            if (synchType == SynchTypes.full || synchType==SynchTypes.except_product_sale_master_detail_tables)
                            {
                                responseObj.sysControllesGroups = dbContext.SysControllesGroup.ToList();
                                //responseObj.sysExecptionLoggings = dbContext.SysExecptionLogging.ToList();
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
                                responseObj.sysInvTypeWiseControlls = dbContext.SysInvTypeWiseControll.ToList();
                                responseObj.sysHtmls = dbContext.SysHtml.Where(g => g.BranchId == _branchId).ToList();
                                
                               
                                //var invProducts = dbContext.InvProduct.Where(g => g.BranchId == _branchId);
                                //foreach (var item in invProducts)
                                //{
                                //    responseObj.invProducts.Add(item);
                                //}
                                responseObj.invProductBatchs = dbContext.InvProductBatch.ToList();
                                //responseObj.invProductLedgers = dbContext.InvProductLedger.Where(g => g.BranchId == _branchId).ToList();
                                responseObj.invSalemanToRoutsMappings = dbContext.InvSalemanToRoutsMapping.ToList();
                                

                                //Usr
                                responseObj.UsrSystemUsers = dbContext.UsrSystemUser.ToList();
                                responseObj.UsrUserFormsMappings = dbContext.UsrUserFormsMapping.ToList();
                                responseObj.invJcMonthSettings = dbContext.InvJcMonthSetting.ToList();
                                if (table_list == "IsBranchFilterY")
                                { //Inv
                                    responseObj.invCategorys = dbContext.InvCategory.Where(g => g.BranchId == _branchId).ToList();
                                    responseObj.invCompanys = dbContext.InvCompany.Where(g => g.BranchId == _branchId).ToList();
                                    responseObj.invCustomers = dbContext.InvCustomer.Where(g => g.BranchId == _branchId).ToList();
                                    responseObj.invCustomerTypes = dbContext.InvCustomerType.Where(g => g.BranchId == _branchId).ToList();
                                    responseObj.invLocations = dbContext.InvLocation.Where(g => g.BranchId == _branchId).ToList();
                                    responseObj.invPackageProductsMappings = dbContext.InvPackageProductsMapping.ToList();
                                    responseObj.invPaymentTypes = dbContext.InvPaymentType.Where(g => g.BranchId == _branchId).ToList();
                                    responseObj.invShifts = dbContext.InvShift.Where(g => g.BranchId == _branchId).ToList();
                                    responseObj.invThirdPartyCustomers = dbContext.InvThirdPartyCustomer.Where(g => g.BranchId == _branchId).ToList();
                                    responseObj.invUnits = dbContext.InvUnit.Where(g => g.BranchId == _branchId).ToList();
                                    responseObj.invVehicles = dbContext.InvVehicle.Where(g => g.BranchId == _branchId).ToList();
                                    responseObj.invVendors = dbContext.InvVendor.Where(g => g.BranchId == _branchId).ToList();
                                    responseObj.invWarehouses = dbContext.InvWarehouse.Where(g => g.BranchId == _branchId).ToList();
                                    //Usr
                                    responseObj.UsrUserBranchesMappings = dbContext.UsrUserBranchesMapping.Where(g => g.BranchId == _branchId).ToList();
                                    
                                    responseObj.UsrUserParmsMappings = dbContext.UsrUserParmsMapping.Where(g => g.BranchId == _branchId).ToList();
                                    //Org
                                    responseObj.OrgBranchs = dbContext.OrgBranch.Where(g => g.Id == _branchId).ToList();
                                   
                                    //AccFiscalYear
                                    responseObj.AccFiscalYears = dbContext.AccFiscalYear.Where(g => g.BranchId == _branchId).ToList();
                                }
                                else
                                { //Inv
                                    responseObj.invCategorys = dbContext.InvCategory.ToList();
                                    responseObj.invCompanys = dbContext.InvCompany.ToList();
                                    responseObj.invCustomers = dbContext.InvCustomer.ToList();
                                    responseObj.invCustomerTypes = dbContext.InvCustomerType.ToList();
                                    responseObj.invLocations = dbContext.InvLocation.ToList();
                                    responseObj.invPackageProductsMappings = dbContext.InvPackageProductsMapping.ToList();
                                    responseObj.invPaymentTypes = dbContext.InvPaymentType.ToList();
                                    responseObj.invShifts = dbContext.InvShift.ToList();
                                    responseObj.invThirdPartyCustomers = dbContext.InvThirdPartyCustomer.ToList();
                                    responseObj.invUnits = dbContext.InvUnit.ToList();
                                    responseObj.invVehicles = dbContext.InvVehicle.ToList();
                                    responseObj.invVendors = dbContext.InvVendor.ToList();
                                    responseObj.invWarehouses = dbContext.InvWarehouse.ToList();
                                    //Usr
                                    responseObj.UsrUserBranchesMappings = dbContext.UsrUserBranchesMapping.ToList();
                                    responseObj.UsrUserParmsMappings = dbContext.UsrUserParmsMapping.ToList();
                                    //Org
                                    responseObj.OrgBranchs = dbContext.OrgBranch.ToList();
                                    //AccFiscalYear
                                    responseObj.AccFiscalYears = dbContext.AccFiscalYear.ToList();
                                }
                                //Org
                                responseObj.OrgFeaturesMappings = dbContext.OrgFeaturesMapping.ToList();
                                responseObj.OrgOrganizations = dbContext.OrgOrganization.ToList();
                                responseObj.OrgOrgSystemsMappings = dbContext.OrgOrgSystemsMapping.ToList();



                                //responseObj.invDeliveryChallanDetails = dbContext.InvDeliveryChallanDetail.ToList();
                                //responseObj.invDeliveryChallanMasters = dbContext.InvDeliveryChallanMaster.ToList();
                                //responseObj.invDemandNotes = dbContext.InvDemandNote.ToList();
                                //responseObj.invDemandNoteDetails = dbContext.InvDemandNoteDetail.ToList();
                                //responseObj.invGatePassInDetails = dbContext.InvGatePassInDetail.ToList();
                                //responseObj.invGatePassInMasters = dbContext.InvGatePassInMaster.ToList();
                                //responseObj.invProductionDetails = dbContext.InvProductionDetail.ToList();
                                //responseObj.invProductionMasters = dbContext.InvProductionMaster.ToList();
                                //responseObj.invPurchaseDetails = dbContext.InvPurchaseDetail.ToList();
                                //responseObj.invPurchaseMasters = dbContext.InvPurchaseMaster.ToList();
                                //responseObj.invPurchaseOrderDetails = dbContext.InvPurchaseOrderDetail.ToList();
                                //responseObj.invPurchaseOrderMasters = dbContext.InvPurchaseOrderMaster.ToList();
                                //responseObj.invQuatationDetails = dbContext.InvQuatationDetail.ToList();
                                //responseObj.invQuatationMasters = dbContext.InvQuatationMaster.ToList();
                                //responseObj.invSaleClosings = dbContext.InvSaleClosing.ToList();
                                //responseObj.invSaleClosingDetails = dbContext.InvSaleClosingDetail.ToList();
                                //responseObj.invSaleDetails = dbContext.InvSaleDetail.ToList();
                                //responseObj.invSaleMasters = dbContext.InvSaleMaster.ToList();
                                //responseObj.invSchemeDetails = dbContext.InvSchemeDetail.ToList();
                                //responseObj.invSchemeMasters = dbContext.InvSchemeMaster.ToList();
                                //responseObj.invStockAdjustments = dbContext.InvStockAdjustment.ToList();
                                //responseObj.invStockAdjustmentDetails = dbContext.InvStockAdjustmentDetail.ToList();
                                //responseObj.invStockTransfers = dbContext.InvStockTransfer.ToList();
                                //responseObj.invStockTransferDetails = dbContext.InvStockTransferDetail.ToList();
                            }
                            else if (synchType == SynchTypes.only_sys_tables)
                            {
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
                                responseObj.sysHtmls = dbContext.SysHtml.Where(g => g.BranchId == _branchId).ToList();
                                responseObj.sysInvTypeWiseControlls = dbContext.SysInvTypeWiseControll.ToList();
                              
                            }

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
        [Route("GetProducts")]
        [HttpGet]
        public IActionResult GetProducts(string branch_id, string max_prod_id,string records_to_fetch,string is_quick)//,string product_ledger
        {
           
            Models.InvProductsResponse responseObj = new Models.InvProductsResponse();
            int _branchId = 0;
            if (!string.IsNullOrEmpty(branch_id))
            {
                if (int.TryParse(branch_id, out _branchId))
                {
                    try
                    {
                        using (Entities dbContext = new Entities())
                        {
                            int prodId = 0;
                            if(string.IsNullOrEmpty(max_prod_id))
                                prodId = -1;
                            if (prodId==-1 || int.TryParse(max_prod_id, out prodId))
                            {
                                int recordsToFetch = 1000;
                                int.TryParse(records_to_fetch, out recordsToFetch);
                                //if(product_ledger.Equals("true"))
                                //{
                                //    if (prodId <= 0)
                                //    {
                                //        prodId = dbContext.InvProductLedger.Where(g => g.BranchId == _branchId).Min(m => m.Id);

                                //    }
                                //    responseObj.invProductLedgers = dbContext.InvProductLedger.Where(g => g.BranchId == _branchId && g.Id > prodId && g.Id <= prodId + 1000).ToList();
                                //}
                                //else
                                {
                                    if (prodId < 0)
                                    {
                                        responseObj.invProducts = dbContext.InvProduct.Where(g => g.BranchId == _branchId && g.IsSynch == true).Take(recordsToFetch).ToList();

                                    }
                                    else
                                    {
                                        if (is_quick == "t")
                                        {
                                            if (prodId == 0)
                                            {
                                                prodId = dbContext.InvProduct.Where(g => g.BranchId == _branchId).Min(m => m.Id);
                                            }
                                            responseObj.invProducts = dbContext.InvProduct.Where(g => g.BranchId == _branchId && g.Id > prodId).Take(recordsToFetch).ToList();

                                        }
                                        else
                                        {
                                            if (prodId == 0)
                                            {
                                                prodId = dbContext.InvProduct.Where(g => g.BranchId == _branchId && g.IsSynch == true).Min(m => m.Id);
                                            }
                                            responseObj.invProducts = dbContext.InvProduct.Where(g => g.BranchId == _branchId && g.IsSynch == true && g.Id > prodId).Take(recordsToFetch).ToList();

                                        }

                                    }
                                   
                                    //responseObj.invProducts.ForEach(p => p.IsSynch = false);
                                    //dbContext.SaveChanges();


                                }
                                //foreach (var item in invProducts)
                                //{
                                //    responseObj.invProducts.Add(item);
                                //}
                            }
                            if (responseObj.invProducts != null)
                            {
                                if(responseObj.invProducts.Count > 0)
                                {
                                    responseObj.Response.Code = ApplicationResponse.SUCCESS_CODE;
                                    responseObj.Response.Message = ApplicationResponse.SUCCESS_MESSAGE;
                                    return Ok(responseObj);
                                }
                                else
                                {
                                    if (dbContext.InvProduct.Where(g => g.BranchId == _branchId).Max(m => m.Id) <= prodId)
                                    {
                                        responseObj.Response.Code = ApplicationResponse.MAX_REACHED_CODE;
                                        responseObj.Response.Message = ApplicationResponse.MAX_REACHED_MESSAGE;
                                        return Ok(responseObj);
                                    }
                                    //else if (product_ledger.Equals("true") && dbContext.InvProductLedger.Where(g => g.BranchId == _branchId).Max(m => m.Id) <= prodId)
                                    //{
                                    //    responseObj.Response.Code = ApplicationResponse.MAX_REACHED_CODE;
                                    //    responseObj.Response.Message = ApplicationResponse.MAX_REACHED_MESSAGE;
                                    //    return Ok(responseObj);
                                    //}
                                    else
                                    return Ok(responseObj);
                                }
                                
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
        [Route("PostUpdatedProducts")]
        [HttpPost]
        public IActionResult PostUpdatedProducts([FromBody] UpdateProductFlag updateResponse)
        {
            ApiResponse apiResponse = new ApiResponse();
            if (!string.IsNullOrEmpty(updateResponse.BranchId))
            {
                int _branchId = 0;
                if (int.TryParse(updateResponse.BranchId, out _branchId) && updateResponse.updatedProducts!=null && updateResponse.updatedProducts.Count>0)
                {
                    try
                    {
                        using (Entities dbContext = new Entities())
                        {
                            using (var transaction = dbContext.Database.BeginTransaction())
                            {
                                List<int> prodIds = updateResponse.updatedProducts.Select(s => s.ProductId).ToList();
                                var ProductsToUpdate = dbContext.InvProduct.Where(p => p.BranchId == _branchId && p.IsSynch==true && prodIds.Contains(p.Id));
                                foreach (var item in ProductsToUpdate)
                                {
                                    item.IsSynch = false;
                                }
                                try
                                {
                                    dbContext.SaveChanges();
                                    transaction.Commit();
                                }
                                catch (Exception ex)
                                {
                                    transaction.Rollback();

                                    throw ex;
                                }
                            }
                            // dbContext.InvSaleMaster.AddRan
                        }

                        apiResponse.Code = ApplicationResponse.SUCCESS_CODE;
                        apiResponse.Message = ApplicationResponse.SUCCESS_MESSAGE;
                        return Ok(apiResponse);
                    }
                    catch (Exception ex)
                    {
                        apiResponse.Code = ApplicationResponse.GENERIC_ERROR_CODE;
                        apiResponse.Message = ex.Message;
                        return BadRequest(apiResponse);
                    }
                }
                else
                {
                    //responseObj.Response.Code = ApplicationResponse.GENERIC_ERROR_CODE;
                    //responseObj.Response.Message = ApplicationResponse.GENERIC_ERROR_MESSAGE;
                    return BadRequest(apiResponse);
                }
            }
            else
            {
                //responseObj.Response.Code = ApplicationResponse.GENERIC_ERROR_CODE;
                //responseObj.Response.Message = ApplicationResponse.GENERIC_ERROR_MESSAGE;
                return BadRequest(apiResponse);
            }
        }

        [Route("GetTableStructure")]
        [HttpGet]
        public IActionResult GetTableStructure(string branch_id,string synch_type,string table_list,string local_db)
        {
            SynchTypes synchType = SynchTypes.full;
            Enum.TryParse(synch_type, out synchType);

            Models.TableStructureResponse responseObj = new Models.TableStructureResponse();
            try
            {
                List<string> SynchTbls = new List<string>();
                if (synchType == SynchTypes.full)
                {
                    using (Entities dbContext = new Entities())
                    {
                        //dbContext.InvSaleMaster
                        //List<string> lstTables = new List<string>();
                        //lstTables.Add()

                        SynchTbls = dbContext.Model.GetEntityTypes().Where(et=>et.GetTableName()!="Timestamp" && et.GetTableName() != "Sequence").Select(et => et.GetTableName()).ToList();

                        //lstTables.ForEach(table =>
                        //{
                        //    SynchTbls.Add(getTableName(table));
                        //});
                    }
                        //SynchTbls.Add("SysControllesGroup");
                        //SynchTbls.Add("SysExecptionLogging");
                        //SynchTbls.Add("SysFeature");
                        //SynchTbls.Add("SysOrgFormsMapping");
                        //SynchTbls.Add("SysForm");
                        //SynchTbls.Add("SysOrgModulesMapping");
                        //SynchTbls.Add("SysLayout");
                        //SynchTbls.Add("SysModule");
                        //SynchTbls.Add("SysModuleFormsMapping");
                        //SynchTbls.Add("SysSystem");
                        //SynchTbls.Add("SysWeekDay");
                        //SynchTbls.Add("SysMonthName");
                        //SynchTbls.Add("SysYear");
                        //SynchTbls.Add("SysLableContent");
                        //SynchTbls.Add("SysHtml");
                        //SynchTbls.Add("SysInvTypeWiseControll");
                        //SynchTbls.Add("InvCategory");
                        //SynchTbls.Add("InvCompany");
                        //SynchTbls.Add("InvCustomer");
                        //SynchTbls.Add("InvCustomerType");
                        //SynchTbls.Add("InvDeliveryChallanDetail");
                        //SynchTbls.Add("InvDeliveryChallanMaster");
                        //SynchTbls.Add("InvDemandNote");
                        //SynchTbls.Add("InvDemandNoteDetail");
                        //SynchTbls.Add("InvGatePassInDetail");
                        //SynchTbls.Add("InvGatePassInMaster");
                        //SynchTbls.Add("InvJcMonthSetting");
                        //SynchTbls.Add("InvLocation");
                        //SynchTbls.Add("InvPackageProductsMapping");
                        //SynchTbls.Add("InvPaymentType");
                        //SynchTbls.Add("InvProduct");
                        //SynchTbls.Add("InvProductBatch");
                        //SynchTbls.Add("InvProductionDetail");
                        //SynchTbls.Add("InvProductionMaster");
                        //SynchTbls.Add("InvProductLedger");
                        //SynchTbls.Add("InvPurchaseDetail");
                        //SynchTbls.Add("InvPurchaseMaster");
                        //SynchTbls.Add("InvPurchaseOrderDetail");
                        //SynchTbls.Add("InvPurchaseOrderMaster");
                        //SynchTbls.Add("InvQuatationDetail");
                        //SynchTbls.Add("InvQuatationMaster");
                        //SynchTbls.Add("InvSaleClosing");
                        //SynchTbls.Add("InvSaleClosingDetail");
                        //SynchTbls.Add("InvSaleDetail");
                        //SynchTbls.Add("InvSalemanToRoutsMapping");
                        //SynchTbls.Add("InvSaleMaster");
                        //SynchTbls.Add("InvSchemeDetail");
                        //SynchTbls.Add("InvSchemeMaster");
                        //SynchTbls.Add("InvShift");
                        //SynchTbls.Add("InvStockAdjustment");
                        //SynchTbls.Add("InvStockAdjustmentDetail");
                        //SynchTbls.Add("InvStockTransfer");
                        //SynchTbls.Add("InvStockTransferDetail");
                        //SynchTbls.Add("InvThirdPartyCustomer");
                        //SynchTbls.Add("InvUnit");
                        //SynchTbls.Add("InvVehicle");
                        //SynchTbls.Add("InvVendor");
                        //SynchTbls.Add("InvWarehouse");
                        //SynchTbls.Add("UsrSystemUser");
                        //SynchTbls.Add("UsrUserBranchesMapping");
                        //SynchTbls.Add("UsrUserFormsMapping");
                        //SynchTbls.Add("UsrUserParmsMapping");
                        //SynchTbls.Add("OrgBranch");
                        //SynchTbls.Add("OrgFeaturesMapping");
                        //SynchTbls.Add("OrgOrganization");
                        //SynchTbls.Add("OrgOrgSystemsMapping");
                        //SynchTbls.Add("AccFiscalYear");

                    }
                else if (synchType == SynchTypes.only_sys_tables)
                {
                    SynchTbls.Add("SysControllesGroup");
                    SynchTbls.Add("SysExecptionLogging");
                    SynchTbls.Add("SysFeature");
                    SynchTbls.Add("SysOrgFormsMapping");
                    SynchTbls.Add("SysForm");
                    SynchTbls.Add("SysOrgModulesMapping");
                    SynchTbls.Add("SysLayout");
                    SynchTbls.Add("SysModule");
                    SynchTbls.Add("SysModuleFormsMapping");
                    SynchTbls.Add("SysSystem");
                    SynchTbls.Add("SysWeekDay");
                    SynchTbls.Add("SysMonthName");
                    SynchTbls.Add("SysYear");
                    SynchTbls.Add("SysLableContent");
                    SynchTbls.Add("SysHtml");
                    SynchTbls.Add("SysInvTypeWiseControll");
                }

                else if (synchType == SynchTypes.only_sale_master_detail_tables)
                {
                    SynchTbls.Add("InvSaleDetail");
                    SynchTbls.Add("InvSaleMaster");
                }
                else if (synchType == SynchTypes.except_sale_master_detail_tables)
                {

                    SynchTbls.Add("SysControllesGroup");
                    SynchTbls.Add("SysExecptionLogging");
                    SynchTbls.Add("SysFeature");
                    SynchTbls.Add("SysOrgFormsMapping");
                    SynchTbls.Add("SysForm");
                    SynchTbls.Add("SysOrgModulesMapping");
                    SynchTbls.Add("SysLayout");
                    SynchTbls.Add("SysModule");
                    SynchTbls.Add("SysModuleFormsMapping");
                    SynchTbls.Add("SysSystem");
                    SynchTbls.Add("SysWeekDay");
                    SynchTbls.Add("SysMonthName");
                    SynchTbls.Add("SysYear");
                    SynchTbls.Add("SysLableContent");
                    SynchTbls.Add("SysHtml");
                    SynchTbls.Add("SysInvTypeWiseControll");
                    SynchTbls.Add("InvCategory");
                    SynchTbls.Add("InvCompany");
                    SynchTbls.Add("InvCustomer");
                    SynchTbls.Add("InvCustomerType");
                    SynchTbls.Add("InvDeliveryChallanDetail");
                    SynchTbls.Add("InvDeliveryChallanMaster");
                    SynchTbls.Add("InvDemandNote");
                    SynchTbls.Add("InvDemandNoteDetail");
                    SynchTbls.Add("InvGatePassInDetail");
                    SynchTbls.Add("InvGatePassInMaster");
                    SynchTbls.Add("InvJcMonthSetting");
                    SynchTbls.Add("InvLocation");
                    SynchTbls.Add("InvPackageProductsMapping");
                    SynchTbls.Add("InvPaymentType");
                    SynchTbls.Add("InvProduct");
                    SynchTbls.Add("InvProductBatch");
                    SynchTbls.Add("InvProductionDetail");
                    SynchTbls.Add("InvProductionMaster");
                    SynchTbls.Add("InvProductLedger");
                    SynchTbls.Add("InvPurchaseDetail");
                    SynchTbls.Add("InvPurchaseMaster");
                    SynchTbls.Add("InvPurchaseOrderDetail");
                    SynchTbls.Add("InvPurchaseOrderMaster");
                    SynchTbls.Add("InvQuatationDetail");
                    SynchTbls.Add("InvQuatationMaster");
                    SynchTbls.Add("InvSaleClosing");
                    SynchTbls.Add("InvSaleClosingDetail");
                    //SynchTbls.Add("InvSaleDetail");
                    SynchTbls.Add("InvSalemanToRoutsMapping");
                    //SynchTbls.Add("InvSaleMaster");
                    SynchTbls.Add("InvSchemeDetail");
                    SynchTbls.Add("InvSchemeMaster");
                    SynchTbls.Add("InvShift");
                    SynchTbls.Add("InvStockAdjustment");
                    SynchTbls.Add("InvStockAdjustmentDetail");
                    SynchTbls.Add("InvStockTransfer");
                    SynchTbls.Add("InvStockTransferDetail");
                    SynchTbls.Add("InvThirdPartyCustomer");
                    SynchTbls.Add("InvUnit");
                    SynchTbls.Add("InvVehicle");
                    SynchTbls.Add("InvVendor");
                    SynchTbls.Add("InvWarehouse");
                    SynchTbls.Add("UsrSystemUser");
                    SynchTbls.Add("UsrUserBranchesMapping");
                    SynchTbls.Add("UsrUserFormsMapping");
                    SynchTbls.Add("UsrUserParmsMapping");
                    SynchTbls.Add("OrgBranch");
                    SynchTbls.Add("OrgFeaturesMapping");
                    SynchTbls.Add("OrgOrganization");
                    SynchTbls.Add("OrgOrgSystemsMapping");
                    SynchTbls.Add("AccFiscalYear");
                }
                else if (synchType == SynchTypes.custom)
                {
                    if (!string.IsNullOrEmpty(table_list))
                        SynchTbls = table_list.Split(',').ToList();
                    else
                    {
                        return BadRequest(responseObj);
                    }
                }
                using (Entities dbContext = new Entities())
                {
                    //dbContext.InvSaleMaster
                    //List<string> lstTables = new List<string>();
                    //lstTables.Add()

                    var lstTables = dbContext.Model.GetEntityTypes().Where(et => SynchTbls.Contains(et.GetTableName())).ToList();

                    //lstTables.ForEach(table =>
                    //{
                    //    responseObj.dropQueries.Add("DROP TABLE IF EXISTS " + getTableName(table.GetTableName(), local_db));
                    //});

                    string qry = string.Empty;
                    switch (local_db)
                    {
                        case Constants.Sqlite:
                            {
                                lstTables.ForEach(table =>
                                {
                                    qry = "create table " + getTableName(table.GetTableName(),local_db) + "(";
                                    var columns = table.GetProperties().ToList();
                                    string cols = string.Empty;
                                    foreach (var column in columns)
                                    {//column.IsNullable
                                        cols += column.Name + " " + ReturnColumnType(column.GetColumnType()) + (column.IsPrimaryKey() ? " primary key" : "") + ",";
                                    }
                                    if (getTableName(table.GetTableName(), local_db).ToLower().Equals("invsalemastertmp"))
                                    {
                                        qry += cols + " IsUploaded bit default 0 " + ")";
                                    }
                                    else
                                        qry += cols.TrimEnd(',') + ")";
                                    responseObj.createQueries.Add(qry);
                                });
                            }
                            break;
                        case Constants.SqlServer:
                            {
                                lstTables.ForEach(table =>
                                {
                                    qry = "create table " + getTableName(table.GetTableName(),local_db) + "(";
                                    
                                    var columns = table.GetProperties().ToList();
                                    string cols = string.Empty;
                                    foreach (var column in columns)
                                    {//column.IsNullable
                                        cols += getColumnName(column.Name) + " " + ReturnColumnTypeSqlserver(column.GetColumnType()) + (column.IsPrimaryKey() ? " PRIMARY KEY" : "") + (column.ValueGenerated == ValueGenerated.OnAdd?isIdentityColumn(column,table):"") + (column.IsColumnNullable() ? " NULL" : " NOT NULL") + ",";
                                    }
                                    if (getTableName(table.GetTableName(), local_db).ToLower().Equals("invsalemaster"))
                                    {
                                        qry += cols+ " IsUploaded bit default 0 " + ")";
                                    }
                                    else
                                    {
                                        qry += cols.TrimEnd(',') + ")";
                                    }
                                    
                                    responseObj.createQueries.Add(qry);
                                });
                            }
                            break;
                        default:
                            break;
                    }
                    


                    //int=dbContext.AccAccountHead.FirstOrDefault().HeadName.ma
                }

                if (responseObj.createQueries != null && responseObj.createQueries.Count() > 0)
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
            catch (Exception ex)
            {
                //responseObj.Response.Code = ApplicationResponse.GENERIC_ERROR_CODE;
                //responseObj.Response.Message = ApplicationResponse.GENERIC_ERROR_MESSAGE;
                return BadRequest(responseObj);
            }
        }
        private string getColumnName(string columnName)
        {
            switch (columnName.ToLower())
            {
                case "group":
                    {
                        columnName= "[group]";
                    }
                    break;
                case "key":
                    {
                        columnName = "[key]";
                    }
                    break;
                default:
                    break;
            }
            return columnName;
        }
        private string isIdentityColumn(IProperty? column,IEntityType table)
        {
            if (column == null)
                return string.Empty;
            string idProp=string.Empty;
            if (column.IsPrimaryKey())
            {
                if (column.GetColumnType() == "int" && !column.IsColumnNullable())
                    idProp = " IDENTITY (1,1)";
            }
            else
            {
                var other_columns = table.GetProperties().Where(p=>p.Name!=column.Name && p.IsPrimaryKey()).ToList();
                if (!other_columns.Any())
                {
                    if (column.GetColumnType() == "int" && !column.IsColumnNullable())
                        idProp = " IDENTITY (1,1)";
                }

            }

            return idProp;
        }
        private string getTableName(string className,string local_db)
        {
            if (local_db.Equals(Constants.SqlServer))
                return className;
            switch (className.ToLower())
            {
                case "invsaledetail":
                    {
                        className = "InvSaleDetailTmp";
                    }
                    break;
                case "invsalemaster":
                    {
                        className = "InvSaleMasterTmp";
                    }
                    break;
                default:
                    break;
            }
            return className;

        }
        private string ReturnColumnType(string colType)
        {
            switch (colType)
            {
                case "string":
                    {
                        colType = "varchar";
                    }
                    break;

                case "decimal":
                    {
                        colType = "decimal";
                    }
                    break;

                case "int":
                    {
                        colType = "int";
                    }
                    break;

                case "datetime":
                    {
                        colType = "datetime";
                    }
                    break;

                case "bool":
                    {
                        colType = "boolean";
                    }
                    break;
                case "nvarchar(max)":
                    {
                        colType = "text";
                    }
                    break;
                default:
                    break;
            }
            //if (maxLength > 0)
            //{
            //    colType = colType/* + " (" + maxLength + ")"*/;
            //}
            return colType;
        }
        private string ReturnColumnTypeSqlserver(string colType)
        {
            switch (colType.ToLower())
            {
                case "string":
                    {
                        colType = "varchar";
                    }
                    break;
                case "number":
                    {
                        colType = "int";
                    }
                    break;

                case "bool":
                    {
                        colType = "bit";
                    }
                    break;               
                default:
                    break;
            }
            //if (maxLength > 0)
            //{
            //    colType = colType/* + " (" + maxLength + ")"*/;
            //}
            return colType;
        }
    }
}
