using AutoSynchClientEngine.Classes;
using AutoSynchSqlite.DbManager;
using AutoSynchSqlServer.Models;
using AutoSynchSqlServerLocal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchClientEngine.DAOs
{
    internal class InvSaleDao
    {

        internal string JvInCaseOfQtsale(string dbtype,string branchId)
        {
            string qry = "select InvCreateJvInCaseOfQtsale from OrgBranch where Id=" + branchId;
            DataTable PendingOrdersSQlite=new DataTable();
            if (dbtype.Equals(Constants.Sqlite))
            {
                SqliteManager sqlite = new SqliteManager();
                PendingOrdersSQlite = sqlite.GetDataTable(qry);
            }
            else
            {
                MsSqlDbManager msSqlDb = new MsSqlDbManager();
                PendingOrdersSQlite = msSqlDb.GetDataTable(qry);
            }
            if (PendingOrdersSQlite.Rows.Count > 0)
            {
                return PendingOrdersSQlite.Rows[0].Field<string>("InvCreateJvInCaseOfQtsale");
            }
            else return "No";
        }
        internal List<InvSaleMaster> GetSaleMaster(string dbtype,string branchId)
        {
            if(dbtype.Equals(Constants.Sqlite))
            {

                SqliteManager sqlite = new SqliteManager();
                string qry = "select * from InvSaleMasterTmp where IsDeleted = 0 and IsCanceled = 0 and OrderStatus='Invoice' and (IsUploaded != 1 or IsUploaded is null) LIMIT 1000";
                if (JvInCaseOfQtsale(dbtype, branchId)=="Yes")
                {
                    qry = "select * from InvSaleMasterTmp where IsDeleted = 0 and IsCanceled = 0 and (IsUploaded != 1 or IsUploaded is null) LIMIT 1000";
                }
                DataTable PendingOrdersSQlite = sqlite.GetDataTable(qry);
                Converter converter = new Converter();
                List<InvSaleMaster> saleMasters = Converter.GetInvSaleMaster(PendingOrdersSQlite);
                return saleMasters;
            }
            else
            {

                MsSqlDbManager msSqlDb = new MsSqlDbManager();
                string qry = @"select top(1000) Id,
ISNULL(ApprovedBy,0) ApprovedBy,
ISNULL(BedId,0) BedId,
BranchId,
BuyerCnic,
BuyerNtn,
BuyerPhoneNumber,
CancelledReason,
Change,
ISNULL(CompletedBy,0) CompletedBy,
CompletedDate,
CreatedBy,
CreatedDate,
CreditLimitFlage,
CurrentReading,
CustomerContact,
ISNULL(CustomerId,0) CustomerId,
CustomerName,
ISNULL(CustomerTypeId,0) CustomerTypeId,
DiscountCalculated,
DiscountPercent,
DiscountRemarks,
EmpCreditType,
ISNULL(EmployeeId,0) EmployeeId,
ExpectReadingOnNext,
FbrInvoiceNumber,
FbrInvoiceTypeCode,
FbrPaymentModeCode,
FbrPosid,
FbrResponse,
FbrResponseCode,
FbrUsin,
FiscalYearId,
FurtherTax,
GrandTotal,
ISNULL(InvoiceNo,0) InvoiceNo,
InvoiceTotal,
IsApproved,
IsCanceled,
IsCashierClosed,
IsDeleted,
IsReturn,
IsSentToFbr,
IsStar,
NextServiceAfterKm,
NextServiceDate,
OrderDate,
OrderNo,
OrderStatus,
PatientId,
PaymentReceived,
PaymentType,
0 PaymentTypeId,
QuatationId,
ReadingPerDay,
Remarks,
ScaleNumber,
ServiceChargesCalculated,
SessionId,
ShiftId,
StoreId,
TaxCalculated,
ISNULL(ThirdPartyId,0) ThirdPartyId,
TotalQuantity,
ISNULL(UpdatedBy,0) UpdatedBy,
UpdatedDate,
VehicleId,
VehicleNo,
WardId,
IsUploaded from InvSaleMaster where IsDeleted = 0 and IsCanceled = 0 and OrderStatus='Invoice' and (IsUploaded != 1 or IsUploaded is null)";
                    // OrderStatus='Invoice' and (IsUploaded != 1 or IsUploaded is null) LIMIT 1000";
                if (JvInCaseOfQtsale(dbtype, branchId) == "Yes")
                {
                    qry = qry.Replace("OrderStatus='Invoice' and ",string.Empty);
                }
                DataTable PendingOrdersSQlite = msSqlDb.GetDataTable(qry);
                Converter converter = new Converter();
                List<InvSaleMaster> saleMasters = Converter.GetInvSaleMaster(PendingOrdersSQlite);
                return saleMasters;
            }
        }
        internal List<InvSaleDetail> GetSaleDetails(decimal BillId, string dbtype)
        {
            if (dbtype.Equals(Constants.Sqlite))
            {
                SqliteManager sqlite = new SqliteManager();
                DataTable PendingOrdersSQlite = sqlite.GetDataTable("select * from InvSaleDetailTmp where BillId = '" + BillId + "'");
                Converter converter = new Converter();
                List<InvSaleDetail> saleDetails = Converter.GetInvSaleDetails(PendingOrdersSQlite);
                return saleDetails;
            }
            else
            {
                MsSqlDbManager msSqlDb = new MsSqlDbManager();
                DataTable PendingOrdersSQlite = msSqlDb.GetDataTable(@"select Id,
BillId,
Discount,
FurtherTax,
InvoiceType,
IsDeleted,
Pctcode,
Price,
PriceExclusiveTax,
ProductId,
Qty,
SaleValue,
TaxCharged,
TaxRate,
Total from InvSaleDetail where BillId = '" + BillId + "'");
                Converter converter = new Converter();
                List<InvSaleDetail> saleDetails = Converter.GetInvSaleDetails(PendingOrdersSQlite);
                return saleDetails;
            }
        }
        internal bool UpdateMasterIsUploaded(List<int> Ids, string dbtype)
        {
            SqliteManager sqlite = new SqliteManager();
            List<string> queries = new List<string>();
            string tblName = "InvSaleMaster";
            if (dbtype.Equals(Constants.Sqlite))
                tblName = "InvSaleMasterTmp";
            Ids.ForEach(id =>
                queries.Add("update "+tblName+" set IsUploaded = 1 where Id = '" + id + "'")
                );
            if (dbtype.Equals(Constants.Sqlite))
            {                
                sqlite.ExecuteTransactionMultiQueries(queries);
                return true;
            }
            else
            {
                MsSqlDbManager msSqlDbManager = new MsSqlDbManager();
                try
                {
                    queries.ForEach(q => { msSqlDbManager.ExecuteTransQuery(q); });
                    msSqlDbManager.Commit();
                    return true;
                }
                catch (Exception)
                {
                    msSqlDbManager.RollBack();
                    return false;
                }


            }
        }
        internal bool DeleteOldQt(string dbtype,int daysToDeleteQT)
        {
            SqliteManager sqlite = new SqliteManager();
            List<string> queries = new List<string>();
            string tblName = "InvSaleMaster";
            if (dbtype.Equals(Constants.Sqlite))
                tblName = "InvSaleMasterTmp";
            queries.Add(@"delete from InvSaleDetail where BillId in 
(
select Id from InvSaleMaster where TRY_CONVERT(DATE, OrderDate)<='" + Utility.GetDateTimeStringDDMMYYYY(Utility.GetOldDateTime(daysToDeleteQT)) + "' and OrderStatus='QT')");
            queries.Add(@"delete from InvProductLedger where ReferenceId in 
(
select Id from InvSaleMaster where TRY_CONVERT(DATE, OrderDate)<='" + Utility.GetDateTimeStringDDMMYYYY(Utility.GetOldDateTime(daysToDeleteQT)) + "' and OrderStatus='QT') and Source = 'Sale'");

            queries.Add(@"delete from InvSaleMaster where TRY_CONVERT(DATE, OrderDate)<='" + Utility.GetDateTimeStringDDMMYYYY(Utility.GetOldDateTime(daysToDeleteQT)) + "' and OrderStatus='QT'");
            if (dbtype.Equals(Constants.Sqlite))
            {
                sqlite.ExecuteTransactionMultiQueries(queries);
                return true;
            }
            else
            {
                MsSqlDbManager msSqlDbManager = new MsSqlDbManager();
                try
                {
                    queries.ForEach(q => { msSqlDbManager.ExecuteTransQuery(q); });
                    msSqlDbManager.Commit();
                    return true;
                }
                catch (Exception)
                {
                    msSqlDbManager.RollBack();
                    return false;
                }


            }
        }
    }
}
