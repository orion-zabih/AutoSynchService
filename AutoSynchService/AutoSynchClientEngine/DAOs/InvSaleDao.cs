using AutoSynchClientEngine.Classes;
using AutoSynchSqlite.DbManager;
using AutoSynchSqlServer.Models;
using AutoSynchSqlServerLocal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        private string master_qry_cols = @"Id,
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
IsUploaded";
        private string sale_qry = @"select top(1000) #cols# from InvSaleMaster where IsDeleted = 0 and IsCanceled = 0 and OrderStatus='Invoice' and (IsUploaded != 1 or IsUploaded is null)";
        
        private string sale_detail_qry = @"select #cols# from InvSaleDetail";
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

                sale_qry = sale_qry.Replace("#cols#", master_qry_cols);
                // OrderStatus='Invoice' and (IsUploaded != 1 or IsUploaded is null) LIMIT 1000";
                if (JvInCaseOfQtsale(dbtype, branchId) == "Yes")
                {
                    sale_qry = sale_qry.Replace("OrderStatus='Invoice' and ",string.Empty);
                }
                DataTable PendingOrdersSQlite = msSqlDb.GetDataTable(sale_qry);
                Converter converter = new Converter();
                List<InvSaleMaster> saleMasters = Converter.GetInvSaleMaster(PendingOrdersSQlite);
                return saleMasters;
            }
        }
        internal List<InvSaleMaster> GetSaleMaster(string dbtype, string branchId,List<int> chunk)
        {
            string whereIds = string.Join(',', chunk).TrimEnd(',');
            if (dbtype.Equals(Constants.Sqlite))
            {

                SqliteManager sqlite = new SqliteManager();
                string qry = $"select * from InvSaleMasterTmp where IsDeleted = 0 and IsCanceled = 0 and OrderStatus='Invoice' and (IsUploaded != 1 or IsUploaded is null) and Id in ({whereIds})";
                if (JvInCaseOfQtsale(dbtype, branchId) == "Yes")
                {
                    qry = $"select * from InvSaleMasterTmp where IsDeleted = 0 and IsCanceled = 0 and (IsUploaded != 1 or IsUploaded is null)  and Id in ({whereIds})";
                }
                DataTable PendingOrdersSQlite = sqlite.GetDataTable(qry);
                Converter converter = new Converter();
                List<InvSaleMaster> saleMasters = Converter.GetInvSaleMaster(PendingOrdersSQlite);
                return saleMasters;
            }
            else
            {

                MsSqlDbManager msSqlDb = new MsSqlDbManager();

                sale_qry = sale_qry.Replace("#cols#", master_qry_cols);
                // OrderStatus='Invoice' and (IsUploaded != 1 or IsUploaded is null) LIMIT 1000";
                if (JvInCaseOfQtsale(dbtype, branchId) == "Yes")
                {
                    sale_qry = sale_qry.Replace("OrderStatus='Invoice' and ", string.Empty);
                }
                sale_qry = sale_qry+$" and Id in ({whereIds})";
                DataTable PendingOrdersSQlite = msSqlDb.GetDataTable(sale_qry);
                Converter converter = new Converter();
                List<InvSaleMaster> saleMasters = Converter.GetInvSaleMaster(PendingOrdersSQlite);
                return saleMasters;
            }
        }
        internal List<int> GetSaleMasterIds(string dbtype, string branchId)
        {
            if (dbtype.Equals(Constants.Sqlite))
            {

                SqliteManager sqlite = new SqliteManager();
                string qry = "select Id from InvSaleMasterTmp where IsDeleted = 0 and IsCanceled = 0 and OrderStatus='Invoice' and (IsUploaded != 1 or IsUploaded is null)";
                if (JvInCaseOfQtsale(dbtype, branchId) == "Yes")
                {
                    qry = "select Id from InvSaleMasterTmp where IsDeleted = 0 and IsCanceled = 0 and (IsUploaded != 1 or IsUploaded is null)";
                }
                DataTable PendingOrdersSQlite = sqlite.GetDataTable(qry);
                List<int> saleMasters = new List<int>();
                for (int i = 0; i < PendingOrdersSQlite.Rows.Count; i++)
                {
                    saleMasters.Add(PendingOrdersSQlite.Rows[i].Field<int>("Id"));
                }

                return saleMasters;
            }
            else
            {

                MsSqlDbManager msSqlDb = new MsSqlDbManager();

                sale_qry = sale_qry.Replace("top(1000) #cols#", "Id");
                // OrderStatus='Invoice' and (IsUploaded != 1 or IsUploaded is null) LIMIT 1000";
                if (JvInCaseOfQtsale(dbtype, branchId) == "Yes")
                {
                    sale_qry = sale_qry.Replace("OrderStatus='Invoice' and ", string.Empty);
                }
                DataTable PendingOrdersSQlite = msSqlDb.GetDataTable(sale_qry);
                List<int> saleMasters = new List<int>();
                for (int i = 0; i < PendingOrdersSQlite.Rows.Count; i++)
                {
                    saleMasters.Add(PendingOrdersSQlite.Rows[i].Field<int>("Id"));
                }

                return saleMasters;
            }
        }
        internal List<string> GetSaleMaster(string dbtype, string branchId, bool isCommaSeparated = false)
        {
            
            {

                MsSqlDbManager msSqlDb = new MsSqlDbManager();
                if (isCommaSeparated)
                {
                    sale_qry = sale_qry.Replace("#cols#", $"CONCAT({master_qry_cols})");
                }
                else
                {
                    sale_qry = sale_qry.Replace("#cols#", master_qry_cols);
                }
                // OrderStatus='Invoice' and (IsUploaded != 1 or IsUploaded is null) LIMIT 1000";
                if (JvInCaseOfQtsale(dbtype, branchId) == "Yes")
                {
                    sale_qry = sale_qry.Replace("OrderStatus='Invoice' and ", string.Empty);
                }
                DataTable PendingOrdersSQlite = msSqlDb.GetDataTable(sale_qry);
                List<string> saleMasters = new List<string>();
                saleMasters.Add(Model.master_header);
                for (int i = 0; i < PendingOrdersSQlite.Rows.Count; i++)
                {
                    DataRow row = PendingOrdersSQlite.Rows[i];

                    saleMasters.Add(row.Field<string>(0));
                }
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
                //if (isCommaSeparated)
                //{
                //    sale_detail_qry = sale_detail_qry.Replace("#cols#", $"CONCAT({master_qry_cols})");
                //}
                //else
                {
                    sale_detail_qry = sale_detail_qry.Replace("#cols#", Model.detail_header);
                }
                DataTable PendingOrdersSQlite = msSqlDb.GetDataTable(sale_detail_qry+" where BillId = '" + BillId + "'");
                Converter converter = new Converter();
                List<InvSaleDetail> saleDetails = Converter.GetInvSaleDetails(PendingOrdersSQlite);
                return saleDetails;
            }
        }
        internal List<string> GetSaleDetails(decimal BillId, string dbtype, bool isCommaSeparated = false)
        {
            
            {
                MsSqlDbManager msSqlDb = new MsSqlDbManager();
                if (isCommaSeparated)
                {
                    sale_detail_qry = sale_detail_qry.Replace("#cols#", $"CONCAT({Model.detail_header})");
                }
                else
                {
                    sale_detail_qry = sale_detail_qry.Replace("#cols#", Model.detail_header);
                }
                DataTable PendingOrdersSQlite = msSqlDb.GetDataTable(sale_detail_qry + " where BillId = '" + BillId + "'");
                List<string> saleMasters = new List<string>();
                saleMasters.Add(Model.detail_header);
                for (int i = 0; i < PendingOrdersSQlite.Rows.Count; i++)
                {
                    DataRow row = PendingOrdersSQlite.Rows[i];

                    saleMasters.Add(row.Field<string>(0));
                }
                return saleMasters;
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
