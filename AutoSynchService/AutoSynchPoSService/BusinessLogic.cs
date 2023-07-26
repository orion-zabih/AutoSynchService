using AutoSynchFtp;
using AutoSynchPosService.Classes;
using AutoSynchPosService.DAOs;
using AutoSynchPoSService.ApiClient;
using AutoSynchPoSService.Classes;
using AutoSynchService.DAOs;
using AutoSynchSqlServer.CustomModels;
using AutoSynchSqlServerLocal;
using FluentFTP.Helpers;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchPoSService
{
    public sealed class BusinessLogic
    {
        private readonly ILogger<BusinessLogic> _logger;
        public BusinessLogic(
            ILogger<BusinessLogic> logger) =>
            (_logger) = (logger);
        internal bool isFreshdb { get; set; }
        public bool GetAndReplaceTablesSqlServer()
        {
            try
            {
                StructureNDataClient sysTablesClient = new StructureNDataClient();
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
                if (!dbManager.CheckDatabaseExists(dbName) && !File.Exists(dbPath))
                {
                    Logger.write("{POS Sale Service BL}", "db does not exist");
                    isFreshdb = true;
                    synchType = SynchTypes.full;
                    TableStructureResponse tableStructureResponse = sysTablesClient.GetTableStructure(synchType, Constants.SqlServer);
                    if (tableStructureResponse != null)
                    {
                        if (!Directory.Exists(dbPath))
                        {
                            Directory.CreateDirectory(dbPath);
                        }
                        //Console.WriteLine("successfully got database structure from server. now creating database.");
                        Logger.write("{POS Sale Service BL}", "successfully got database structure from server. now creating database.");
                        if (dbManager.CreateDb(dbName, dbPath, dbSize, dbMaxSize, dbGrowth))
                        {
                            //Console.WriteLine("successfully created database file. now creating tables.");
                            Logger.write("{POS Sale Service BL}", "successfully created database file. now creating tables.");
                            if (ReCreateStructureTables._CreateDBTables(DateTime.Now, tableStructureResponse, Constants.SqlServer))
                            {
                                Logger.write("{POS Sale Service BL}", "successfully created tables in database");
                               //Console.WriteLine("successfully created tables in database");
                                return true;
                            }
                            else
                            {
                                Logger.write("{POS Sale Service BL}", "unable to create tables in database");
                               //Console.WriteLine("unable to create tables in database");
                                return false;
                            }
                        }
                        else
                        {
                            Logger.write("{POS Sale Service BL}", "unable to create database");
                               //Console.WriteLine("unable to create database");
                            return false;
                        }
                    }
                    else
                    {
                        Logger.write("{POS Sale Service BL}", "unable to receive database tables structure from service");
                               //Console.WriteLine("unable to receive database tables structure from service");
                        return false;
                    }
                }
                else
                {
                    if (!synchSettingsDao.CheckSynchTable(Constants.SqlServer))
                    {
                        Logger.write("{POS Sale Service BL}", "db structure does not exist");
                        isFreshdb = true;
                        synchType = SynchTypes.full;
                        TableStructureResponse tableStructureResponse = sysTablesClient.GetTableStructure(synchType, Constants.SqlServer);
                        if (tableStructureResponse != null)
                        {
                            Logger.write("{POS Sale Service BL}", "successfully got database structure. now creating tables.");
                            if (ReCreateStructureTables._CreateDBTables(DateTime.Now, tableStructureResponse, Constants.SqlServer))
                            {
                                Logger.write("{POS Sale Service BL}", "successfully created tables in database");
                                //Console.WriteLine("successfully created tables in database");
                                return true;
                            }
                            else
                            {
                                Logger.write("{POS Sale Service BL}", "unable to create tables in database");
                                //Console.WriteLine("unable to create tables in database");
                                return false;
                            }
                        }
                        else
                        {
                            Logger.write("{POS Sale Service BL}", "unable to receive database tables structure from service");
                            //Console.WriteLine("unable to receive database tables structure from service");
                            return false;
                        }
                    }
                    pendingSynchSettings = synchSettingsDao.GetPendingSynchSetting(SynchMethods.database_structure.ToString(), Constants.SqlServer, "ready");
                    if (pendingSynchSettings != null && pendingSynchSettings.Count > 0)
                    {
                        lastSynchSetting = pendingSynchSettings.OrderByDescending(s => s.setting_id).First();
                        if (lastSynchSetting != null)
                        {
                            if (lastSynchSetting.synch_method == SynchMethods.database_structure.ToString() && (lastSynchSetting.sync_timestamp.HasValue && DateTime.Compare(lastSynchSetting.sync_timestamp.Value, DateTime.Now) <= 0))
                            {
                                if (Enum.TryParse<SynchTypes>(lastSynchSetting.synch_type, true, out synchType))  // ignore cases
                                {
                                    
                                    TableStructureResponse tableStructureResponse = sysTablesClient.GetTableStructure(synchType, Constants.SqlServer);
                                    if (tableStructureResponse != null)
                                    {
                                        Logger.write("{POS Sale Service BL}", "successfully got database structure from server. now creating database.");
                                        {
                                            Logger.write("{POS Sale Service BL}", "successfully created database file. now creating tables.");
                                            if (ReCreateStructureTables._CreateDBTables(DateTime.Now, tableStructureResponse, Constants.SqlServer))
                                            {
                                                synchSettingsDao.UpdatePendingSynchSettings(new List<int> { lastSynchSetting.setting_id }, "done", Constants.SqlServer);
                                                Logger.write("{POS Sale Service BL}", "successfully created tables in database");
                                                return true;
                                            }
                                            else
                                            {
                                                Logger.write("{POS Sale Service BL}", "unable to create tables in database");
                                                return false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Logger.write("{POS Sale Service BL}", "unable to receive database tables structure from service");
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
                //Logger.write("{POS Sale Service}", ex.Message);
                //Console.WriteLine(ex.Message);
                throw ex;
                //return false;
            }

        }

        public bool AlterTablesSqlServer()
        {
            try
            {
                StructureNDataClient sysTablesClient = new StructureNDataClient();

                IConfigurationRoot configuration = new ConfigurationBuilder()
             .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
             .AddJsonFile("appsettings.json")
             .Build();

                SynchSettingsDao synchSettingsDao = new SynchSettingsDao();

                List<SynchSetting> pendingSynchSettings = new List<SynchSetting>();
                SynchSetting lastSynchSetting = null;
                SynchTypes synchType = SynchTypes.full;
                MsSqlDbManager dbManager = new MsSqlDbManager();


                pendingSynchSettings = synchSettingsDao.GetPendingSynchSetting(SynchMethods.database_structure_alter.ToString(), Constants.SqlServer, "ready");
                if (pendingSynchSettings != null && pendingSynchSettings.Count > 0)
                {
                    lastSynchSetting = pendingSynchSettings.OrderByDescending(s => s.setting_id).First();
                    if (lastSynchSetting != null)
                    {
                        if (lastSynchSetting.synch_method == SynchMethods.database_structure_alter.ToString() && (lastSynchSetting.sync_timestamp.HasValue && DateTime.Compare(lastSynchSetting.sync_timestamp.Value, DateTime.Now) <= 0))
                        {
                            if (Enum.TryParse<SynchTypes>(lastSynchSetting.synch_type, true, out synchType))  // ignore cases
                            {
                                string tblLst = string.Empty;
                                if (synchType == SynchTypes.custom)
                                {
                                    tblLst = lastSynchSetting.table_names;
                                }
                                TableStructureResponse tableStructureResponse = sysTablesClient.GetTableColumns(synchType, tblLst, Constants.SqlServer);

                                if (tableStructureResponse != null)
                                {
                                    Logger.write("{POS Sale Service BL}", "successfully got database structure from server. now creating database.");
                                    {
                                        Logger.write("{POS Sale Service BL}", "successfully created database file. now creating tables.");
                                        List<string> tables = Utility.ConverCommaSeparatedToList(tblLst);
                                        List<string> queries= new List<string>();
                                        foreach (var item in tables)
                                        {
                                            List<TableStructure> localStructure = synchSettingsDao.GetTableColumnsInfo(item);
                                            List<TableStructure> onlineStructure = tableStructureResponse.tableStructures.Where(a => a.TableName == item).ToList();
                                            if(localStructure!=null && localStructure.Count > 0 && onlineStructure!=null && onlineStructure.Count > 0)
                                            {
                                                List<string> differentColumns = onlineStructure.Select(c => c.ColumnName).Except(localStructure.Select(l => l.ColumnName)).ToList();
                                                List<TableStructure> newStructure = onlineStructure.Where(o=>differentColumns.Contains(o.ColumnName)).ToList();
                                                if(newStructure.Count>0)
                                                {
                                                    string qry = "alter table " + item + " add ";
                                                    string cols = "";
                                                    newStructure.ForEach(n => {
                                                        cols += n.ColumnName + " " + n.DataType + (n.IsNullable == "YES" ? " NULL" : " NOT NULL") + (n.IsPrimaryKey == "YES" ? " PRIMARY KEY" : " ") + (!string.IsNullOrEmpty(n.ColumnDefault) ? (" DEFAULT "+ n.ColumnDefault) : "") + ",";
                                                    });
                                                    queries.Add(qry + cols.TrimEnd(','));
                                                }
                                            }
                                        }
                                        if (queries.Count > 0)
                                        {
                                            if (ReCreateStructureTables._AlterDBTables(DateTime.Now, queries, Constants.SqlServer))
                                            {
                                                synchSettingsDao.UpdatePendingSynchSettings(new List<int> { lastSynchSetting.setting_id }, "done", Constants.SqlServer);
                                                Logger.write("{POS Sale Service BL}", "successfully altered tables in database");
                                                return true;
                                            }
                                            else
                                            {
                                                Logger.write("{POS Sale Service BL}", "unable to alter tables in database");
                                                return false;
                                            }
                                        }
                                        else
                                            return false;
                                        
                                    }
                                }
                                else
                                {
                                    Logger.write("{POS Sale Service BL}", "unable to receive database tables structure from service");
                                    return false;
                                }
                            }

                        }
                    }
                }
                return true;

            }
            catch (Exception ex)
            {
                //Logger.write("{POS Sale Service}", ex.Message);
                //Console.WriteLine(ex.Message);
                throw ex;
                //return false;
            }

        }
        public bool UploadInvSaleToServer(string dbtype)
        {
            try
            {
                InvSaleDao invSaleDao = new InvSaleDao();
                DataResponse dataResponse = new DataResponse();
                dataResponse.invSaleMaster = invSaleDao.GetSaleMaster(dbtype);
                if (dataResponse.invSaleMaster != null && dataResponse.invSaleMaster.Count > 0)
                {

                    dataResponse.invSaleMaster.ForEach(m => {
                        dataResponse.invSaleDetails.AddRange(invSaleDao.GetSaleDetails(m.Id, dbtype));
                    });
                    if (dataResponse.invSaleDetails != null && dataResponse.invSaleDetails.Count > 0)
                    {
                        InvSaleClient invSaleClient = new InvSaleClient();
                        Logger.write("{POS Sale Service BL}", "uploading sales data");
                               //Console.WriteLine("uploading sales data");
                        ApiResponse apiResponse = invSaleClient.PostInvSaleDetails(dataResponse);
                        if (apiResponse.Code == ApplicationResponse.SUCCESS_CODE)
                        {
                            Logger.write("{POS Sale Service BL}", "sales data uploaded successfully");
                               //Console.WriteLine("sales data uploaded successfully");
                            invSaleDao.UpdateMasterIsUploaded(dataResponse.invSaleMaster.Select(m => m.Id).ToList(), dbtype);

                        }
                        else
                        {
                            Logger.write("{POS Sale Service BL}", apiResponse.Message);
                               //Console.WriteLine(apiResponse.Message);
                            Logger.write("{POS Sale Service BL}", "sales data did not uploaded successfully. please contact support");
                               //Console.WriteLine("sales data did not uploaded successfully. please contact support");
                        }
                    }
                    else
                    {
                        Logger.write("{POS Sale Service BL}", "no pending sales data");
                               //Console.WriteLine("no pending sales data");
                    }
                }
                else
                {
                    Logger.write("{POS Sale Service BL}", "no pending sales data");
                               //Console.WriteLine("no pending sales data");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.write("{POS Sale Service BL}", ex.Message);
                //Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }
        public bool DeleteQt(string dbtype,int daysToDeleteQT)
        {
            try
            {
                InvSaleDao invSaleDao = new InvSaleDao();

                if (invSaleDao.DeleteOldQt(dbtype, daysToDeleteQT))
                {
                    Logger.write("{POS Sale Service BL}", "Deleted old QT successfully");
                    //Console.WriteLine("no pending sales data");
                    return true;
                }
                else
                {
                    Logger.write("{POS Sale Service BL}", "unable to delete old QT");
                    //Console.WriteLine("no pending sales data");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.write("{POS Sale Service BL}", ex.Message);
                //Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public static bool DownloadPublish(FtpCredentials ftpCredentials)
        {
            FtpManager ftpManager = new FtpManager();


            string folderName = ftpCredentials.Directory;
            string filename = "Release.zip";
            string localFolder = System.IO.Path.Combine(folderName, "Publish Folder");
            string binFolder = System.IO.Path.Combine(folderName, "bin");
            string pathString = System.IO.Path.Combine(folderName, "Publish Folder", filename);
            string extractPath = System.IO.Path.Combine(folderName, "Publish Extract Folder");
            if (!Directory.Exists(localFolder))
                System.IO.Directory.CreateDirectory(localFolder);
            if (!Directory.Exists(extractPath))
                System.IO.Directory.CreateDirectory(extractPath);

            if (ftpManager.DownloadFilesFromFtp(ftpCredentials.IP, ftpCredentials.Port, ftpCredentials.Username, ftpCredentials.Password, pathString, filename))
            {

                ZipFile.ExtractToDirectory(pathString, extractPath, true);

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
                    if (!string.IsNullOrEmpty(pathBackupString))
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
        public  bool GetAndReplaceDataSqlServer(int recordsToFetch, string IsBranchFilter)
        {
            try
            {
                StructureNDataClient sysTablesClient = new StructureNDataClient();
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
                string format = "yyyy-MM-dd HH:mm:ss";
                MsSqlDbManager dbManager = new MsSqlDbManager();
                if (!synchSettingsDao.CheckSynchSetting(Constants.SqlServer))
                {
                    List<string> queries = new List<string>();
                 
                    string qry = " insert into synch_setting(setting_id,synch_method,synch_type,table_names,status,sync_timestamp,insertion_timestamp,update_timestamp) values(1,'" + SynchMethods.database_structure + "','" + SynchTypes.structure_only + "','','done','" + DateTime.Now.ToString(format) + "','" + DateTime.Now.ToString(format) + "','" + DateTime.Now.ToString(format) + "')";
                    string qry2 = " insert into synch_setting(setting_id,synch_method,synch_type,table_names,status,sync_timestamp,insertion_timestamp,update_timestamp) values(2,'" + SynchMethods.database_data + "','" + SynchTypes.except_product_sale_master_detail_tables + "','','ready','" + DateTime.Now.ToString(format) + "','" + DateTime.Now.ToString(format) + "','" + DateTime.Now.ToString(format) + "')";
                    queries.Add(qry);
                    queries.Add(qry2);
                    synchSettingsDao.ExecuteQry(queries);
                }
                pendingSynchSettings = synchSettingsDao.GetPendingSynchSetting(SynchMethods.database_data.ToString(), Constants.SqlServer, "ready");

                if (pendingSynchSettings != null && pendingSynchSettings.Count > 0)
                {
                    lastSynchSetting = pendingSynchSettings.OrderBy(s => s.setting_id).First();
                    if (lastSynchSetting != null)
                    {
                        if (lastSynchSetting.synch_method == SynchMethods.database_data.ToString() && (!lastSynchSetting.sync_timestamp.HasValue || (lastSynchSetting.sync_timestamp.HasValue && DateTime.Compare(lastSynchSetting.sync_timestamp.Value, DateTime.Now) <= 0)))
                        {
                            if (Enum.TryParse<SynchTypes>(lastSynchSetting.synch_type, true, out synchType))  // ignore cases
                            {
                                //if (lastSynchSetting.synch_type.Equals(SynchTypes.products_recent.ToString()))
                                //{
                                //    bool downlaodedAll = false;
                                //    bool firsthit = true; string prodid = "0";
                                //    while (!downlaodedAll)
                                //    {
                                //        ProductsDao productsDao = new ProductsDao();
                                //        InvProductsResponse invProductsResponse = null;
                                //        {
                                //            Logger.write("{POS Sale Service BL}", "Getting some products");
                                            
                                //            Logger.write("{POS Sale Service BL}", prodid);
                                //            invProductsResponse = sysTablesClient.GetProducts(prodid, recordsToFetch, "r");
                                //            if (invProductsResponse != null)
                                //            {
                                //                if (invProductsResponse.Response.Code == ApplicationResponse.MAX_REACHED_CODE)
                                //                {
                                //                    Logger.write("{POS Sale Service BL}", "All products downloaded.");
                                //                    //Console.WriteLine("All products downloaded.");
                                //                    downlaodedAll = true;
                                //                    synchSettingsDao.UpdatePendingSynchSettings(new List<int> { lastSynchSetting.setting_id }, "done", Constants.SqlServer);
                                //                    //synchSettingsDao.InsertSynchSettings(SynchMethods.database_data.ToString(), SynchTypes.products_recent.ToString(), Utility.GetNextDayMorningDateTime(), "ready", Constants.SqlServer);

                                //                }
                                //                else if (invProductsResponse.invProducts != null && invProductsResponse.invProducts.Count > 0)
                                //                {
                                //                    prodid = invProductsResponse.invProducts.Max(pid => pid.Id).ToString();
                                //                    Logger.write("{POS Sale Service BL}", "Saving products.");
                                //                    //Console.WriteLine("Saving products.");
                                //                    if (ReCreateStructureTables._InsertData(DateTime.Now, null, invProductsResponse, Constants.SqlServer))
                                //                    {
                                //                        Logger.write("{POS Sale Service BL}", "Saved products.sending acknowledgement to server");
                                //                        UpdateProductFlag updateProductFlag = new UpdateProductFlag();
                                //                        updateProductFlag.BranchId = Global.BranchId.ToString();
                                //                        invProductsResponse.invProducts.ForEach(p => {
                                //                            updateProductFlag.updatedProducts.Add(new UpdatedProduct { ProductId = p.Id, RetailPrice = p.RetailPrice, UpdateStatus = "t" });
                                //                        });
                                //                        ApiResponse ackResponse = sysTablesClient.PostUpdatedProducts(updateProductFlag);
                                //                        if (ackResponse != null)
                                //                            Logger.write("{POS Sale Service BL}", ackResponse.Code + ": " + ackResponse.Message);
                                //                    }
                                //                }
                                //            }
                                //        }
                                //    }
                                //    return true;
                                //}
                                //else
                                if (lastSynchSetting.synch_type.Equals(SynchTypes.products_quick.ToString()))
                                {
                                    bool downlaodedAll = false;
                                    bool firsthit = true; string prodid = "0";
                                    while (!downlaodedAll)
                                    {
                                        ProductsDao productsDao = new ProductsDao();
                                        InvProductsResponse invProductsResponse = null;
                                        //if (lastSynchSetting.synch_type.Equals(SynchTypes.products_quick.ToString()))
                                        {
                                            Logger.write("{POS Sale Service BL}", "Getting some products");

                                            Logger.write("{POS Sale Service BL}", prodid);
                                            invProductsResponse = sysTablesClient.GetProducts(prodid, recordsToFetch, "t");
                                            if (invProductsResponse != null)
                                            {
                                                if (invProductsResponse.Response.Code == ApplicationResponse.MAX_REACHED_CODE)
                                                {
                                                    Logger.write("{POS Sale Service BL}", "All products downloaded.");
                                                    //Console.WriteLine("All products downloaded.");
                                                    downlaodedAll = true;
                                                    synchSettingsDao.UpdatePendingSynchSettings(new List<int> { lastSynchSetting.setting_id }, "done", Constants.SqlServer);
                                                    //synchSettingsDao.InsertSynchSettings(SynchMethods.database_data.ToString(), SynchTypes.products_quick.ToString(),Utility.GetNextWeekMorningDateTime(), "ready", Constants.SqlServer);
                                                }
                                                else if (invProductsResponse.invProducts != null && invProductsResponse.invProducts.Count > 0)
                                                {
                                                    prodid = invProductsResponse.invProducts.Max(pid => pid.Id).ToString();
                                                    Logger.write("{POS Sale Service BL}", "Saving products.");
                                                    //Console.WriteLine("Saving products.");
                                                    if (ReCreateStructureTables._InsertData(DateTime.Now, null, invProductsResponse, Constants.SqlServer))
                                                    {
                                                        Logger.write("{POS Sale Service BL}", "Saved products.sending acknowledgement to server");
                                                        //UpdateProductFlag updateProductFlag = new UpdateProductFlag();
                                                        //updateProductFlag.BranchId = Global.BranchId.ToString();
                                                        //invProductsResponse.invProducts.ForEach(p =>
                                                        //{
                                                        //    updateProductFlag.updatedProducts.Add(new UpdatedProduct { ProductId = p.Id, RetailPrice = p.RetailPrice, UpdateStatus = "t" });
                                                        //});
                                                        //ApiResponse ackResponse = sysTablesClient.PostUpdatedProducts(updateProductFlag);
                                                        //if (ackResponse != null)
                                                        //    Logger.write("{POS Sale Service BL}", ackResponse.Code + ": " + ackResponse.Message);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    return true;
                                }
                                else if (lastSynchSetting.synch_type.Equals(SynchTypes.except_product_sale_master_detail_tables.ToString()))
                                {
                                    SysTablesResponse sysTablesResponse = sysTablesClient.GetTableData(synchType, IsBranchFilter);
                                    if (sysTablesResponse != null)
                                    {
                                        if (ReCreateStructureTables._InsertData(DateTime.Now, sysTablesResponse, null, Constants.SqlServer))
                                        {
                                            synchSettingsDao.UpdatePendingSynchSettings(pendingSynchSettings.Where(w=>w.synch_type== lastSynchSetting.synch_type).Select(s => s.setting_id).ToList(), "done", Constants.SqlServer);
                                            //if(pendingSynchSettings.FirstOrDefault(w => w.synch_type == SynchTypes.products_quick.ToString())==null)
                                             //synchSettingsDao.InsertSynchSettings(SynchMethods.database_data.ToString(), SynchTypes.products_quick.ToString(), DateTime.Now, "ready", Constants.SqlServer);
                                            // synchSettingsDao.InsertSynchSettings(SynchMethods.database_data.ToString(), lastSynchSetting.synch_type, new DateTime(DateTime.Now.Year+1,1,1), "ready", Constants.SqlServer);
                                            //if (pendingSynchSettings.FirstOrDefault(w => w.synch_type == SynchTypes.products_recent.ToString()) == null)
                                            //    synchSettingsDao.InsertSynchSettings(SynchMethods.database_data.ToString(), SynchTypes.products_recent.ToString(), Utility.GetNextDayMorningDateTime(), "ready", Constants.SqlServer);
                                            return true;
                                        }

                                    }
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

                Logger.write("{GetAndReplaceDataSqlServer}", ex.Message);
                //Console.WriteLine(ex.Message);
                return false;
            }

        }
        public  bool GetProductsOnlySqlServer(int recordsToFetch)
        {
            try
            {
                StructureNDataClient sysTablesClient = new StructureNDataClient();
                ProductsDao productsDao = new ProductsDao();
                InvProductsResponse invProductsResponse = null;
                SynchSettingsDao synchSettingsDao = new SynchSettingsDao();
                Logger.write("{POS Sale Service BL}", "Getting some products only");
                               //Console.WriteLine("Getting some products");
                invProductsResponse = sysTablesClient.GetProducts("-1", recordsToFetch,"f");
                if (invProductsResponse != null)
                {
                    Logger.write("{POS Sale Service BL}", "Saving products.");
                               //Console.WriteLine("Saving products.");
                    if (ReCreateStructureTables._InsertData(DateTime.Now, null, invProductsResponse, Constants.SqlServer))
                    {
                        Logger.write("{POS Sale Service BL}", "Saved products.sending acknowledgement to server");

                        UpdateProductFlag updateProductFlag = new UpdateProductFlag();
                        updateProductFlag.BranchId = Global.BranchId.ToString();
                        invProductsResponse.invProducts.ForEach(p => {
                            updateProductFlag.updatedProducts.Add(new UpdatedProduct { ProductId=p.Id,RetailPrice=p.RetailPrice,UpdateStatus="t" });
                        });
                        ApiResponse ackResponse = sysTablesClient.PostUpdatedProducts(updateProductFlag);
                        if (ackResponse != null)
                            Logger.write("{POS Sale Service BL}", ackResponse.Code+": "+ackResponse.Message);
                        return true;

                    }

                }
            }
            catch (Exception ex)
            {
                Logger.write("{GetProductsOnlySqlServer}", ex.Message.ToString());
                //Console.WriteLine(ex.Message.ToString());
                return false;
            }
            return false;
        }
        public bool GetAndReplaceSysTablesSqlite(string IsBranchFilter)
        {
            try
            {
                StructureNDataClient sysTablesClient = new StructureNDataClient();
                bool fileExists = false;
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
                    fileExists = true;
                    if (!synchSettingsDao.CheckSynchTable(Constants.Sqlite))
                    {
                        synchType = SynchTypes.except_sale_master_detail_tables;
                    }
                    else
                    {
                        pendingSynchSettings = synchSettingsDao.GetPendingSynchSetting("database", Constants.Sqlite);
                        if (pendingSynchSettings != null && pendingSynchSettings.Count() > 0)
                        {
                            lastSynchSetting = pendingSynchSettings[0];
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
                    if (lastSynchSetting != null)
                        synchSettingsDao.UpdatePendingSynchSettings(new List<int> { lastSynchSetting.setting_id }, "in process", Constants.Sqlite);
                    if (ReCreateStructureTables._CreateDBTables(new DateTime(), tableStructureResponse, Constants.Sqlite))
                    {

                        SysTablesResponse sysTablesResponse = sysTablesClient.GetTableData(synchType, IsBranchFilter);
                        if (sysTablesResponse != null)
                        {
                            if (ReCreateStructureTables._InsertData(DateTime.Now, sysTablesResponse, null, Constants.Sqlite))
                            {
                                if (pendingSynchSettings != null && pendingSynchSettings.Count() > 0)
                                    synchSettingsDao.UpdatePendingSynchSettings(pendingSynchSettings.Select(s => s.setting_id).ToList(), "done", Constants.Sqlite);
                                return true;
                            }
                            else if (fileExists)
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
                if (lastSynchSetting != null)
                    synchSettingsDao.UpdatePendingSynchSettings(new List<int> { lastSynchSetting.setting_id }, "create table failed", Constants.Sqlite);
                return false;
            }
            catch (Exception ex)
            {
                Logger.write(ex.Message);
               // Console.WriteLine(ex.Message);
                return false;
            }

        }
        public bool GetFixProblematicAccVouchers()
        {
            try
            {
                InvSaleClient invSaleClient = new InvSaleClient();
                
                string response= invSaleClient.GetFixAccVoucher();
                if (response != null)
                {
                    Logger.write("{POS Sale Service BL}", response);
                    //Console.WriteLine("Saving products.");
                    if (response.Equals("success"))
                    {
                        return true;

                    }

                }
            }
            catch (Exception ex)
            {
                Logger.write("{GetFixProblematicAccVouchers}", ex.Message.ToString());
                //Console.WriteLine(ex.Message.ToString());
                return false;
            }
            return false;
        }
    }
}
