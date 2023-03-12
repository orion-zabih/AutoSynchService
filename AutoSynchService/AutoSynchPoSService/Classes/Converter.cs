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
                    OrderDate = row.Field<DateTime?>("OrderDate"),
                    CustomerId = row.Field<int?>("CustomerId"),
                    CustomerTypeId = row.Field<int?>("CustomerTypeId"),
                    InvoiceTotal = row.Field<decimal?>("InvoiceTotal"),
                    DiscountPercent = row.Field<decimal?>("DiscountPercent"),
                    TaxCalculated = row.Field<decimal?>("TaxCalculated"),
                    ServiceChargesCalculated = row.Field<decimal?>("ServiceChargesCalculated"),
                    GrandTotal = row.Field<decimal?>("GrandTotal"),
                    PaymentReceived = row.Field<decimal?>("PaymentReceived"),
                    Change = row.Field<decimal>("Change"),
                    IsCanceled = row.Field<bool>("IsCanceled"),
                    OrderStatus = row.Field<string>("OrderStatus"),
                    DiscountRemarks = row.Field<string?>("DiscountRemarks"),
                    BranchId = row.Field<int>("BranchId"),
                    SessionId = row.Field<int>("SessionId"),
                    IsDeleted = row.Field<bool>("IsDeleted"),
                    ThirdPartyId = row.Field<int?>("ThirdPartyId"),
                    CustomerName = row.Field<string?>("CustomerName"),
                    CustomerContact = row.Field<string?>("CustomerContact"),
                    EmployeeId = row.Field<int?>("EmployeeId"),
                    Remarks = row.Field<string?>("Remarks"),
                    CreatedBy = row.Field<int>("CreatedBy"),
                    CreatedDate = row.Field<DateTime?>("CreatedDate"),
                    UpdatedBy = row.Field<int?>("UpdatedBy"),
                    UpdatedDate = row.Field<DateTime?>("UpdatedDate"),
                    CompletedBy = row.Field<int?>("CompletedBy"),
                    CompletedDate = row.Field<DateTime?>("CompletedDate"),
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
                    IsSentToFbr = row.Field<int>("IsSentToFbr"),
                    FbrInvoiceNumber = row.Field<string?>("FbrInvoiceNumber"),
                    FbrResponseCode = row.Field<string?>("FbrResponseCode"),
                    FbrResponse = row.Field<string?>("FbrResponse"),
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
                    PaymentType = row.Field<string?>("PaymentType")

                };

                invSaleMasterList.Add(invSaleMaster);
            }
            return invSaleMasterList;
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

    }
}
