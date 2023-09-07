using AutoSynchPosService.Classes;
using AutoSynchSqlite.DbManager;
using AutoSynchSqlServer.Models;
using AutoSynchSqlServerLocal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchPosService.DAOs
{
    internal class InvPurchaseDao
    {
        internal List<InvPurchaseMaster> GetPurchaseMaster(string dbtype)
        {
            if(dbtype.Equals(Constants.Sqlite))
            {

                SqliteManager sqlite = new SqliteManager();
                DataTable PendingOrdersSQlite = sqlite.GetDataTable("select * from InvPurchaseMaster where IsDeleted = 0 and IsCanceled = 0 and (IsUploaded != 1 or IsUploaded is null) LIMIT 1000");
                Converter converter = new Converter();
                List<InvPurchaseMaster> PurchaseMasters = Converter.GetInvPurchaseMaster(PendingOrdersSQlite);
                return PurchaseMasters;
            }
            else
            {

                MsSqlDbManager msSqlDb = new MsSqlDbManager();
                DataTable PendingOrdersSQlite = msSqlDb.GetDataTable(@"select top(1000) Id,
                    InvoiceNo,
                    InvoiceDate,
                    VendorId ,
                    InvoiceDisc ,
                    Frieght ,
                    IsReturn,
                    PaymentTypeId,
                    InvoiceTotal ,                    
                    GrandTotal,
                    ReferenceId,
                    Source,
                    WarehouseId,
                    BranchId,
                    IsCancel,
                    Status,
                    CreatedBy,
                    CreatedDate,
                    UpdatedBy,
                    UpdatedDate,
                    CurrencyId,
                    CurrencyRate,
                    GatePassNo ,
                    BiltyNo,
                    BiltyDate,
                    VehicleNo ,
                    DriverName,
                    DriverContactNo,
                    Commission,
                    Tax,
                    Remarks ,                    
                    FiscalYearId,
                    LoadingCharges,
                    OtherCharges,
                    GatePassId,
                    GrandTotalBeforeWhTax,
                    WithholdingTaxInAmount,
                    WithholdingTaxInPer ,
                    PaymentType,                    
                    AdvanceTaxAmount,
                    CancelRemarks,
IsUploaded from InvPurchaseMaster where IsCancel = 0 and (IsUploaded != 1 or IsUploaded is null)");
                Converter converter = new Converter();
                List<InvPurchaseMaster> purchaseMasters = Converter.GetInvPurchaseMaster(PendingOrdersSQlite);
                return purchaseMasters;
            }
        }
        internal List<InvPurchaseDetail> GetPurchaseDetails(decimal MasterId, string dbtype)
        {
            string qry = @"select Id,AdditionalTaxAmount,AverageCost,BatchNo,CostPrice,CutQty,
Disc,ExpiryDate,IsBatchChange,MasterId,ProductId,Qty,RetailPrice,SaleTaxInPercent,
Scheme,TaxAmount,UnitId from InvPurchaseDetail where MasterId= '" + MasterId + "'";
            if (dbtype.Equals(Constants.Sqlite))
            {
                SqliteManager sqlite = new SqliteManager();
                DataTable PendingOrdersSQlite = sqlite.GetDataTable(qry);
                Converter converter = new Converter();
                List<InvPurchaseDetail> purchaseDetails = Converter.GetInvPurchaseDetails(PendingOrdersSQlite);
                return purchaseDetails;
            }
            else
            {
                MsSqlDbManager msSqlDb = new MsSqlDbManager();
                DataTable PendingOrdersSQlite = msSqlDb.GetDataTable(qry);
                Converter converter = new Converter();
                List<InvPurchaseDetail> invPurchaseDetails = Converter.GetInvPurchaseDetails(PendingOrdersSQlite);
                return invPurchaseDetails;
            }
        }
        internal List<InvProductLedger> GetProductLedgers(decimal MasterId, string dbtype)
        {
            string qry = @"select Id,AverageCost,BatchBarcode,BatchNo,BranchId,Cost,CreatedBy,
CreatedDate,ExpiryDate,FiscalYearId,IsCancel,MaterialId,PackageId,ProductId,
QtyCut,QtyIn,QtyOut,ReferenceId,Remarks,RetailPrice,Source,SourceParty,
TransDate,UnitId,WarehouseId from InvProductLedger where ReferenceId= '" + MasterId + "'";
            if (dbtype.Equals(Constants.Sqlite))
            {
                SqliteManager sqlite = new SqliteManager();
                DataTable PendingOrdersSQlite = sqlite.GetDataTable(qry);
                Converter converter = new Converter();
                List<InvProductLedger> productLedgers = Converter.GetInvProductLedgers(PendingOrdersSQlite);
                return productLedgers;
            }
            else
            {
                MsSqlDbManager msSqlDb = new MsSqlDbManager();
                DataTable PendingOrdersSQlite = msSqlDb.GetDataTable(qry);
                Converter converter = new Converter();
                List<InvProductLedger> productLedgers = Converter.GetInvProductLedgers(PendingOrdersSQlite);
                return productLedgers;
            }
        }
        internal bool UpdateMasterIsUploaded(List<int> Ids, string dbtype)
        {
            SqliteManager sqlite = new SqliteManager();
            List<string> queries = new List<string>();
            string tblName = "InvSaleMaster";
            if (dbtype.Equals(Constants.Sqlite))
                tblName = "InvPurchaseMaster";
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
select Id from InvSaleMaster where TRY_CONVERT(DATE, OrderDate)<'"+Utility.GetDateTimeStringDDMMYYYY(Utility.GetOldDateTime(daysToDeleteQT))+"' and OrderStatus='QT')");

            queries.Add(@"delete from InvSaleMaster where TRY_CONVERT(DATE, OrderDate)<'" + Utility.GetDateTimeStringDDMMYYYY(Utility.GetOldDateTime(daysToDeleteQT)) + "' and OrderStatus='QT'");
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
