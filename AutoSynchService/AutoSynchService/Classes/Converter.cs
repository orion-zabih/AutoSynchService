using AutoSynchService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchService.Classes
{
    internal class Converter
    {
        internal static List<InvSaleDetail> GetInvSaleDetails(DataTable table)
        {
            var invSaleDetailList = new List<InvSaleDetail>(table.Rows.Count);
            foreach (DataRow row in table.Rows)
            {
                var values = row.ItemArray;
                var invSaleDetail = new InvSaleDetail()
                {
                    Id = Convert.ToInt32(values[0]),
                    BillId = Convert.ToInt32(values[1]),
                    ProductId = Convert.ToInt32(values[2]),
                    Price = Convert.ToDecimal(values[3]),
                    Qty = Convert.ToDecimal(values[4]),
                    Total = Convert.ToDecimal(values[5]),
                    IsDeleted = (bool)values[6],
                    SaleValue = Convert.ToDecimal(values[7]),
                    TaxCharged = Convert.ToDecimal(values[8]),
                    TaxRate = Convert.ToDecimal(values[9]),
                    Pctcode = Convert.ToString(values[10] != DBNull.Value ? values[10]:""),
                    FurtherTax = Convert.ToDecimal(values[11]),
                    Discount = Convert.ToDecimal(values[12]),
                    InvoiceType = Convert.ToInt32(values[13]),
                    PriceExclusiveTax = Convert.ToDecimal(values[14])
                    
                };
                invSaleDetailList.Add(invSaleDetail);
            }
            return invSaleDetailList;
        }
        internal static List<InvSaleMaster> GetInvSaleMaster(DataTable table)
        {
            var invSaleMasterList = new List<InvSaleMaster>(table.Rows.Count);
            foreach (DataRow row in table.Rows)
            {
                var values = row.ItemArray;
                var invSaleMaster = new InvSaleMaster()
                {
                    Id = Convert.ToInt32(row["Id"]),
                    OrderDate = Convert.ToDateTime(row["OrderDate"]),
                    CustomerId = Convert.ToInt32(row["CustomerId"]),
                    CustomerTypeId = Convert.ToInt32(row["CustomerTypeId"]),
                    InvoiceTotal = Convert.ToDecimal(row["InvoiceTotal"]),
                    DiscountPercent = Convert.ToDecimal(row["DiscountPercent"]),
                    TaxCalculated = Convert.ToDecimal(row["TaxCalculated"]),
                    ServiceChargesCalculated = Convert.ToDecimal(row["ServiceChargesCalculated"]),
                    GrandTotal = Convert.ToDecimal(row["GrandTotal"]),
                    PaymentReceived = Convert.ToDecimal(row["PaymentReceived"]),
                    Change = Convert.ToDecimal(row["Change"]),
                    IsCanceled = Convert.ToBoolean(row["IsCanceled"]),
                    OrderStatus = Convert.ToString(row["OrderStatus"]),
                    DiscountRemarks = row["DiscountRemarks"].ToString(),
                    BranchId = Convert.ToInt32(row["BranchId"]),
                    SessionId = Convert.ToInt32(row["SessionId"]),
                    IsDeleted = Convert.ToBoolean(row["IsDeleted"]),
                    ThirdPartyId = Convert.ToInt32(row["ThirdPartyId"]),
                    CustomerName = row["CustomerName"].ToString(),
                    CustomerContact = row["CustomerContact"].ToString(),
                    EmployeeId = Convert.ToInt32(row["EmployeeId"]),
                    Remarks = row["Remarks"].ToString(),
                    CreatedBy = Convert.ToInt32(row["CreatedBy"]),
                    CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                    UpdatedBy = Convert.ToInt32(row["UpdatedBy"]),
                    UpdatedDate = Convert.ToDateTime(row["UpdatedDate"]),
                    CompletedBy = Convert.ToInt32(row["CompletedBy"]),
                    CompletedDate = Convert.ToDateTime(row["CompletedDate"]),
                    ShiftId = Convert.ToInt32(row["ShiftId"]),
                    StoreId = Convert.ToInt32(row["StoreId"]),
                    IsReturn = Convert.ToBoolean(row["IsReturn"]),
                    FiscalYearId = Convert.ToInt32(row["FiscalYearId"]),
                    VehicleId = Convert.ToInt32(row["VehicleId"]),
                    CurrentReading = Convert.ToInt32(row["CurrentReading"]),
                    NextServiceAfterKm = Convert.ToInt32(row["NextServiceAfterKm"]),
                    ExpectReadingOnNext = Convert.ToInt32(row["ExpectReadingOnNext"]),
                    ReadingPerDay = Convert.ToInt32(row["ReadingPerDay"]),
                    NextServiceDate = Convert.ToDateTime(row["NextServiceDate"]),
                    IsSentToFbr = Convert.ToInt32(row["IsSentToFbr"]),
                    FbrInvoiceNumber = row["FbrInvoiceNumber"].ToString(),
                    FbrResponseCode = row["FbrResponseCode"].ToString(),
                    FbrResponse = row["FbrResponse"].ToString(),
                    FbrPosid = Convert.ToInt32(row["FbrPOSID"]),
                    FbrUsin = row["FbrUSIN"].ToString(),
                    BuyerNtn = row["BuyerNTN"].ToString(),
                    BuyerCnic = row["BuyerCNIC"].ToString(),
                    BuyerPhoneNumber = row["BuyerPhoneNumber"].ToString(),
                    FbrPaymentModeCode = Convert.ToInt32(row["FbrPaymentModeCode"]),
                    DiscountCalculated = Convert.ToDecimal(row["DiscountCalculated"]),
                    TotalQuantity = Convert.ToDecimal(row["TotalQuantity"]),
                    FurtherTax = Convert.ToDecimal(row["FurtherTax"]),
                    FbrInvoiceTypeCode = Convert.ToInt32(row["FbrInvoiceTypeCode"]),
                    PaymentType = row["PaymentType"].ToString()

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
                    insertion_timestamp = Convert.ToDateTime(values[5] != DBNull.Value ? values[5] : null),
                    update_timestamp = Convert.ToDateTime(values[6] != DBNull.Value ? values[6] : null),


                };
                synchSettingList.Add(synchSetting);
            }
            return synchSettingList;
        }

    }
}
