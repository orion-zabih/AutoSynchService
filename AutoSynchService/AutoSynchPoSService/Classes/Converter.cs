using AutoSynchPoSService.Classes;
using AutoSynchSqlServer.Models;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchPosService.Classes
{
    internal class Converter
    {
        internal static List<InvSaleDetail> GetInvSaleDetails(DataTable table)
        {
            var invSaleDetailList = new List<InvSaleDetail>(table.Rows.Count);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
            
                var invSaleDetail = new InvSaleDetail()
                {
                    Id = row.Field<int>("Id"),
                    BillId = row.Field<int>("BillId"),
                    ProductId = row.Field<int>("ProductId"),
                    Price = row.Field<decimal>("Price"),
                    Qty = row.Field<decimal>("Qty"),
                    Total = row.Field<decimal>("Total"),
                    IsDeleted = row.Field<bool>("IsDeleted"),
                    SaleValue = row.Field<decimal>("SaleValue"),
                    TaxCharged = row.Field<decimal>("TaxCharged"),
                    TaxRate = row.Field<decimal>("TaxRate"),
                    Pctcode = row["Pctcode"] != DBNull.Value ? row.Field<string>("Pctcode") :"",
                    FurtherTax = row.Field<decimal>("FurtherTax"),
                    Discount = row.Field<decimal>("Discount"),
                    InvoiceType = row.Field<int>("InvoiceType"),
                    PriceExclusiveTax = row.Field<decimal>("PriceExclusiveTax")
                    
                };
                invSaleDetailList.Add(invSaleDetail);
            }
            return invSaleDetailList;
        }
        internal static List<InvSaleMaster> GetInvSaleMaster(DataTable table)
        {
            var invSaleMasterList = new List<InvSaleMaster>(table.Rows.Count);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];

                var invSaleMaster = new InvSaleMaster()
                {
                    Id = row.Field<int>("Id"),
                    OrderNo = row.Field<int>("OrderNo"),
                    InvoiceNo = row.Field<int>("InvoiceNo"),
                    OrderDate = row.Field<DateTime?>("OrderDate"),
                    CustomerId = row.Field<int?>("CustomerId"),
                    CustomerTypeId = row.Field<int?>("CustomerTypeId"),
                    //PaymentTypeId = row.Field<int?>("PaymentTypeId"),
                    InvoiceTotal = row.Field<decimal?>("InvoiceTotal"),
                    DiscountPercent = row.Field<decimal?>("DiscountPercent"),
                    TaxCalculated = row.Field<decimal?>("TaxCalculated"),
                    ServiceChargesCalculated = row.Field<decimal?>("ServiceChargesCalculated"),
                    GrandTotal = row.Field<decimal?>("GrandTotal"),
                    PaymentReceived = row.Field<decimal?>("PaymentReceived"),
                    Change = row.Field<decimal>("Change"),
                    IsCanceled = row.Field<bool>("IsCanceled"),
                    CancelledReason = row.Field<string?>("CancelledReason"),
                    OrderStatus = row.Field<string>("OrderStatus"),
                    DiscountRemarks = row.Field<string?>("DiscountRemarks"),
                    BranchId = row.Field<int>("BranchId"),
                    SessionId = row.Field<int>("SessionId"),
                    IsDeleted = row.Field<bool>("IsDeleted"),
                    ThirdPartyId = row.Field<int?>("ThirdPartyId"),
                    CustomerName = row.Field<string?>("CustomerName"),
                    CustomerContact = row.Field<string?>("CustomerContact"),
                    EmployeeId = row.Field<int?>("EmployeeId"),
                    WardId = row.Field<int>("WardId"),
                    BedId = row.Field<int>("BedId"),
                    EmpCreditType = row.Field<string?>("EmpCreditType"),
                    IsApproved = row.Field<bool>("IsApproved"),
                    ApprovedBy = row.Field<int>("ApprovedBy"),
                    IsCashierClosed = row.Field<bool>("IsCashierClosed"),
                    Remarks = row.Field<string?>("Remarks"),
                    IsStar = row.Field<bool>("IsStar"),
                    PatientId = row.Field<int>("PatientId"),
                    CreatedBy = row.Field<int>("CreatedBy"),
                    CreatedDate = row.Field<DateTime?>("CreatedDate"),
                    UpdatedBy = row.Field<int?>("UpdatedBy"),
                    UpdatedDate = row.Field<DateTime?>("UpdatedDate"),
                    CompletedBy = row.Field<int?>("CompletedBy"),
                    CompletedDate = row.Field<DateTime?>("CompletedDate"),
                    CreditLimitFlage = row.Field<bool>("CreditLimitFlage"),
                    ShiftId = row.Field<int>("ShiftId"),
                    StoreId = row.Field<int>("StoreId"),
                    IsReturn = row.Field<bool>("IsReturn"),
                    FiscalYearId = row.Field<int>("FiscalYearId"),
                    VehicleId = row.Field<int>("VehicleId"),
                    CurrentReading = row.Field<int>("CurrentReading"),
                    NextServiceAfterKm = row.Field<int>("NextServiceAfterKm"),
                    ExpectReadingOnNext = row.Field<int>("ExpectReadingOnNext"),
                    ReadingPerDay = row.Field<int>("ReadingPerDay"),
                    NextServiceDate = row.Field<DateTime?>("NextServiceDate"),
                    FbrInvoiceNumber = row.Field<string?>("FbrInvoiceNumber"),
                    FbrPosid = row.Field<int>("FbrPOSID"),
                    FbrUsin = row.Field<string?>("FbrUSIN"),
                    BuyerNtn = row.Field<string?>("BuyerNTN"),
                    BuyerCnic = row.Field<string?>("BuyerCNIC"),
                    BuyerPhoneNumber = row.Field<string?>("BuyerPhoneNumber"),
                    FbrPaymentModeCode = row.Field<int>("FbrPaymentModeCode"),
                    DiscountCalculated = row.Field<decimal?>("DiscountCalculated"),
                    TotalQuantity = row.Field<decimal?>("TotalQuantity"),
                    FurtherTax = row.Field<decimal?>("FurtherTax"),
                    FbrInvoiceTypeCode = row.Field<int>("FbrInvoiceTypeCode"),
                    IsSentToFbr = row.Field<int>("IsSentToFbr"),
                    FbrResponseCode = row.Field<string?>("FbrResponseCode"),
                    FbrResponse = row.Field<string?>("FbrResponse"),
                    PaymentType = row.Field<string?>("PaymentType"),
                    VehicleNo = row.Field<string?>("VehicleNo"),
                    ScaleNumber = row.Field<string?>("ScaleNumber"),
                    QuatationId = row.Field<int>("QuatationId")

                };

                invSaleMasterList.Add(invSaleMaster);
            }
            return invSaleMasterList;
        }
        internal static List<InvPurchaseMaster> GetInvPurchaseMaster(DataTable table)
        {
            var invPurchaseMasterList = new List<InvPurchaseMaster>(table.Rows.Count);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];

                var invSPurchaseMaster = new InvPurchaseMaster()
                {
                    Id = row.Field<int>("Id"),
                    InvoiceNo = row.Field<int>("InvoiceNo"),
                    InvoiceDate = row.Field<DateTime?>("InvoiceDate"),
                    VendorId = row.Field<int?>("VendorId"),
                    InvoiceDisc = row.Field<decimal?>("InvoiceDisc"),
                    Frieght = row.Field<decimal?>("Frieght"),
                    IsReturn = row.Field<bool>("IsReturn"),
                    PaymentTypeId = row["PaymentTypeId"] != DBNull.Value ? row.Field<int>("PaymentTypeId"):0,
                    InvoiceTotal = row.Field<decimal>("InvoiceTotal"),                    
                    GrandTotal = row.Field<decimal>("GrandTotal"),
                    ReferenceId= row.Field<int?>("ReferenceId"),
                    Source= row.Field<string?>("Source"),
                    WarehouseId= row.Field<int>("WarehouseId"),
                    BranchId = row.Field<int>("BranchId"),
                    IsCancel= row.Field<bool>("IsCancel"),
                    Status = row.Field<string?>("Status"),
                    CreatedBy = row.Field<int>("CreatedBy"),
                    CreatedDate = row.Field<DateTime?>("CreatedDate"),
                    UpdatedBy = row.Field<int>("UpdatedBy"),
                    UpdatedDate = row.Field<DateTime?>("UpdatedDate"),
                    CurrencyId= row.Field<int>("CurrencyId"),
                    CurrencyRate= row.Field<decimal>("CurrencyRate"),
                    GatePassNo = row.Field<int?>("GatePassNo"),
                    BiltyNo = row.Field<string?>("BiltyNo"),
                    BiltyDate= row.Field<DateTime?>("BiltyDate"),
                    VehicleNo = row.Field<string?>("VehicleNo"),
                    DriverName= row.Field<string?>("DriverName"),
                    DriverContactNo= row.Field<string?>("DriverContactNo"),
                    Commission= row.Field<decimal>("Commission"),
                    Tax= row.Field<decimal>("Tax"),
                    Remarks = row.Field<string?>("Remarks"),                    
                    FiscalYearId = row.Field<int>("FiscalYearId"),
                    LoadingCharges= row.Field<decimal>("LoadingCharges"),
                    OtherCharges = row.Field<decimal>("OtherCharges"),
                    GatePassId= row.Field<int>("GatePassId"),
                    GrandTotalBeforeWhTax= row.Field<decimal>("GrandTotalBeforeWhTax"),
                    WithholdingTaxInAmount = row.Field<decimal>("WithholdingTaxInAmount"),
                    WithholdingTaxInPer = row.Field<decimal>("WithholdingTaxInPer"),
                    PaymentType = row.Field<string?>("PaymentType"),                    
                    AdvanceTaxAmount= row.Field<decimal>("AdvanceTaxAmount"),
                    CancelRemarks= row.Field<string?>("CancelRemarks")

                };

                invPurchaseMasterList.Add(invSPurchaseMaster);
            }
            return invPurchaseMasterList;
        }
        internal static List<InvPurchaseDetail> GetInvPurchaseDetails(DataTable table)
        {
            var invPurchaseDetailList = new List<InvPurchaseDetail>(table.Rows.Count);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];

                var invpurchaseDetail = new InvPurchaseDetail()
                {
                    /*Id,AdditionalTaxAmount,AverageCost,BatchNo,CostPrice,CutQty,
Disc,ExpiryDate,IsBatchChange,MasterId,ProductId,Qty,RetailPrice,SaleTaxInPercent,
Scheme,TaxAmount,UnitId*/
                    Id = row.Field<int>("Id"),
                    AdditionalTaxAmount = row.Field<decimal>("AdditionalTaxAmount"),
                    AverageCost = row.Field<decimal>("AverageCost"),
                    BatchNo = row["BatchNo"] != DBNull.Value ? row.Field<string>("BatchNo") : "",
                    CostPrice = row.Field<decimal>("CostPrice"),
                    CutQty = row.Field<decimal>("CutQty"),
                    Disc = row.Field<decimal>("Disc"),
                    ExpiryDate = row.Field<DateTime?>("ExpiryDate"),
                    IsBatchChange = row.Field<bool>("IsBatchChange"),
                    MasterId = row.Field<int>("MasterId"),
                    ProductId = row.Field<int>("ProductId"),                    
                    Qty = row.Field<decimal>("Qty"),
                    RetailPrice = row.Field<decimal>("RetailPrice"),
                    SaleTaxInPercent = row.Field<decimal>("SaleTaxInPercent"),
                    Scheme = row.Field<decimal>("Scheme"),
                    TaxAmount = row.Field<decimal>("TaxAmount"),
                    UnitId = row.Field<int>("UnitId")
                };
                invPurchaseDetailList.Add(invpurchaseDetail);
            }
            return invPurchaseDetailList;
        }
        internal static List<InvProductLedger> GetInvProductLedgers(DataTable table)
        {
            var invProductLedgerList = new List<InvProductLedger>(table.Rows.Count);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];

                var invProductLedger = new InvProductLedger()
                {
                    /*Id,AverageCost,BatchBarcode,BatchNo,BranchId,Cost,CreatedBy,
CreatedDate,ExpiryDate,FiscalYearId,IsCancel,MaterialId,PackageId,ProductId,
QtyCut,QtyIn,QtyOut,ReferenceId,Remarks,RetailPrice,Source,SourceParty,
TransDate,UnitId,WarehouseId */
                    Id = row.Field<int>("Id"),
                    AverageCost = row.Field<decimal>("AverageCost"),
                    BatchBarcode= row["BatchBarcode"] != DBNull.Value ? row.Field<string>("BatchBarcode") : "",
                    BatchNo = row["BatchNo"] != DBNull.Value ? row.Field<string>("BatchNo") : "",
                    BranchId= row.Field<int>("BranchId"),
                    Cost= row.Field<decimal>("Cost"),
                    CreatedBy= row.Field<int>("CreatedBy"),
                    CreatedDate = row.Field<DateTime?>("CreatedDate"),
                    ExpiryDate = row.Field<DateTime?>("ExpiryDate"),
                    FiscalYearId= row.Field<int>("FiscalYearId"),
                    IsCancel= row.Field<bool>("IsCancel"),
                    MaterialId = row.Field<int>("MaterialId"),
                    PackageId = row.Field<int>("PackageId"),
                    ProductId = row.Field<int>("ProductId"),
                    QtyCut = row.Field<decimal>("QtyCut"),
                    QtyIn = row.Field<decimal>("QtyIn"),
                    QtyOut = row.Field<decimal>("QtyOut"),
                    ReferenceId = row.Field<int>("ReferenceId"),
                    Remarks= row["Remarks"] != DBNull.Value ? row.Field<string>("Remarks") : "",
                    RetailPrice= row.Field<decimal>("RetailPrice"),
                    Source= row.Field<string>("Source"),
                    SourceParty = row["SourceParty"] != DBNull.Value ? row.Field<string>("SourceParty") : "",
                    TransDate = row.Field<DateTime?>("TransDate"),
                    UnitId = row.Field<int>("UnitId"),
                    WarehouseId= row.Field<int>("WarehouseId"),
                };
                invProductLedgerList.Add(invProductLedger);
            }
            return invProductLedgerList;
        }

        internal static List<SynchSetting> GetSynchSetting(DataTable table)
        {
            var synchSettingList = new List<SynchSetting>(table.Rows.Count);
            foreach (DataRow row in table.Rows)
            {
                var values = row.ItemArray;
                var synchSetting = new SynchSetting()
                {//setting_id,synch_method,synch_type,table_names,last_update_date,status
                    setting_id = Convert.ToInt32(values[0]),
                    synch_method = Convert.ToString(values[1] != DBNull.Value ? values[1] : ""),
                    synch_type = Convert.ToString(values[2] != DBNull.Value ? values[2] : ""),
                    table_names = Convert.ToString(values[3] != DBNull.Value ? values[3] : ""),
                    status = Convert.ToString(values[4] != DBNull.Value ? values[4] : ""),
                    sync_timestamp = Convert.ToDateTime(values[5] != DBNull.Value ? values[5] : null),
                    insertion_timestamp = Convert.ToDateTime(values[6] != DBNull.Value ? values[6] : null),
                    update_timestamp = Convert.ToDateTime(values[7] != DBNull.Value ? values[7] : null),


                };
                synchSettingList.Add(synchSetting);
            }
            return synchSettingList;
        }
        internal static List<AutoSynchSqlServer.CustomModels.TableStructure> GetTableStructure(DataTable table)
        {
            var tableStructureList = new List<AutoSynchSqlServer.CustomModels.TableStructure>(table.Rows.Count);
            foreach (DataRow row in table.Rows)
            {
                var values = row.ItemArray;
                var tableStructure = new AutoSynchSqlServer.CustomModels.TableStructure()
                {//setting_id,synch_method,synch_type,table_names,last_update_date,status

                    TableName = Convert.ToString(values[0] != DBNull.Value ? values[0] : ""),
                    ColumnName = Convert.ToString(values[1] != DBNull.Value ? values[1] : ""),
                    ColumnDefault = Convert.ToString(values[2] != DBNull.Value ? values[2] : ""),
                    IsNullable= Convert.ToString(values[3] != DBNull.Value ? values[3] : ""),
                    DataType = Convert.ToString(values[4] != DBNull.Value ? values[4] : null),
                    CharacterMaximumLength= Convert.ToString(values[5] != DBNull.Value ? values[5] : null)
                };
                tableStructureList.Add(tableStructure);
            }
            return tableStructureList;
        }

    }
}
