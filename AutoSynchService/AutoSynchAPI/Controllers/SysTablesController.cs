using AutoSynchAPI.Classes;
using AutoSynchService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            SynchTypes synchType = SynchTypes.Full;
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
                            if (synchType == SynchTypes.Full)
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
                                responseObj.sysHtmls = dbContext.SysHtml.ToList();
                                responseObj.sysInvTypeWiseControlls = dbContext.SysInvTypeWiseControll.ToList();
                                //Inv
                                responseObj.invCategorys = dbContext.InvCategory.ToList();
                                responseObj.invCompanys = dbContext.InvCompany.ToList();
                                responseObj.invCustomers = dbContext.InvCustomer.ToList();
                                responseObj.invCustomerTypes = dbContext.InvCustomerType.ToList();
                                //responseObj.invDeliveryChallanDetails = dbContext.InvDeliveryChallanDetail.ToList();
                                //responseObj.invDeliveryChallanMasters = dbContext.InvDeliveryChallanMaster.ToList();
                                //responseObj.invDemandNotes = dbContext.InvDemandNote.ToList();
                                //responseObj.invDemandNoteDetails = dbContext.InvDemandNoteDetail.ToList();
                                //responseObj.invGatePassInDetails = dbContext.InvGatePassInDetail.ToList();
                                //responseObj.invGatePassInMasters = dbContext.InvGatePassInMaster.ToList();
                                responseObj.invJcMonthSettings = dbContext.InvJcMonthSetting.ToList();
                                responseObj.invLocations = dbContext.InvLocation.ToList();
                                responseObj.invPackageProductsMappings = dbContext.InvPackageProductsMapping.ToList();
                                responseObj.invPaymentTypes = dbContext.InvPaymentType.ToList();
                                responseObj.invProducts = dbContext.InvProduct.ToList();
                                responseObj.invProductBatchs = dbContext.InvProductBatch.ToList();
                                //responseObj.invProductionDetails = dbContext.InvProductionDetail.ToList();
                                //responseObj.invProductionMasters = dbContext.InvProductionMaster.ToList();
                                responseObj.invProductLedgers = dbContext.InvProductLedger.ToList();
                                //responseObj.invPurchaseDetails = dbContext.InvPurchaseDetail.ToList();
                                //responseObj.invPurchaseMasters = dbContext.InvPurchaseMaster.ToList();
                                //responseObj.invPurchaseOrderDetails = dbContext.InvPurchaseOrderDetail.ToList();
                                //responseObj.invPurchaseOrderMasters = dbContext.InvPurchaseOrderMaster.ToList();
                                //responseObj.invQuatationDetails = dbContext.InvQuatationDetail.ToList();
                                //responseObj.invQuatationMasters = dbContext.InvQuatationMaster.ToList();
                                //responseObj.invSaleClosings = dbContext.InvSaleClosing.ToList();
                                //responseObj.invSaleClosingDetails = dbContext.InvSaleClosingDetail.ToList();
                                //responseObj.invSaleDetails = dbContext.InvSaleDetail.ToList();
                                responseObj.invSalemanToRoutsMappings = dbContext.InvSalemanToRoutsMapping.ToList();
                                //responseObj.invSaleMasters = dbContext.InvSaleMaster.ToList();
                                //responseObj.invSchemeDetails = dbContext.InvSchemeDetail.ToList();
                                //responseObj.invSchemeMasters = dbContext.InvSchemeMaster.ToList();
                                responseObj.invShifts = dbContext.InvShift.ToList();
                                //responseObj.invStockAdjustments = dbContext.InvStockAdjustment.ToList();
                                //responseObj.invStockAdjustmentDetails = dbContext.InvStockAdjustmentDetail.ToList();
                                //responseObj.invStockTransfers = dbContext.InvStockTransfer.ToList();
                                //responseObj.invStockTransferDetails = dbContext.InvStockTransferDetail.ToList();
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
                            }
                            else if (synchType == SynchTypes.OnlySysTables)
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
                                responseObj.sysHtmls = dbContext.SysHtml.ToList();
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

        [Route("GetTableStructure")]
        [HttpGet]
        public IActionResult GetTableStructure(string branch_id,string synch_type,string table_list)
        {
            SynchTypes synchType = SynchTypes.Full;
            Enum.TryParse(synch_type, out synchType);

            Models.TableStructureResponse responseObj = new Models.TableStructureResponse();
            try
            {
                List<string> SynchTbls = new List<string>();
                if (synchType == SynchTypes.Full)
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
                    SynchTbls.Add("InvSaleDetail");
                    SynchTbls.Add("InvSalemanToRoutsMapping");
                    SynchTbls.Add("InvSaleMaster");
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
                else if (synchType == SynchTypes.OnlySysTables)
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

                else if (synchType == SynchTypes.OnlySaleMasterDetailTables)
                {
                    SynchTbls.Add("InvSaleDetail");
                    SynchTbls.Add("InvSaleMaster");
                }
                else if (synchType == SynchTypes.ExceptSaleMasterDetailTables)
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
                else if (synchType == SynchTypes.Custom)
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

                    lstTables.ForEach(table =>
                    {
                        responseObj.dropQueries.Add("DROP TABLE IF EXISTS " + table.GetTableName());
                    });

                    string qry = string.Empty;

                    lstTables.ForEach(table =>
                    {
                        qry = "create table " + table.GetTableName() + "(";
                        var columns = table.GetProperties().ToList();
                        string cols = string.Empty;
                        foreach (var column in columns)
                        {//column.IsNullable
                            cols += column.Name + " " + ReturnColumnType(column.GetColumnType()) + (column.IsPrimaryKey() ? " primary key" : "") + ",";
                        }
                        qry += cols.TrimEnd(',') + ")";
                        responseObj.createQueries.Add(qry);
                    });


                    //int=dbContext.AccAccountHead.FirstOrDefault().HeadName.ma
                }

                if (responseObj.dropQueries != null && responseObj.dropQueries.Count() > 0)
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
    }
}
