using AutoSynchAPI.Classes;
using AutoSynchAPI.Models;
using AutoSynchSqlServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AutoSynchAPI.Controllers
{
    public class PurchaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("SavePurchaseOrder")]
        [HttpPost]
        public IActionResult SavePurchaseOrder([FromBody] DataResponse dataResponse)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                
            using (Entities dbContext = new Entities())
            {
                OrgBranch orgBranch = dbContext.OrgBranch.FirstOrDefault(o => o.Id == dataResponse.BranchId);
                OrgOrganization orgOrganization = dbContext.OrgOrganization.FirstOrDefault(o => o.Id == orgBranch.OrgId);
                int? BranchId = orgBranch.Id;
                int OrgId = orgOrganization.Id;
                var FiscalYear = Utility.GetCurrentFiscalYear(dbContext, OrgId, (int)BranchId);



                //int? UserId = HttpContext.Session.GetInt32("UserId");


                //if (FiscalYear == null)
                //{
                //    result.Success = false;
                //    result.Message = "WARNING: No Fiscal Year is Defiend. Please contact Admininistor";
                //    return Json(result, new Newtonsoft.Json.JsonSerializerSettings());
                //}
                int oldId = 0;
                foreach (var masterData in dataResponse.invPurchaseOrderMasters)
                {
                    using (var transaction = dbContext.Database.BeginTransaction())
                    {
                        try
                        {


                            oldId = masterData.Id;
                            List<InvPurchaseOrderDetail> invPurchaseOrderDetails = dataResponse.invPurchaseOrderDetails.Where(d => d.MasterId == oldId).ToList();

                            if (masterData != null)
                            {
                                if (masterData.Id==0)//isnew
                                {
                                    //masterData.CreatedBy = (int)UserId;

                                    masterData.CreatedDate = DateTime.Now;
                                    //masterData.BranchId = (int)BranchId;
                                    masterData.FiscalYearId = FiscalYear.Id;
                                    masterData.Status = "Open";
                                    dbContext.InvPurchaseOrderMaster.Add(masterData);
                                    dbContext.SaveChanges();
                                    //result.Message = "Purchase Order Saved Successfully";
                                    //result.Success = true;
                                }
                                else
                                {
                                    var master = (from x in dbContext.InvPurchaseOrderMaster where x.Id == masterData.Id select x).FirstOrDefault();
                                    if (master != null)
                                    {
                                        //master.UpdatedBy = (int)UserId;
                                        master.UpdatedDate = DateTime.Now;
                                        master.VendorId = masterData.VendorId;
                                        master.Reference = masterData.Reference;
                                        master.OrderDateTime = masterData.OrderDateTime;
                                        master.DueDate = masterData.DueDate;
                                        master.WarehouseId = masterData.WarehouseId;
                                        master.TotalCost = masterData.TotalCost;
                                        dbContext.Entry(master);
                                        dbContext.SaveChanges();
                                        //result.Success = true;
                                        //result.Message = "Purchase Order Updated Successfully";
                                    }
                                }
                                if (masterData.Id > 0)//&& !masterData.IsNew
                                    {
                                    dbContext.InvPurchaseOrderDetail.RemoveRange(dbContext.InvPurchaseOrderDetail.Where(x => x.MasterId == masterData.Id));
                                    foreach (var item in invPurchaseOrderDetails)
                                    {
                                        //if (item.QtyOrdered == 0)
                                        //{
                                        //    result.Success = false;
                                        //    result.Message = "WARNING! The Item Qty Should not be Zero";
                                        //    transaction.Rollback();
                                        //    return Json(result, new Newtonsoft.Json.JsonSerializerSettings());
                                        //}
                                        var product = (from x in dbContext.InvProduct where x.Id == item.ProductId select x).FirstOrDefault();
                                        InvPurchaseOrderDetail detail = new InvPurchaseOrderDetail();
                                        detail.MasterId = masterData.Id;
                                        detail.ProductId = item.ProductId;
                                        detail.QtyOrdered = item.QtyOrdered;
                                        detail.UnitCost = item.UnitCost;
                                        detail.SalePrice = item.SalePrice;
                                        detail.UnitId = product.PurchaseUnitId.HasValue ? product.PurchaseUnitId.Value : 0;
                                        dbContext.InvPurchaseOrderDetail.Add(detail);
                                        dbContext.SaveChanges();

                                    }
                                }
                                //result.ReturnId = masterData.Id;
                                transaction.Commit();
                            }
                            //dbContext.InvPurchaseOrderMaster.Add(masterData);
                            //dbContext.SaveChanges();
                            //foreach (var detailData in invPurchaseOrderDetails)
                            //{
                            //    detailData.MasterId = masterData.Id;
                            //    dbContext.InvPurchaseOrderDetail.Add(detailData);
                            //    dbContext.SaveChanges();
                            //}
                        }

                        catch (Exception ex)
                        {
                            transaction.Rollback();

                        }
                    }
                    //transaction.Commit();

                }

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
        //[Route("SavePurchaseOrder")]
        //[HttpPost]
        //public IActionResult SavePurchase([FromBody] DataResponse dataResponse)
        //{
        //    ApiResponse apiResponse = new ApiResponse();
        //    int JvVoucherMasterId = 0;
        //    using (Entities dataContext = new Entities())
        //    {
        //        OrgBranch orgBranch = dataContext.OrgBranch.FirstOrDefault(o => o.Id == dataResponse.BranchId);
        //        OrgOrganization orgOrganization = dataContext.OrgOrganization.FirstOrDefault(o => o.Id == orgBranch.OrgId);
        //        int? BranchId = orgBranch.Id;
        //        int OrgId = orgOrganization.Id;
        //        bool IsUpdate = false;
        //        string DetailForMesage = "" + Environment.NewLine;
        //        //int UserId = (int)HttpContext.Session.GetInt32("UserId");

        //        var BranchDefaults = orgBranch;//common.GetBranchDefaultSettings(BranchId);
        //        var FiscalYear = Utility.GetPurchaseFiscalYear(dataContext, OrgId, BranchId.Value);
        //        if (FiscalYear == null)
        //        {
        //            apiResponse.Code = ApplicationResponse.GENERIC_ERROR_CODE;
        //            apiResponse.Message = "WARNING: No Fiscal Year is Defiend. Please contact Admininistor";
        //            return Ok(apiResponse);
        //            //return Json(result, new Newtonsoft.Json.JsonSerializerSettings());
        //        }
        //        //var FiscalYear = Utility.GetCurrentFiscalYear(dataContext, OrgId, (int)BranchId);
                
        //        foreach (var masterData in dataResponse.invPurchaseMasters)
        //                {
        //                    List<InvPurchaseDetail> purchaseDetails = dataResponse.invPurchaseDetails.Where(d=>d.MasterId==masterData.Id).ToList();
        //            using (var transaction = dataContext.Database.BeginTransaction())
        //            {
        //                //dataContext.Database.SetCommandTimeout(TimeSpan.FromMinutes(20));
        //                try
        //                {
        //                    //var masterData = (from x in orderItems where x.Flage == "master" select x).FirstOrDefault();
        //                    string vendorName = (from x in dataContext.InvVendor where x.Id == masterData.VendorId select x.Name).FirstOrDefault();
        //                    if (masterData != null)
        //                    {
        //                        List<AccVoucherDetail> accVoucherDetails = new List<AccVoucherDetail>();
        //                        if (Utility.GetOrganization(dataContext,OrgId,(int)BranchId).AccountIntegration == "Yes" && Utility.GetBranchDefaultSettings(dataContext, (int)BranchId).InvPurchaseAccInteg == "Bill")
        //                        {
        //                            if (masterData.InvoiceDate <= FiscalYear.StartDate || masterData.InvoiceDate >= FiscalYear.EndDate)
        //                            {
        //                                apiResponse.Code=ApplicationResponse.GENERIC_ERROR_CODE;
        //                                apiResponse.Message = "WARNING: The GRN Date should be Within Current Fiscal Year";
        //                                return Ok(apiResponse);
        //                                //return Json(result, new Newtonsoft.Json.JsonSerializerSettings());
        //                            }
        //                            //accVoucherDetails = GetPurchaseJournal(orderItems, masterData.IsReturn);
        //                            //if (masterData.JvFlage == 5)
        //                            //{
        //                            //    return Json(accVoucherDetails, new Newtonsoft.Json.JsonSerializerSettings());
        //                            //}
        //                            //int debitTotal = Convert.ToInt32(accVoucherDetails.Select(x => x.AmountDebit).Sum());
        //                            //int creditTotal = Convert.ToInt32(accVoucherDetails.Select(x => x.AmountCredit).Sum());
        //                            //if (accVoucherDetails.Count() == 0 || debitTotal != creditTotal || (debitTotal + creditTotal) == 0)
        //                            //{
        //                            //    result.Message = "Purchase Journal Debit and Credit are not Equal";
        //                            //    result.Success = false;
        //                            //    return Json(result, new Newtonsoft.Json.JsonSerializerSettings());
        //                            //}
        //                        }
        //                        if (masterData.Id == 0)
        //                        {
        //                            //masterData.CreatedBy = masterData.;
        //                            masterData.InvoiceNo = ((from x in dataContext.InvPurchaseMaster
        //                                                     join y in dataContext.OrgBranch on x.BranchId equals y.Id
        //                                                     where x.FiscalYearId == (BranchDefaults.AccPurchaseFiscalYearId == 0 ? FiscalYear.Id : BranchDefaults.AccPurchaseFiscalYearId) && y.OrgId == OrgId
        //                                                     select (int?)x.InvoiceNo).Max() ?? 0) + 1;
        //                            masterData.CreatedDate = DateTime.Now;
        //                            masterData.BranchId = (int)BranchId;
        //                            masterData.FiscalYearId = (BranchDefaults.AccPurchaseFiscalYearId == 0 ? FiscalYear.Id : BranchDefaults.AccPurchaseFiscalYearId);
        //                            masterData.Status = "Complete";
        //                            dataContext.InvPurchaseMaster.Add(masterData);
        //                            dataContext.SaveChanges();
        //                            apiResponse.Message = "Good Received Note Saved Successfully";
        //                            apiResponse.Code = message;
        //                        }
        //                        else
        //                        {
        //                            IsUpdate = true;
        //                            var master = (from x in dataContext.InvPurchaseMaster where x.Id == masterData.Id select x).FirstOrDefault();
        //                            if (master != null)
        //                            {
        //                                masterData.InvoiceNo = master.InvoiceNo;
        //                                master.UpdatedBy = masterData.UpdatedBy;
        //                                master.UpdatedDate = DateTime.Now;
        //                                master.VendorId = masterData.VendorId;
        //                                master.ReferenceId = masterData.ReferenceId;
        //                                master.InvoiceDate = masterData.InvoiceDate;
        //                                master.Frieght = masterData.Frieght;
        //                                master.Source = masterData.Source;
        //                                master.IsReturn = masterData.IsReturn;
        //                                master.InvoiceTotal = masterData.InvoiceTotal;
        //                                master.InvoiceDisc = masterData.InvoiceDisc;
        //                                master.WarehouseId = masterData.WarehouseId;
        //                                master.GatePassNo = masterData.GatePassNo;
        //                                master.BiltyNo = masterData.BiltyNo;
        //                                master.VehicleNo = masterData.VehicleNo;
        //                                master.DriverName = masterData.DriverName;
        //                                master.PaymentType = masterData.PaymentType;
        //                                master.WithholdingTaxInPer = masterData.WithholdingTaxInPer;
        //                                master.WithholdingTaxInAmount = masterData.WithholdingTaxInAmount;
        //                                master.GrandTotalBeforeWhTax = masterData.GrandTotalBeforeWhTax;
        //                                master.GrandTotal = masterData.GrandTotal;
        //                                master.Remarks = masterData.Remarks;
        //                                master.LoadingCharges = masterData.LoadingCharges;
        //                                master.OtherCharges = masterData.OtherCharges;
        //                                master.AdvanceTaxAmount = masterData.AdvanceTaxAmount;
        //                                dataContext.Entry(master);
        //                                dataContext.SaveChanges();
        //                                apiResponse.Code= true;
        //                                apiResponse.Message = "Good Received Note Updated Successfully";
        //                            }
        //                        }
        //                        if (masterData.Id > 0)
        //                        {
        //                            dataContext.InvPurchaseDetail.RemoveRange(dataContext.InvPurchaseDetail.Where(x => x.MasterId == masterData.Id));
        //                            if (masterData.IsReturn == true)
        //                            {
        //                                dataContext.InvProductLedger.RemoveRange(dataContext.InvProductLedger.Where(x => x.ReferenceId == masterData.Id && x.Source == "Purchase Return"));
        //                            }
        //                            else
        //                            {
        //                                dataContext.InvProductLedger.RemoveRange(dataContext.InvProductLedger.Where(x => x.ReferenceId == masterData.Id && x.Source == "Purchase"));
        //                            }
        //                            foreach (var item in orderItems.Where(x => x.Flage == "items"))
        //                            {
        //                                item.ItemId = item.ProductId;
        //                                var product = (from x in dataContext.Products where x.Id == item.ProductId select x).FirstOrDefault();
        //                                DetailForMesage = DetailForMesage + product.Name + ", Qty " + item.Qty + Environment.NewLine;
        //                                if (item.Qty == 0)
        //                                {
        //                                    result.Success = false;
        //                                    result.Message = "WARNING! The Item Qty Should not be Zero";
        //                                    transaction.Rollback();
        //                                    return Json(result, new Newtonsoft.Json.JsonSerializerSettings());
        //                                }
        //                                decimal NewCost = 0;
        //                                var ProductUnitsInfo = (from x in dataContext.Products
        //                                                        join y in dataContext.InvCategories on x.CategoryId equals y.Id
        //                                                        join z in dataContext.Units on x.PurchaseUnitId equals z.Id
        //                                                        join s in dataContext.Units on x.SaleUnitId equals s.Id
        //                                                        where x.Id == item.ItemId
        //                                                        select new InvProductVm
        //                                                        {
        //                                                            Id = x.Id,
        //                                                            PurchaseUnitId = x.PurchaseUnitId,
        //                                                            SaleUnitId = s.Id,
        //                                                            PurchaseUnitOfConversion = z.UnitOfConversion,
        //                                                            SaleUnitOfConversion = s.UnitOfConversion
        //                                                        }).FirstOrDefault();
        //                                var Product = (from x in dataContext.Products where x.Id == item.ItemId select x).FirstOrDefault();
        //                                if (Product.Type == "Material")
        //                                {
        //                                    var mixture = (from x in dataContext.Products where x.Id == Product.MixtureId select x).FirstOrDefault();
        //                                    if (mixture != null)
        //                                    {
        //                                        Product = mixture;
        //                                        item.ItemId = mixture.Id;

        //                                    }
        //                                }
        //                                if (masterData.IsReturn == false)
        //                                {
        //                                    if (Product != null && common.GetBranchDefaultSettings((int)BranchId).InvUpdateLastPrices == "Yes")
        //                                    {
        //                                        if (item.Qty != 0)
        //                                        {
        //                                            decimal SOH = (from x in dataContext.ProductLedgers
        //                                                           join y in dataContext.Units on x.UnitId equals y.Id
        //                                                           where x.FiscalYearId == FiscalYear.Id && x.WarehouseId == masterData.WarehouseId && x.ProductId == item.ItemId && x.IsCancel != true
        //                                                           select (x.QtyIn * y.UnitOfConversion) / ProductUnitsInfo.SaleUnitOfConversion - (x.QtyOut * y.UnitOfConversion) / ProductUnitsInfo.SaleUnitOfConversion).Sum();
        //                                            if (SOH < 0)
        //                                            {
        //                                                SOH = SOH * (-1);
        //                                            }
        //                                            decimal OldAmount = SOH * Product.AverageCost;
        //                                            decimal TotalQty = SOH + (item.Qty * ProductUnitsInfo.PurchaseUnitOfConversion / ProductUnitsInfo.SaleUnitOfConversion);
        //                                            decimal TotalAmount = OldAmount + ((((item.CostPrice * item.Qty) - item.Disc) / item.Qty)) * item.Qty;
        //                                            NewCost = Math.Round(TotalAmount / TotalQty, 6);
        //                                            Product.AverageCost = NewCost;
        //                                            Product.Cost = item.CostPrice;
        //                                            Product.CostIncTax = item.CostIncTax;
        //                                            Product.RetailPrice = (item.RetailPrice / ProductUnitsInfo.PurchaseUnitOfConversion) * ProductUnitsInfo.SaleUnitOfConversion;
        //                                            Product.Tax = item.SaleTaxInPercent;
        //                                            Product.TaxValue = (item.SaleTaxInPercent / (100 + item.SaleTaxInPercent)) * Product.RetailPrice;
        //                                            dataContext.Entry(Product);
        //                                            dataContext.SaveChanges();
        //                                        }
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    if (Product != null && common.GetBranchDefaultSettings((int)BranchId).InvUpdateLastPrices == "Yes")
        //                                    {
        //                                        if (item.Qty != 0)
        //                                        {
        //                                            item.Qty = item.Qty * (-1);
        //                                            decimal SOH = (from x in dataContext.ProductLedgers
        //                                                           join y in dataContext.Units on x.UnitId equals y.Id
        //                                                           where x.FiscalYearId == FiscalYear.Id && x.WarehouseId == masterData.WarehouseId && x.ProductId == item.ItemId && x.IsCancel != true
        //                                                           select (x.QtyIn * y.UnitOfConversion) / ProductUnitsInfo.SaleUnitOfConversion - (x.QtyOut * y.UnitOfConversion) / ProductUnitsInfo.SaleUnitOfConversion).Sum();
        //                                            decimal OldAmount = SOH * Product.AverageCost;
        //                                            decimal TotalQty = SOH + (item.Qty * ProductUnitsInfo.PurchaseUnitOfConversion / ProductUnitsInfo.SaleUnitOfConversion);
        //                                            decimal TotalAmount = OldAmount + ((((item.CostPrice * item.Qty) - item.Disc) / item.Qty)) * item.Qty;
        //                                            NewCost = Math.Round(TotalAmount / TotalQty, 6);
        //                                            Product.AverageCost = NewCost;
        //                                            Product.Cost = item.CostPrice;
        //                                            Product.RetailPrice = (item.RetailPrice / ProductUnitsInfo.PurchaseUnitOfConversion) * ProductUnitsInfo.SaleUnitOfConversion;
        //                                            Product.CostIncTax = item.CostIncTax;
        //                                            item.Qty = item.Qty * (-1);
        //                                            Product.Tax = item.SaleTaxInPercent;
        //                                            Product.TaxValue = (item.SaleTaxInPercent / (100 + item.SaleTaxInPercent)) * Product.RetailPrice;
        //                                            dataContext.Entry(Product);
        //                                            dataContext.SaveChanges();
        //                                        }
        //                                    }
        //                                }
        //                                InvPurchaseDetail detail = new InvPurchaseDetail();
        //                                detail.MasterId = masterData.Id;
        //                                detail.ProductId = item.ItemId;
        //                                detail.Qty = item.Qty;
        //                                detail.CutQty = item.CutQty;
        //                                detail.CostPrice = item.CostPrice;
        //                                detail.AverageCost = NewCost;
        //                                detail.RetailPrice = item.RetailPrice;
        //                                detail.UnitId = ProductUnitsInfo.PurchaseUnitId;
        //                                detail.Disc = item.Disc;
        //                                detail.TaxAmount = item.TaxAmount;
        //                                detail.AdditionalTaxAmount = item.AdditionalTaxAmount;
        //                                detail.SaleTaxInPercent = item.ItemWiseSaleTaxTotal;
        //                                dataContext.PurchaseDetails.Add(detail);
        //                                dataContext.SaveChanges();
        //                                var purchase = (from x in dataContext.PurchaseMasters where x.Id == masterData.Id select x).FirstOrDefault();
        //                                if (Product.Type != "Service")
        //                                {
        //                                    InvProductLedger ledger = new InvProductLedger();
        //                                    ledger.ProductId = item.ItemId;
        //                                    ledger.TransDate = masterData.InvoiceDate;
        //                                    ledger.MaterialId = item.ProductId;
        //                                    ledger.Remarks = "Purchase of Product " + ProductUnitsInfo.Name + ", Qty: " + item.Qty;
        //                                    ledger.Source = "Purchase";
        //                                    ledger.QtyIn = item.Qty;
        //                                    ledger.QtyOut = 0;
        //                                    ledger.QtyCut = item.CutQty;
        //                                    ledger.Cost = ((item.CostPrice * item.Qty) - item.Disc) / item.Qty;
        //                                    ledger.AverageCost = Product.AverageCost;
        //                                    ledger.RetailPrice = item.RetailPrice;
        //                                    ledger.SourceParty = vendorName;
        //                                    if (masterData.IsReturn == true)
        //                                    {
        //                                        ledger.Remarks = "Purchase Return of Product " + ProductUnitsInfo.Name + ", Qty: " + item.Qty;
        //                                        ledger.Source = "Purchase Return";
        //                                        ledger.QtyIn = 0;
        //                                        ledger.QtyOut = item.Qty;
        //                                    }
        //                                    ledger.ReferenceId = masterData.Id;
        //                                    ledger.BranchId = purchase.BranchId;
        //                                    ledger.FiscalYearId = (BranchDefaults.AccPurchaseFiscalYearId == 0 ? FiscalYear.Id : BranchDefaults.AccPurchaseFiscalYearId);
        //                                    ledger.CreatedBy = purchase.CreatedBy;
        //                                    ledger.CreatedDate = purchase.CreatedDate;
        //                                    ledger.UnitId = ProductUnitsInfo.PurchaseUnitId;
        //                                    ledger.WarehouseId = masterData.WarehouseId;
        //                                    dataContext.ProductLedgers.Add(ledger);
        //                                    dataContext.SaveChanges();
        //                                }
        //                                result.Success = true;
        //                            }
        //                        }
        //                        result.ReturnId = masterData.Id;
        //                        if (masterData.ReferenceId != 0)
        //                        {
        //                            decimal TotalOrderedQty = (from x in dataContext.PurchaseOrderDetails where x.MasterId == masterData.ReferenceId select x.QtyOrdered).DefaultIfEmpty().Sum();
        //                            decimal TotalReceivedQty = (from x in dataContext.PurchaseMasters
        //                                                        join y in dataContext.PurchaseDetails on x.Id equals y.MasterId
        //                                                        where x.ReferenceId == masterData.ReferenceId
        //                                                        select y.Qty).DefaultIfEmpty().Sum();
        //                            if (TotalReceivedQty >= TotalOrderedQty)
        //                            {
        //                                var order = (from x in dataContext.PurchaseOrderMasters where x.Id == masterData.ReferenceId select x).FirstOrDefault();
        //                                if (order != null)
        //                                {
        //                                    order.Status = "Closed";
        //                                    dataContext.Entry(order);
        //                                    dataContext.SaveChanges();
        //                                }
        //                            }
        //                        }
        //                        //inserting jv transactions
        //                        if (common.GetOrganization().AccountIntegration == "Yes" && common.GetBranchDefaultSettings((int)BranchId).InvPurchaseAccInteg == "Bill")
        //                        {
        //                            if (masterData != null)
        //                            {
        //                                string VendorName = "";
        //                                if (dataContext.VoucherMasters.Any(x => x.VoucherType == "PUR" && x.ReferenceId == masterData.Id))
        //                                {
        //                                    var Jvmaster = (from x in dataContext.VoucherMasters where x.ReferenceId == masterData.Id && x.VoucherType == "PUR" select x).FirstOrDefault();
        //                                    if (Jvmaster != null)
        //                                    {
        //                                        Jvmaster.VoucherNo = masterData.InvoiceNo.ToString();
        //                                        Jvmaster.VoucherDate = masterData.InvoiceDate;
        //                                        Jvmaster.LastUpdatedBy = UserId;
        //                                        Jvmaster.LastUpdatedDate = DateTime.Now;
        //                                        Jvmaster.VoucherStatus = "Approved";
        //                                        if (masterData.VendorId != 0)
        //                                        {
        //                                            VendorName = (from x in dataContext.Vendors where x.Id == masterData.VendorId select x.Name).FirstOrDefault();
        //                                            if (VendorName != "" && VendorName != null)
        //                                            {
        //                                                VendorName = VendorName + " - ";
        //                                            }
        //                                        }
        //                                        if (masterData.Remarks != "" && masterData.Remarks != null)
        //                                        {
        //                                            Jvmaster.Description = masterData.Remarks + " (" + VendorName + masterData.PaymentType + ")";
        //                                        }
        //                                        else
        //                                        {
        //                                            Jvmaster.Description = VendorName + masterData.PaymentType;
        //                                        }
        //                                        dataContext.Entry(Jvmaster);
        //                                        dataContext.SaveChanges();
        //                                        JvVoucherMasterId = Jvmaster.Id;
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    AccVoucherMaster accVoucherMaster = new AccVoucherMaster();
        //                                    accVoucherMaster.VoucherNo = masterData.InvoiceNo.ToString();
        //                                    accVoucherMaster.CreatedBy = UserId;
        //                                    accVoucherMaster.VoucherDate = masterData.InvoiceDate;
        //                                    accVoucherMaster.CreatedDate = DateTime.Now;
        //                                    accVoucherMaster.BranchId = BranchId;
        //                                    accVoucherMaster.FiscalYearId = (BranchDefaults.AccPurchaseFiscalYearId == 0 ? FiscalYear.Id : BranchDefaults.AccPurchaseFiscalYearId);
        //                                    accVoucherMaster.VoucherType = "PUR";
        //                                    accVoucherMaster.VoucherStatus = "Approved";
        //                                    accVoucherMaster.ReferenceId = masterData.Id;
        //                                    if (masterData.VendorId != 0)
        //                                    {
        //                                        VendorName = (from x in dataContext.Vendors where x.Id == masterData.VendorId select x.Name).FirstOrDefault();
        //                                        if (VendorName != "" && VendorName != null)
        //                                        {
        //                                            VendorName = VendorName + " - ";
        //                                        }
        //                                    }
        //                                    if (masterData.Remarks != "" && masterData.Remarks != null)
        //                                    {
        //                                        accVoucherMaster.Description = masterData.Remarks + " (" + VendorName + masterData.PaymentType + ")";
        //                                    }
        //                                    else
        //                                    {
        //                                        accVoucherMaster.Description = VendorName + masterData.PaymentType;
        //                                    }
        //                                    dataContext.VoucherMasters.Add(accVoucherMaster);
        //                                    dataContext.SaveChanges();
        //                                    JvVoucherMasterId = accVoucherMaster.Id;
        //                                    //if (voucherNumbering != null)
        //                                    //{
        //                                    //    voucherNumbering.CurrentNo = voucherNumbering.CurrentNo + 1;
        //                                    //    dataContext.Entry(voucherNumbering);
        //                                    //    dataContext.SaveChanges();
        //                                    //}
        //                                }
        //                                dataContext.VoucherDetails.RemoveRange(dataContext.VoucherDetails.Where(x => x.VoucherMasterId == JvVoucherMasterId));
        //                                foreach (var detail in accVoucherDetails)
        //                                {
        //                                    detail.VoucherMasterId = JvVoucherMasterId;
        //                                    detail.Type = "Detail";
        //                                    dataContext.VoucherDetails.Add(detail);
        //                                    dataContext.SaveChanges();
        //                                }
        //                            }
        //                        }
        //                    }
        //                    if (common.GetBranchDefaultSettings((int)BranchId).InvIsEnableSmsOnPurchase)
        //                    {
        //                        try
        //                        {
        //                            if (masterData.DriverContactNo != "" && masterData.IsReturn == false && IsUpdate == false)
        //                            {
        //                                string Customer = "";
        //                                string VehicleNo = "";
        //                                if (masterData.VendorId != 0)
        //                                {
        //                                    var customerInfo = (from x in dataContext.Vendors where x.Id == masterData.VendorId select x).FirstOrDefault();
        //                                    if (customerInfo != null)
        //                                    {
        //                                        Customer = customerInfo.Name;
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    Customer = masterData.PaymentType;
        //                                }
        //                                if (masterData.VendorId != 0)
        //                                {
        //                                    var Vehicle = (from x in dataContext.Vehicles where x.Id == masterData.VendorId select x).FirstOrDefault();
        //                                    if (Vehicle != null)
        //                                    {
        //                                        VehicleNo = Vehicle.VehicleNo;
        //                                    }
        //                                }
        //                                var OrgInfo = common.GetOrganizationById(OrgId);
        //                                string message = "Dear," + Environment.NewLine + "Vehicle#: " + VehicleNo + Environment.NewLine + Environment.NewLine + Environment.NewLine + "Bill# Order: " + masterData.BiltyNo + Environment.NewLine + "Payment Type: " + masterData.PaymentType + Environment.NewLine + "Total Bill: " + masterData.GrandTotal + Environment.NewLine;
        //                                message = message + DetailForMesage + Environment.NewLine + "Thank You " + OrgInfo.OrgShortName + "Ph # " + OrgInfo.PhoneNumber;
        //                                try
        //                                {
        //                                    common.SendSMS(masterData.DriverContactNo, message);
        //                                }
        //                                catch (Exception ex)
        //                                {
        //                                    result.Message = ex.Message;
        //                                    if (ex.InnerException != null)
        //                                    {
        //                                        result.Message = ex.InnerException.Message;
        //                                    }
        //                                    CustomLogging.InsertErrorLog("Purchase", "sendSMS", ex.Message, dataContext);
        //                                }
        //                            }
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            CustomLogging.InsertErrorLog("Purchase", "MessageOnSavePurchase", ex.Message, dataContext);
        //                        }
        //                    }
        //                    transaction.Commit();
        //                }
        //                catch (Exception ex)
        //                {
        //                    transaction.Rollback();
        //                    result.Message = ex.Message;
        //                    if (ex.InnerException != null)
        //                    {
        //                        result.Message = ex.InnerException.Message;
        //                    }
        //                    result.Success = false;
        //                    CustomLogging.InsertErrorLog("Purchase", "SavePurchase", ex.Message, dataContext);
        //                }
        //            }


        //        }


        //    }
        //    return Ok(apiResponse); //Json(result, new Newtonsoft.Json.JsonSerializerSettings());
        //}

        //private List<AccVoucherDetail> GetPurchaseJournal(Entities dataContext, InvPurchaseMaster masterData, bool IsReturn)
        //{
        //    List<AccVoucherDetail> Journal = new List<AccVoucherDetail>();
        //    try
        //    {
        //        int OrgId = 0;
        //        int BranchId = 0;
        //        //var masterData = (from x in purchaseData where x.flag== "master" select x).FirstOrDefault();
        //        var Items = (from x in purchaseData where x.Flage != "master" select x).ToList();
        //        if (masterData != null)
        //        {
        //            foreach (var item in Items)
        //            {
        //                item.ItemId = item.ProductId;
        //                var Product = (from x in dataContext.InvProduct where x.Id == item.ItemId select x).FirstOrDefault();
        //                item.ProductName = Product.Name;
        //                if (Product.SaleTaxCalMethodInPur == "Sheddule")
        //                {
        //                    item.ItemCostValue = item.CostPrice * (item.Qty - item.CutQty) - item.Disc;
        //                    item.ItemDisc = 0;
        //                }
        //                else
        //                {
        //                    item.ItemCostValue = item.CostPrice * (item.Qty - item.CutQty);
        //                    item.ItemDisc = item.Disc;
        //                }
        //                item.ItemCutValue = item.CostPrice * item.CutQty;
        //                item.ItemNetAmount = item.ItemCostValue - item.Disc + item.TaxAmount + item.AdditionalTaxAmount;
        //                if (Product.Type == "Material")
        //                {
        //                    var mixture = (from x in dataContext.Products where x.Id == Product.MixtureId select x).FirstOrDefault();
        //                    if (mixture != null)
        //                    {
        //                        item.ItemId = mixture.Id;
        //                    }
        //                }
        //            }
        //            List<AccAccountsMapping> AccountsDetails = new List<AccAccountsMapping>();
        //            if (IsReturn)
        //            {
        //                AccountsDetails = (from x in dataContext.AccountsMappings
        //                                   join a in dataContext.Accounts on x.AccountId equals a.Id
        //                                   join b in dataContext.Branches on x.BranchId equals b.Id
        //                                   where x.MappingForm == "Purchase Return" && x.TransactionType == "Line" && b.OrgId == OrgId
        //                                   select new AccAccountsMapping { Id = x.Id, AccountId = x.AccountId, MappingSource = x.MappingSource, DebitOrCredit = x.DebitOrCredit, Description = x.Description, Account = a.AccountName, BranchId = x.BranchId }).ToList();
        //                if (common.GetOrganization().DmAccMapping == "Separate")
        //                {
        //                    AccountsDetails = AccountsDetails.Where(x => x.BranchId == BranchId).ToList();
        //                }
        //            }
        //            else
        //            {
        //                AccountsDetails = (from x in dataContext.AccountsMappings
        //                                   join a in dataContext.Accounts on x.AccountId equals a.Id
        //                                   join b in dataContext.Branches on x.BranchId equals b.Id
        //                                   where x.MappingForm == "Purchase" && x.TransactionType == "Line" && b.OrgId == OrgId
        //                                   select new AccAccountsMapping { Id = x.Id, AccountId = x.AccountId, MappingSource = x.MappingSource, DebitOrCredit = x.DebitOrCredit, Description = x.Description, Account = a.AccountName, BranchId = x.BranchId }).ToList();
        //                if (common.GetOrganization().DmAccMapping == "Separate")
        //                {
        //                    AccountsDetails = AccountsDetails.Where(x => x.BranchId == BranchId).ToList();
        //                }
        //            }
        //            foreach (var map in AccountsDetails)
        //            {
        //                if (map.MappingSource == "Items Cost")
        //                {
        //                    foreach (var item in Items)
        //                    {
        //                        AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                        voucherDetail.AccountId = map.AccountId;
        //                        voucherDetail.Description = map.Description + " (" + item.ProductName + ")";
        //                        voucherDetail.PartnerId = item.ItemId;
        //                        voucherDetail.PartnerType = "Product";
        //                        voucherDetail.AccountName = map.Account;
        //                        voucherDetail.MappingSource = map.MappingSource;
        //                        if (map.DebitOrCredit == "Debit")
        //                        {
        //                            voucherDetail.AmountDebit = Math.Round(item.ItemCostValue, 2);
        //                            voucherDetail.AmountCredit = 0;
        //                        }
        //                        else
        //                        {
        //                            voucherDetail.AmountDebit = 0;
        //                            voucherDetail.AmountCredit = Math.Round(item.ItemCostValue, 2);
        //                        }
        //                        Journal.Add(voucherDetail);
        //                    }
        //                }
        //                else if (map.MappingSource == "Items Discount")
        //                {
        //                    foreach (var item in Items.Where(x => x.ItemDisc > 0))
        //                    {
        //                        AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                        voucherDetail.AccountId = map.AccountId;
        //                        voucherDetail.Description = map.Description + " (" + item.ProductName + ")";
        //                        voucherDetail.PartnerId = item.ItemId;
        //                        voucherDetail.PartnerType = "Product";
        //                        voucherDetail.AccountName = map.Account;
        //                        voucherDetail.MappingSource = map.MappingSource;
        //                        if (map.DebitOrCredit == "Debit")
        //                        {
        //                            voucherDetail.AmountDebit = Math.Round(item.ItemDisc, 2);
        //                            voucherDetail.AmountCredit = 0;
        //                        }
        //                        else
        //                        {
        //                            voucherDetail.AmountDebit = 0;
        //                            voucherDetail.AmountCredit = Math.Round(item.ItemDisc, 2);
        //                        }
        //                        Journal.Add(voucherDetail);
        //                    }
        //                }
        //                else if (map.MappingSource == "Items Cut")
        //                {
        //                    foreach (var item in Items.Where(x => x.ItemCutValue > 0))
        //                    {
        //                        AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                        voucherDetail.AccountId = map.AccountId;
        //                        voucherDetail.Description = map.Description + " (" + item.ProductName + ")";
        //                        voucherDetail.PartnerId = item.ItemId;
        //                        voucherDetail.PartnerType = "Product";
        //                        voucherDetail.AccountName = map.Account;
        //                        voucherDetail.MappingSource = map.MappingSource;
        //                        if (map.DebitOrCredit == "Debit")
        //                        {
        //                            voucherDetail.AmountDebit = Math.Round(item.ItemCutValue, 2);
        //                            voucherDetail.AmountCredit = 0;
        //                        }
        //                        else
        //                        {
        //                            voucherDetail.AmountDebit = 0;
        //                            voucherDetail.AmountCredit = Math.Round(item.ItemCutValue, 2);
        //                        }
        //                        Journal.Add(voucherDetail);
        //                    }
        //                }
        //                else if (map.MappingSource == "Items Tax")
        //                {
        //                    foreach (var item in Items.Where(x => x.TaxAmount > 0))
        //                    {
        //                        var Product = (from x in dataContext.Products where x.Id == item.ItemId select x).FirstOrDefault();
        //                        AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                        voucherDetail.AccountId = map.AccountId;
        //                        voucherDetail.Description = map.Description + " (" + Product.Name + ")";
        //                        voucherDetail.PartnerId = item.ItemId;
        //                        voucherDetail.PartnerType = "Product";
        //                        voucherDetail.AccountName = map.Account;
        //                        voucherDetail.MappingSource = map.MappingSource;
        //                        if (map.DebitOrCredit == "Debit")
        //                        {
        //                            voucherDetail.AmountDebit = Math.Round(item.TaxAmount, 2);
        //                            voucherDetail.AmountCredit = 0;
        //                        }
        //                        else
        //                        {
        //                            voucherDetail.AmountDebit = 0;
        //                            voucherDetail.AmountCredit = Math.Round(item.TaxAmount, 2);
        //                        }
        //                        Journzdal.Add(voucherDetail);
        //                    }
        //                }
        //                else if (map.MappingSource == "Items ExTax")
        //                {
        //                    foreach (var item in Items.Where(x => x.AdditionalTaxAmount > 0))
        //                    {
        //                        var Product = (from x in dataContext.Products where x.Id == item.ItemId select x).FirstOrDefault();
        //                        AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                        voucherDetail.AccountId = map.AccountId;
        //                        voucherDetail.Description = map.Description + " (" + Product.Name + ")";
        //                        voucherDetail.PartnerId = item.ItemId;
        //                        voucherDetail.PartnerType = "Product";
        //                        voucherDetail.AccountName = map.Account;
        //                        voucherDetail.MappingSource = map.MappingSource;
        //                        if (map.DebitOrCredit == "Debit")
        //                        {
        //                            voucherDetail.AmountDebit = Math.Round(item.AdditionalTaxAmount, 2);
        //                            voucherDetail.AmountCredit = 0;
        //                        }
        //                        else
        //                        {
        //                            voucherDetail.AmountDebit = 0;
        //                            voucherDetail.AmountCredit = Math.Round(item.AdditionalTaxAmount, 2);
        //                        }
        //                        Journal.Add(voucherDetail);
        //                    }
        //                }
        //                else if (map.MappingSource == "Items TotalTax")
        //                {
        //                    foreach (var item in Items.Where(x => x.TaxAmount + x.AdditionalTaxAmount > 0))
        //                    {
        //                        var Product = (from x in dataContext.Products where x.Id == item.ItemId select x).FirstOrDefault();
        //                        AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                        voucherDetail.AccountId = map.AccountId;
        //                        voucherDetail.Description = map.Description + " (" + Product.Name + ")";
        //                        voucherDetail.PartnerId = item.ItemId;
        //                        voucherDetail.PartnerType = "Product";
        //                        voucherDetail.AccountName = map.Account;
        //                        voucherDetail.MappingSource = map.MappingSource;
        //                        if (map.DebitOrCredit == "Debit")
        //                        {
        //                            voucherDetail.AmountDebit = Math.Round(item.TaxAmount + item.AdditionalTaxAmount, 2);
        //                            voucherDetail.AmountCredit = 0;
        //                        }
        //                        else
        //                        {
        //                            voucherDetail.AmountDebit = 0;
        //                            voucherDetail.AmountCredit = Math.Round(item.TaxAmount + item.AdditionalTaxAmount, 2);
        //                        }
        //                        Journal.Add(voucherDetail);
        //                    }
        //                }
        //                else if (map.MappingSource == "Items Net")
        //                {
        //                    foreach (var item in Items.Where(x => x.ItemNetAmount > 0))
        //                    {
        //                        var Product = (from x in dataContext.Products where x.Id == item.ItemId select x).FirstOrDefault();
        //                        AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                        voucherDetail.PartnerId = item.ItemId;
        //                        voucherDetail.PartnerType = "Product";
        //                        voucherDetail.Description = map.Description + " (" + Product.Name + ")";
        //                        voucherDetail.ProductId = item.ItemId;
        //                        voucherDetail.AccountName = map.Account;
        //                        voucherDetail.MappingSource = map.MappingSource;
        //                        if (map.DebitOrCredit == "Debit")
        //                        {
        //                            voucherDetail.AmountDebit = Math.Round(item.ItemNetAmount, 2);
        //                            voucherDetail.AmountCredit = 0;
        //                        }
        //                        else
        //                        {
        //                            voucherDetail.AmountDebit = 0;
        //                            voucherDetail.AmountCredit = Math.Round(item.ItemNetAmount, 2);
        //                        }
        //                        Journal.Add(voucherDetail);
        //                    }
        //                }
        //            }
        //            List<AccAccountsMapping> AccountsTotal = new List<AccAccountsMapping>();
        //            if (IsReturn)
        //            {
        //                AccountsTotal = (from x in dataContext.AccountsMappings
        //                                 join a in dataContext.Accounts on x.AccountId equals a.Id
        //                                 join b in dataContext.Branches on x.BranchId equals b.Id
        //                                 where x.MappingForm == "Purchase Return" && x.TransactionType == "Total" && b.OrgId == OrgId
        //                                 select new AccAccountsMapping { Id = x.Id, AccountId = x.AccountId, MappingSource = x.MappingSource, DebitOrCredit = x.DebitOrCredit, Description = x.Description, Account = a.AccountName, BranchId = x.BranchId }).ToList();
        //                if (common.GetOrganization().DmAccMapping == "Separate")
        //                {
        //                    AccountsTotal = AccountsTotal.Where(x => x.BranchId == BranchId).ToList();
        //                }
        //            }
        //            else
        //            {
        //                AccountsTotal = (from x in dataContext.AccountsMappings
        //                                 join a in dataContext.Accounts on x.AccountId equals a.Id
        //                                 join b in dataContext.Branches on x.BranchId equals b.Id
        //                                 where x.MappingForm == "Purchase" && x.TransactionType == "Total" && b.OrgId == OrgId
        //                                 select new AccAccountsMapping { Id = x.Id, AccountId = x.AccountId, MappingSource = x.MappingSource, DebitOrCredit = x.DebitOrCredit, Description = x.Description, Account = a.AccountName, BranchId = x.BranchId }).ToList();
        //                if (common.GetOrganization().DmAccMapping == "Separate")
        //                {
        //                    AccountsTotal = AccountsTotal.Where(x => x.BranchId == BranchId).ToList();
        //                }
        //            }
        //            foreach (var AccTotal in AccountsTotal)
        //            {
        //                if (AccTotal.MappingSource == "Items Cost" && Items.Select(x => x.ItemCostValue).Sum() > 0)
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Math.Round(Items.Select(x => x.ItemCostValue).Sum(), 2);
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Math.Round(Items.Select(x => x.ItemCostValue).Sum(), 2);
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Items Cut" && Items.Select(x => x.ItemCutValue).Sum() > 0)
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Math.Round(Items.Select(x => x.ItemCutValue).Sum(), 2);
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Math.Round(Items.Select(x => x.ItemCutValue).Sum(), 2);
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Items Discount" && Items.Select(x => x.ItemDisc).Sum() > 0)
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Math.Round(Items.Select(x => x.ItemDisc).Sum(), 2);
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Math.Round(Items.Select(x => x.ItemDisc).Sum(), 2);
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Items Tax" && Items.Select(x => x.TaxAmount).Sum() > 0)
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Math.Round(Items.Select(x => x.TaxAmount).Sum(), 2);
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Math.Round(Items.Select(x => x.TaxAmount).Sum(), 2);
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Items ExTax" && Items.Select(x => x.AdditionalTaxAmount).Sum() > 0)
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Math.Round(Items.Select(x => x.AdditionalTaxAmount).Sum(), 2);
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Math.Round(Items.Select(x => x.AdditionalTaxAmount).Sum(), 2);
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Items TotalTax" && Items.Select(x => x.TaxAmount + x.AdditionalTaxAmount).Sum() > 0)
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Math.Round(Items.Select(x => x.TaxAmount + x.AdditionalTaxAmount).Sum(), 2);
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Math.Round(Items.Select(x => x.TaxAmount + x.AdditionalTaxAmount).Sum(), 2);
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Items Net" && Items.Select(x => x.ItemNetAmount).Sum() > 0)
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Math.Round(Items.Select(x => x.ItemNetAmount).Sum(), 2);
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Math.Round(Items.Select(x => x.ItemNetAmount).Sum(), 2);
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Discounts" && masterData.InvoiceDisc != 0)
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Math.Round(masterData.InvoiceDisc, 2);
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Math.Round(masterData.InvoiceDisc, 2);
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Withholding Tax" && masterData.WithholdingTaxInAmount != 0)
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Math.Round(masterData.WithholdingTaxInAmount, 2);
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Math.Round(masterData.WithholdingTaxInAmount, 2);
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Other" && masterData.OtherCharges != 0)
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Math.Round(masterData.OtherCharges, 2);
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Math.Round(masterData.OtherCharges, 2);
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Freight" && masterData.Frieght != 0)
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Math.Round(masterData.Frieght, 2);
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Math.Round(masterData.Frieght, 2);
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Loading" && masterData.LoadingCharges != 0)
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Math.Round(masterData.LoadingCharges, 2);
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Math.Round(masterData.LoadingCharges, 2);
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Advance Tax" && masterData.AdvanceTaxAmount != 0)
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Math.Round(masterData.AdvanceTaxAmount, 2);
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Math.Round(masterData.AdvanceTaxAmount, 2);
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Net Payable Credit" && masterData.PaymentType == "Credit" && masterData.VendorId != 0)
        //                {
        //                    string VendorName = "";
        //                    var vendor = (from x in dataContext.Vendors where x.Id == masterData.VendorId select x).FirstOrDefault();
        //                    if (vendor != null)
        //                    {
        //                        VendorName = vendor.Name;
        //                    }
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description + " (" + VendorName + ")";
        //                    voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    voucherDetail.PartnerId = masterData.VendorId;
        //                    voucherDetail.PartnerType = "Supplier";
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Math.Round(masterData.GrandTotal, 2);
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Math.Round(masterData.GrandTotal, 2);
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Net Payable Cash" && masterData.PaymentType == "Cash")
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Math.Round(masterData.GrandTotal, 2);
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Math.Round(masterData.GrandTotal, 2);
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //                else if (AccTotal.MappingSource == "Net Payable CreditCard" && masterData.PaymentType == "Credit Card")
        //                {
        //                    AccVoucherDetail voucherDetail = new AccVoucherDetail();
        //                    voucherDetail.AccountId = AccTotal.AccountId;
        //                    voucherDetail.Description = AccTotal.Description;
        //                    voucherDetail.AccountName = AccTotal.Account;
        //                    voucherDetail.MappingSource = AccTotal.MappingSource;
        //                    if (AccTotal.DebitOrCredit == "Debit")
        //                    {
        //                        voucherDetail.AmountDebit = Math.Round(masterData.GrandTotal, 2);
        //                        voucherDetail.AmountCredit = 0;
        //                    }
        //                    else
        //                    {
        //                        voucherDetail.AmountDebit = 0;
        //                        voucherDetail.AmountCredit = Math.Round(masterData.GrandTotal, 2);
        //                    }
        //                    Journal.Add(voucherDetail);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        CustomLogging.InsertErrorLog("Purchase", "GetSaleJournal", ex.Message, dataContext);
        //    }
        //    return Journal;
        //}


    }
}
