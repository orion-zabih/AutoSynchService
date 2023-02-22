using AutoSynchService.Classes;
using AutoSynchService.Models;
using AutoSynchSqlite.DbManager;
using AutoSynchSqlServer.Models;
using AutoSynchSqlServerLocal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchService.DAOs
{
    internal class InvSaleDao
    {
        internal List<InvSaleMaster> GetSaleMaster(string dbtype)
        {
            if(dbtype.Equals(Constants.Sqlite))
            {

                SqliteManager sqlite = new SqliteManager();
                DataTable PendingOrdersSQlite = sqlite.GetDataTable("select * from InvSaleMasterTmp where IsDeleted = 0 and IsCanceled = 0 and (IsUploaded != 1 or IsUploaded is null) LIMIT 1000");
                Converter converter = new Converter();
                List<InvSaleMaster> saleMasters = Converter.GetInvSaleMaster(PendingOrdersSQlite);
                return saleMasters;
            }
            else
            {

                MsSqlDbManager msSqlDb = new MsSqlDbManager();
                DataTable PendingOrdersSQlite = msSqlDb.GetDataTable("select top(1000) * from InvSaleMaster where IsDeleted = 0 and IsCanceled = 0 and (IsUploaded != 1 or IsUploaded is null)");
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
                DataTable PendingOrdersSQlite = msSqlDb.GetDataTable("select * from InvSaleDetail where BillId = '" + BillId + "'");
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
    }
}
