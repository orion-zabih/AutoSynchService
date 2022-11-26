using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AutoSynchSqlite.DbManager
{
    public class SqliteManager
    {

        string dbConnection;

        public SqliteManager()
        {
            _SqliteManager();

        }

        private void _SqliteManager()
        {
            try
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
              .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
              .AddJsonFile("appsettings.json")
              .Build();
                dbConnection = configuration.GetConnectionString("DefaultSqliteConnection");//"Data Source=C:\\autosynch\\cms_bakeman_db.db;Version=3;New=True;Compress=True;";

                //string dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                //string DbBinFile = "localLibrary.sqlite";
                //string DbBinPath = string.Concat(dir, "/", DbBinFile);
                //string projectName = "DOORSTEP_APP";
                //// string FullDbFolderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + DbFolder;
                //dbConnection = "Data Source=" + DbBinPath;// + DbName;
                //System.Reflection.Assembly myassembly = System.Reflection.Assembly.GetExecutingAssembly();
                //FileInfo fi = new FileInfo(myassembly.Location);

                //string binPath = NativeMethods.AssemblyDirectory;
                //var index = binPath.IndexOf(projectName, binPath.IndexOf(projectName));

                //var aStringBuilder = new StringBuilder(binPath);
                //aStringBuilder.Remove(index, projectName.Length);
                //aStringBuilder.Insert(index, "DataAccess");
                //binPath = aStringBuilder.ToString();

                //string xpath = "x86";
                //if (IntPtr.Size == 8) // or: if(Environment.Is64BitProcess) // .NET 4.0
                //{
                //    xpath = "x64";
                //}

                //string path = binPath + "\\" + xpath + "\\SQLite.Interop.DLL";
                ////string paths = @"C:\Users\TNDUser\Documents\Visual Studio 2017\Projects\NcsBarcodeApp\DataAccess\bin\Debug\x86\\SQLite.Interop.DLL";
                //System.IntPtr moduleHandle = NativeMethods.LoadLibrary(path);

            }
            catch (Exception ex)
            {
                Log("[SqliteManager:SqliteManager()]", ex.Message);
            }
        }

        public DataTable GetDataTable(string Query)
        {
            try
            {
                using (SQLiteConnection sqliteCon = new SQLiteConnection(dbConnection))
                {
                    sqliteCon.Open();
                    SQLiteCommand cmd = new SQLiteCommand(Query, sqliteCon);
                    
                    SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    cmd.Dispose();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                Log("[SqliteManager:GetDataTable()]", ex.Message);
                return new DataTable();
            }
        }
        public int ExecuteNonQuery(string Query)
        {
            int rowsUpdated = 0;
            try
            {
                using (SQLiteConnection sqliteCon = new SQLiteConnection(dbConnection))
                {
                    sqliteCon.Open();
                    SQLiteCommand mycommand = new SQLiteCommand(sqliteCon);
                    mycommand.CommandText = Query;
                    rowsUpdated = mycommand.ExecuteNonQuery();
                    mycommand.Dispose();
                    //sqliteCon.Dispose();
                }

                return rowsUpdated;
            }
            catch (Exception ex)
            {
                Log("[SqliteManager:ExecuteNonQuery()]", ex.Message);
                Log("[SqliteManager:dbPath]", dbConnection);
                if (Query.Contains("create table"))
                {
                    return -1;
                }
                else
                    return 0;
            }
            finally
            {

            }
        }
        public string ExecuteScalar(string Query)
        {
            try
            {

                using (SQLiteConnection sqliteCon = new SQLiteConnection(dbConnection))
                {
                    sqliteCon.Open();
                    SQLiteCommand mycommand = new SQLiteCommand(sqliteCon);
                    mycommand.CommandText = Query;
                    object value = mycommand.ExecuteScalar();
                    mycommand.Dispose();
                    if (value != null)
                    {
                        return value.ToString();
                    }
                    return "";
                }

            }
            catch (Exception ex)
            {
                Log("[SqliteManager:ExecuteScalar()]", ex.Message);
                return "";
            }
        }


        public bool ExecuteTransaction(string Query)
        {
            try
            {
                using (SQLiteConnection sqliteCon = new SQLiteConnection(dbConnection))
                {
                    sqliteCon.Open();
                    using (var cmd = new SQLiteCommand(sqliteCon))
                    {
                        using (var transaction = sqliteCon.BeginTransaction())
                        {
                            //Add your query here.
                            cmd.CommandText = Query;
                            try
                            {
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                throw ex;
                            }
                            transaction.Commit();

                        }
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                Log("[SqliteManager:ExecuteTransaction()]", ex.Message);
                return false;
            }


        }
        public bool ExecuteTransactionMultiQueries(List<string> Queries)
        {
            try
            {
                using (SQLiteConnection sqliteCon = new SQLiteConnection(dbConnection))
                {
                    sqliteCon.Open();
                    using (var cmd = new SQLiteCommand(sqliteCon))
                    {
                        using (var transaction = sqliteCon.BeginTransaction())
                        {
                            //Add your query here.
                            try
                            {
                                foreach (string Query in Queries)
                                {
                                    cmd.CommandText = Query;
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                throw ex;
                            }
                            transaction.Commit();

                        }
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                Log("[SqliteManager:ExecuteTransactionMultiQueries()]", ex.Message);
                return false;
            }


        }

        public bool CreateDbFile(string Path)
        {
            try
            {
                SQLiteConnection.CreateFile(Path);
                return true;
            }
            catch (Exception ex)
            {
                Log("[SqliteManager:CreateDbFile()]", ex.Message);
                return false;
            }
        }

        public bool DeleteDbFile(string Path)
        {
            try
            {
                if (File.Exists(Path))
                {
                    //try
                    //{
                    //    ClearDB();
                    SQLiteConnection.ClearAllPools();

                    //}
                    //catch (Exception)
                    //{
                    //}


                    SQLiteConnection sqliteCon = new SQLiteConnection(dbConnection);
                    sqliteCon.Close();
                    sqliteCon.Dispose();

                    GC.Collect();
                    File.Delete(Path);//error throws from here.     
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log("[SqliteManager:DeleteDbFile()]", ex.Message);
                return false;
            }

        }
        /// <summary>
        ///     Allows the programmer to easily delete all data from the DB.
        /// </summary>
        /// <returns>A boolean true or false to signify success or failure.</returns>
        public bool ClearDB()
        {
            DataTable tables;
            try
            {
                tables = this.GetDataTable("select NAME from SQLITE_MASTER where type='table' order by NAME;");
                foreach (DataRow table in tables.Rows)
                {
                    this.ClearTable(table["NAME"].ToString());
                }
                return true;
            }
            catch (Exception ex)
            {
                Log("[SqliteManager:ClearDB()]", ex.Message);
                return false;
            }
        }

        /// <summary>
        ///     Allows the user to easily clear all data from a specific table.
        /// </summary>
        /// <param name="table">The name of the table to clear.</param>
        /// <returns>A boolean true or false to signify success or failure.</returns>
        public bool ClearTable(String table)
        {
            try
            {
                this.ExecuteNonQuery(String.Format("delete from {0};", table));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static void Log(string logType, string description)
        {
            string logdrive = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string LogFolderName = logdrive + "\\LogFolder\\";
            if (!Directory.Exists(LogFolderName))
            {
                Directory.CreateDirectory(LogFolderName);
            }
            string logfilename = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            string isLogEnabledFromConfig = "true";
            bool isLogEnabled = false;
            bool.TryParse(isLogEnabledFromConfig, out isLogEnabled);

            if (isLogEnabled)
            {
                DirectoryInfo folder = new DirectoryInfo(LogFolderName);

                if (folder.Exists == false)
                {
                    folder.Create();
                    logType = "Warning";
                    description = "The logs folder was deleted, hence the process has re-created";
                }

                StreamWriter streamwriter = new StreamWriter(File.Open(LogFolderName + logfilename, FileMode.Append));
                streamwriter.WriteLine(DateTime.Now + " | " + logType + " | " + description + " | ");
                streamwriter.Flush();
                streamwriter.Close();
            }
        }
    }

}
