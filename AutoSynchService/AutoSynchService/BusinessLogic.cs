using AutoSynchFtp;
using AutoSynchService.ApiClient;
using AutoSynchService.Classes;
using AutoSynchService.DAOs;
using AutoSynchService.Models;
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
        public static bool UploadInvSaleToServer()
        {
            try
            {
                InvSaleDao invSaleDao = new InvSaleDao();
                DataResponse dataResponse = new DataResponse();
                dataResponse.invSaleMaster = invSaleDao.GetSaleMaster();
                dataResponse.invSaleMaster.ForEach(m => {
                    dataResponse.invSaleDetails.AddRange(invSaleDao.GetSaleDetails(m.Id));
                });

                InvSaleClient invSaleClient = new InvSaleClient();

                ApiResponse apiResponse = invSaleClient.PostInvSaleDetails(dataResponse);
                if (apiResponse.Code == ApplicationResponse.SUCCESS_CODE)
                {

                    invSaleDao.UpdateMasterIsUploaded(dataResponse.invSaleMaster.Select(m => m.Id).ToList());

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
        public static bool GetAndReplaceSysTables()
        {
            try
            {
                SysTablesClient sysTablesClient = new SysTablesClient();

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
                    ReCreateStructureTables._CreateDatabase(dbPath);
                    synchType = SynchTypes.full;
                }
                else
                {
                    if (!synchSettingsDao.CheckSynchTable())
                    {
                        synchType = SynchTypes.except_sale_master_detail_tables;
                    }
                    else
                    {
                        pendingSynchSettings = synchSettingsDao.GetPendingSynchSetting("database");
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
                TableStructureResponse tableStructureResponse = sysTablesClient.GetTableStructure(synchType);
                if (tableStructureResponse != null)
                {
                    if(lastSynchSetting!=null)
                        synchSettingsDao.UpdatePendingSynchSettings(new List<int> { lastSynchSetting.setting_id }, "in process");
                    if (ReCreateStructureTables._CreateDBTables(new DateTime(), tableStructureResponse))
                    {

                        SysTablesResponse sysTablesResponse = sysTablesClient.GetTableData(synchType);
                        if (sysTablesResponse != null)
                        {
                            if(ReCreateStructureTables._InsertData(DateTime.Now, sysTablesResponse))
                            {
                                if(pendingSynchSettings != null && pendingSynchSettings.Count() > 0)
                                    synchSettingsDao.UpdatePendingSynchSettings(pendingSynchSettings.Select(s=>s.setting_id).ToList(), "done");
                                return true;
                            }
                            else
                            {

                                if (lastSynchSetting != null)
                                    synchSettingsDao.UpdatePendingSynchSettings(new List<int> { lastSynchSetting.setting_id }, "insert data failed");
                                return false;
                            }
                        }
                        else
                        {
                            if (lastSynchSetting != null)
                                synchSettingsDao.UpdatePendingSynchSettings(new List<int> { lastSynchSetting.setting_id }, "insert data failed");
                            return false;
                        }
                    }
                    if (lastSynchSetting != null)
                        synchSettingsDao.UpdatePendingSynchSettings(new List<int> { lastSynchSetting.setting_id }, "create table failed");
                    return false;
                }
                if(lastSynchSetting!=null)
                    synchSettingsDao.UpdatePendingSynchSettings(new List<int> { lastSynchSetting.setting_id }, "create table failed");
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
