using AutoSynchAPI.Classes;
using AutoSynchAPI.Models;
using AutoSynchSqlServer.CustomModels;
using AutoSynchSqlServer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Cryptography;

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
                    using (var transaction = dbContext.Database.BeginTransaction())
                    {
                        int oldId = 0;
                        //int newId = 0;
                        bool IsJournalEqual = true;
                        OrgBranch orgBranch = dbContext.OrgBranch.FirstOrDefault(o => o.Id == dataResponse.invSaleMaster.FirstOrDefault().BranchId);
                        OrgOrganization orgOrganization = dbContext.OrgOrganization.FirstOrDefault(o => o.Id == orgBranch.OrgId);
                        dataResponse.invSaleMaster.ForEach(m =>
                        {
                            
                            List<AccVoucherDetail> accVoucherDetails = new List<AccVoucherDetail>();
                            oldId = m.Id;
                            //if (newId == 0)
                            //    m.Id = newId = (int)dbContext.GetSequence("Id", "InvSaleMaster");
                            //else
                            //{
                            //    m.Id = newId++;
                            //}
                            m.OrderNo = (int)dbContext.GetSequence("OrderNo", "InvSaleMaster", m.BranchId);
                           // InvSaleMaster dbSaleMaster = new InvSaleMaster();
                            m.Id = 0;
                            m.Source = "local";
                            m.MealServingTime = new TimeSpan( 0, 0, 0, 0, 0);
                            dbContext.InvSaleMaster.Add(m);
                            dbContext.SaveChanges();
                            List<InvSaleDetail> invSaleDetails = dataResponse.invSaleDetails.Where(d => d.BillId == oldId).ToList();
                            if (orgOrganization.AccountIntegration == "Yes" && orgBranch.InvSaleAccInteg == "Bill")
                            {
                                //IsJournalEqual = false;
                                accVoucherDetails = GetSaleJournal(dbContext, m, invSaleDetails, orgOrganization.Id, orgOrganization, orgBranch);
                                //accVoucherDetails = GetSaleJournal(dbContext, m, invSaleDetails);
                                int debitTotal = Convert.ToInt32(accVoucherDetails.Select(x => x.AmountDebit).Sum());
                                int creditTotal = Convert.ToInt32(accVoucherDetails.Select(x => x.AmountCredit).Sum());
                                if (accVoucherDetails.Count() == 0 || debitTotal != creditTotal || (debitTotal + creditTotal) == 0)
                                {
                                    accVoucherDetails = new List<AccVoucherDetail>();
                                    accVoucherDetails = GetSaleJournal(dbContext, m, invSaleDetails);
                                    debitTotal = Convert.ToInt32(accVoucherDetails.Select(x => x.AmountDebit).Sum());
                                    creditTotal = Convert.ToInt32(accVoucherDetails.Select(x => x.AmountCredit).Sum());
                                    if (accVoucherDetails.Count() == 0 || debitTotal != creditTotal || (debitTotal + creditTotal) == 0)
                                    {
                                        IsJournalEqual = false;
                                    }
                                    else
                                    {
                                        IsJournalEqual = true;
                                    }
                                    
                                }
                                else
                                {
                                    IsJournalEqual = true;
                                }
                            }

                            invSaleDetails.ForEach(d =>
                            {
                                InvProduct invProduct = dbContext.InvProduct.FirstOrDefault(p => p.Id == d.ProductId);
                                //d.BillId = m.Id;
                                //d.Id = 0;

                                dbContext.InvSaleDetail.Add(new InvSaleDetail
                                {
                                    BillId = m.Id,
                                    ProductId = d.ProductId,
                                    Price = d.Price,
                                    Qty = d.Qty,
                                    Total = d.Total,
                                    IsDeleted = d.IsDeleted,
                                    SaleValue = d.SaleValue,
                                    TaxCharged = d.TaxCharged,
                                    TaxRate = d.TaxRate,
                                    Pctcode = d.Pctcode,
                                    FurtherTax = d.FurtherTax,
                                    Discount = d.Discount,
                                    InvoiceType = d.InvoiceType,
                                    PriceExclusiveTax = d.PriceExclusiveTax,
                                });
                                
                                if (invProduct != null)
                                {
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
                                        ledger.ExpiryDate = new DateTime(1, 1, 1);
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
                                    accVoucherMaster.ChequeDate = accVoucherMaster.CreatedDate;
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
                                    //accVoucherMaster.Id = (int)dbContext.GetSequence("Id", "AccVoucherMaster");

                                    dbContext.AccVoucherMaster.Add(accVoucherMaster);
                                    dbContext.SaveChanges();
                                    if (accVoucherMaster.Id != 0)
                                    {
                                        //JvVoucherMasterId = accVoucherMaster.Id;
                                        foreach (var detail in accVoucherDetails)
                                        {
                                            detail.VoucherMasterId = accVoucherMaster.Id;
                                            detail.Type = "Detail";
                                            //dbContext.AccVoucherDetail.Add(new AccVoucherDetail { Type="Detail"});
                                            dbContext.AccVoucherDetail.Add(detail);//.Add(new AccVoucherDetail { Type = "Detail", VoucherMasterId = accVoucherMaster.Id });
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception("Invalid AccVoucherMasterId");
                                    }
                                }
                            }

                        });
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
        //[Route("FixProblematicAccVouchers")]
        //[HttpGet]
        //public IActionResult FixProblematicAccVouchers(int branch_id)
        //{
        //    try
        //    {
        //        using (Entities dbContext = new Entities())
        //        {
        //            IEnumerable<int> accOverallMasterIds = from a in dbContext.InvSaleMaster
        //                                                   where a.BranchId == branch_id
        //                                                   && a.OrderStatus == "Invoice"
        //                                                   select a.Id;

        //            IEnumerable<int> accMasterWithVoucherIds = from a in dbContext.InvSaleMaster
        //                                                       join b in dbContext.AccVoucherMaster
        //                                             on a.Id equals b.ReferenceId
        //                                                       where a.BranchId == branch_id
        //                                                       && a.OrderStatus == "Invoice"
        //                                                       select a.Id;

        //            List<int> missingVouchers = accOverallMasterIds.Except(accMasterWithVoucherIds).ToList();
                    

        //                //int newId = 0;
        //                bool IsJournalEqual = true;
        //                OrgBranch orgBranch = dbContext.OrgBranch.FirstOrDefault(o => o.Id == branch_id);
        //                OrgOrganization orgOrganization = dbContext.OrgOrganization.FirstOrDefault(o => o.Id == orgBranch.OrgId);
        //                missingVouchers.ToList().ForEach(m =>
        //                {
        //                    IsJournalEqual = true;
        //                    //OrgBranch orgBranch = dbContext.OrgBranch.FirstOrDefault(o => o.Id == branch_id);
        //                    //OrgOrganization orgOrganization = dbContext.OrgOrganization.FirstOrDefault(o => o.Id == orgBranch.OrgId);
        //                    InvSaleMaster invSaleMaster = dbContext.InvSaleMaster.FirstOrDefault(sm => sm.Id == m);
        //                    List<AccVoucherDetail> accVoucherDetails = new List<AccVoucherDetail>();
                            
        //                    // oldId = m;
        //                    //if (newId == 0)
        //                    //    invSaleMaster.Id = newId = (int)dbContext.GetSequence("Id", "InvSaleMaster");
        //                    //else
        //                    //{
        //                    //    invSaleMaster.Id = newId++;
        //                    //}
        //                    // invSaleMaster.OrderNo = (int)dbContext.GetSequence("OrderNo", "InvSaleMaster", invSaleMaster.BranchId);
        //                    // InvSaleMaster dbSaleMaster = new InvSaleMaster();
        //                    // invSaleMaster.Id = 0;
        //                    //dbContext.InvSaleMaster.Add(m);
        //                    //dbContext.SaveChanges();
        //                    List<InvSaleDetail> invSaleDetails = dbContext.InvSaleDetail.Where(d => d.BillId == m).ToList();
        //                    //if (orgBranch.InvSaleAccInteg == "Bill")//orgOrganization.AccountIntegration == "Yes" && 
        //                    {
        //                        //IsJournalEqual = false;
        //                        accVoucherDetails = GetSaleJournal(dbContext, invSaleMaster, invSaleDetails, orgOrganization.Id, orgOrganization, orgBranch);
        //                        int debitTotal = Convert.ToInt32(accVoucherDetails.Select(x => x.AmountDebit).Sum());
        //                        int creditTotal = Convert.ToInt32(accVoucherDetails.Select(x => x.AmountCredit).Sum());
        //                        if (accVoucherDetails.Count() == 0 || debitTotal != creditTotal || (debitTotal + creditTotal) == 0)
        //                        {
        //                            IsJournalEqual = false;
        //                        }
        //                        else
        //                        {
        //                            IsJournalEqual = true;
        //                        }
        //                    }

        //                    //inserting jv transactions

        //                    int JvVoucherMasterId = 0;
        //                    //if (orgOrganization.AccountIntegration == "Yes" && orgBranch.InvSaleAccInteg == "Bill" && IsJournalEqual == true && (invSaleMaster.OrderStatus == "Invoice" || (invSaleMaster.OrderStatus == "QT" && orgBranch.InvCreateJvInCaseOfQtsale == "Yes")))
        //                    if (IsJournalEqual == true)
        //                    {
        //                        using (var transaction = dbContext.Database.BeginTransaction())
        //                        {

        //                            //if (m != null && invSaleMaster.Id > 0)
        //                            {
        //                                string CustomerName = "";
        //                                AccVoucherMaster accVoucherMaster = new AccVoucherMaster();
        //                                accVoucherMaster.VoucherNo = invSaleMaster.OrderNo.ToString();
        //                                accVoucherMaster.CreatedBy = invSaleMaster.CreatedBy;
        //                                accVoucherMaster.CreatedDate = invSaleMaster.CreatedDate.HasValue ? invSaleMaster.CreatedDate.Value : DateTime.Now;
        //                                accVoucherMaster.VoucherDate = invSaleMaster.OrderDate.HasValue ? invSaleMaster.OrderDate.Value : DateTime.Now;
        //                                accVoucherMaster.BranchId = invSaleMaster.BranchId;
        //                                accVoucherMaster.FiscalYearId = invSaleMaster.FiscalYearId;
        //                                accVoucherMaster.VoucherType = "SAL";
        //                                accVoucherMaster.VoucherStatus = "Approved";
        //                                accVoucherMaster.ChequeDate = invSaleMaster.CreatedDate;
        //                                accVoucherMaster.Description = invSaleMaster.Remarks;
        //                                if (invSaleMaster.CustomerId != 0)
        //                                {
        //                                    CustomerName = dbContext.InvCustomer.FirstOrDefault(ic => ic.Id == invSaleMaster.CustomerId).Name;
        //                                    if (CustomerName != "" && CustomerName != null)
        //                                    {
        //                                        CustomerName = CustomerName + " - ";
        //                                    }
        //                                }
        //                                if (invSaleMaster.Remarks != "" && invSaleMaster.Remarks != null)
        //                                {
        //                                    accVoucherMaster.Description = invSaleMaster.Remarks + " (" + CustomerName + invSaleMaster.PaymentType + ")";
        //                                }
        //                                else
        //                                {
        //                                    accVoucherMaster.Description = CustomerName + invSaleMaster.PaymentType;
        //                                }
        //                                accVoucherMaster.ReferenceId = invSaleMaster.Id;
        //                                //accVoucherMaster.Id = (int)dbContext.GetSequence("Id", "AccVoucherMaster");
        //                                dbContext.AccVoucherMaster.Add(accVoucherMaster);
        //                                dbContext.SaveChanges();
        //                                if (accVoucherMaster.Id != 0)
        //                                {
        //                                    //JvVoucherMasterId = accVoucherMaster.Id;
        //                                    foreach (var detail in accVoucherDetails)
        //                                    {
        //                                        detail.VoucherMasterId = accVoucherMaster.Id;
        //                                        detail.Type = "Detail";
        //                                        //dbContext.AccVoucherDetail.Add(new AccVoucherDetail { Type="Detail"});
        //                                        dbContext.AccVoucherDetail.Add(detail);//.Add(new AccVoucherDetail { Type = "Detail", VoucherMasterId = accVoucherMaster.Id });
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    throw new Exception("Invalid AccVoucherMasterId");
        //                                }

        //                            }
        //                            try
        //                            {
        //                                dbContext.SaveChanges();
        //                                transaction.Commit();
        //                            }
        //                            catch (Exception ex)
        //                            {
        //                                transaction.Rollback();

        //                                throw ex;
        //                            }
        //                        }
        //                    }
                            
        //                });
                        
                    

        //           // dbContext.SaveChanges();

        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return Ok("success");
        //}
        //[Route("FixProblematicOnes")]
        //[HttpGet]
        //public IActionResult FixProblematicOnes(int branch_id)
        //{
        //    try
        //    {
        //        using (Entities dbContext = new Entities())
        //        {
        //            IEnumerable<int> accVoucherIds = from b in dbContext.AccVoucherMaster
        //                                             join c in dbContext.InvSaleMaster
        //                                             on b.ReferenceId equals c.Id
        //                                             where c.BranchId == branch_id
        //                                             select b.Id;
        //            IEnumerable<int> accVoucherDetailMids = dbContext.AccVoucherDetail.Select(vd => vd.VoucherMasterId);
        //            IEnumerable<int> accVoucherMasters = dbContext.AccVoucherMaster.Select(vm => vm.Id);
        //            IEnumerable<int> missingVouchers = accVoucherMasters.Except(accVoucherDetailMids);
        //            missingVouchers = missingVouchers.Intersect(accVoucherIds).ToList();
        //            List<AccVoucherDetail> accVoucherDetails = new List<AccVoucherDetail>();
        //            bool IsJournalEqual = true;
        //            OrgBranch orgBranch = dbContext.OrgBranch.FirstOrDefault(o => o.Id == branch_id);
        //            OrgOrganization orgOrganization = dbContext.OrgOrganization.FirstOrDefault(o => o.Id == orgBranch.OrgId);
        //            foreach (int mv in missingVouchers)
        //            {
        //                var accVoucherMaster = dbContext.AccVoucherMaster.FirstOrDefault(v => v.Id == mv);
        //                if (accVoucherMaster != null)
        //                {
        //                    var invSaleMaster = dbContext.InvSaleMaster.FirstOrDefault(v => v.Id == accVoucherMaster.ReferenceId);
        //                    if (invSaleMaster != null)
        //                    {
        //                        List<InvSaleDetail> invSaleDetails = dbContext.InvSaleDetail.Where(v => v.BillId == invSaleMaster.Id).ToList();
                               
        //                        if (orgOrganization.AccountIntegration == "Yes" && orgBranch.InvSaleAccInteg == "Bill")
        //                        {
        //                            //IsJournalEqual = false;
        //                            accVoucherDetails = GetSaleJournal(dbContext, invSaleMaster, invSaleDetails, orgBranch.Id, orgOrganization,orgBranch);
        //                            int debitTotal = Convert.ToInt32(accVoucherDetails.Select(x => x.AmountDebit).Sum());
        //                            int creditTotal = Convert.ToInt32(accVoucherDetails.Select(x => x.AmountCredit).Sum());
        //                            if (accVoucherDetails.Count() == 0 || debitTotal != creditTotal || (debitTotal + creditTotal) == 0)
        //                            {
        //                                IsJournalEqual = false;
        //                            }
        //                            if (orgOrganization.AccountIntegration == "Yes" && orgBranch.InvSaleAccInteg == "Bill" && IsJournalEqual == true && (invSaleMaster.OrderStatus == "Invoice" || (invSaleMaster.OrderStatus == "QT" && orgBranch.InvCreateJvInCaseOfQtsale == "Yes")))
        //                            {

        //                                if (invSaleMaster.Id > 0)
        //                                {

        //                                    if (accVoucherMaster.Id != 0)
        //                                    {
        //                                        //JvVoucherMasterId = accVoucherMaster.Id;
        //                                        foreach (var detail in accVoucherDetails)
        //                                        {
        //                                            detail.VoucherMasterId = accVoucherMaster.Id;
        //                                            detail.Type = "Detail";
        //                                            //dbContext.AccVoucherDetail.Add(new AccVoucherDetail { Type="Detail"});
        //                                            dbContext.AccVoucherDetail.Add(detail);//.Add(new AccVoucherDetail { Type = "Detail", VoucherMasterId = accVoucherMaster.Id });
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        throw new Exception("Invalid AccVoucherMasterId");
        //                                    }

        //                                }
        //                            }

        //                        }

        //                    }
        //                }

        //            }
        //            dbContext.SaveChanges();

        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return Ok("success");
        //}
        private List<InvSaleDetailExtended> GetDetailExtendeds(List<InvSaleDetail> Items)
        {
            List<InvSaleDetailExtended> list = new List<InvSaleDetailExtended>();
            Items.ForEach(i => {
                list.Add(new InvSaleDetailExtended {
                    Id = i.Id,
                    BillId = i.BillId,
                    ProductId=i.ProductId,
                    Price=i.Price,
                    Qty=i.Qty,
                    Total=i.Total,
                    IsDeleted=i.IsDeleted,
                    SaleValue=i.SaleValue,
                    TaxCharged=i.TaxCharged,
                    TaxRate=i.TaxRate,
                    Pctcode=i.Pctcode,
                    FurtherTax=i.FurtherTax,
                    Discount=i.Discount,
                    InvoiceType=i.InvoiceType,
                    PriceExclusiveTax=i.PriceExclusiveTax
                });
            });
            return list;
        }
        private List<AccVoucherDetail> GetSaleJournal(Entities dbContext, InvSaleMaster masterData, List<InvSaleDetail> ItemsDet, int OrgId, OrgOrganization OrgInfo, OrgBranch branch)
        {
            List<AccVoucherDetail> Journal = new List<AccVoucherDetail>();
            try
            {
                List<InvSaleDetailExtended> Items = GetDetailExtendeds(ItemsDet);
                if (masterData != null)
                {
                    foreach (var item in Items)
                    {
                        InvProduct Product = dbContext.InvProduct.FirstOrDefault(p => p.Id == item.ProductId);

                        if (Product != null)
                        {
                            item.ItemCostValue = Product.AverageCost;
                            item.ProductName = Product.Name;
                            if (item.ItemCostValue <= 0)
                            {
                                item.ItemCostValue = item.Price * item.Qty;
                            }
                            decimal ProductTaxValue = (Product.Tax / (100 + Product.Tax)) * item.Price;
                            if (Convert.ToBoolean(Product.IsRetailPriceInclusiveTax) == true)
                            {
                                decimal PriceExclusiveTax = item.Price - ProductTaxValue;
                                item.ItemSaleValue = (item.Price - ProductTaxValue) * item.Qty;
                                item.ItemTaxCharged = ProductTaxValue * item.Qty;
                            }
                            else
                            {
                                decimal PriceExclusiveTax = item.Price;
                                item.ItemSaleValue = item.Price * item.Qty;
                                item.ItemTaxCharged = ProductTaxValue * item.Qty;
                            }
                            item.ItemNetTotal = item.ItemSaleValue - item.ItemDiscount + item.ItemTaxCharged + item.ItemExtraTaxCharged;
                            decimal ProfitAndLost = item.ItemSaleValue - item.ItemCostValue;
                            if (ProfitAndLost > 0)
                            {
                                item.ItemProfit = ProfitAndLost;
                            }
                            else if (ProfitAndLost < 0)
                            {
                                item.ItemLost = ProfitAndLost * (-1);
                            }
                        }

                    }
                    List<AccAccountsMapping> AccountsDetails = new List<AccAccountsMapping>();
                    if (masterData.IsReturn)
                    {
                        AccountsDetails = (from x in dbContext.AccAccountsMapping
                                           join a in dbContext.AccAccount on x.AccountId equals a.Id
                                           join b in dbContext.OrgBranch on x.BranchId equals b.Id
                                           where x.MappingForm == "Sales Return" && x.TransactionType == "Line" && b.OrgId == OrgId
                                           select new AccAccountsMapping { Id = x.Id, AccountId = x.AccountId, MappingSource = x.MappingSource, DebitOrCredit = x.DebitOrCredit, Description = x.Description, BranchId = x.BranchId }).ToList();
                        if (GetOrganization(OrgId, masterData.BranchId, OrgInfo, branch).DmAccMapping == "Separate")
                        {
                            AccountsDetails = AccountsDetails.Where(x => x.BranchId == masterData.BranchId).ToList();
                        }
                    }
                    else
                    {
                        AccountsDetails = (from x in dbContext.AccAccountsMapping
                                           join a in dbContext.AccAccount on x.AccountId equals a.Id
                                           join b in dbContext.OrgBranch on x.BranchId equals b.Id
                                           where x.MappingForm == "Sales" && x.TransactionType == "Line" && b.OrgId == OrgId
                                           select new AccAccountsMapping { Id = x.Id, AccountId = x.AccountId, MappingSource = x.MappingSource, DebitOrCredit = x.DebitOrCredit, Description = x.Description, BranchId = x.BranchId }).ToList();
                        if (GetOrganization(OrgId, masterData.BranchId, OrgInfo, branch).DmAccMapping == "Separate")
                        {
                            AccountsDetails = AccountsDetails.Where(x => x.BranchId == masterData.BranchId).ToList();
                        }
                    }
                    //{
                    //    var dtMappings = dbContext.AccAccountsMapping.Where(x=> x.MappingForm == "Sales Return" && x.TransactionType == "Line" && x.BranchId ==  masterData.BranchId);
                    //    foreach (AccAccountsMapping item in dtMappings)
                    //    {
                    //        AccAccountsMapping mapping = new AccAccountsMapping();
                    //        mapping.Id = Convert.ToInt32(item.Id);
                    //        mapping.AccountId = Convert.ToInt32(item.AccountId);
                    //        mapping.MappingSource = item.MappingSource.ToString();
                    //        mapping.DebitOrCredit = item.DebitOrCredit.ToString();
                    //        mapping.Description = item.Description.ToString();
                    //        mapping.AccountId = item.AccountId;
                    //        AccountsDetails.Add(mapping);
                    //    }
                    //}
                    //else
                    //{
                    //    var dtMappings = dbContext.AccAccountsMapping.Where(x => x.MappingForm == "Sales" && x.TransactionType == "Line" && x.BranchId == masterData.BranchId);
                    //    foreach (AccAccountsMapping item in dtMappings)
                    //    {
                    //        AccAccountsMapping mapping = new AccAccountsMapping();
                    //        mapping.Id = Convert.ToInt32(item.Id);
                    //        mapping.AccountId = Convert.ToInt32(item.AccountId);
                    //        mapping.MappingSource = item.MappingSource.ToString();
                    //        mapping.DebitOrCredit = item.DebitOrCredit.ToString();
                    //        mapping.Description = item.Description.ToString();
                    //        mapping.AccountId = item.AccountId;
                    //        AccountsDetails.Add(mapping);
                    //    }
                    //}
                    foreach (var map in AccountsDetails)
                    {
                        if (map.MappingSource == "Items Sale")
                        {
                            foreach (var item in Items.Where(x => x.ItemSaleValue > 0))
                            {
                                AccVoucherDetail voucherDetail = new AccVoucherDetail();
                                voucherDetail.AccountId = map.AccountId;
                                voucherDetail.Description = map.Description + " (" + item.ProductName + ")";
                                voucherDetail.PartnerId = item.ProductId;
                                voucherDetail.PartnerType = "Product";
                                //voucherDetailAccountId = map.AccountId;
                                voucherDetail.MappingSource = map.MappingSource;
                                if (map.DebitOrCredit == "Debit")
                                {
                                    voucherDetail.AmountDebit = item.ItemSaleValue;
                                    voucherDetail.AmountCredit = 0;
                                }
                                else
                                {
                                    voucherDetail.AmountDebit = 0;
                                    voucherDetail.AmountCredit = item.ItemSaleValue;
                                }
                                Journal.Add(voucherDetail);
                            }
                        }
                        else if (map.MappingSource == "Items Cost")
                        {
                            foreach (var item in Items)
                            {
                                AccVoucherDetail voucherDetail = new AccVoucherDetail();
                                voucherDetail.AccountId = map.AccountId;
                                voucherDetail.Description = map.Description + " (" + item.ProductName + ")";
                                voucherDetail.PartnerId = item.ProductId;
                                voucherDetail.PartnerType = "Product";
                                //voucherDetailAccountId = map.AccountId;
                                voucherDetail.MappingSource = map.MappingSource;
                                if (map.DebitOrCredit == "Debit")
                                {
                                    voucherDetail.AmountDebit = item.ItemCostValue;
                                    voucherDetail.AmountCredit = 0;
                                }
                                else
                                {
                                    voucherDetail.AmountDebit = 0;
                                    voucherDetail.AmountCredit = item.ItemCostValue;
                                }
                                Journal.Add(voucherDetail);
                            }
                        }
                        else if (map.MappingSource == "Items Discount")
                        {
                            foreach (var item in Items.Where(x => x.ItemDiscount > 0))
                            {
                                AccVoucherDetail voucherDetail = new AccVoucherDetail();
                                voucherDetail.AccountId = map.AccountId;
                                voucherDetail.Description = map.Description + " (" + item.ProductName + ")";
                                voucherDetail.PartnerId = item.ProductId;
                                voucherDetail.PartnerType = "Product";
                                //voucherDetail.AccountName = map.Account;
                                voucherDetail.MappingSource = map.MappingSource;
                                if (map.DebitOrCredit == "Debit")
                                {
                                    voucherDetail.AmountDebit = item.ItemDiscount;
                                    voucherDetail.AmountCredit = 0;
                                }
                                else
                                {
                                    voucherDetail.AmountDebit = 0;
                                    voucherDetail.AmountCredit = item.ItemDiscount;
                                }
                                Journal.Add(voucherDetail);
                            }
                        }
                        else if (map.MappingSource == "Items Tax")
                        {
                            foreach (var item in Items.Where(x => x.ItemTaxCharged > 0))
                            {
                                var Product = dbContext.InvProduct.FirstOrDefault(p => p.Id == item.ProductId);
                                AccVoucherDetail voucherDetail = new AccVoucherDetail();
                                voucherDetail.AccountId = map.AccountId;
                                voucherDetail.Description = map.Description + " (" + Product.Name + ")";
                                voucherDetail.PartnerId = item.ProductId;
                                voucherDetail.PartnerType = "Product";
                                //voucherDetail.AccountName = map.Account;
                                voucherDetail.MappingSource = map.MappingSource;
                                if (map.DebitOrCredit == "Debit")
                                {
                                    voucherDetail.AmountDebit = item.ItemTaxCharged;
                                    voucherDetail.AmountCredit = 0;
                                }
                                else
                                {
                                    voucherDetail.AmountDebit = 0;
                                    voucherDetail.AmountCredit = item.ItemTaxCharged;
                                }
                                Journal.Add(voucherDetail);
                            }
                        }
                        else if (map.MappingSource == "Items ExTax")
                        {
                            foreach (var item in Items.Where(x => x.ItemExtraTaxCharged > 0))
                            {
                                var Product = dbContext.InvProduct.FirstOrDefault(p => p.Id == item.ProductId);
                                AccVoucherDetail voucherDetail = new AccVoucherDetail();
                                voucherDetail.AccountId = map.AccountId;
                                voucherDetail.Description = map.Description + " (" + Product.Name + ")";
                                voucherDetail.PartnerId = item.ProductId;
                                voucherDetail.PartnerType = "Product";
                                //voucherDetail.AccountName = map.Account;
                                voucherDetail.MappingSource = map.MappingSource;
                                if (map.DebitOrCredit == "Debit")
                                {
                                    voucherDetail.AmountDebit = item.ItemExtraTaxCharged;
                                    voucherDetail.AmountCredit = 0;
                                }
                                else
                                {
                                    voucherDetail.AmountDebit = 0;
                                    voucherDetail.AmountCredit = item.ItemExtraTaxCharged;
                                }
                                Journal.Add(voucherDetail);
                            }
                        }
                        else if (map.MappingSource == "Items TotalTax")
                        {
                            foreach (var item in Items.Where(x => x.ItemTaxCharged + x.ItemExtraTaxCharged > 0))
                            {
                                var Product = dbContext.InvProduct.FirstOrDefault(p => p.Id == item.ProductId);

                                //DataTable Product = ObjSqlServerRepository.GetDataTable("select * from InvProduct where Id = '" + item.ProductId + "'");
                                AccVoucherDetail voucherDetail = new AccVoucherDetail();
                                voucherDetail.AccountId = map.AccountId;
                                voucherDetail.Description = map.Description + " (" + Product.Name.ToString() + ")";
                                voucherDetail.PartnerId = item.ProductId;
                                voucherDetail.PartnerType = "Product";
                                //voucherDetail.AccountName = map.Account;
                                voucherDetail.MappingSource = map.MappingSource;
                                if (map.DebitOrCredit == "Debit")
                                {
                                    voucherDetail.AmountDebit = item.ItemTaxCharged + item.ItemExtraTaxCharged;
                                    voucherDetail.AmountCredit = 0;
                                }
                                else
                                {
                                    voucherDetail.AmountDebit = 0;
                                    voucherDetail.AmountCredit = item.ItemTaxCharged + item.ItemExtraTaxCharged;
                                }
                                Journal.Add(voucherDetail);
                            }
                        }
                        else if (map.MappingSource == "Items Profit")
                        {
                            foreach (var item in Items.Where(x => x.ItemProfit > 0))
                            {
                                var Product = dbContext.InvProduct.FirstOrDefault(p => p.Id == item.ProductId);

                                //DataTable Product = ObjSqlServerRepository.GetDataTable("select * from InvProduct where Id = '" + item.ProductId + "'");
                                AccVoucherDetail voucherDetail = new AccVoucherDetail();
                                voucherDetail.AccountId = map.AccountId;
                                voucherDetail.Description = map.Description + " (" + Product.Name + ")";
                                voucherDetail.PartnerId = item.ProductId;
                                voucherDetail.PartnerType = "Product";
                                //voucherDetail.AccountName = map.Account;
                                voucherDetail.MappingSource = map.MappingSource;
                                if (map.DebitOrCredit == "Debit")
                                {
                                    voucherDetail.AmountDebit = item.ItemProfit;
                                    voucherDetail.AmountCredit = 0;
                                }
                                else
                                {
                                    voucherDetail.AmountDebit = 0;
                                    voucherDetail.AmountCredit = item.ItemProfit;
                                }
                                Journal.Add(voucherDetail);
                            }
                        }
                        else if (map.MappingSource == "Items Lost")
                        {
                            foreach (var item in Items.Where(x => x.ItemLost > 0))
                            {
                                var Product = dbContext.InvProduct.FirstOrDefault(p => p.Id == item.ProductId);

                                //DataTable Product = ObjSqlServerRepository.GetDataTable("select * from InvProduct where Id = '" + item.ProductId + "'");
                                AccVoucherDetail voucherDetail = new AccVoucherDetail();
                                voucherDetail.AccountId = map.AccountId;
                                voucherDetail.Description = map.Description + " (" + Product.Name + ")";
                                voucherDetail.PartnerId = item.ProductId;
                                voucherDetail.PartnerType = "Product";
                                //voucherDetail.AccountName = map.Account;
                                voucherDetail.MappingSource = map.MappingSource;
                                if (map.DebitOrCredit == "Debit")
                                {
                                    voucherDetail.AmountDebit = item.ItemLost;
                                    voucherDetail.AmountCredit = 0;
                                }
                                else
                                {
                                    voucherDetail.AmountDebit = 0;
                                    voucherDetail.AmountCredit = item.ItemLost;
                                }
                                Journal.Add(voucherDetail);
                            }
                        }
                        else if (map.MappingSource == "Items Net")
                        {
                            foreach (var item in Items.Where(x => x.ItemSaleValue - x.ItemDiscount + x.ItemTaxCharged + x.ItemExtraTaxCharged > 0))
                            {
                                var Product = dbContext.InvProduct.FirstOrDefault(p => p.Id == item.ProductId);
                                AccVoucherDetail voucherDetail = new AccVoucherDetail();
                                voucherDetail.AccountId = map.AccountId;
                                voucherDetail.Description = map.Description + " (" + Product.Name + ")";
                                voucherDetail.PartnerId = item.ProductId;
                                voucherDetail.PartnerType = "Product";
                                //voucherDetail.AccountName = map.Account;
                                voucherDetail.MappingSource = map.MappingSource;
                                if (map.DebitOrCredit == "Debit")
                                {
                                    voucherDetail.AmountDebit = item.ItemNetTotal;
                                    voucherDetail.AmountCredit = 0;
                                }
                                else
                                {
                                    voucherDetail.AmountDebit = 0;
                                    voucherDetail.AmountCredit = item.ItemNetTotal;
                                }
                                Journal.Add(voucherDetail);
                            }
                        }
                    }
                    List<AccAccountsMapping> AccountsTotal = new List<AccAccountsMapping>();
                    if (masterData.IsReturn)
                    {
                        AccountsTotal = (from x in dbContext.AccAccountsMapping
                                         join a in dbContext.AccAccount on x.AccountId equals a.Id
                                         join b in dbContext.OrgBranch on x.BranchId equals b.Id
                                         where x.MappingForm == "Sales Return" && x.TransactionType == "Total" && b.OrgId == OrgId
                                         select new AccAccountsMapping { Id = x.Id, AccountId = x.AccountId, MappingSource = x.MappingSource, DebitOrCredit = x.DebitOrCredit, Description = x.Description, BranchId = x.BranchId }).ToList();
                        if (GetOrganization(OrgId, masterData.BranchId, OrgInfo, branch).DmAccMapping == "Separate")
                        {
                            AccountsTotal = AccountsTotal.Where(x => x.BranchId == masterData.BranchId).ToList();
                        }
                    }
                    else
                    {
                        AccountsTotal = (from x in dbContext.AccAccountsMapping
                                         join a in dbContext.AccAccount on x.AccountId equals a.Id
                                         join b in dbContext.OrgBranch on x.BranchId equals b.Id
                                         where x.MappingForm == "Sales" && x.TransactionType == "Total" && b.OrgId == OrgId
                                         select new AccAccountsMapping { Id = x.Id, AccountId = x.AccountId, MappingSource = x.MappingSource, DebitOrCredit = x.DebitOrCredit, Description = x.Description, BranchId = x.BranchId }).ToList();
                        if (GetOrganization(OrgId, masterData.BranchId, OrgInfo, branch).DmAccMapping == "Separate")
                        {
                            AccountsTotal = AccountsTotal.Where(x => x.BranchId == masterData.BranchId).ToList();
                        }
                    }
                    //{
                    //    var dtMappings = dbContext.AccAccountsMapping.Where(x=> x.MappingForm == "Sales Return" && x.TransactionType == "Total" && x.BranchId == masterData.BranchId);
                    //    foreach (AccAccountsMapping item in dtMappings)
                    //    {
                    //        AccAccountsMapping mapping = new AccAccountsMapping();
                    //        mapping.Id = Convert.ToInt32(item.Id);
                    //        mapping.AccountId = Convert.ToInt32(item.AccountId);
                    //        mapping.MappingSource = item.MappingSource.ToString();
                    //        mapping.DebitOrCredit = item.DebitOrCredit.ToString();
                    //        mapping.Description = item.Description.ToString();
                    //        //mapping.Account = item.AccountName.ToString();
                    //        AccountsTotal.Add(mapping);
                    //    }
                    //}
                    //else
                    //{
                    //    //DataTable dtMappings = ObjSqlServerRepository.GetDataTable("select * from AccAccountsMapping as x inner join AccAccount as a on x.AccountId = a.id where x.MappingForm = 'Sales' and x.TransactionType = 'Total' and x.BranchId = '" + masterData.BranchId + "'");
                    //    var dtMappings = dbContext.AccAccountsMapping.Where(x => x.MappingForm == "Sales" && x.TransactionType == "Total" && x.BranchId == masterData.BranchId);
                    //    foreach (AccAccountsMapping item in dtMappings)

                    //    {
                    //        AccAccountsMapping mapping = new AccAccountsMapping();
                    //        mapping.Id = Convert.ToInt32(item.Id);
                    //        mapping.AccountId = Convert.ToInt32(item.AccountId);
                    //        mapping.MappingSource = item.MappingSource.ToString();
                    //        mapping.DebitOrCredit = item.DebitOrCredit.ToString();
                    //        mapping.Description = item.Description.ToString();
                    //        //mapping.Account = item.AccountName.ToString();
                    //        AccountsTotal.Add(mapping);
                    //    }
                    //}
                    foreach (var AccTotal in AccountsTotal)
                    {
                        if (AccTotal.MappingSource == "Items Sale")
                        {
                            AccVoucherDetail voucherDetail = new AccVoucherDetail();
                            voucherDetail.AccountId = AccTotal.AccountId;
                            voucherDetail.Description = AccTotal.Description;
                            //voucherDetail.AccountName = AccTotal.Account;
                            voucherDetail.MappingSource = AccTotal.MappingSource;
                            if (AccTotal.DebitOrCredit == "Debit")
                            {
                                voucherDetail.AmountDebit = Items.Select(x => x.ItemSaleValue).Sum();
                                voucherDetail.AmountCredit = 0;
                            }
                            else
                            {
                                voucherDetail.AmountDebit = 0;
                                voucherDetail.AmountCredit = Items.Select(x => x.ItemSaleValue).Sum();
                            }
                            Journal.Add(voucherDetail);
                        }
                        else if (AccTotal.MappingSource == "Items Cost")
                        {
                            AccVoucherDetail voucherDetail = new AccVoucherDetail();
                            voucherDetail.AccountId = AccTotal.AccountId;
                            voucherDetail.Description = AccTotal.Description;
                            //voucherDetail.AccountName = AccTotal.Account;
                            voucherDetail.MappingSource = AccTotal.MappingSource;
                            if (AccTotal.DebitOrCredit == "Debit")
                            {
                                voucherDetail.AmountDebit = Items.Select(x => x.ItemCostValue).Sum();
                                voucherDetail.AmountCredit = 0;
                            }
                            else
                            {
                                voucherDetail.AmountDebit = 0;
                                voucherDetail.AmountCredit = Items.Select(x => x.ItemCostValue).Sum();
                            }
                            Journal.Add(voucherDetail);
                        }
                        else if (AccTotal.MappingSource == "Items Discount")
                        {
                            AccVoucherDetail voucherDetail = new AccVoucherDetail();
                            voucherDetail.AccountId = AccTotal.AccountId;
                            voucherDetail.Description = AccTotal.Description;
                            //voucherDetail.AccountName = AccTotal.Account;
                            voucherDetail.MappingSource = AccTotal.MappingSource;
                            if (AccTotal.DebitOrCredit == "Debit")
                            {
                                voucherDetail.AmountDebit = Items.Select(x => x.ItemDiscount).Sum();
                                voucherDetail.AmountCredit = 0;
                            }
                            else
                            {
                                voucherDetail.AmountDebit = 0;
                                voucherDetail.AmountCredit = Items.Select(x => x.ItemDiscount).Sum();
                            }
                            Journal.Add(voucherDetail);
                        }
                        else if (AccTotal.MappingSource == "Items Tax")
                        {
                            AccVoucherDetail voucherDetail = new AccVoucherDetail();
                            voucherDetail.AccountId = AccTotal.AccountId;
                            voucherDetail.Description = AccTotal.Description;
                            //voucherDetail.AccountName = AccTotal.Account;
                            voucherDetail.MappingSource = AccTotal.MappingSource;
                            if (AccTotal.DebitOrCredit == "Debit")
                            {
                                voucherDetail.AmountDebit = Items.Select(x => x.ItemTaxCharged).Sum();
                                voucherDetail.AmountCredit = 0;
                            }
                            else
                            {
                                voucherDetail.AmountDebit = 0;
                                voucherDetail.AmountCredit = Items.Select(x => x.ItemTaxCharged).Sum();
                            }
                            Journal.Add(voucherDetail);
                        }
                        else if (AccTotal.MappingSource == "Items ExTax")
                        {
                            AccVoucherDetail voucherDetail = new AccVoucherDetail();
                            voucherDetail.AccountId = AccTotal.AccountId;
                            voucherDetail.Description = AccTotal.Description;
                            //voucherDetail.AccountName = AccTotal.Account;
                            voucherDetail.MappingSource = AccTotal.MappingSource;
                            if (AccTotal.DebitOrCredit == "Debit")
                            {
                                voucherDetail.AmountDebit = Items.Select(x => x.ItemExtraTaxCharged).Sum();
                                voucherDetail.AmountCredit = 0;
                            }
                            else
                            {
                                voucherDetail.AmountDebit = 0;
                                voucherDetail.AmountCredit = Items.Select(x => x.ItemExtraTaxCharged).Sum();
                            }
                            Journal.Add(voucherDetail);
                        }
                        else if (AccTotal.MappingSource == "Items TotalTax")
                        {
                            AccVoucherDetail voucherDetail = new AccVoucherDetail();
                            voucherDetail.AccountId = AccTotal.AccountId;
                            voucherDetail.Description = AccTotal.Description;
                            //voucherDetail.AccountName = AccTotal.Account;
                            voucherDetail.MappingSource = AccTotal.MappingSource;
                            if (AccTotal.DebitOrCredit == "Debit")
                            {
                                voucherDetail.AmountDebit = Items.Select(x => x.ItemExtraTaxCharged + x.ItemTaxCharged).Sum();
                                voucherDetail.AmountCredit = 0;
                            }
                            else
                            {
                                voucherDetail.AmountDebit = 0;
                                voucherDetail.AmountCredit = Items.Select(x => x.ItemExtraTaxCharged + x.ItemTaxCharged).Sum();
                            }
                            Journal.Add(voucherDetail);
                        }
                        else if (AccTotal.MappingSource == "Items Profit")
                        {
                            AccVoucherDetail voucherDetail = new AccVoucherDetail();
                            voucherDetail.AccountId = AccTotal.AccountId;
                            voucherDetail.Description = AccTotal.Description;
                            //voucherDetail.AccountName = AccTotal.Account;
                            voucherDetail.MappingSource = AccTotal.MappingSource;
                            if (AccTotal.DebitOrCredit == "Debit")
                            {
                                voucherDetail.AmountDebit = Items.Select(x => x.ItemProfit).Sum();
                                voucherDetail.AmountCredit = 0;
                            }
                            else
                            {
                                voucherDetail.AmountDebit = 0;
                                voucherDetail.AmountCredit = Items.Select(x => x.ItemProfit).Sum();
                            }
                            Journal.Add(voucherDetail);
                        }
                        else if (AccTotal.MappingSource == "Items Lost")
                        {
                            AccVoucherDetail voucherDetail = new AccVoucherDetail();
                            voucherDetail.AccountId = AccTotal.AccountId;
                            voucherDetail.Description = AccTotal.Description;
                            //voucherDetail.AccountName = AccTotal.Account;
                            voucherDetail.MappingSource = AccTotal.MappingSource;
                            if (AccTotal.DebitOrCredit == "Debit")
                            {
                                voucherDetail.AmountDebit = Items.Select(x => x.ItemLost).Sum();
                                voucherDetail.AmountCredit = 0;
                            }
                            else
                            {
                                voucherDetail.AmountDebit = 0;
                                voucherDetail.AmountCredit = Items.Select(x => x.ItemLost).Sum();
                            }
                            Journal.Add(voucherDetail);
                        }
                        else if (AccTotal.MappingSource == "Items Net")
                        {
                            AccVoucherDetail voucherDetail = new AccVoucherDetail();
                            voucherDetail.AccountId = AccTotal.AccountId;
                            voucherDetail.Description = AccTotal.Description;
                            //voucherDetail.AccountName = AccTotal.Account;
                            voucherDetail.MappingSource = AccTotal.MappingSource;
                            if (AccTotal.DebitOrCredit == "Debit")
                            {
                                voucherDetail.AmountDebit = Items.Select(x => x.ItemNetTotal).Sum();
                                voucherDetail.AmountCredit = 0;
                            }
                            else
                            {
                                voucherDetail.AmountDebit = 0;
                                voucherDetail.AmountCredit = Items.Select(x => x.ItemNetTotal).Sum();
                            }
                            Journal.Add(voucherDetail);
                        }
                        else if (AccTotal.MappingSource == "Discounts")
                        {
                            AccVoucherDetail voucherDetail = new AccVoucherDetail();
                            voucherDetail.AccountId = AccTotal.AccountId;
                            voucherDetail.Description = AccTotal.Description;
                            //voucherDetail.AccountName = AccTotal.Account;
                            voucherDetail.MappingSource = AccTotal.MappingSource;
                            if (AccTotal.DebitOrCredit == "Debit")
                            {
                                voucherDetail.AmountDebit = masterData.DiscountCalculated.HasValue ? masterData.DiscountCalculated.Value : 0;
                                voucherDetail.AmountCredit = 0;
                            }
                            else
                            {
                                voucherDetail.AmountDebit = 0;
                                voucherDetail.AmountCredit = masterData.DiscountCalculated.HasValue ? masterData.DiscountCalculated.Value : 0;
                            }
                            Journal.Add(voucherDetail);
                        }
                        else if (AccTotal.MappingSource == "Tax")
                        {
                            AccVoucherDetail voucherDetail = new AccVoucherDetail();
                            voucherDetail.AccountId = AccTotal.AccountId;
                            voucherDetail.Description = AccTotal.Description;
                            //voucherDetail.AccountName = AccTotal.Account;
                            voucherDetail.MappingSource = AccTotal.MappingSource;
                            if (AccTotal.DebitOrCredit == "Debit")
                            {
                                voucherDetail.AmountDebit = masterData.TaxCalculated.HasValue ? masterData.TaxCalculated.Value : 0;
                                voucherDetail.AmountCredit = 0;
                            }
                            else
                            {
                                voucherDetail.AmountDebit = 0;
                                voucherDetail.AmountCredit = masterData.TaxCalculated.HasValue ? masterData.TaxCalculated.Value : 0;
                            }
                            Journal.Add(voucherDetail);
                        }
                        else if (AccTotal.MappingSource == "Service Charges")
                        {
                            AccVoucherDetail voucherDetail = new AccVoucherDetail();
                            voucherDetail.AccountId = AccTotal.AccountId;
                            voucherDetail.Description = AccTotal.Description;
                            //voucherDetail.AccountName = AccTotal.Account;
                            voucherDetail.MappingSource = AccTotal.MappingSource;
                            if (AccTotal.DebitOrCredit == "Debit")
                            {
                                voucherDetail.AmountDebit = masterData.ServiceChargesCalculated.HasValue ? masterData.ServiceChargesCalculated.Value : 0;
                                voucherDetail.AmountCredit = 0;
                            }
                            else
                            {
                                voucherDetail.AmountDebit = 0;
                                voucherDetail.AmountCredit = masterData.ServiceChargesCalculated.HasValue ? masterData.ServiceChargesCalculated.Value : 0;
                            }
                            Journal.Add(voucherDetail);
                        }
                        else if (AccTotal.MappingSource == "Net Payable Credit" && masterData.PaymentType == "Credit" && masterData.CustomerId != 0)
                        {
                            string CustomerName = "";
                            var Customer = dbContext.InvCustomer.FirstOrDefault(c => c.Id == masterData.CustomerId);
                            if (Customer != null)
                            {
                                CustomerName = Customer.Name;
                            }
                            AccVoucherDetail voucherDetail = new AccVoucherDetail();
                            voucherDetail.AccountId = AccTotal.AccountId;
                            voucherDetail.Description = AccTotal.Description + " (" + CustomerName + ")";
                            //voucherDetail.AccountName = AccTotal.Account;
                            voucherDetail.MappingSource = AccTotal.MappingSource;
                            voucherDetail.PartnerId = masterData.CustomerId.HasValue ? masterData.CustomerId.Value : 0;
                            voucherDetail.PartnerType = "Customer";
                            if (AccTotal.DebitOrCredit == "Debit")
                            {
                                voucherDetail.AmountDebit = masterData.GrandTotal.HasValue ? masterData.GrandTotal.Value : 0;
                                voucherDetail.AmountCredit = 0;
                            }
                            else
                            {
                                voucherDetail.AmountDebit = 0;
                                voucherDetail.AmountCredit = masterData.GrandTotal.HasValue ? masterData.GrandTotal.Value : 0;
                            }
                            Journal.Add(voucherDetail);
                        }
                        else if (AccTotal.MappingSource == "Net Payable Cash" && masterData.PaymentType == "Cash")
                        {
                            AccVoucherDetail voucherDetail = new AccVoucherDetail();
                            voucherDetail.AccountId = AccTotal.AccountId;
                            voucherDetail.Description = AccTotal.Description;
                            //voucherDetail.AccountName = AccTotal.Account;
                            voucherDetail.MappingSource = AccTotal.MappingSource;
                            if (AccTotal.DebitOrCredit == "Debit")
                            {
                                voucherDetail.AmountDebit = masterData.GrandTotal.HasValue ? masterData.GrandTotal.Value : 0;
                                voucherDetail.AmountCredit = 0;
                            }
                            else
                            {
                                voucherDetail.AmountDebit = 0;
                                voucherDetail.AmountCredit = masterData.GrandTotal.HasValue ? masterData.GrandTotal.Value : 0;
                            }
                            Journal.Add(voucherDetail);
                        }
                        else if (AccTotal.MappingSource == "Net Payable CreditCard" && masterData.PaymentType == "Credit Card")
                        {
                            AccVoucherDetail voucherDetail = new AccVoucherDetail();
                            voucherDetail.AccountId = AccTotal.AccountId;
                            voucherDetail.Description = AccTotal.Description;
                            //voucherDetail.AccountName = AccTotal.Account;
                            voucherDetail.MappingSource = AccTotal.MappingSource;
                            if (AccTotal.DebitOrCredit == "Debit")
                            {
                                voucherDetail.AmountDebit = masterData.GrandTotal.HasValue ? masterData.GrandTotal.Value : 0;
                                voucherDetail.AmountCredit = 0;
                            }
                            else
                            {
                                voucherDetail.AmountDebit = 0;
                                voucherDetail.AmountCredit = masterData.GrandTotal.HasValue ? masterData.GrandTotal.Value : 0;
                            }
                            Journal.Add(voucherDetail);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //CustomLogging.Log("[DataUplodingService:(GetSaleJournal)]", ex.Message);
            }
            return Journal;
        }
        private List<AccVoucherDetail> GetSaleJournal(Entities dbContext, InvSaleMaster masterData, List<InvSaleDetail> ItemsDet)
        {
            List<AccVoucherDetail> Journal = new List<AccVoucherDetail>();
            try
            {
                List<InvSaleDetailExtended> Items = GetDetailExtendeds(ItemsDet);
                if (masterData != null)
                {
                    foreach (var item in Items)
                    {
                        InvProduct Product = dbContext.InvProduct.FirstOrDefault(p => p.Id == item.ProductId);
                        if (Product != null)
                        {
                            item.ItemCostValue = Product.AverageCost;
                            item.ProductName = Product.Name;
                            if (item.ItemCostValue <= 0)
                            {
                                item.ItemCostValue = item.Price * item.Qty;
                            }
                            decimal ProductTaxValue = (Product.Tax / (100 + Product.Tax)) * item.Price;
                            if (Convert.ToBoolean(Product.IsRetailPriceInclusiveTax) == true)
                            {
                                decimal PriceExclusiveTax = item.Price - ProductTaxValue;
                                item.ItemSaleValue = (item.Price - ProductTaxValue) * item.Qty;
                                item.ItemTaxCharged = ProductTaxValue * item.Qty;
                            }
                            else
                            {
                                decimal PriceExclusiveTax = item.Price;
                                item.ItemSaleValue = item.Price * item.Qty;
                                item.ItemTaxCharged = ProductTaxValue * item.Qty;
                            }
                            item.ItemNetTotal = item.ItemSaleValue - item.ItemDiscount + item.ItemTaxCharged + item.ItemExtraTaxCharged;
                            decimal ProfitAndLost = item.ItemSaleValue - item.ItemCostValue;
                            if (ProfitAndLost > 0)
                            {
                                item.ItemProfit = ProfitAndLost;
                            }
                            else if (ProfitAndLost < 0)
                            {
                                item.ItemLost = ProfitAndLost * (-1);
                            }
                        }

                    }
                    List<AccAccountsMapping> AccountsDetails = new List<AccAccountsMapping>();
                    if (masterData.IsReturn)
                    {
                        var dtMappings = dbContext.AccAccountsMapping.Where(x => x.MappingForm == "Sales Return" && x.TransactionType == "Line" && x.BranchId == masterData.BranchId);
                        foreach (AccAccountsMapping item in dtMappings)
                        {
                            AccAccountsMapping mapping = new AccAccountsMapping();
                            mapping.Id = Convert.ToInt32(item.Id);
                            mapping.AccountId = Convert.ToInt32(item.AccountId);
                            mapping.MappingSource = item.MappingSource.ToString();
                            mapping.DebitOrCredit = item.DebitOrCredit.ToString();
                            mapping.Description = item.Description.ToString();
                            mapping.AccountId = item.AccountId;
                            AccountsDetails.Add(mapping);
                        }
                    }
                    else
                    {
                        var dtMappings = dbContext.AccAccountsMapping.Where(x => x.MappingForm == "Sales" && x.TransactionType == "Line" && x.BranchId == masterData.BranchId);
                        foreach (AccAccountsMapping item in dtMappings)
                        {
                            AccAccountsMapping mapping = new AccAccountsMapping();
                            mapping.Id = Convert.ToInt32(item.Id);
                            mapping.AccountId = Convert.ToInt32(item.AccountId);
                            mapping.MappingSource = item.MappingSource.ToString();
                            mapping.DebitOrCredit = item.DebitOrCredit.ToString();
                            mapping.Description = item.Description.ToString();
                            mapping.AccountId = item.AccountId;
                            AccountsDetails.Add(mapping);
                        }
                    }
                    foreach (var map in AccountsDetails)
                    {
                        if (map.MappingSource == "Items Sale")
                        {
                            foreach (var item in Items.Where(x => x.ItemSaleValue > 0))
                            {
                                AccVoucherDetail voucherDetail = new AccVoucherDetail();
                                voucherDetail.AccountId = map.AccountId;
                                voucherDetail.Description = map.Description + " (" + item.ProductName + ")";
                                voucherDetail.PartnerId = item.ProductId;
                                voucherDetail.PartnerType = "Product";
                                //voucherDetailAccountId = map.AccountId;
                                voucherDetail.MappingSource = map.MappingSource;
                                if (map.DebitOrCredit == "Debit")
                                {
                                    voucherDetail.AmountDebit = item.ItemSaleValue;
                                    voucherDetail.AmountCredit = 0;
                                }
                                else
                                {
                                    voucherDetail.AmountDebit = 0;
                                    voucherDetail.AmountCredit = item.ItemSaleValue;
                                }
                                Journal.Add(voucherDetail);
                            }
                        }
                        else if (map.MappingSource == "Items Cost")
                        {
                            foreach (var item in Items)
                            {
                                AccVoucherDetail voucherDetail = new AccVoucherDetail();
                                voucherDetail.AccountId = map.AccountId;
                                voucherDetail.Description = map.Description + " (" + item.ProductName + ")";
                                voucherDetail.PartnerId = item.ProductId;
                                voucherDetail.PartnerType = "Product";
                                //voucherDetailAccountId = map.AccountId;
                                voucherDetail.MappingSource = map.MappingSource;
                                if (map.DebitOrCredit == "Debit")
                                {
                                    voucherDetail.AmountDebit = item.ItemCostValue;
                                    voucherDetail.AmountCredit = 0;
                                }
                                else
                                {
                                    voucherDetail.AmountDebit = 0;
                                    voucherDetail.AmountCredit = item.ItemCostValue;
                                }
                                Journal.Add(voucherDetail);
                            }
                        }
                        else if (map.MappingSource == "Items Discount")
                        {
                            foreach (var item in Items.Where(x => x.ItemDiscount > 0))
                            {
                                AccVoucherDetail voucherDetail = new AccVoucherDetail();
                                voucherDetail.AccountId = map.AccountId;
                                voucherDetail.Description = map.Description + " (" + item.ProductName + ")";
                                voucherDetail.PartnerId = item.ProductId;
                                voucherDetail.PartnerType = "Product";
                                //voucherDetail.AccountName = map.Account;
                                voucherDetail.MappingSource = map.MappingSource;
                                if (map.DebitOrCredit == "Debit")
                                {
                                    voucherDetail.AmountDebit = item.ItemDiscount;
                                    voucherDetail.AmountCredit = 0;
                                }
                                else
                                {
                                    voucherDetail.AmountDebit = 0;
                                    voucherDetail.AmountCredit = item.ItemDiscount;
                                }
                                Journal.Add(voucherDetail);
                            }
                        }
                        else if (map.MappingSource == "Items Tax")
                        {
                            foreach (var item in Items.Where(x => x.ItemTaxCharged > 0))
                            {
                                var Product = dbContext.InvProduct.FirstOrDefault(p => p.Id == item.ProductId);
                                AccVoucherDetail voucherDetail = new AccVoucherDetail();
                                voucherDetail.AccountId = map.AccountId;
                                voucherDetail.Description = map.Description + " (" + Product.Name + ")";
                                voucherDetail.PartnerId = item.ProductId;
                                voucherDetail.PartnerType = "Product";
                                //voucherDetail.AccountName = map.Account;
                                voucherDetail.MappingSource = map.MappingSource;
                                if (map.DebitOrCredit == "Debit")
                                {
                                    voucherDetail.AmountDebit = item.ItemTaxCharged;
                                    voucherDetail.AmountCredit = 0;
                                }
                                else
                                {
                                    voucherDetail.AmountDebit = 0;
                                    voucherDetail.AmountCredit = item.ItemTaxCharged;
                                }
                                Journal.Add(voucherDetail);
                            }
                        }
                        else if (map.MappingSource == "Items ExTax")
                        {
                            foreach (var item in Items.Where(x => x.ItemExtraTaxCharged > 0))
                            {
                                var Product = dbContext.InvProduct.FirstOrDefault(p => p.Id == item.ProductId);
                                AccVoucherDetail voucherDetail = new AccVoucherDetail();
                                voucherDetail.AccountId = map.AccountId;
                                voucherDetail.Description = map.Description + " (" + Product.Name + ")";
                                voucherDetail.PartnerId = item.ProductId;
                                voucherDetail.PartnerType = "Product";
                                //voucherDetail.AccountName = map.Account;
                                voucherDetail.MappingSource = map.MappingSource;
                                if (map.DebitOrCredit == "Debit")
                                {
                                    voucherDetail.AmountDebit = item.ItemExtraTaxCharged;
                                    voucherDetail.AmountCredit = 0;
                                }
                                else
                                {
                                    voucherDetail.AmountDebit = 0;
                                    voucherDetail.AmountCredit = item.ItemExtraTaxCharged;
                                }
                                Journal.Add(voucherDetail);
                            }
                        }
                        else if (map.MappingSource == "Items TotalTax")
                        {
                            foreach (var item in Items.Where(x => x.ItemTaxCharged + x.ItemExtraTaxCharged > 0))
                            {
                                var Product = dbContext.InvProduct.FirstOrDefault(p => p.Id == item.ProductId);

                                //DataTable Product = ObjSqlServerRepository.GetDataTable("select * from InvProduct where Id = '" + item.ProductId + "'");
                                AccVoucherDetail voucherDetail = new AccVoucherDetail();
                                voucherDetail.AccountId = map.AccountId;
                                voucherDetail.Description = map.Description + " (" + Product.Name.ToString() + ")";
                                voucherDetail.PartnerId = item.ProductId;
                                voucherDetail.PartnerType = "Product";
                                //voucherDetail.AccountName = map.Account;
                                voucherDetail.MappingSource = map.MappingSource;
                                if (map.DebitOrCredit == "Debit")
                                {
                                    voucherDetail.AmountDebit = item.ItemTaxCharged + item.ItemExtraTaxCharged;
                                    voucherDetail.AmountCredit = 0;
                                }
                                else
                                {
                                    voucherDetail.AmountDebit = 0;
                                    voucherDetail.AmountCredit = item.ItemTaxCharged + item.ItemExtraTaxCharged;
                                }
                                Journal.Add(voucherDetail);
                            }
                        }
                        else if (map.MappingSource == "Items Profit")
                        {
                            foreach (var item in Items.Where(x => x.ItemProfit > 0))
                            {
                                var Product = dbContext.InvProduct.FirstOrDefault(p => p.Id == item.ProductId);

                                //DataTable Product = ObjSqlServerRepository.GetDataTable("select * from InvProduct where Id = '" + item.ProductId + "'");
                                AccVoucherDetail voucherDetail = new AccVoucherDetail();
                                voucherDetail.AccountId = map.AccountId;
                                voucherDetail.Description = map.Description + " (" + Product.Name + ")";
                                voucherDetail.PartnerId = item.ProductId;
                                voucherDetail.PartnerType = "Product";
                                //voucherDetail.AccountName = map.Account;
                                voucherDetail.MappingSource = map.MappingSource;
                                if (map.DebitOrCredit == "Debit")
                                {
                                    voucherDetail.AmountDebit = item.ItemProfit;
                                    voucherDetail.AmountCredit = 0;
                                }
                                else
                                {
                                    voucherDetail.AmountDebit = 0;
                                    voucherDetail.AmountCredit = item.ItemProfit;
                                }
                                Journal.Add(voucherDetail);
                            }
                        }
                        else if (map.MappingSource == "Items Lost")
                        {
                            foreach (var item in Items.Where(x => x.ItemLost > 0))
                            {
                                var Product = dbContext.InvProduct.FirstOrDefault(p => p.Id == item.ProductId);

                                //DataTable Product = ObjSqlServerRepository.GetDataTable("select * from InvProduct where Id = '" + item.ProductId + "'");
                                AccVoucherDetail voucherDetail = new AccVoucherDetail();
                                voucherDetail.AccountId = map.AccountId;
                                voucherDetail.Description = map.Description + " (" + Product.Name + ")";
                                voucherDetail.PartnerId = item.ProductId;
                                voucherDetail.PartnerType = "Product";
                                //voucherDetail.AccountName = map.Account;
                                voucherDetail.MappingSource = map.MappingSource;
                                if (map.DebitOrCredit == "Debit")
                                {
                                    voucherDetail.AmountDebit = item.ItemLost;
                                    voucherDetail.AmountCredit = 0;
                                }
                                else
                                {
                                    voucherDetail.AmountDebit = 0;
                                    voucherDetail.AmountCredit = item.ItemLost;
                                }
                                Journal.Add(voucherDetail);
                            }
                        }
                        else if (map.MappingSource == "Items Net")
                        {
                            foreach (var item in Items.Where(x => x.ItemSaleValue - x.ItemDiscount + x.ItemTaxCharged + x.ItemExtraTaxCharged > 0))
                            {
                                var Product = dbContext.InvProduct.FirstOrDefault(p => p.Id == item.ProductId);
                                AccVoucherDetail voucherDetail = new AccVoucherDetail();
                                voucherDetail.AccountId = map.AccountId;
                                voucherDetail.Description = map.Description + " (" + Product.Name + ")";
                                voucherDetail.PartnerId = item.ProductId;
                                voucherDetail.PartnerType = "Product";
                                //voucherDetail.AccountName = map.Account;
                                voucherDetail.MappingSource = map.MappingSource;
                                if (map.DebitOrCredit == "Debit")
                                {
                                    voucherDetail.AmountDebit = item.ItemNetTotal;
                                    voucherDetail.AmountCredit = 0;
                                }
                                else
                                {
                                    voucherDetail.AmountDebit = 0;
                                    voucherDetail.AmountCredit = item.ItemNetTotal;
                                }
                                Journal.Add(voucherDetail);
                            }
                        }
                    }
                    List<AccAccountsMapping> AccountsTotal = new List<AccAccountsMapping>();
                    if (masterData.IsReturn)
                    {
                        var dtMappings = dbContext.AccAccountsMapping.Where(x => x.MappingForm == "Sales Return" && x.TransactionType == "Total" && x.BranchId == masterData.BranchId);
                        foreach (AccAccountsMapping item in dtMappings)
                        {
                            AccAccountsMapping mapping = new AccAccountsMapping();
                            mapping.Id = Convert.ToInt32(item.Id);
                            mapping.AccountId = Convert.ToInt32(item.AccountId);
                            mapping.MappingSource = item.MappingSource.ToString();
                            mapping.DebitOrCredit = item.DebitOrCredit.ToString();
                            mapping.Description = item.Description.ToString();
                            //mapping.Account = item.AccountName.ToString();
                            AccountsTotal.Add(mapping);
                        }
                    }
                    else
                    {
                        //DataTable dtMappings = ObjSqlServerRepository.GetDataTable("select * from AccAccountsMapping as x inner join AccAccount as a on x.AccountId = a.id where x.MappingForm = 'Sales' and x.TransactionType = 'Total' and x.BranchId = '" + masterData.BranchId + "'");
                        var dtMappings = dbContext.AccAccountsMapping.Where(x => x.MappingForm == "Sales" && x.TransactionType == "Total" && x.BranchId == masterData.BranchId);
                        foreach (AccAccountsMapping item in dtMappings)

                        {
                            AccAccountsMapping mapping = new AccAccountsMapping();
                            mapping.Id = Convert.ToInt32(item.Id);
                            mapping.AccountId = Convert.ToInt32(item.AccountId);
                            mapping.MappingSource = item.MappingSource.ToString();
                            mapping.DebitOrCredit = item.DebitOrCredit.ToString();
                            mapping.Description = item.Description.ToString();
                            //mapping.Account = item.AccountName.ToString();
                            AccountsTotal.Add(mapping);
                        }
                    }
                    foreach (var AccTotal in AccountsTotal)
                    {
                        if (AccTotal.MappingSource == "Items Sale")
                        {
                            AccVoucherDetail voucherDetail = new AccVoucherDetail();
                            voucherDetail.AccountId = AccTotal.AccountId;
                            voucherDetail.Description = AccTotal.Description;
                            //voucherDetail.AccountName = AccTotal.Account;
                            voucherDetail.MappingSource = AccTotal.MappingSource;
                            if (AccTotal.DebitOrCredit == "Debit")
                            {
                                voucherDetail.AmountDebit = Items.Select(x => x.ItemSaleValue).Sum();
                                voucherDetail.AmountCredit = 0;
                            }
                            else
                            {
                                voucherDetail.AmountDebit = 0;
                                voucherDetail.AmountCredit = Items.Select(x => x.ItemSaleValue).Sum();
                            }
                            Journal.Add(voucherDetail);
                        }
                        else if (AccTotal.MappingSource == "Items Cost")
                        {
                            AccVoucherDetail voucherDetail = new AccVoucherDetail();
                            voucherDetail.AccountId = AccTotal.AccountId;
                            voucherDetail.Description = AccTotal.Description;
                            //voucherDetail.AccountName = AccTotal.Account;
                            voucherDetail.MappingSource = AccTotal.MappingSource;
                            if (AccTotal.DebitOrCredit == "Debit")
                            {
                                voucherDetail.AmountDebit = Items.Select(x => x.ItemCostValue).Sum();
                                voucherDetail.AmountCredit = 0;
                            }
                            else
                            {
                                voucherDetail.AmountDebit = 0;
                                voucherDetail.AmountCredit = Items.Select(x => x.ItemCostValue).Sum();
                            }
                            Journal.Add(voucherDetail);
                        }
                        else if (AccTotal.MappingSource == "Items Discount")
                        {
                            AccVoucherDetail voucherDetail = new AccVoucherDetail();
                            voucherDetail.AccountId = AccTotal.AccountId;
                            voucherDetail.Description = AccTotal.Description;
                            //voucherDetail.AccountName = AccTotal.Account;
                            voucherDetail.MappingSource = AccTotal.MappingSource;
                            if (AccTotal.DebitOrCredit == "Debit")
                            {
                                voucherDetail.AmountDebit = Items.Select(x => x.ItemDiscount).Sum();
                                voucherDetail.AmountCredit = 0;
                            }
                            else
                            {
                                voucherDetail.AmountDebit = 0;
                                voucherDetail.AmountCredit = Items.Select(x => x.ItemDiscount).Sum();
                            }
                            Journal.Add(voucherDetail);
                        }
                        else if (AccTotal.MappingSource == "Items Tax")
                        {
                            AccVoucherDetail voucherDetail = new AccVoucherDetail();
                            voucherDetail.AccountId = AccTotal.AccountId;
                            voucherDetail.Description = AccTotal.Description;
                            //voucherDetail.AccountName = AccTotal.Account;
                            voucherDetail.MappingSource = AccTotal.MappingSource;
                            if (AccTotal.DebitOrCredit == "Debit")
                            {
                                voucherDetail.AmountDebit = Items.Select(x => x.ItemTaxCharged).Sum();
                                voucherDetail.AmountCredit = 0;
                            }
                            else
                            {
                                voucherDetail.AmountDebit = 0;
                                voucherDetail.AmountCredit = Items.Select(x => x.ItemTaxCharged).Sum();
                            }
                            Journal.Add(voucherDetail);
                        }
                        else if (AccTotal.MappingSource == "Items ExTax")
                        {
                            AccVoucherDetail voucherDetail = new AccVoucherDetail();
                            voucherDetail.AccountId = AccTotal.AccountId;
                            voucherDetail.Description = AccTotal.Description;
                            //voucherDetail.AccountName = AccTotal.Account;
                            voucherDetail.MappingSource = AccTotal.MappingSource;
                            if (AccTotal.DebitOrCredit == "Debit")
                            {
                                voucherDetail.AmountDebit = Items.Select(x => x.ItemExtraTaxCharged).Sum();
                                voucherDetail.AmountCredit = 0;
                            }
                            else
                            {
                                voucherDetail.AmountDebit = 0;
                                voucherDetail.AmountCredit = Items.Select(x => x.ItemExtraTaxCharged).Sum();
                            }
                            Journal.Add(voucherDetail);
                        }
                        else if (AccTotal.MappingSource == "Items TotalTax")
                        {
                            AccVoucherDetail voucherDetail = new AccVoucherDetail();
                            voucherDetail.AccountId = AccTotal.AccountId;
                            voucherDetail.Description = AccTotal.Description;
                            //voucherDetail.AccountName = AccTotal.Account;
                            voucherDetail.MappingSource = AccTotal.MappingSource;
                            if (AccTotal.DebitOrCredit == "Debit")
                            {
                                voucherDetail.AmountDebit = Items.Select(x => x.ItemExtraTaxCharged + x.ItemTaxCharged).Sum();
                                voucherDetail.AmountCredit = 0;
                            }
                            else
                            {
                                voucherDetail.AmountDebit = 0;
                                voucherDetail.AmountCredit = Items.Select(x => x.ItemExtraTaxCharged + x.ItemTaxCharged).Sum();
                            }
                            Journal.Add(voucherDetail);
                        }
                        else if (AccTotal.MappingSource == "Items Profit")
                        {
                            AccVoucherDetail voucherDetail = new AccVoucherDetail();
                            voucherDetail.AccountId = AccTotal.AccountId;
                            voucherDetail.Description = AccTotal.Description;
                            //voucherDetail.AccountName = AccTotal.Account;
                            voucherDetail.MappingSource = AccTotal.MappingSource;
                            if (AccTotal.DebitOrCredit == "Debit")
                            {
                                voucherDetail.AmountDebit = Items.Select(x => x.ItemProfit).Sum();
                                voucherDetail.AmountCredit = 0;
                            }
                            else
                            {
                                voucherDetail.AmountDebit = 0;
                                voucherDetail.AmountCredit = Items.Select(x => x.ItemProfit).Sum();
                            }
                            Journal.Add(voucherDetail);
                        }
                        else if (AccTotal.MappingSource == "Items Lost")
                        {
                            AccVoucherDetail voucherDetail = new AccVoucherDetail();
                            voucherDetail.AccountId = AccTotal.AccountId;
                            voucherDetail.Description = AccTotal.Description;
                            //voucherDetail.AccountName = AccTotal.Account;
                            voucherDetail.MappingSource = AccTotal.MappingSource;
                            if (AccTotal.DebitOrCredit == "Debit")
                            {
                                voucherDetail.AmountDebit = Items.Select(x => x.ItemLost).Sum();
                                voucherDetail.AmountCredit = 0;
                            }
                            else
                            {
                                voucherDetail.AmountDebit = 0;
                                voucherDetail.AmountCredit = Items.Select(x => x.ItemLost).Sum();
                            }
                            Journal.Add(voucherDetail);
                        }
                        else if (AccTotal.MappingSource == "Items Net")
                        {
                            AccVoucherDetail voucherDetail = new AccVoucherDetail();
                            voucherDetail.AccountId = AccTotal.AccountId;
                            voucherDetail.Description = AccTotal.Description;
                            //voucherDetail.AccountName = AccTotal.Account;
                            voucherDetail.MappingSource = AccTotal.MappingSource;
                            if (AccTotal.DebitOrCredit == "Debit")
                            {
                                voucherDetail.AmountDebit = Items.Select(x => x.ItemNetTotal).Sum();
                                voucherDetail.AmountCredit = 0;
                            }
                            else
                            {
                                voucherDetail.AmountDebit = 0;
                                voucherDetail.AmountCredit = Items.Select(x => x.ItemNetTotal).Sum();
                            }
                            Journal.Add(voucherDetail);
                        }
                        else if (AccTotal.MappingSource == "Discounts")
                        {
                            AccVoucherDetail voucherDetail = new AccVoucherDetail();
                            voucherDetail.AccountId = AccTotal.AccountId;
                            voucherDetail.Description = AccTotal.Description;
                            //voucherDetail.AccountName = AccTotal.Account;
                            voucherDetail.MappingSource = AccTotal.MappingSource;
                            if (AccTotal.DebitOrCredit == "Debit")
                            {
                                voucherDetail.AmountDebit = masterData.DiscountCalculated.HasValue ? masterData.DiscountCalculated.Value : 0;
                                voucherDetail.AmountCredit = 0;
                            }
                            else
                            {
                                voucherDetail.AmountDebit = 0;
                                voucherDetail.AmountCredit = masterData.DiscountCalculated.HasValue ? masterData.DiscountCalculated.Value : 0;
                            }
                            Journal.Add(voucherDetail);
                        }
                        else if (AccTotal.MappingSource == "Tax")
                        {
                            AccVoucherDetail voucherDetail = new AccVoucherDetail();
                            voucherDetail.AccountId = AccTotal.AccountId;
                            voucherDetail.Description = AccTotal.Description;
                            //voucherDetail.AccountName = AccTotal.Account;
                            voucherDetail.MappingSource = AccTotal.MappingSource;
                            if (AccTotal.DebitOrCredit == "Debit")
                            {
                                voucherDetail.AmountDebit = masterData.TaxCalculated.HasValue ? masterData.TaxCalculated.Value : 0;
                                voucherDetail.AmountCredit = 0;
                            }
                            else
                            {
                                voucherDetail.AmountDebit = 0;
                                voucherDetail.AmountCredit = masterData.TaxCalculated.HasValue ? masterData.TaxCalculated.Value : 0;
                            }
                            Journal.Add(voucherDetail);
                        }
                        else if (AccTotal.MappingSource == "Service Charges")
                        {
                            AccVoucherDetail voucherDetail = new AccVoucherDetail();
                            voucherDetail.AccountId = AccTotal.AccountId;
                            voucherDetail.Description = AccTotal.Description;
                            //voucherDetail.AccountName = AccTotal.Account;
                            voucherDetail.MappingSource = AccTotal.MappingSource;
                            if (AccTotal.DebitOrCredit == "Debit")
                            {
                                voucherDetail.AmountDebit = masterData.ServiceChargesCalculated.HasValue ? masterData.ServiceChargesCalculated.Value : 0;
                                voucherDetail.AmountCredit = 0;
                            }
                            else
                            {
                                voucherDetail.AmountDebit = 0;
                                voucherDetail.AmountCredit = masterData.ServiceChargesCalculated.HasValue ? masterData.ServiceChargesCalculated.Value : 0;
                            }
                            Journal.Add(voucherDetail);
                        }
                        else if (AccTotal.MappingSource == "Net Payable Credit" && masterData.PaymentType == "Credit" && masterData.CustomerId != 0)
                        {
                            string CustomerName = "";
                            var Customer = dbContext.InvCustomer.FirstOrDefault(c => c.Id == masterData.CustomerId);
                            if (Customer != null)
                            {
                                CustomerName = Customer.Name;
                            }
                            AccVoucherDetail voucherDetail = new AccVoucherDetail();
                            voucherDetail.AccountId = AccTotal.AccountId;
                            voucherDetail.Description = AccTotal.Description + " (" + CustomerName + ")";
                            //voucherDetail.AccountName = AccTotal.Account;
                            voucherDetail.MappingSource = AccTotal.MappingSource;
                            voucherDetail.PartnerId = masterData.CustomerId.HasValue ? masterData.CustomerId.Value : 0;
                            voucherDetail.PartnerType = "Customer";
                            if (AccTotal.DebitOrCredit == "Debit")
                            {
                                voucherDetail.AmountDebit = masterData.GrandTotal.HasValue ? masterData.GrandTotal.Value : 0;
                                voucherDetail.AmountCredit = 0;
                            }
                            else
                            {
                                voucherDetail.AmountDebit = 0;
                                voucherDetail.AmountCredit = masterData.GrandTotal.HasValue ? masterData.GrandTotal.Value : 0;
                            }
                            Journal.Add(voucherDetail);
                        }
                        else if (AccTotal.MappingSource == "Net Payable Cash" && masterData.PaymentType == "Cash")
                        {
                            AccVoucherDetail voucherDetail = new AccVoucherDetail();
                            voucherDetail.AccountId = AccTotal.AccountId;
                            voucherDetail.Description = AccTotal.Description;
                            //voucherDetail.AccountName = AccTotal.Account;
                            voucherDetail.MappingSource = AccTotal.MappingSource;
                            if (AccTotal.DebitOrCredit == "Debit")
                            {
                                voucherDetail.AmountDebit = masterData.GrandTotal.HasValue ? masterData.GrandTotal.Value : 0;
                                voucherDetail.AmountCredit = 0;
                            }
                            else
                            {
                                voucherDetail.AmountDebit = 0;
                                voucherDetail.AmountCredit = masterData.GrandTotal.HasValue ? masterData.GrandTotal.Value : 0;
                            }
                            Journal.Add(voucherDetail);
                        }
                        else if (AccTotal.MappingSource == "Net Payable CreditCard" && masterData.PaymentType == "Credit Card")
                        {
                            AccVoucherDetail voucherDetail = new AccVoucherDetail();
                            voucherDetail.AccountId = AccTotal.AccountId;
                            voucherDetail.Description = AccTotal.Description;
                            //voucherDetail.AccountName = AccTotal.Account;
                            voucherDetail.MappingSource = AccTotal.MappingSource;
                            if (AccTotal.DebitOrCredit == "Debit")
                            {
                                voucherDetail.AmountDebit = masterData.GrandTotal.HasValue ? masterData.GrandTotal.Value : 0;
                                voucherDetail.AmountCredit = 0;
                            }
                            else
                            {
                                voucherDetail.AmountDebit = 0;
                                voucherDetail.AmountCredit = masterData.GrandTotal.HasValue ? masterData.GrandTotal.Value : 0;
                            }
                            Journal.Add(voucherDetail);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //CustomLogging.Log("[DataUplodingService:(GetSaleJournal)]", ex.Message);
            }
            return Journal;
        }

        //private List<AccVoucherDetail> GetSaleJournal(Entities dbContext, InvSaleMaster masterData, List<InvSaleDetail> ItemsDet, int OrgId, OrgOrganization OrgInfo, OrgBranch branch)
        //{
        //    List<AccVoucherDetail> Journal = new List<AccVoucherDetail>();
        //    try
        //    {
        //        List<InvSaleDetailExtended> Items = GetDetailExtendeds(ItemsDet);
        //        if (masterData != null)
        //        {
        //            foreach (var item in Items)
        //            {
        //                InvProduct Product = dbContext.InvProduct.FirstOrDefault(p => p.Id == item.ProductId);
        //                if (Product != null)
        //                {
        //                    item.ItemCostValue = Product.AverageCost;
        //                    item.ProductName = Product.Name;
        //                    if (item.ItemCostValue <= 0)
        //                    {
        //                        item.ItemCostValue = item.Price * item.Qty;
        //                    }
        //                    decimal ProductTaxValue = (Product.Tax / (100 + Product.Tax)) * item.Price;
        //                    if (Convert.ToBoolean(Product.IsRetailPriceInclusiveTax) == true)
        //                    {
        //                        decimal PriceExclusiveTax = item.Price - ProductTaxValue;
        //                        item.ItemSaleValue = (item.Price - ProductTaxValue) * item.Qty;
        //                        item.ItemTaxCharged = ProductTaxValue * item.Qty;
        //                    }
        //                    else
        //                    {
        //                        decimal PriceExclusiveTax = item.Price;
        //                        item.ItemSaleValue = item.Price * item.Qty;
        //                        item.ItemTaxCharged = ProductTaxValue * item.Qty;
        //                    }
        //                    item.ItemNetTotal = item.ItemSaleValue - item.ItemDiscount + item.ItemTaxCharged + item.ItemExtraTaxCharged;
        //                    decimal ProfitAndLost = item.ItemSaleValue - item.ItemCostValue;
        //                    if (ProfitAndLost > 0)
        //                    {
        //                        item.ItemProfit = ProfitAndLost;
        //                    }
        //                    else if (ProfitAndLost < 0)
        //                    {
        //                        item.ItemLost = ProfitAndLost * (-1);
        //                    }
        //                }

        //            }
        //            List<AccAccountsMapping> AccountsDetails = new List<AccAccountsMapping>();
        //            if (masterData.IsReturn)
        //            {
        //                AccountsDetails = (from x in dbContext.AccAccountsMapping
        //                                   join a in dbContext.AccAccount on x.AccountId equals a.Id
        //                                   join b in dbContext.OrgBranch on x.BranchId equals b.Id
        //                                   where x.MappingForm == "Sales Return" && x.TransactionType == "Line" && b.OrgId == OrgId
        //                                   select new AccAccountsMapping { Id = x.Id, AccountId = x.AccountId, MappingSource = x.MappingSource, DebitOrCredit = x.DebitOrCredit, Description = x.Description, BranchId = x.BranchId }).ToList();
        //                if (GetOrganization(OrgId, masterData.BranchId, OrgInfo, branch).DmAccMapping == "Separate")
        //                {
        //                    AccountsDetails = AccountsDetails.Where(x => x.BranchId == masterData.BranchId).ToList();
        //                }
        //            }
        //            else
        //            {
        //                AccountsDetails = (from x in dbContext.AccAccountsMapping
        //                                   join a in dbContext.AccAccount on x.AccountId equals a.Id
        //                                   join b in dbContext.OrgBranch on x.BranchId equals b.Id
        //                                   where x.MappingForm == "Sales" && x.TransactionType == "Line" && b.OrgId == OrgId
        //                                   select new AccAccountsMapping { Id = x.Id, AccountId = x.AccountId, MappingSource = x.MappingSource, DebitOrCredit = x.DebitOrCredit, Description = x.Description, BranchId = x.BranchId }).ToList();
        //                if (GetOrganization(OrgId, masterData.BranchId, OrgInfo, branch).DmAccMapping == "Separate")
        //                {
        //                    AccountsDetails = AccountsDetails.Where(x => x.BranchId == masterData.BranchId).ToList();
        //                }
        //            }
        //            //{
        //            //    var dtMappings = dbContext.AccAccountsMapping.Where(x=> x.MappingForm == "Sales Return" && x.TransactionType == "Line" && x.BranchId ==  masterData.BranchId);
        //            //    foreach (AccAccountsMapping item in dtMappings)
        //            //    {
        //            //        AccAccountsMapping mapping = new AccAccountsMapping();
        //            //        mapping.Id = Convert.ToInt32(item.Id);
        //            //        mapping.AccountId = Convert.ToInt32(item.AccountId);
        //            //        mapping.MappingSource = item.MappingSource.ToString();
        //            //        mapping.DebitOrCredit = item.DebitOrCredit.ToString();
        //            //        mapping.Description = item.Description.ToString();
        //            //        mapping.AccountId = item.AccountId;
        //            //        AccountsDetails.Add(mapping);
        //            //    }
        //            //}
        //            //else
        //            //{
        //            //    var dtMappings = dbContext.AccAccountsMapping.Where(x => x.MappingForm == "Sales" && x.TransactionType == "Line" && x.BranchId == masterData.BranchId);
        //            //    foreach (AccAccountsMapping item in dtMappings)
        //            //    {
        //            //        AccAccountsMapping mapping = new AccAccountsMapping();
        //            //        mapping.Id = Convert.ToInt32(item.Id);
        //            //        mapping.AccountId = Convert.ToInt32(item.AccountId);
        //            //        mapping.MappingSource = item.MappingSource.ToString();
        //            //        mapping.DebitOrCredit = item.DebitOrCredit.ToString();
        //            //        mapping.Description = item.Description.ToString();
        //            //        mapping.AccountId = item.AccountId;
        //            //        AccountsDetails.Add(mapping);
        //            //    }
        //            //}
        //            foreach (var map in AccountsDetails)
        //            {
        //                if (map.MappingSource == "Items Sale")
        //                {
        //                    foreach (var item in Items.Where(x => x.ItemSaleValue > 0))
        //                    {
        //                        AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                        voucherDetail.AccountId = map.AccountId;
        //                        voucherDetail.Description = map.Description + " (" + item.ProductName + ")";
        //                        voucherDetail.PartnerId = item.ProductId;
        //                        voucherDetail.PartnerType = "Product";
        //                        //voucherDetailAccountId = map.AccountId;
        //                        voucherDetail.MappingSource = map.MappingSource;
        //                        if (map.DebitOrCredit == "Debit")
        //                        {
        //                            voucherDetail.AmountDebit = item.ItemSaleValue;
        //                            voucherDetail.AmountCredit = 0;
        //                        }
        //                        else
        //                        {
        //                            voucherDetail.AmountDebit = 0;
        //                            voucherDetail.AmountCredit = item.ItemSaleValue;
        //                        }
        //                        Journal.Add(voucherDetail);
        //                    }
        //                }
        //                else if (map.MappingSource == "Items Cost")
        //                {
        //                    foreach (var item in Items)
        //                    {
        //                        AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                        voucherDetail.AccountId = map.AccountId;
        //                        voucherDetail.Description = map.Description + " (" + item.ProductName + ")";
        //                        voucherDetail.PartnerId = item.ProductId;
        //                        voucherDetail.PartnerType = "Product";
        //                        //voucherDetailAccountId = map.AccountId;
        //                        voucherDetail.MappingSource = map.MappingSource;
        //                        if (map.DebitOrCredit == "Debit")
        //                        {
        //                            voucherDetail.AmountDebit = item.ItemCostValue;
        //                            voucherDetail.AmountCredit = 0;
        //                        }
        //                        else
        //                        {
        //                            voucherDetail.AmountDebit = 0;
        //                            voucherDetail.AmountCredit = item.ItemCostValue;
        //                        }
        //                        Journal.Add(voucherDetail);
        //                    }
        //                }
        //                else if (map.MappingSource == "Items Discount")
        //                {
        //                    foreach (var item in Items.Where(x => x.ItemDiscount > 0))
        //                    {
        //                        AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                        voucherDetail.AccountId = map.AccountId;
        //                        voucherDetail.Description = map.Description + " (" + item.ProductName + ")";
        //                        voucherDetail.PartnerId = item.ProductId;
        //                        voucherDetail.PartnerType = "Product";
        //                        //voucherDetail.AccountName = map.Account;
        //                        voucherDetail.MappingSource = map.MappingSource;
        //                        if (map.DebitOrCredit == "Debit")
        //                        {
        //                            voucherDetail.AmountDebit = item.ItemDiscount;
        //                            voucherDetail.AmountCredit = 0;
        //                        }
        //                        else
        //                        {
        //                            voucherDetail.AmountDebit = 0;
        //                            voucherDetail.AmountCredit = item.ItemDiscount;
        //                        }
        //                        Journal.Add(voucherDetail);
        //                    }
        //                }
        //                else if (map.MappingSource == "Items Tax")
        //                {
        //                    foreach (var item in Items.Where(x => x.ItemTaxCharged > 0))
        //                    {
        //                        var Product = dbContext.InvProduct.FirstOrDefault(p => p.Id == item.ProductId);
        //                        AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                        voucherDetail.AccountId = map.AccountId;
        //                        voucherDetail.Description = map.Description + " (" + Product.Name + ")";
        //                        voucherDetail.PartnerId = item.ProductId;
        //                        voucherDetail.PartnerType = "Product";
        //                        //voucherDetail.AccountName = map.Account;
        //                        voucherDetail.MappingSource = map.MappingSource;
        //                        if (map.DebitOrCredit == "Debit")
        //                        {
        //                            voucherDetail.AmountDebit = item.ItemTaxCharged;
        //                            voucherDetail.AmountCredit = 0;
        //                        }
        //                        else
        //                        {
        //                            voucherDetail.AmountDebit = 0;
        //                            voucherDetail.AmountCredit = item.ItemTaxCharged;
        //                        }
        //                        Journal.Add(voucherDetail);
        //                    }
        //                }
        //                else if (map.MappingSource == "Items ExTax")
        //                {
        //                    foreach (var item in Items.Where(x => x.ItemExtraTaxCharged > 0))
        //                    {
        //                        var Product = dbContext.InvProduct.FirstOrDefault(p => p.Id == item.ProductId);
        //                        AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                        voucherDetail.AccountId = map.AccountId;
        //                        voucherDetail.Description = map.Description + " (" + Product.Name + ")";
        //                        voucherDetail.PartnerId = item.ProductId;
        //                        voucherDetail.PartnerType = "Product";
        //                        //voucherDetail.AccountName = map.Account;
        //                        voucherDetail.MappingSource = map.MappingSource;
        //                        if (map.DebitOrCredit == "Debit")
        //                        {
        //                            voucherDetail.AmountDebit = item.ItemExtraTaxCharged;
        //                            voucherDetail.AmountCredit = 0;
        //                        }
        //                        else
        //                        {
        //                            voucherDetail.AmountDebit = 0;
        //                            voucherDetail.AmountCredit = item.ItemExtraTaxCharged;
        //                        }
        //                        Journal.Add(voucherDetail);
        //                    }
        //                }
        //                else if (map.MappingSource == "Items TotalTax")
        //                {
        //                    foreach (var item in Items.Where(x => x.ItemTaxCharged + x.ItemExtraTaxCharged > 0))
        //                    {
        //                        var Product = dbContext.InvProduct.FirstOrDefault(p => p.Id == item.ProductId);

        //                        //DataTable Product = ObjSqlServerRepository.GetDataTable("select * from InvProduct where Id = '" + item.ProductId + "'");
        //                        AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                        voucherDetail.AccountId = map.AccountId;
        //                        voucherDetail.Description = map.Description + " (" + Product.Name.ToString() + ")";
        //                        voucherDetail.PartnerId = item.ProductId;
        //                        voucherDetail.PartnerType = "Product";
        //                        //voucherDetail.AccountName = map.Account;
        //                        voucherDetail.MappingSource = map.MappingSource;
        //                        if (map.DebitOrCredit == "Debit")
        //                        {
        //                            voucherDetail.AmountDebit = item.ItemTaxCharged + item.ItemExtraTaxCharged;
        //                            voucherDetail.AmountCredit = 0;
        //                        }
        //                        else
        //                        {
        //                            voucherDetail.AmountDebit = 0;
        //                            voucherDetail.AmountCredit = item.ItemTaxCharged + item.ItemExtraTaxCharged;
        //                        }
        //                        Journal.Add(voucherDetail);
        //                    }
        //                }
        //                else if (map.MappingSource == "Items Profit")
        //                {
        //                    foreach (var item in Items.Where(x => x.ItemProfit > 0))
        //                    {
        //                        var Product = dbContext.InvProduct.FirstOrDefault(p => p.Id == item.ProductId);

        //                        //DataTable Product = ObjSqlServerRepository.GetDataTable("select * from InvProduct where Id = '" + item.ProductId + "'");
        //                        AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                        voucherDetail.AccountId = map.AccountId;
        //                        voucherDetail.Description = map.Description + " (" + Product.Name + ")";
        //                        voucherDetail.PartnerId = item.ProductId;
        //                        voucherDetail.PartnerType = "Product";
        //                        //voucherDetail.AccountName = map.Account;
        //                        voucherDetail.MappingSource = map.MappingSource;
        //                        if (map.DebitOrCredit == "Debit")
        //                        {
        //                            voucherDetail.AmountDebit = item.ItemProfit;
        //                            voucherDetail.AmountCredit = 0;
        //                        }
        //                        else
        //                        {
        //                            voucherDetail.AmountDebit = 0;
        //                            voucherDetail.AmountCredit = item.ItemProfit;
        //                        }
        //                        Journal.Add(voucherDetail);
        //                    }
        //                }
        //                else if (map.MappingSource == "Items Lost")
        //                {
        //                    foreach (var item in Items.Where(x => x.ItemLost > 0))
        //                    {
        //                        var Product = dbContext.InvProduct.FirstOrDefault(p => p.Id == item.ProductId);

        //                        //DataTable Product = ObjSqlServerRepository.GetDataTable("select * from InvProduct where Id = '" + item.ProductId + "'");
        //                        AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                        voucherDetail.AccountId = map.AccountId;
        //                        voucherDetail.Description = map.Description + " (" + Product.Name + ")";
        //                        voucherDetail.PartnerId = item.ProductId;
        //                        voucherDetail.PartnerType = "Product";
        //                        //voucherDetail.AccountName = map.Account;
        //                        voucherDetail.MappingSource = map.MappingSource;
        //                        if (map.DebitOrCredit == "Debit")
        //                        {
        //                            voucherDetail.AmountDebit = item.ItemLost;
        //                            voucherDetail.AmountCredit = 0;
        //                        }
        //                        else
        //                        {
        //                            voucherDetail.AmountDebit = 0;
        //                            voucherDetail.AmountCredit = item.ItemLost;
        //                        }
        //                        Journal.Add(voucherDetail);
        //                    }
        //                }
        //                else if (map.MappingSource == "Items Net")
        //                {
        //                    foreach (var item in Items.Where(x => x.ItemSaleValue - x.ItemDiscount + x.ItemTaxCharged + x.ItemExtraTaxCharged > 0))
        //                    {
        //                        var Product = dbContext.InvProduct.FirstOrDefault(p => p.Id == item.ProductId);
        //                        AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                        voucherDetail.AccountId = map.AccountId;
        //                        voucherDetail.Description = map.Description + " (" + Product.Name + ")";
        //                        voucherDetail.PartnerId = item.ProductId;
        //                        voucherDetail.PartnerType = "Product";
        //                        //voucherDetail.AccountName = map.Account;
        //                        voucherDetail.MappingSource = map.MappingSource;
        //                        if (map.DebitOrCredit == "Debit")
        //                        {
        //                            voucherDetail.AmountDebit = item.ItemNetTotal;
        //                            voucherDetail.AmountCredit = 0;
        //                        }
        //                        else
        //                        {
        //                            voucherDetail.AmountDebit = 0;
        //                            voucherDetail.AmountCredit = item.ItemNetTotal;
        //                        }
        //                        Journal.Add(voucherDetail);
        //                    }
        //                }
        //            }
        //            List<AccAccountsMapping> AccountsTotal = new List<AccAccountsMapping>();
        //            if (masterData.IsReturn)
        //            {
        //                AccountsTotal = (from x in dbContext.AccAccountsMapping
        //                                 join a in dbContext.AccAccount on x.AccountId equals a.Id
        //                                 join b in dbContext.OrgBranch on x.BranchId equals b.Id
        //                                 where x.MappingForm == "Sales Return" && x.TransactionType == "Total" && b.OrgId == OrgId
        //                                 select new AccAccountsMapping { Id = x.Id, AccountId = x.AccountId, MappingSource = x.MappingSource, DebitOrCredit = x.DebitOrCredit, Description = x.Description, BranchId = x.BranchId }).ToList();
        //                if (GetOrganization(OrgId, masterData.BranchId, OrgInfo, branch).DmAccMapping == "Separate")
        //                {
        //                    AccountsTotal = AccountsTotal.Where(x => x.BranchId == masterData.BranchId).ToList();
        //                }
        //            }
        //            else
        //            {
        //                AccountsTotal = (from x in dbContext.AccAccountsMapping
        //                                 join a in dbContext.AccAccount on x.AccountId equals a.Id
        //                                 join b in dbContext.OrgBranch on x.BranchId equals b.Id
        //                                 where x.MappingForm == "Sales" && x.TransactionType == "Total" && b.OrgId == OrgId
        //                                 select new AccAccountsMapping { Id = x.Id, AccountId = x.AccountId, MappingSource = x.MappingSource, DebitOrCredit = x.DebitOrCredit, Description = x.Description, BranchId = x.BranchId }).ToList();
        //                if (GetOrganization(OrgId, masterData.BranchId, OrgInfo, branch).DmAccMapping == "Separate")
        //                {
        //                    AccountsTotal = AccountsTotal.Where(x => x.BranchId == masterData.BranchId).ToList();
        //                }
        //            }
        //            //{
        //            //    var dtMappings = dbContext.AccAccountsMapping.Where(x=> x.MappingForm == "Sales Return" && x.TransactionType == "Total" && x.BranchId == masterData.BranchId);
        //            //    foreach (AccAccountsMapping item in dtMappings)
        //            //    {
        //            //        AccAccountsMapping mapping = new AccAccountsMapping();
        //            //        mapping.Id = Convert.ToInt32(item.Id);
        //            //        mapping.AccountId = Convert.ToInt32(item.AccountId);
        //            //        mapping.MappingSource = item.MappingSource.ToString();
        //            //        mapping.DebitOrCredit = item.DebitOrCredit.ToString();
        //            //        mapping.Description = item.Description.ToString();
        //            //        //mapping.Account = item.AccountName.ToString();
        //            //        AccountsTotal.Add(mapping);
        //            //    }
        //            //}
        //            //else
        //            //{
        //            //    //DataTable dtMappings = ObjSqlServerRepository.GetDataTable("select * from AccAccountsMapping as x inner join AccAccount as a on x.AccountId = a.id where x.MappingForm = 'Sales' and x.TransactionType = 'Total' and x.BranchId = '" + masterData.BranchId + "'");
        //            //    var dtMappings = dbContext.AccAccountsMapping.Where(x => x.MappingForm == "Sales" && x.TransactionType == "Total" && x.BranchId == masterData.BranchId);
        //            //    foreach (AccAccountsMapping item in dtMappings)

        //            //    {
        //            //        AccAccountsMapping mapping = new AccAccountsMapping();
        //            //        mapping.Id = Convert.ToInt32(item.Id);
        //            //        mapping.AccountId = Convert.ToInt32(item.AccountId);
        //            //        mapping.MappingSource = item.MappingSource.ToString();
        //            //        mapping.DebitOrCredit = item.DebitOrCredit.ToString();
        //            //        mapping.Description = item.Description.ToString();
        //            //        //mapping.Account = item.AccountName.ToString();
        //            //        AccountsTotal.Add(mapping);
        //            //    }
        //            //}
        //            foreach (var AccTotal in AccountsTotal)
        //            {
        //                if (AccTotal.MappingSource == "Items Sale")
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    //voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Items.Select(x => x.ItemSaleValue).Sum();
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Items.Select(x => x.ItemSaleValue).Sum();
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Items Cost")
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    //voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Items.Select(x => x.ItemCostValue).Sum();
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Items.Select(x => x.ItemCostValue).Sum();
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Items Discount")
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    //voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Items.Select(x => x.ItemDiscount).Sum();
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Items.Select(x => x.ItemDiscount).Sum();
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Items Tax")
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    //voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Items.Select(x => x.ItemTaxCharged).Sum();
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Items.Select(x => x.ItemTaxCharged).Sum();
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Items ExTax")
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    //voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Items.Select(x => x.ItemExtraTaxCharged).Sum();
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Items.Select(x => x.ItemExtraTaxCharged).Sum();
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Items TotalTax")
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    //voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Items.Select(x => x.ItemExtraTaxCharged + x.ItemTaxCharged).Sum();
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Items.Select(x => x.ItemExtraTaxCharged + x.ItemTaxCharged).Sum();
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Items Profit")
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    //voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Items.Select(x => x.ItemProfit).Sum();
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Items.Select(x => x.ItemProfit).Sum();
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Items Lost")
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    //voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Items.Select(x => x.ItemLost).Sum();
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Items.Select(x => x.ItemLost).Sum();
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Items Net")
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    //voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Items.Select(x => x.ItemNetTotal).Sum();
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Items.Select(x => x.ItemNetTotal).Sum();
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Discounts")
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    //voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = masterData.DiscountCalculated.HasValue ? masterData.DiscountCalculated.Value : 0;
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = masterData.DiscountCalculated.HasValue ? masterData.DiscountCalculated.Value : 0;
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Tax")
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    //voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = masterData.TaxCalculated.HasValue ? masterData.TaxCalculated.Value : 0;
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = masterData.TaxCalculated.HasValue ? masterData.TaxCalculated.Value : 0;
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Service Charges")
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    //voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = masterData.ServiceChargesCalculated.HasValue ? masterData.ServiceChargesCalculated.Value : 0;
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = masterData.ServiceChargesCalculated.HasValue ? masterData.ServiceChargesCalculated.Value : 0;
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Net Payable Credit" && masterData.PaymentType == "Credit" && masterData.CustomerId != 0)
        //                {
        //                    string CustomerName = "";
        //                    var Customer = dbContext.InvCustomer.FirstOrDefault(c => c.Id == masterData.CustomerId);
        //                    if (Customer != null)
        //                    {
        //                        CustomerName = Customer.Name;
        //                    }
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description + " (" + CustomerName + ")";
        //                    //voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    voucherDetail.PartnerId = masterData.CustomerId.HasValue ? masterData.CustomerId.Value : 0;
        //                    voucherDetail.PartnerType = "Customer";
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = masterData.GrandTotal.HasValue ? masterData.GrandTotal.Value : 0;
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = masterData.GrandTotal.HasValue ? masterData.GrandTotal.Value : 0;
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Net Payable Cash" && masterData.PaymentType == "Cash")
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    //voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = masterData.GrandTotal.HasValue ? masterData.GrandTotal.Value : 0;
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = masterData.GrandTotal.HasValue ? masterData.GrandTotal.Value : 0;
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Net Payable CreditCard" && masterData.PaymentType == "Credit Card")
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    //voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = masterData.GrandTotal.HasValue ? masterData.GrandTotal.Value : 0;
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = masterData.GrandTotal.HasValue ? masterData.GrandTotal.Value : 0;
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //CustomLogging.Log("[DataUplodingService:(GetSaleJournal)]", ex.Message);
        //    }
        //    return Journal;
        //}

        //private List<AccVoucherDetail> GetSaleJournal(Entities dbContext,InvSaleMaster masterData, List<InvSaleDetail> ItemsDet)
        //{
        //    List<AccVoucherDetail> Journal = new List<AccVoucherDetail>();
        //    try
        //    {
        //        List<InvSaleDetailExtended> Items=GetDetailExtendeds(ItemsDet);
        //        if (masterData != null)
        //        {
        //            foreach (var item in Items)
        //            {
        //                InvProduct Product = dbContext.InvProduct.FirstOrDefault(p=> p.Id ==  item.ProductId);
        //                if (Product != null)
        //                {
        //                    item.ItemCostValue = Product.AverageCost;
        //                    item.ProductName = Product.Name;
        //                    if (item.ItemCostValue <= 0)
        //                    {
        //                        item.ItemCostValue = item.Price * item.Qty;
        //                    }
        //                    decimal ProductTaxValue = (Product.Tax / (100 + Product.Tax)) * item.Price;
        //                    if (Convert.ToBoolean(Product.IsRetailPriceInclusiveTax) == true)
        //                    {
        //                        decimal PriceExclusiveTax = item.Price - ProductTaxValue;
        //                        item.ItemSaleValue = (item.Price - ProductTaxValue) * item.Qty;
        //                        item.ItemTaxCharged = ProductTaxValue * item.Qty;
        //                    }
        //                    else
        //                    {
        //                        decimal PriceExclusiveTax = item.Price;
        //                        item.ItemSaleValue = item.Price * item.Qty;
        //                        item.ItemTaxCharged = ProductTaxValue * item.Qty;
        //                    }
        //                    item.ItemNetTotal = item.ItemSaleValue - item.ItemDiscount + item.ItemTaxCharged + item.ItemExtraTaxCharged;
        //                    decimal ProfitAndLost = item.ItemSaleValue - item.ItemCostValue;
        //                    if (ProfitAndLost > 0)
        //                    {
        //                        item.ItemProfit = ProfitAndLost;
        //                    }
        //                    else if (ProfitAndLost < 0)
        //                    {
        //                        item.ItemLost = ProfitAndLost * (-1);
        //                    }
        //                }

        //            }
        //            List<AccAccountsMapping> AccountsDetails = new List<AccAccountsMapping>();
        //            if (masterData.IsReturn)
        //            {
        //                AccountsDetails = (from x in dbContext.AccAccountsMapping
        //                                   join a in dbContext.AccAccount on x.AccountId equals a.Id
        //                                   join b in dbContext.OrgBranch on x.BranchId equals b.Id
        //                                   where x.MappingForm == "Sales Return" && x.TransactionType == "Line" && b.OrgId == OrgId
        //                                   select new AccAccountsMapping { Id = x.Id, AccountId = x.AccountId, MappingSource = x.MappingSource, DebitOrCredit = x.DebitOrCredit, Description = x.Description, BranchId = x.BranchId }).ToList();
        //                if (GetOrganization(OrgId, masterData.BranchId, dbContext).DmAccMapping == "Separate")
        //                {
        //                    AccountsDetails = AccountsDetails.Where(x => x.BranchId == masterData.BranchId).ToList();
        //                }
        //            }
        //            else
        //            {
        //                AccountsDetails = (from x in dbContext.AccAccountsMapping
        //                                   join a in dbContext.AccAccount on x.AccountId equals a.Id
        //                                   join b in dbContext.OrgBranch on x.BranchId equals b.Id
        //                                   where x.MappingForm == "Sales" && x.TransactionType == "Line" && b.OrgId == OrgId
        //                                   select new AccAccountsMapping { Id = x.Id, AccountId = x.AccountId, MappingSource = x.MappingSource, DebitOrCredit = x.DebitOrCredit, Description = x.Description, BranchId = x.BranchId }).ToList();
        //                if (GetOrganization(OrgId, masterData.BranchId, dbContext).DmAccMapping == "Separate")
        //                {
        //                    AccountsDetails = AccountsDetails.Where(x => x.BranchId == masterData.BranchId).ToList();
        //                }
        //            }
        //            //{
        //            //    var dtMappings = dbContext.AccAccountsMapping.Where(x=> x.MappingForm == "Sales Return" && x.TransactionType == "Line" && x.BranchId ==  masterData.BranchId);
        //            //    foreach (AccAccountsMapping item in dtMappings)
        //            //    {
        //            //        AccAccountsMapping mapping = new AccAccountsMapping();
        //            //        mapping.Id = Convert.ToInt32(item.Id);
        //            //        mapping.AccountId = Convert.ToInt32(item.AccountId);
        //            //        mapping.MappingSource = item.MappingSource.ToString();
        //            //        mapping.DebitOrCredit = item.DebitOrCredit.ToString();
        //            //        mapping.Description = item.Description.ToString();
        //            //        mapping.AccountId = item.AccountId;
        //            //        AccountsDetails.Add(mapping);
        //            //    }
        //            //}
        //            //else
        //            //{
        //            //    var dtMappings = dbContext.AccAccountsMapping.Where(x => x.MappingForm == "Sales" && x.TransactionType == "Line" && x.BranchId == masterData.BranchId);
        //            //    foreach (AccAccountsMapping item in dtMappings)
        //            //    {
        //            //        AccAccountsMapping mapping = new AccAccountsMapping();
        //            //        mapping.Id = Convert.ToInt32(item.Id);
        //            //        mapping.AccountId = Convert.ToInt32(item.AccountId);
        //            //        mapping.MappingSource = item.MappingSource.ToString();
        //            //        mapping.DebitOrCredit = item.DebitOrCredit.ToString();
        //            //        mapping.Description = item.Description.ToString();
        //            //        mapping.AccountId = item.AccountId;
        //            //        AccountsDetails.Add(mapping);
        //            //    }
        //            //}
        //            foreach (var map in AccountsDetails)
        //            {
        //                if (map.MappingSource == "Items Sale")
        //                {
        //                    foreach (var item in Items.Where(x => x.ItemSaleValue > 0))
        //                    {
        //                        AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                        voucherDetail.AccountId = map.AccountId;
        //                        voucherDetail.Description = map.Description + " (" + item.ProductName + ")";
        //                        voucherDetail.PartnerId = item.ProductId;
        //                        voucherDetail.PartnerType = "Product";
        //                        //voucherDetailAccountId = map.AccountId;
        //                        voucherDetail.MappingSource = map.MappingSource;
        //                        if (map.DebitOrCredit == "Debit")
        //                        {
        //                            voucherDetail.AmountDebit = item.ItemSaleValue;
        //                            voucherDetail.AmountCredit = 0;
        //                        }
        //                        else
        //                        {
        //                            voucherDetail.AmountDebit = 0;
        //                            voucherDetail.AmountCredit = item.ItemSaleValue;
        //                        }
        //                        Journal.Add(voucherDetail);
        //                    }
        //                }
        //                else if (map.MappingSource == "Items Cost")
        //                {
        //                    foreach (var item in Items)
        //                    {
        //                        AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                        voucherDetail.AccountId = map.AccountId;
        //                        voucherDetail.Description = map.Description + " (" + item.ProductName + ")";
        //                        voucherDetail.PartnerId = item.ProductId;
        //                        voucherDetail.PartnerType = "Product";
        //                        //voucherDetailAccountId = map.AccountId;
        //                        voucherDetail.MappingSource = map.MappingSource;
        //                        if (map.DebitOrCredit == "Debit")
        //                        {
        //                            voucherDetail.AmountDebit = item.ItemCostValue;
        //                            voucherDetail.AmountCredit = 0;
        //                        }
        //                        else
        //                        {
        //                            voucherDetail.AmountDebit = 0;
        //                            voucherDetail.AmountCredit = item.ItemCostValue;
        //                        }
        //                        Journal.Add(voucherDetail);
        //                    }
        //                }
        //                else if (map.MappingSource == "Items Discount")
        //                {
        //                    foreach (var item in Items.Where(x => x.ItemDiscount > 0))
        //                    {
        //                        AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                        voucherDetail.AccountId = map.AccountId;
        //                        voucherDetail.Description = map.Description + " (" + item.ProductName + ")";
        //                        voucherDetail.PartnerId = item.ProductId;
        //                        voucherDetail.PartnerType = "Product";
        //                        //voucherDetail.AccountName = map.Account;
        //                        voucherDetail.MappingSource = map.MappingSource;
        //                        if (map.DebitOrCredit == "Debit")
        //                        {
        //                            voucherDetail.AmountDebit = item.ItemDiscount;
        //                            voucherDetail.AmountCredit = 0;
        //                        }
        //                        else
        //                        {
        //                            voucherDetail.AmountDebit = 0;
        //                            voucherDetail.AmountCredit = item.ItemDiscount;
        //                        }
        //                        Journal.Add(voucherDetail);
        //                    }
        //                }
        //                else if (map.MappingSource == "Items Tax")
        //                {
        //                    foreach (var item in Items.Where(x => x.ItemTaxCharged > 0))
        //                    {
        //                        var Product = dbContext.InvProduct.FirstOrDefault(p=>p.Id == item.ProductId);
        //                        AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                        voucherDetail.AccountId = map.AccountId;
        //                        voucherDetail.Description = map.Description + " (" + Product.Name + ")";
        //                        voucherDetail.PartnerId = item.ProductId;
        //                        voucherDetail.PartnerType = "Product";
        //                        //voucherDetail.AccountName = map.Account;
        //                        voucherDetail.MappingSource = map.MappingSource;
        //                        if (map.DebitOrCredit == "Debit")
        //                        {
        //                            voucherDetail.AmountDebit = item.ItemTaxCharged;
        //                            voucherDetail.AmountCredit = 0;
        //                        }
        //                        else
        //                        {
        //                            voucherDetail.AmountDebit = 0;
        //                            voucherDetail.AmountCredit = item.ItemTaxCharged;
        //                        }
        //                        Journal.Add(voucherDetail);
        //                    }
        //                }
        //                else if (map.MappingSource == "Items ExTax")
        //                {
        //                    foreach (var item in Items.Where(x => x.ItemExtraTaxCharged > 0))
        //                    {
        //                        var Product = dbContext.InvProduct.FirstOrDefault(p=> p.Id == item.ProductId);
        //                        AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                        voucherDetail.AccountId = map.AccountId;
        //                        voucherDetail.Description = map.Description + " (" + Product.Name + ")";
        //                        voucherDetail.PartnerId = item.ProductId;
        //                        voucherDetail.PartnerType = "Product";
        //                        //voucherDetail.AccountName = map.Account;
        //                        voucherDetail.MappingSource = map.MappingSource;
        //                        if (map.DebitOrCredit == "Debit")
        //                        {
        //                            voucherDetail.AmountDebit = item.ItemExtraTaxCharged;
        //                            voucherDetail.AmountCredit = 0;
        //                        }
        //                        else
        //                        {
        //                            voucherDetail.AmountDebit = 0;
        //                            voucherDetail.AmountCredit = item.ItemExtraTaxCharged;
        //                        }
        //                        Journal.Add(voucherDetail);
        //                    }
        //                }
        //                else if (map.MappingSource == "Items TotalTax")
        //                {
        //                    foreach (var item in Items.Where(x => x.ItemTaxCharged + x.ItemExtraTaxCharged > 0))
        //                    {
        //                        var Product = dbContext.InvProduct.FirstOrDefault(p => p.Id == item.ProductId);

        //                        //DataTable Product = ObjSqlServerRepository.GetDataTable("select * from InvProduct where Id = '" + item.ProductId + "'");
        //                        AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                        voucherDetail.AccountId = map.AccountId;
        //                        voucherDetail.Description = map.Description + " (" + Product.Name.ToString() + ")";
        //                        voucherDetail.PartnerId = item.ProductId;
        //                        voucherDetail.PartnerType = "Product";
        //                        //voucherDetail.AccountName = map.Account;
        //                        voucherDetail.MappingSource = map.MappingSource;
        //                        if (map.DebitOrCredit == "Debit")
        //                        {
        //                            voucherDetail.AmountDebit = item.ItemTaxCharged + item.ItemExtraTaxCharged;
        //                            voucherDetail.AmountCredit = 0;
        //                        }
        //                        else
        //                        {
        //                            voucherDetail.AmountDebit = 0;
        //                            voucherDetail.AmountCredit = item.ItemTaxCharged + item.ItemExtraTaxCharged;
        //                        }
        //                        Journal.Add(voucherDetail);
        //                    }
        //                }
        //                else if (map.MappingSource == "Items Profit")
        //                {
        //                    foreach (var item in Items.Where(x => x.ItemProfit > 0))
        //                    {
        //                        var Product = dbContext.InvProduct.FirstOrDefault(p => p.Id == item.ProductId);

        //                        //DataTable Product = ObjSqlServerRepository.GetDataTable("select * from InvProduct where Id = '" + item.ProductId + "'");
        //                        AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                        voucherDetail.AccountId = map.AccountId;
        //                        voucherDetail.Description = map.Description + " (" + Product.Name + ")";
        //                        voucherDetail.PartnerId = item.ProductId;
        //                        voucherDetail.PartnerType = "Product";
        //                        //voucherDetail.AccountName = map.Account;
        //                        voucherDetail.MappingSource = map.MappingSource;
        //                        if (map.DebitOrCredit == "Debit")
        //                        {
        //                            voucherDetail.AmountDebit = item.ItemProfit;
        //                            voucherDetail.AmountCredit = 0;
        //                        }
        //                        else
        //                        {
        //                            voucherDetail.AmountDebit = 0;
        //                            voucherDetail.AmountCredit = item.ItemProfit;
        //                        }
        //                        Journal.Add(voucherDetail);
        //                    }
        //                }
        //                else if (map.MappingSource == "Items Lost")
        //                {
        //                    foreach (var item in Items.Where(x => x.ItemLost > 0))
        //                    {
        //                        var Product = dbContext.InvProduct.FirstOrDefault(p => p.Id == item.ProductId);

        //                        //DataTable Product = ObjSqlServerRepository.GetDataTable("select * from InvProduct where Id = '" + item.ProductId + "'");
        //                        AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                        voucherDetail.AccountId = map.AccountId;
        //                        voucherDetail.Description = map.Description + " (" + Product.Name + ")";
        //                        voucherDetail.PartnerId = item.ProductId;
        //                        voucherDetail.PartnerType = "Product";
        //                        //voucherDetail.AccountName = map.Account;
        //                        voucherDetail.MappingSource = map.MappingSource;
        //                        if (map.DebitOrCredit == "Debit")
        //                        {
        //                            voucherDetail.AmountDebit = item.ItemLost;
        //                            voucherDetail.AmountCredit = 0;
        //                        }
        //                        else
        //                        {
        //                            voucherDetail.AmountDebit = 0;
        //                            voucherDetail.AmountCredit = item.ItemLost;
        //                        }
        //                        Journal.Add(voucherDetail);
        //                    }
        //                }
        //                else if (map.MappingSource == "Items Net")
        //                {
        //                    foreach (var item in Items.Where(x => x.ItemSaleValue - x.ItemDiscount + x.ItemTaxCharged + x.ItemExtraTaxCharged > 0))
        //                    {
        //                        var Product = dbContext.InvProduct.FirstOrDefault(p=> p.Id == item.ProductId);
        //                        AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                        voucherDetail.AccountId = map.AccountId;
        //                        voucherDetail.Description = map.Description + " (" + Product.Name + ")";
        //                        voucherDetail.PartnerId = item.ProductId;
        //                        voucherDetail.PartnerType = "Product";
        //                        //voucherDetail.AccountName = map.Account;
        //                        voucherDetail.MappingSource = map.MappingSource;
        //                        if (map.DebitOrCredit == "Debit")
        //                        {
        //                            voucherDetail.AmountDebit = item.ItemNetTotal;
        //                            voucherDetail.AmountCredit = 0;
        //                        }
        //                        else
        //                        {
        //                            voucherDetail.AmountDebit = 0;
        //                            voucherDetail.AmountCredit = item.ItemNetTotal;
        //                        }
        //                        Journal.Add(voucherDetail);
        //                    }
        //                }
        //            }
        //            List<AccAccountsMapping> AccountsTotal = new List<AccAccountsMapping>();
        //            if (masterData.IsReturn)
        //            {
        //                AccountsTotal = (from x in dbContext.AccAccountsMapping
        //                                 join a in dbContext.AccAccount on x.AccountId equals a.Id
        //                                 join b in dbContext.OrgBranch on x.BranchId equals b.Id
        //                                 where x.MappingForm == "Sales Return" && x.TransactionType == "Total" && b.OrgId == OrgId
        //                                 select new AccAccountsMapping { Id = x.Id, AccountId = x.AccountId, MappingSource = x.MappingSource, DebitOrCredit = x.DebitOrCredit, Description = x.Description, BranchId = x.BranchId }).ToList();
        //                if (GetOrganization(OrgId, masterData.BranchId, dbContext).DmAccMapping == "Separate")
        //                {
        //                    AccountsTotal = AccountsTotal.Where(x => x.BranchId == masterData.BranchId).ToList();
        //                }
        //            }
        //            else
        //            {
        //                AccountsTotal = (from x in dbContext.AccAccountsMapping
        //                                 join a in dbContext.AccAccount on x.AccountId equals a.Id
        //                                 join b in dbContext.OrgBranch on x.BranchId equals b.Id
        //                                 where x.MappingForm == "Sales" && x.TransactionType == "Total" && b.OrgId == OrgId
        //                                 select new AccAccountsMapping { Id = x.Id, AccountId = x.AccountId, MappingSource = x.MappingSource, DebitOrCredit = x.DebitOrCredit, Description = x.Description, BranchId = x.BranchId }).ToList();
        //                if (GetOrganization(OrgId, masterData.BranchId, dbContext).DmAccMapping == "Separate")
        //                {
        //                    AccountsTotal = AccountsTotal.Where(x => x.BranchId == masterData.BranchId).ToList();
        //                }
        //            }
        //            //{
        //            //    var dtMappings = dbContext.AccAccountsMapping.Where(x=> x.MappingForm == "Sales Return" && x.TransactionType == "Total" && x.BranchId == masterData.BranchId);
        //            //    foreach (AccAccountsMapping item in dtMappings)
        //            //    {
        //            //        AccAccountsMapping mapping = new AccAccountsMapping();
        //            //        mapping.Id = Convert.ToInt32(item.Id);
        //            //        mapping.AccountId = Convert.ToInt32(item.AccountId);
        //            //        mapping.MappingSource = item.MappingSource.ToString();
        //            //        mapping.DebitOrCredit = item.DebitOrCredit.ToString();
        //            //        mapping.Description = item.Description.ToString();
        //            //        //mapping.Account = item.AccountName.ToString();
        //            //        AccountsTotal.Add(mapping);
        //            //    }
        //            //}
        //            //else
        //            //{
        //            //    //DataTable dtMappings = ObjSqlServerRepository.GetDataTable("select * from AccAccountsMapping as x inner join AccAccount as a on x.AccountId = a.id where x.MappingForm = 'Sales' and x.TransactionType = 'Total' and x.BranchId = '" + masterData.BranchId + "'");
        //            //    var dtMappings = dbContext.AccAccountsMapping.Where(x => x.MappingForm == "Sales" && x.TransactionType == "Total" && x.BranchId == masterData.BranchId);
        //            //    foreach (AccAccountsMapping item in dtMappings)

        //            //    {
        //            //        AccAccountsMapping mapping = new AccAccountsMapping();
        //            //        mapping.Id = Convert.ToInt32(item.Id);
        //            //        mapping.AccountId = Convert.ToInt32(item.AccountId);
        //            //        mapping.MappingSource = item.MappingSource.ToString();
        //            //        mapping.DebitOrCredit = item.DebitOrCredit.ToString();
        //            //        mapping.Description = item.Description.ToString();
        //            //        //mapping.Account = item.AccountName.ToString();
        //            //        AccountsTotal.Add(mapping);
        //            //    }
        //            //}
        //            foreach (var AccTotal in AccountsTotal)
        //            {
        //                if (AccTotal.MappingSource == "Items Sale")
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    //voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Items.Select(x => x.ItemSaleValue).Sum();
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Items.Select(x => x.ItemSaleValue).Sum();
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Items Cost")
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    //voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Items.Select(x => x.ItemCostValue).Sum();
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Items.Select(x => x.ItemCostValue).Sum();
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Items Discount")
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    //voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Items.Select(x => x.ItemDiscount).Sum();
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Items.Select(x => x.ItemDiscount).Sum();
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Items Tax")
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    //voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Items.Select(x => x.ItemTaxCharged).Sum();
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Items.Select(x => x.ItemTaxCharged).Sum();
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Items ExTax")
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    //voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Items.Select(x => x.ItemExtraTaxCharged).Sum();
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Items.Select(x => x.ItemExtraTaxCharged).Sum();
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Items TotalTax")
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    //voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Items.Select(x => x.ItemExtraTaxCharged + x.ItemTaxCharged).Sum();
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Items.Select(x => x.ItemExtraTaxCharged + x.ItemTaxCharged).Sum();
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Items Profit")
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    //voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Items.Select(x => x.ItemProfit).Sum();
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Items.Select(x => x.ItemProfit).Sum();
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Items Lost")
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    //voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Items.Select(x => x.ItemLost).Sum();
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Items.Select(x => x.ItemLost).Sum();
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Items Net")
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    //voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Items.Select(x => x.ItemNetTotal).Sum();
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Items.Select(x => x.ItemNetTotal).Sum();
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Discounts")
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    //voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = masterData.DiscountCalculated.HasValue? masterData.DiscountCalculated.Value:0;
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = masterData.DiscountCalculated.HasValue ? masterData.DiscountCalculated.Value : 0;
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Tax")
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    //voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = masterData.TaxCalculated.HasValue ? masterData.TaxCalculated.Value : 0;
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = masterData.TaxCalculated.HasValue ? masterData.TaxCalculated.Value : 0;
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Service Charges")
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    //voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = masterData.ServiceChargesCalculated.HasValue ? masterData.ServiceChargesCalculated.Value : 0;
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = masterData.ServiceChargesCalculated.HasValue ? masterData.ServiceChargesCalculated.Value : 0;
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Net Payable Credit" && masterData.PaymentType == "Credit" && masterData.CustomerId != 0)
        //                {
        //                    string CustomerName = "";
        //                    var Customer = dbContext.InvCustomer.FirstOrDefault(c=>c.Id == masterData.CustomerId);
        //                    if (Customer != null)
        //                    {
        //                        CustomerName = Customer.Name;
        //                    }
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description + " (" + CustomerName + ")";
        //                    //voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    voucherDetail.PartnerId = masterData.CustomerId.HasValue ? masterData.CustomerId.Value : 0;
        //                    voucherDetail.PartnerType = "Customer";
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = masterData.GrandTotal.HasValue ? masterData.GrandTotal.Value : 0;
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = masterData.GrandTotal.HasValue ? masterData.GrandTotal.Value : 0;
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Net Payable Cash" && masterData.PaymentType == "Cash")
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    //voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = masterData.GrandTotal.HasValue ? masterData.GrandTotal.Value : 0;
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = masterData.GrandTotal.HasValue ? masterData.GrandTotal.Value : 0;
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Net Payable CreditCard" && masterData.PaymentType == "Credit Card")
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    //voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = masterData.GrandTotal.HasValue ? masterData.GrandTotal.Value : 0;
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = masterData.GrandTotal.HasValue ? masterData.GrandTotal.Value : 0;
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //CustomLogging.Log("[DataUplodingService:(GetSaleJournal)]", ex.Message);
        //    }
        //    return Journal;
        //}
        private OrgOrganization GetOrganization(int OrgId, int BranchId, OrgOrganization OrgInfo, OrgBranch branch)
        {
           // var OrgInfo = new OrgOrganization();


            //OrgInfo = (from x in dbContext.OrgOrganization where x.Id == OrgId select x).FirstOrDefault();

            if (OrgInfo.UseInfoForAllBranches == false)
            {
                //List<OrgBranch> Branches = new List<OrgBranch>();
                //OrgBranch branch = new OrgBranch();

                //branch = (from x in dbContext.OrgBranch where x.Id == BranchId select x).FirstOrDefault();

                OrgInfo.OrgName = branch.BranchName;
                OrgInfo.OrgLogo = branch.BranchLogoName;
                OrgInfo.LongAddress = branch.LongAddress;
                OrgInfo.ShortAddress = branch.ShortAddress;
                OrgInfo.MobileNumber = branch.MobileNumber;
                OrgInfo.PhoneNumber = branch.PhoneNumber;
                OrgInfo.Email = branch.Email;
                OrgInfo.Website = branch.Website;
                OrgInfo.KpraNo = OrgInfo.KpraNo;

            }
            return OrgInfo;
        }
    }

}
