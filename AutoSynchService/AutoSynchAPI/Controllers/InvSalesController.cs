using AutoSynchAPI.Classes;
using AutoSynchAPI.Models;
using AutoSynchService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AutoSynchAPI.Controllers
{
    [Route("api/InvSales")]
    [ApiController]
    public class InvSalesController : ControllerBase
    {

        private readonly ILogger<InvSalesController> _logger;

        public InvSalesController(ILogger<InvSalesController> logger)
        {
            _logger = logger;
        }
        [Route("GetSaleDetails")]
        [HttpGet]
        public IActionResult GetSaleDetails()
        {
            Models.DataResponse responseObj = new Models.DataResponse();
            try
            {
                using (Entities dbContext = new Entities())
                {
                    responseObj.invSaleDetails = dbContext.InvSaleDetail.ToList();
                    if (responseObj.invSaleDetails != null && responseObj.invSaleDetails.Count != 0)
                    {
                        responseObj.Response.Code = ApplicationResponse.SUCCESS_CODE;
                        responseObj.Response.Message = ApplicationResponse.SUCCESS_MESSAGE;
                        return Ok(responseObj);
                    }
                    else
                    {
                        responseObj.Response.Code = ApplicationResponse.NOT_EXISTS_CODE;
                        responseObj.Response.Message = ApplicationResponse.NOT_EXISTS_MESSAGE;
                        return NotFound(responseObj);
                    }
                }
            }
            catch (Exception ex)
            {
                responseObj.Response.Code = ApplicationResponse.GENERIC_ERROR_CODE;
                responseObj.Response.Message = ApplicationResponse.GENERIC_ERROR_MESSAGE;
                return BadRequest(responseObj);
            }
        }
        // POST api/<InvSalesController>

        [Route("PostSaleDetails")]
        [HttpPost]
        public IActionResult PostSaleDetails([FromBody] DataResponse dataResponse)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                using (Entities dbContext = new Entities())
                {
                    int oldId = 0;
                    bool IsJournalEqual = false;
                    dataResponse.invSaleMaster.ForEach(m => {
                        OrgBranch orgBranch = dbContext.OrgBranch.FirstOrDefault(o => o.Id == m.BranchId);
                        OrgOrganization orgOrganization = dbContext.OrgOrganization.FirstOrDefault(o => o.Id == orgBranch.OrgId);
                        List<AccVoucherDetail> accVoucherDetails = new List<AccVoucherDetail>();
                        if (orgOrganization.AccountIntegration == "Yes" && orgBranch.InvSaleAccInteg == "Bill")
                        {
                            IsJournalEqual = false;
                            //accVoucherDetails = GetSaleJournal(masterData, Items);
                            //int debitTotal = Convert.ToInt32(accVoucherDetails.Select(x => x.AmountDebit).Sum());
                            //int creditTotal = Convert.ToInt32(accVoucherDetails.Select(x => x.AmountCredit).Sum());
                            //if (accVoucherDetails.Count() == 0 || debitTotal != creditTotal || (debitTotal + creditTotal) == 0)
                            //{
                            //    IsJournalEqual = false;
                            //}
                        }
                        oldId = m.Id;
                        m.Id = (int)dbContext.GetSequence("Id", "InvSaleMaster");
                        m.OrderNo = (int)dbContext.GetSequence("OrderNo", "InvSaleMaster", m.BranchId);
                        dbContext.InvSaleMaster.Add(m);
                        List<InvSaleDetail> invSaleDetails = dataResponse.invSaleDetails.Where(d => d.BillId == oldId).ToList();
                        invSaleDetails.ForEach(d => {
                            InvProduct invProduct = dbContext.InvProduct.FirstOrDefault(p => p.Id == d.ProductId);
                            d.BillId = m.Id;
                            dbContext.InvSaleDetail.Add(d);
                            if (invProduct.Type == "Standard" || invProduct.Type == "Mixture")
                            {
                                InvProductLedger ledger = new InvProductLedger();
                                ledger.TransDate = m.OrderDate;
                                ledger.ProductId = d.ProductId;
                                ledger.ReferenceId = m.Id;
                                if (m.IsReturn)
                                {
                                    ledger.Source = "Sale Return";
                                    ledger.QtyIn = d.Qty;
                                    ledger.QtyOut = 0;
                                    ledger.Remarks = "Sale Return of Product " + invProduct.Name + ", Qty: " + d.Qty;
                                }
                                else
                                {
                                    ledger.Source = "Sale";
                                    ledger.QtyIn = 0;
                                    ledger.QtyOut = d.Qty;
                                    ledger.Remarks = "Sale of Product " + invProduct.Name + ", Qty: " + d.Qty;
                                }
                                ledger.BranchId = m.BranchId;
                                ledger.CreatedBy = m.CompletedBy.HasValue ? m.CompletedBy.Value : 0;
                                ledger.CreatedDate = m.CreatedDate;
                                ledger.UnitId = invProduct.SaleUnitId.HasValue ? invProduct.SaleUnitId.Value : 0;
                                ledger.WarehouseId = m.StoreId;
                                ledger.Cost = invProduct.Cost;
                                ledger.RetailPrice = d.Price;
                                ledger.SourceParty = m.StoreId.ToString();
                                ledger.FiscalYearId = m.FiscalYearId;
                                dbContext.InvProductLedger.Add(ledger);
                            }
                            else if (invProduct.Type == "Package")
                            {

                                List<InvPackageProductsMapping> PackageProducts = dbContext.InvPackageProductsMapping.Where(pm => pm.PackageId == d.ProductId).ToList();
                                PackageProducts.ForEach(pm =>
                                {
                                    InvProduct PackageItem = dbContext.InvProduct.FirstOrDefault(pi => pi.Id == pm.ProductId);
                                    InvProductLedger ledger = new InvProductLedger();
                                    ledger.TransDate = m.OrderDate;
                                    ledger.ProductId = pm.ProductId;
                                    ledger.PackageId = d.ProductId;
                                    ledger.ReferenceId = m.Id;
                                    if (m.IsReturn)
                                    {
                                        ledger.Source = "Sale Return";
                                        ledger.QtyIn = d.Qty * pm.Qty;
                                        ledger.QtyOut = 0;
                                        ledger.Remarks = "Sale Return of Product " + PackageItem.Name + " (Package: " + invProduct.Name + "), Qty: " + ledger.QtyIn;
                                    }
                                    else
                                    {
                                        ledger.Source = "Sale";
                                        ledger.QtyIn = 0;
                                        ledger.QtyOut = d.Qty * pm.Qty;
                                        ledger.Remarks = "Sale of Product " + PackageItem.Name + " (Package: " + invProduct.Name + "), Qty: " + ledger.QtyOut;
                                    }
                                    ledger.BranchId = m.BranchId;
                                    ledger.CreatedBy = m.CompletedBy.HasValue ? m.CompletedBy.Value : 0;
                                    ledger.CreatedDate = m.CreatedDate;
                                    ledger.UnitId = invProduct.SaleUnitId.HasValue ? invProduct.SaleUnitId.Value : 0;
                                    ledger.WarehouseId = m.StoreId;
                                    ledger.Cost = invProduct.Cost;
                                    ledger.RetailPrice = d.Price;
                                    ledger.SourceParty = m.StoreId.ToString();
                                    ledger.FiscalYearId = m.FiscalYearId;
                                    dbContext.InvProductLedger.Add(ledger);
                                });
                            }
                            else if (invProduct.Type == "Material")
                            {
                                InvProductLedger ledger = new InvProductLedger();
                                if (Convert.ToInt32(invProduct.Type) == 0)
                                {
                                    ledger.ProductId = d.ProductId;
                                }
                                else
                                {
                                    ledger.ProductId = Convert.ToInt32(invProduct.MixtureId);
                                    ledger.MaterialId = d.ProductId;
                                }
                                ledger.TransDate = m.OrderDate;
                                ledger.ReferenceId = m.Id;
                                if (m.IsReturn)
                                {
                                    ledger.Source = "Sale Return";
                                    ledger.QtyIn = d.Qty;
                                    ledger.QtyOut = 0;
                                    ledger.Remarks = "Sale Return of Product " + invProduct.Name + ", Qty: " + d.Qty;
                                }
                                else
                                {
                                    ledger.Source = "Sale";
                                    ledger.QtyIn = 0;
                                    ledger.QtyOut = d.Qty;
                                    ledger.Remarks = "Sale of Product " + invProduct.Name + ", Qty: " + d.Qty;
                                }
                                ledger.BranchId = m.BranchId;
                                ledger.CreatedBy = m.CompletedBy.HasValue ? m.CompletedBy.Value : 0;
                                ledger.CreatedDate = m.CreatedDate;
                                ledger.UnitId = invProduct.SaleUnitId.HasValue ? invProduct.SaleUnitId.Value : 0;
                                ledger.WarehouseId = m.StoreId;
                                ledger.Cost = invProduct.Cost;
                                ledger.RetailPrice = d.Price;
                                ledger.SourceParty = m.StoreId.ToString();
                                ledger.FiscalYearId = m.FiscalYearId;
                                dbContext.InvProductLedger.Add(ledger);
                            }

                        });
                        //inserting jv transactions

                        int JvVoucherMasterId = 0;
                        if (orgOrganization.AccountIntegration == "Yes" && orgBranch.InvSaleAccInteg == "Bill" && IsJournalEqual == true && (m.OrderStatus == "Invoice" || (m.OrderStatus == "QT" && orgBranch.InvCreateJvInCaseOfQtsale == "Yes")))
                        {

                            if (m != null && m.Id > 0)
                            {
                                string CustomerName = "";
                                AccVoucherMaster accVoucherMaster = new AccVoucherMaster();
                                accVoucherMaster.VoucherNo = m.OrderNo.ToString();
                                accVoucherMaster.CreatedBy = m.CreatedBy;
                                accVoucherMaster.CreatedDate = m.CreatedDate.HasValue ? m.CreatedDate.Value : DateTime.Now;
                                accVoucherMaster.VoucherDate = m.OrderDate.HasValue ? m.OrderDate.Value : DateTime.Now;
                                accVoucherMaster.BranchId = m.BranchId;
                                accVoucherMaster.FiscalYearId = m.FiscalYearId;
                                accVoucherMaster.VoucherType = "SAL";
                                accVoucherMaster.VoucherStatus = "Approved";
                                accVoucherMaster.Description = m.Remarks;
                                if (m.CustomerId != 0)
                                {
                                    CustomerName = dbContext.InvCustomer.FirstOrDefault(ic => ic.Id == m.CustomerId).Name;
                                    if (CustomerName != "" && CustomerName != null)
                                    {
                                        CustomerName = CustomerName + " - ";
                                    }
                                }
                                if (m.Remarks != "" && m.Remarks != null)
                                {
                                    accVoucherMaster.Description = m.Remarks + " (" + CustomerName + m.PaymentType + ")";
                                }
                                else
                                {
                                    accVoucherMaster.Description = CustomerName + m.PaymentType;
                                }
                                accVoucherMaster.ReferenceId = m.Id;
                                accVoucherMaster.Id = (int)dbContext.GetSequence("Id", "AccVoucherMaster");
                                dbContext.AccVoucherMaster.Add(accVoucherMaster);
                                if (accVoucherMaster.Id != 0)
                                {
                                    JvVoucherMasterId = accVoucherMaster.Id;
                                    foreach (var detail in accVoucherDetails)
                                    {
                                        detail.VoucherMasterId = JvVoucherMasterId;
                                        detail.Type = "Detail";
                                        dbContext.AccVoucherDetail.Add(detail);
                                    }
                                }
                            }
                        }

                    });
                    //dbContext.GetSequence()
                    // dbContext.InvSaleMaster.AddRan
                }

                apiResponse.Code = ApplicationResponse.SUCCESS_CODE;
                apiResponse.Message = ApplicationResponse.SUCCESS_MESSAGE;
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.Code = ApplicationResponse.GENERIC_ERROR_CODE;
                apiResponse.Message = ApplicationResponse.GENERIC_ERROR_MESSAGE;
                return BadRequest(apiResponse);
            }
            
        }
    }

}
