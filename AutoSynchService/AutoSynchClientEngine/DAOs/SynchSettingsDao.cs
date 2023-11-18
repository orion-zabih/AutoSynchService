using AutoSynchClientEngine.Classes;
using AutoSynchClientEngine.Classes;
using AutoSynchSqlite.DbManager;
using AutoSynchSqlServerLocal;
using FluentFTP.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchClientEngine.DAOs
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
        public bool BackupDatabase(string dbname, string dbpath)
        {
            MsSqlDbManager sqlDbManager = new MsSqlDbManager();
            try
            {

                sqlDbManager.ExecuteNonQuery($"Backup database {dbname} to disk='{dbpath + "//" + dbname}.bak'");
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool ExecuteQry(List<string> queries)
        {
            MsSqlDbManager msSqlDbManager = new MsSqlDbManager();
            try
            {
                queries.ForEach(q =>
                {
                    try
                    {
                        msSqlDbManager.ExecuteTransQuery(q);
                    }
                    catch (Exception ex)
                    {
                        Logger.write("Add Synch setting Sql Server:" + ex.Message, true);
                        
                    }
                });
                msSqlDbManager.Commit();
                return true;
            }
            catch (Exception)
            {
                msSqlDbManager.RollBack();
                throw;
            }
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
        internal bool CheckSynchTable(string dbtype)
        {
            if (dbtype.Equals(Constants.Sqlite)) {

                string qry = "SELECT name FROM sqlite_master WHERE type='table' AND name='synch_setting'";
                SqliteManager sqlite = new SqliteManager();
                DataTable dt = sqlite.GetDataTable(qry);
                if (dt.Rows.Count == 0)
                {
                    return false;
                }
                return true;
            }
            else {

                string qry = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' and TABLE_NAME='synch_setting'";
                MsSqlDbManager sqlDbManager = new MsSqlDbManager();
                DataTable dt = sqlDbManager.GetDataTable(qry);
                if (dt.Rows.Count == 0)
                {
                    return false;
                }
                return true;
            }
            
        }
        internal bool CheckSynchSetting(string dbtype)
        {
            string qry = "select * from synch_setting";
            try
            {
                if (dbtype.Equals(Constants.Sqlite))
                {
                    SqliteManager sqlite = new SqliteManager();
                    DataTable PendingOrdersSQlite = sqlite.GetDataTable(qry);
                    if (PendingOrdersSQlite.Rows.Count == 0)
                    {
                        return false;
                    }
                }
                else
                {
                    MsSqlDbManager sqlDbManager = new MsSqlDbManager();
                    DataTable PendingOrdersSQlite = sqlDbManager.GetDataTable(qry);
                    if (PendingOrdersSQlite.Rows.Count == 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {

                return false;
            }
            
            return true;
        }

        internal List<SynchSetting> GetPendingSynchSetting(string synch_method,string dbtype,string status="ready")
        {
            string qry = "select * from synch_setting where synch_method='" + synch_method + "' and status = '"+status+"'";
            if (status == null){
                qry = "select * from synch_setting where synch_method='" + synch_method + "'+";
            }
            else if (status.Equals("ready"))
            {
                qry = "select * from synch_setting where synch_method='" + synch_method + "' and status = '"+status+"'";
            }
            if (dbtype.Equals(Constants.Sqlite))
            {
                SqliteManager sqlite = new SqliteManager();
                DataTable PendingOrdersSQlite = sqlite.GetDataTable(qry);
                Converter converter = new Converter();
                List<SynchSetting> saleDetails = Converter.GetSynchSetting(PendingOrdersSQlite);
                return saleDetails;
            }
            else
            {
                MsSqlDbManager sqlDbManager = new MsSqlDbManager();
                DataTable PendingOrdersSQlite = sqlDbManager.GetDataTable(qry);
                Converter converter = new Converter();
                List<SynchSetting> saleDetails = Converter.GetSynchSetting(PendingOrdersSQlite);
                return saleDetails;
            }
        }
        internal bool UpdatePendingSynchSettings(List<int> Ids,string status, string dbtype)
        {
           
            List<string> queries = new List<string>();
            string format = "yyyy-MM-dd HH:mm:ss";
            Ids.ForEach(id =>
            queries.Add("update synch_setting set status = '"+ status + "',update_timestamp='"+DateTime.Now.ToString(format) +"' where setting_id = '" + id + "'")
            );
            if (dbtype.Equals(Constants.Sqlite))
            {
                SqliteManager sqlite = new SqliteManager();
                sqlite.ExecuteTransactionMultiQueries(queries);
                return true;
            }
            else
            {
                MsSqlDbManager sqlDbManager= new MsSqlDbManager();
                try
                {
                    queries.ForEach(q => { sqlDbManager.ExecuteTransQuery(q); });
                    sqlDbManager.Commit();
                    return true;
                }
                catch (Exception)
                {
                    sqlDbManager.RollBack();
                    return false;
                }
                
            }
        }
        internal int GetMaxId(string table,string col)
        {
            try
            {

                MsSqlDbManager sqlDbManager = new MsSqlDbManager();
                var maxid = sqlDbManager.ExecuteScalar($"select max(isnull({col},0)) from {table}");
                if (maxid == null)
                    return 0;
                else return int.Parse(s: maxid.ToString());
            }
            catch (Exception ex)
            {
                
                return 0;
            }
        }
        internal bool InsertSynchSettings(string synchMethod,string synchType,DateTime synchTime, string status, string dbtype)
        {

            List<string> queries = new List<string>();
            string format = "yyyy-MM-dd HH:mm:ss";
            string qry2 = $"insert into synch_setting(setting_id,synch_method,synch_type,table_names,status,sync_timestamp,insertion_timestamp,update_timestamp) values({GetMaxId("synch_setting", "setting_id")+1}, '" + synchMethod + "','" + synchType + "','','" + status + "','" + synchTime.ToString(format) + "','" + DateTime.Now.ToString(format) + "','" + DateTime.Now.ToString(format) + "')";
            if (dbtype.Equals(Constants.Sqlite))
            {
                SqliteManager sqlite = new SqliteManager();
                sqlite.ExecuteTransaction(qry2);
                return true;
            }
            else
            {
                MsSqlDbManager sqlDbManager = new MsSqlDbManager();
                try
                {
                    sqlDbManager.ExecuteTransQuery(qry2);
                    sqlDbManager.Commit();
                    return true;
                }
                catch (Exception)
                {
                    sqlDbManager.RollBack();
                    return false;
                }

            }
        }
        internal List<AutoSynchSqlServer.CustomModels.TableStructure> GetTableColumnsInfo(string tblName)
        {
            MsSqlDbManager sqlDbManager= new MsSqlDbManager();
            try
            {
                DataTable dataTable = sqlDbManager.GetDataTable(@"SELECT TABLE_NAME,COLUMN_NAME,COLUMN_DEFAULT,IS_NULLABLE,DATA_TYPE,CHARACTER_MAXIMUM_LENGTH
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = N'"+tblName+"'");
                Converter converter = new Converter();
                List<AutoSynchSqlServer.CustomModels.TableStructure> tblStructures = Converter.GetTableStructure(dataTable);
                return tblStructures;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
