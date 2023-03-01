using AutoSynchFtp;
using AutoSynchService.ApiClient;
using AutoSynchService.Classes;
using AutoSynchService.DAOs;
using AutoSynchService.Models;
using AutoSynchSqlServerLocal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchService
{
    internal static class BusinessLogic
    {
        public static bool UploadInvSaleToServer(string dbtype)
        {
            try
            {
                InvSaleDao invSaleDao = new InvSaleDao();
                DataResponse dataResponse = new DataResponse();
                dataResponse.invSaleMaster = invSaleDao.GetSaleMaster(dbtype);
                if(dataResponse.invSaleMaster != null && dataResponse.invSaleMaster.Count>0)
                {

                    dataResponse.invSaleMaster.ForEach(m => {
                        dataResponse.invSaleDetails.AddRange(invSaleDao.GetSaleDetails(m.Id, dbtype));
                    });
                    if(dataResponse.invSaleDetails!=null && dataResponse.invSaleDetails.Count>0)
                    {
                        InvSaleClient invSaleClient = new InvSaleClient();
                        Console.WriteLine("uploading sales data");
                        ApiResponse apiResponse = invSaleClient.PostInvSaleDetails(dataResponse);
                        if (apiResponse.Code == ApplicationResponse.SUCCESS_CODE)
                        {
                            Console.WriteLine("sales data uploaded successfully");
                            invSaleDao.UpdateMasterIsUploaded(dataResponse.invSaleMaster.Select(m => m.Id).ToList(), dbtype);

                        }
                        else
                        {
                            Console.WriteLine(apiResponse.Message);
                            Console.WriteLine("sales data did not uploaded successfully. please contact support");
                        }
                    }
                    else
                    {
                        Console.WriteLine("no pending sales data");
                    }
                }
                else
                {
                    Console.WriteLine("no pending sales data");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            
            return true;
        }

        public static bool DownloadPublish(FtpCredentials ftpCredentials) 
        {
            FtpManager ftpManager = new FtpManager();
                      
            
            string folderName =ftpCredentials.Directory;
            string filename = "Release.zip";
            string localFolder = System.IO.Path.Combine(folderName, "Publish Folder");
            string binFolder = System.IO.Path.Combine(folderName, "bin");
            string pathString = System.IO.Path.Combine(folderName, "Publish Folder", filename);
            string extractPath = System.IO.Path.Combine(folderName, "Publish Extract Folder");
            if(!Directory.Exists(localFolder))
                System.IO.Directory.CreateDirectory(localFolder);
            if (!Directory.Exists(extractPath)) 
                System.IO.Directory.CreateDirectory(extractPath);

            if ( ftpManager.DownloadFilesFromFtp(ftpCredentials.IP, ftpCredentials.Port, ftpCredentials.Username, ftpCredentials.Password, pathString, filename))
            {

                ZipFile.ExtractToDirectory(pathString, extractPath,true);

                string pathBackupString = string.Empty;
                
                if (!Directory.Exists(binFolder)) 
                    System.IO.Directory.CreateDirectory(binFolder);
                else
                {
                    string format = "yyyy-MM-dd HHmmss";
                    pathBackupString = System.IO.Path.Combine(folderName, "Publish Folder", "backup_" + DateTime.Now.ToString(format) + ".zip");
                    ZipFile.CreateFromDirectory(binFolder, pathBackupString);
                }
                try
                {
                    CopyFilesRecursively(extractPath, binFolder);
                }
                catch (Exception)
                {
                    //if fails then restore backup 
                    if(!string.IsNullOrEmpty(pathBackupString))
                        ZipFile.ExtractToDirectory(pathBackupString, binFolder, true);
                }
                return true;
            }
            return false;
        }
        private static void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            }
        }
        internal static bool isFreshdb { get; set; }
        //public static bool GetAndReplaceSysTablesSqlServer()
        //{
        //    try
        //    {
        //        SysTablesClient sysTablesClient = new SysTablesClient();
        //        bool fileExists = false;
        //        string dbName = string.Empty;
        //        string dbPath = string.Empty;
        //        int dbSize = 10;
        //        int dbMaxSize = 20;
        //        int dbGrowth = 10;
        //        IConfigurationRoot configuration = new ConfigurationBuilder()
        //     .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        //     .AddJsonFile("appsettings.json")
        //     .Build();
        //        dbPath = configuration.GetConnectionString("SqlDbPath");
        //        dbName = configuration.GetConnectionString("SqlDb");
        //        dbSize = int.Parse(configuration.GetConnectionString("SqlDbSize"));
        //        dbMaxSize = int.Parse(configuration.GetConnectionString("SqlDbMaxSize"));
        //        dbGrowth = int.Parse(configuration.GetConnectionString("SqlDbGrowth"));

        //        SynchSettingsDao synchSettingsDao = new SynchSettingsDao();

        //        List<SynchSetting> pendingSynchSettings = new List<SynchSetting>();
        //        SynchSetting lastSynchSetting = null;
        //        SynchTypes synchType = SynchTypes.full;
        //        MsSqlDbManager dbManager = new MsSqlDbManager();
        //        if (!dbManager.CheckDatabaseExists(dbName))
        //        {
        //            isFreshdb = true;
        //            if (dbManager.CreateDb(dbName,dbPath,dbSize,dbMaxSize,dbGrowth))
        //            synchType = SynchTypes.full;
        //            else
        //            {
        //                Console.WriteLine("unable to create database");
        //                return false; }    
        //        }
        //        else
        //        {
        //            fileExists = true;
        //            if (!synchSettingsDao.CheckSynchTable(Constants.SqlServer))
        //            {
        //                synchType = SynchTypes.except_sale_master_detail_tables;
        //            }
        //            else
        //            {
        //                pendingSynchSettings = synchSettingsDao.GetPendingSynchSetting("database",Constants.SqlServer);
        //                if (pendingSynchSettings != null && pendingSynchSettings.Count() > 0)
        //                {
        //                    lastSynchSetting = pendingSynchSettings[0];
        //                    if (lastSynchSetting != null)
        //                    {
        //                        Enum.TryParse(lastSynchSetting.synch_type, out synchType);
        //                    }
        //                    else
        //                        return false;
        //                }
        //            }
        //        }
        //        TableStructureResponse tableStructureResponse = sysTablesClient.GetTableStructure(synchType, Constants.SqlServer);
        //        if (tableStructureResponse != null)
        //        {
        //            if (fileExists)
        //            {
        //                //string DefaultSqliteBackup = configuration.GetConnectionString("DefaultSqliteBackup");
        //                //string dbConnection = configuration.GetConnectionString("DefaultSqliteConnection");
        //                //ReCreateStructureTables._CreateDatabase(DefaultSqliteBackup);
        //                //dbManager = new MsSqlDbManager();
        //                synchSettingsDao.BackupDatabase(dbName,dbPath);
        //            }
        //            if (lastSynchSetting != null)
        //                synchSettingsDao.UpdatePendingSynchSettings(new List<int> { lastSynchSetting.setting_id }, "in process",Constants.SqlServer);
        //            if (ReCreateStructureTables._CreateDBTables(new DateTime(), tableStructureResponse,Constants.SqlServer))
        //            {

        //                SysTablesResponse sysTablesResponse = sysTablesClient.GetTableData(synchType);
        //                if (sysTablesResponse != null)
        //                {
        //                    if (ReCreateStructureTables._InsertData(DateTime.Now, sysTablesResponse,Constants.SqlServer))
        //                    {
        //                        if (pendingSynchSettings != null && pendingSynchSettings.Count() > 0)
        //                            synchSettingsDao.UpdatePendingSynchSettings(pendingSynchSettings.Select(s => s.setting_id).ToList(), "done", Constants.SqlServer);
        //                        return true;
        //                    }
        //                    else if (fileExists)
        //                    {
        //                        synchSettingsDao.RestoreDB(synchSettingsDao.filePath, synchSettingsDao.bkupFilename, synchSettingsDao.filename, true);
        //                        if (lastSynchSetting != null)
        //                            synchSettingsDao.UpdatePendingSynchSettings(new List<int> { lastSynchSetting.setting_id }, "insert data failed", Constants.SqlServer);
        //                        return false;
        //                    }
        //                }
        //                else if (fileExists)
        //                {
        //                    synchSettingsDao.RestoreDB(synchSettingsDao.filePath, synchSettingsDao.bkupFilename, synchSettingsDao.filename, true);

        //                    if (lastSynchSetting != null)
        //                        synchSettingsDao.UpdatePendingSynchSettings(new List<int> { lastSynchSetting.setting_id }, "insert data failed", Constants.SqlServer);
        //                    return false;
        //                }
        //            }
        //            if (lastSynchSetting != null)
        //                synchSettingsDao.UpdatePendingSynchSettings(new List<int> { lastSynchSetting.setting_id }, "create table failed", Constants.SqlServer);
        //            return false;
        //        }
        //        else
        //        {
        //            Console.WriteLine("unable to receive database tables structure from service");
        //        }
        //        if (lastSynchSetting != null)
        //            synchSettingsDao.UpdatePendingSynchSettings(new List<int> { lastSynchSetting.setting_id }, "create table failed", Constants.SqlServer);
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {

        //        Console.WriteLine(ex.Message);
        //        return false;
        //    }

        //}
        public static bool GetAndReplaceTablesSqlServer()
        {
            try
            {
                SysTablesClient sysTablesClient = new SysTablesClient();
                bool fileExists = false;
                string dbName = string.Empty;
                string dbPath = string.Empty;
                int dbSize = 10;
                int dbMaxSize = 20;
                int dbGrowth = 10;
                IConfigurationRoot configuration = new ConfigurationBuilder()
             .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
             .AddJsonFile("appsettings.json")
             .Build();
                dbPath = configuration.GetConnectionString("SqlDbPath");
                dbName = configuration.GetConnectionString("SqlDb");
                dbSize = int.Parse(configuration.GetConnectionString("SqlDbSize"));
                dbMaxSize = int.Parse(configuration.GetConnectionString("SqlDbMaxSize"));
                dbGrowth = int.Parse(configuration.GetConnectionString("SqlDbGrowth"));

                SynchSettingsDao synchSettingsDao = new SynchSettingsDao();

                List<SynchSetting> pendingSynchSettings = new List<SynchSetting>();
                SynchSetting lastSynchSetting = null;
                SynchTypes synchType = SynchTypes.full;
                MsSqlDbManager dbManager = new MsSqlDbManager();
                if (!dbManager.CheckDatabaseExists(dbName))
                {
                    isFreshdb = true;
                    synchType = SynchTypes.full;
                    TableStructureResponse tableStructureResponse = sysTablesClient.GetTableStructure(synchType, Constants.SqlServer);
                    if (tableStructureResponse != null)
                    {
                        Console.WriteLine("successfully got database structure from server. now creating database.");
                        if (dbManager.CreateDb(dbName, dbPath, dbSize, dbMaxSize, dbGrowth))
                        {
                            Console.WriteLine("successfully created database file. now creating tables.");
                            if (ReCreateStructureTables._CreateDBTables(DateTime.Now, tableStructureResponse, Constants.SqlServer))
                            {
                                Console.WriteLine("successfully created tables in database");
                                return true;
                            }
                            else
                            {
                                Console.WriteLine("unable to create tables in database");
                                return false;
                            }
                        }
                        else
                        {
                            Console.WriteLine("unable to create database");
                            return false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("unable to receive database tables structure from service");
                        return false;
                    }
                }
                else
                {
                    pendingSynchSettings = synchSettingsDao.GetPendingSynchSetting("database", Constants.SqlServer, "ready");
                    if (pendingSynchSettings != null && pendingSynchSettings.Count > 0)
                    {
                        lastSynchSetting = pendingSynchSettings.OrderByDescending(s => s.setting_id).First();
                        if (lastSynchSetting != null)
                        {
                            if(lastSynchSetting.synch_method ==SynchMethods.database_structure.ToString() && (lastSynchSetting.sync_timestamp.HasValue && DateTime.Compare(lastSynchSetting.sync_timestamp.Value, DateTime.Now) > 0))
                            {
                                if (Enum.TryParse<SynchTypes>(lastSynchSetting.synch_type, true, out synchType))  // ignore cases
                                {
                                    TableStructureResponse tableStructureResponse = sysTablesClient.GetTableStructure(synchType, Constants.SqlServer);
                                    if (tableStructureResponse != null)
                                    {
                                        Console.WriteLine("successfully got database structure from server. now creating database.");
                                        if (dbManager.CreateDb(dbName, dbPath, dbSize, dbMaxSize, dbGrowth))
                                        {
                                            Console.WriteLine("successfully created database file. now creating tables.");
                                            if (ReCreateStructureTables._CreateDBTables(DateTime.Now, tableStructureResponse, Constants.SqlServer))
                                            {
                                                Console.WriteLine("successfully created tables in database");
                                                return true;
                                            }
                                            else
                                            {
                                                Console.WriteLine("unable to create tables in database");
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("unable to create database");
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("unable to receive database tables structure from service");
                                        return false;
                                    }
                                }
                                
                            }
                        }
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;
            }

        }
        public static bool GetAndReplaceDataSqlServer(int recordsToFetch)
        {
            try
            {
                SysTablesClient sysTablesClient = new SysTablesClient();
                bool fileExists = false;
                string dbName = string.Empty;
                string dbPath = string.Empty;
                int dbSize = 10;
                int dbMaxSize = 20;
                int dbGrowth = 10;
                IConfigurationRoot configuration = new ConfigurationBuilder()
             .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
             .AddJsonFile("appsettings.json")
             .Build();
                dbPath = configuration.GetConnectionString("SqlDbPath");
                dbName = configuration.GetConnectionString("SqlDb");
                dbSize = int.Parse(configuration.GetConnectionString("SqlDbSize"));
                dbMaxSize = int.Parse(configuration.GetConnectionString("SqlDbMaxSize"));
                dbGrowth = int.Parse(configuration.GetConnectionString("SqlDbGrowth"));

                SynchSettingsDao synchSettingsDao = new SynchSettingsDao();

                List<SynchSetting> pendingSynchSettings = new List<SynchSetting>();
                SynchSetting lastSynchSetting = null;
                SynchTypes synchType = SynchTypes.full;
                MsSqlDbManager dbManager = new MsSqlDbManager();
                pendingSynchSettings = synchSettingsDao.GetPendingSynchSetting(SynchMethods.database_data.ToString(), Constants.SqlServer,"ready");
                if(pendingSynchSettings != null && pendingSynchSettings.Count>0)
                {
                    lastSynchSetting= pendingSynchSettings.OrderBy(s=>s.setting_id).First();
                    if (lastSynchSetting != null)
                    {
                        if (lastSynchSetting.synch_method == SynchMethods.database_data.ToString() && (!lastSynchSetting.sync_timestamp.HasValue || (lastSynchSetting.sync_timestamp.HasValue && DateTime.Compare(lastSynchSetting.sync_timestamp.Value, DateTime.Now) <= 0)))
                        {
                            if (Enum.TryParse<SynchTypes>(lastSynchSetting.synch_type, true, out synchType))  // ignore cases
                            {
                                if(!lastSynchSetting.synch_type.Equals(SynchTypes.products_quick.ToString()))
                                {
                                    SysTablesResponse sysTablesResponse = sysTablesClient.GetTableData(synchType);
                                    if (sysTablesResponse != null)
                                    {
                                        if (ReCreateStructureTables._InsertData(DateTime.Now, sysTablesResponse, null, Constants.SqlServer))
                                        {
                                            synchSettingsDao.UpdatePendingSynchSettings(pendingSynchSettings.Select(s => s.setting_id).ToList(), "done", Constants.SqlServer);
                                            synchSettingsDao.InsertSynchSettings(SynchMethods.database_data.ToString(), SynchTypes.products_quick.ToString(), DateTime.Now, "ready", Constants.SqlServer);
                                           // synchSettingsDao.InsertSynchSettings(SynchMethods.database_data.ToString(), SynchTypes.products_ledger_quick.ToString(), DateTime.Now, "ready", Constants.SqlServer);
                                            return true;
                                        }

                                    }
                                }
                                else
                                {
                                    bool downlaodedAll = false;
                                    while (!downlaodedAll)
                                    {
                                        ProductsDao productsDao = new ProductsDao();
                                        InvProductsResponse invProductsResponse = null;
                                        if (lastSynchSetting.synch_type.Equals(SynchTypes.products_quick.ToString()))
                                        {
                                            Console.WriteLine("Getting some products");
                                            invProductsResponse= sysTablesClient.GetProducts(synchSettingsDao.GetMaxId("InvProduct", "Id").ToString(), recordsToFetch);
                                        }
                                        //else if (lastSynchSetting.synch_type.Equals(SynchTypes.products_ledger_quick.ToString())){

                                        //    invProductsResponse = sysTablesClient.GetProducts(synchSettingsDao.GetMaxId("InvProductLedger", "Id").ToString(), "true");
                                        //}
                                         
                                        if (invProductsResponse != null)
                                        {
                                            Console.WriteLine("Saving products.");
                                            if (ReCreateStructureTables._InsertData(DateTime.Now, null,invProductsResponse, Constants.SqlServer))
                                            {
                                                if (invProductsResponse.Response.Code == ApplicationResponse.MAX_REACHED_CODE)
                                                {
                                                    Console.WriteLine("All products downloaded.");
                                                    downlaodedAll = true;
                                                }
                                                
                                            }

                                        }

                                    }
                                    synchSettingsDao.UpdatePendingSynchSettings(new List<int>{ lastSynchSetting.setting_id}, "done", Constants.SqlServer);
                                    return true;
                                }
                                
                            }
                        }
                    }
                }
               
                if (lastSynchSetting != null)
                    synchSettingsDao.UpdatePendingSynchSettings(new List<int> { lastSynchSetting.setting_id }, "create table failed", Constants.SqlServer);
                return false;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;
            }

        }
        public static bool GetProductsOnlySqlServer(int recordsToFetch)
        {
            try
            {
                SysTablesClient sysTablesClient = new SysTablesClient();
                ProductsDao productsDao = new ProductsDao();
                InvProductsResponse invProductsResponse = null;
                SynchSettingsDao synchSettingsDao = new SynchSettingsDao();
                Console.WriteLine("Getting some products");
                invProductsResponse = sysTablesClient.GetProducts("-1", recordsToFetch);
                if (invProductsResponse != null)
                {
                    Console.WriteLine("Saving products.");
                    if (ReCreateStructureTables._InsertData(DateTime.Now, null, invProductsResponse, Constants.SqlServer))
                    {
                        return true;

                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
            return false;
        }
        public static bool GetAndReplaceSysTablesSqlite()
        {
            try
            {
                SysTablesClient sysTablesClient = new SysTablesClient();
                bool fileExists=false;
                string dbPath = string.Empty;
                IConfigurationRoot configuration = new ConfigurationBuilder()
             .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
             .AddJsonFile("appsettings.json")
             .Build();
                dbPath = configuration.GetConnectionString("DbPath");
                
                SynchSettingsDao synchSettingsDao = new SynchSettingsDao();

                List<SynchSetting> pendingSynchSettings = new List<SynchSetting>();
                SynchSetting lastSynchSetting = null;
                SynchTypes synchType = SynchTypes.full;
                
                if (!File.Exists(dbPath))
                {
                    isFreshdb = true;
                    ReCreateStructureTables._CreateDatabase(dbPath);
                    synchType = SynchTypes.full;
                }
                else
                {
                    fileExists=true;
                    if (!synchSettingsDao.CheckSynchTable(Constants.Sqlite))
                    {
                        synchType = SynchTypes.except_sale_master_detail_tables;
                    }
                    else
                    {
                        pendingSynchSettings = synchSettingsDao.GetPendingSynchSetting("database", Constants.Sqlite);
                        if(pendingSynchSettings != null && pendingSynchSettings.Count() > 0)
                        {
                            lastSynchSetting=pendingSynchSettings[0];
                            if (lastSynchSetting != null)
                            {
                                Enum.TryParse(lastSynchSetting.synch_type, out synchType);
                            }
                            else
                                return false;
                        }
                    }
                }
                TableStructureResponse tableStructureResponse = sysTablesClient.GetTableStructure(synchType, Constants.Sqlite);
                if (tableStructureResponse != null)
                {
                    if (fileExists)
                    {
                        //string DefaultSqliteBackup = configuration.GetConnectionString("DefaultSqliteBackup");
                        //string dbConnection = configuration.GetConnectionString("DefaultSqliteConnection");
                        //ReCreateStructureTables._CreateDatabase(DefaultSqliteBackup);
                        synchSettingsDao.BackupDatabase(dbPath);
                    }
                    if (lastSynchSetting!=null)
                        synchSettingsDao.UpdatePendingSynchSettings(new List<int> { lastSynchSetting.setting_id }, "in process", Constants.Sqlite);
                    if (ReCreateStructureTables._CreateDBTables(new DateTime(), tableStructureResponse,Constants.Sqlite))
                    {

                        SysTablesResponse sysTablesResponse = sysTablesClient.GetTableData(synchType);
                        if (sysTablesResponse != null)
                        {
                            if(ReCreateStructureTables._InsertData(DateTime.Now, sysTablesResponse,null,Constants.Sqlite))
                            {
                                if(pendingSynchSettings != null && pendingSynchSettings.Count() > 0)
                                    synchSettingsDao.UpdatePendingSynchSettings(pendingSynchSettings.Select(s=>s.setting_id).ToList(), "done", Constants.Sqlite);
                                return true;
                            }
                            else if(fileExists)
                            {
                                synchSettingsDao.RestoreDB(synchSettingsDao.filePath, synchSettingsDao.bkupFilename, synchSettingsDao.filename, true);
                                if (lastSynchSetting != null)
                                    synchSettingsDao.UpdatePendingSynchSettings(new List<int> { lastSynchSetting.setting_id }, "insert data failed", Constants.Sqlite);
                                return false;
                            }
                        }
                        else if (fileExists)
                        {
                            synchSettingsDao.RestoreDB(synchSettingsDao.filePath, synchSettingsDao.bkupFilename, synchSettingsDao.filename, true);

                            if (lastSynchSetting != null)
                                synchSettingsDao.UpdatePendingSynchSettings(new List<int> { lastSynchSetting.setting_id }, "insert data failed", Constants.Sqlite);
                            return false;
                        }
                    }
                    if (lastSynchSetting != null)
                        synchSettingsDao.UpdatePendingSynchSettings(new List<int> { lastSynchSetting.setting_id }, "create table failed", Constants.Sqlite);
                    return false;
                }
                if(lastSynchSetting!=null)
                    synchSettingsDao.UpdatePendingSynchSettings(new List<int> { lastSynchSetting.setting_id }, "create table failed", Constants.Sqlite);
                return false;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;
            }

        }
        
    }
}
