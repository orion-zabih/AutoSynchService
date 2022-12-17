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
        //private static string filePath = Environment.CurrentDirectory;
        public string filePath { get; set; } = string.Empty;
        public string filename { get; set; } = string.Empty;
        public string bkupFilename { get; set; } = string.Empty;
        internal string BackupDatabase(string sourcDpPath) {
            filename = Path.GetFileName(sourcDpPath);
            bkupFilename = Path.GetFileNameWithoutExtension(filename) + ".bak";

            //CreateDB(filePath, filename);
            filePath = Path.GetDirectoryName(sourcDpPath);
            BackupDB(filePath, filename, bkupFilename);
           
            string format = "yyyyMMddHHmmss";
           return filePath;
        }

        internal  bool RestoreDB(string filePath, string srcFilename, string destFileName, bool IsCopy = false)
        {
            var srcfile = Path.Combine(filePath, srcFilename);
            var destfile = Path.Combine(filePath, destFileName);

            if (File.Exists(destfile)) File.Delete(destfile);

            if (IsCopy)
                BackupDB(filePath, srcFilename, destFileName);
            else
                File.Move(srcfile, destfile);
            return true;
        }

        private static void BackupDB(string filePath, string srcFilename, string destFileName)
        {
            var srcfile = Path.Combine(filePath, srcFilename);
            var destfile = Path.Combine(filePath, destFileName);

            if (File.Exists(destfile)) File.Delete(destfile);

            try
            {

                File.Copy(srcfile, destfile);
            }
            catch (Exception)
            {

            }
        }

        private static void CreateDB(string filePath, string filename)
        {
            var fullfile = Path.Combine(filePath, filename);
            if (File.Exists(fullfile)) File.Delete(fullfile);

            File.WriteAllText(fullfile, "this is the dummy data");
        }
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
