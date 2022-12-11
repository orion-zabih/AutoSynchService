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
    internal class SynchSettingsDao
    {
        internal bool CheckSynchTable()
        {
            SqliteManager sqlite = new SqliteManager();
            DataTable dt = sqlite.GetDataTable("SELECT name FROM sqlite_master WHERE type='table' AND name='synch_setting'");
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            return true;
        }
        internal List<SynchSetting> GetPendingSynchSetting(string synch_method)
        {
            SqliteManager sqlite = new SqliteManager();
            DataTable PendingOrdersSQlite = sqlite.GetDataTable("select * from synch_setting where synch_method='"+synch_method+ "' and status = 'ready' order by insertion_timestamp desc");
            Converter converter = new Converter();
            List<SynchSetting> saleDetails = Converter.GetSynchSetting(PendingOrdersSQlite);
            return saleDetails;
        }
        internal bool UpdatePendingSynchSettings(List<int> Ids,string status)
        {
            SqliteManager sqlite = new SqliteManager();
            List<string> queries = new List<string>();
            string format = "yyyy-MM-dd HH:mm:ss";
            Ids.ForEach(id =>
            queries.Add("update synch_setting set status = '"+ status + "',update_timestamp='"+DateTime.Now.ToString(format) +"' where setting_id = '" + id + "'")
            );
            sqlite.ExecuteTransactionMultiQueries(queries);
            return true;
        }
    }
}
