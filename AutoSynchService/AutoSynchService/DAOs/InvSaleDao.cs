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
        internal List<InvSaleDetail> GetSaleDetails()
        {
            SqliteManager sqlite = new SqliteManager();
            DataTable PendingOrdersSQlite = sqlite.GetDataTable("select * from InvSaleDetailTmp");
            Converter converter = new Converter();
            List<InvSaleDetail> saleDetails = Converter.GetInvSaleDetails(PendingOrdersSQlite);
            return saleDetails;
        }
    }
}
