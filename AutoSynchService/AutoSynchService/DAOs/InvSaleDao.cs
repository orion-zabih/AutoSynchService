using AutoSynchService.Classes;
using AutoSynchService.Models;
using AutoSynchSqlite.DbManager;
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
        internal List<InvSaleMaster> GetSaleMaster()
        {
            SqliteManager sqlite = new SqliteManager();
            DataTable PendingOrdersSQlite = sqlite.GetDataTable("select * from InvSaleMaster where IsDeleted = 0 and IsCanceled = 0 and (IsUploaded != 1 or IsUploaded is null) LIMIT 1000");
            Converter converter = new Converter();
            List<InvSaleMaster> saleMasters = Converter.GetInvSaleMaster(PendingOrdersSQlite);
            return saleMasters;
        }
        internal List<InvSaleDetail> GetSaleDetails(decimal BillId)
        {
            SqliteManager sqlite = new SqliteManager();
            DataTable PendingOrdersSQlite = sqlite.GetDataTable("select * from InvSaleDetail where BillId = '" + BillId + "'");
            Converter converter = new Converter();
            List<InvSaleDetail> saleDetails = Converter.GetInvSaleDetails(PendingOrdersSQlite);
            return saleDetails;
        }
        internal bool UpdateMasterIsUploaded(List<int> Ids)
        {
            SqliteManager sqlite = new SqliteManager();
            List<string> queries = new List<string>();
            Ids.ForEach(id =>
            queries.Add("update InvSaleMaster set IsUploaded = 1 where Id = '" + id + "'")
            );
            sqlite.ExecuteTransactionMultiQueries(queries);
            return true;
        }
    }
}
