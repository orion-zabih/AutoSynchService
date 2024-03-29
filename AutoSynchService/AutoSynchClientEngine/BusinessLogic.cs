﻿using AutoSynchFtp;
using AutoSynchClientEngine.Classes;
using AutoSynchClientEngine.DAOs;
using AutoSynchClientEngine.ApiClient;
using AutoSynchClientEngine.Classes;
using AutoSynchClientEngine.DAOs;
using AutoSynchSqlServer.CustomModels;
using AutoSynchSqlServer.Models;
using AutoSynchSqlServerLocal;
using FluentFTP.Helpers;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections;

namespace AutoSynchClientEngine
{
    public sealed class BusinessLogic
    {
        private readonly ILogger<BusinessLogic> _logger;
        public BusinessLogic(
            ILogger<BusinessLogic> logger) =>
            (_logger) = (logger);
        public BusinessLogic()
        {
           
        }
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
                        lastSynchSetting = pendingSynchSettings.OrderBy(s => s.setting_id).First();
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
                //SynchSetting lastSynchSetting = null;
                SynchTypes synchType = SynchTypes.full;
                MsSqlDbManager dbManager = new MsSqlDbManager();


                pendingSynchSettings = synchSettingsDao.GetPendingSynchSetting(SynchMethods.database_structure_alter.ToString(), Constants.SqlServer, "ready");
                if (pendingSynchSettings != null && pendingSynchSettings.Count > 0)
                {
                    foreach (SynchSetting lastSynchSetting in pendingSynchSettings.OrderBy(s => s.setting_id))
                    {
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
                                    else
                                    {
                                        tblLst = String.Join(',', GetTables(synchType));
                                    }
                                    TableStructureResponse tableStructureResponse = sysTablesClient.GetTableColumns(synchType, (synchType==SynchTypes.custom)? tblLst:"empty", Constants.SqlServer);

                                    if (tableStructureResponse != null)
                                    {
                                        Logger.write("{POS Sale Service BL}", "successfully got database structure from server. now creating database.");
                                        {
                                            Logger.write("{POS Sale Service BL}", "successfully created database file. now creating tables.");
                                            List<string> tables = Utility.ConverCommaSeparatedToList(tblLst);
                                            List<string> queries = new List<string>();
                                            foreach (var item in tables)
                                            {
                                                List<TableStructure> localStructure = synchSettingsDao.GetTableColumnsInfo(item);
                                                List<TableStructure> onlineStructure = tableStructureResponse.tableStructures.Where(a => a.TableName == item).ToList();
                                                if (localStructure != null && localStructure.Count > 0 && onlineStructure != null && onlineStructure.Count > 0)
                                                {
                                                    List<string> differentColumns = onlineStructure.Select(c => c.ColumnName.ToLower()).Except(localStructure.Select(l => l.ColumnName.ToLower())).ToList();
                                                    List<TableStructure> newStructure = onlineStructure.Where(o => differentColumns.Contains(o.ColumnName.ToLower())).ToList();
                                                    if (newStructure.Count > 0)
                                                    {
                                                        string qry = "alter table " + item + " add ";
                                                        string cols = "";
                                                        newStructure.ForEach(n => {
                                                            cols += n.ColumnName + " " + n.DataType + (n.IsNullable == "YES" ? " NULL" : " NOT NULL") + (n.IsPrimaryKey == "YES" ? " PRIMARY KEY" : " ") + (!string.IsNullOrEmpty(n.ColumnDefault) ? (" DEFAULT " + n.ColumnDefault) : "") + ",";
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
                                                    
                                                }
                                                else
                                                {
                                                    synchSettingsDao.UpdatePendingSynchSettings(new List<int> { lastSynchSetting.setting_id }, "done", Constants.SqlServer);
                                                    Logger.write("{POS Sale Service BL}", "no different column to alter tables in database");
                                                    //return true;
                                                }
                                            }
                                            else
                                            {
                                                synchSettingsDao.UpdatePendingSynchSettings(new List<int> { lastSynchSetting.setting_id }, "done", Constants.SqlServer);
                                                Logger.write("{POS Sale Service BL}", "no defference found for alter database for "+lastSynchSetting.synch_type);
                                            }
                                            //else
                                                //return false;

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
                    //lastSynchSetting = pendingSynchSettings.OrderBy(s => s.setting_id).First();
                    
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
        public UsrSystemUser GetUsrSystemUser(string loginname,string password,string dbtype)
        {
            UsersDao usersDao = new UsersDao();
            UsrSystemUser? usrSystemUser = usersDao.GetUser(loginname, password, dbtype);
            if (usrSystemUser != null)
            {
                usrSystemUser.LoginName = loginname;
                usrSystemUser.Password = password;
                return usrSystemUser;
            }
            else
                return null;
                
        }
        private List<string> GetTables(SynchTypes synchType)
        {
            List<string> SynchTbls = new List<string>();
            if (synchType == SynchTypes.full)
            {
                //using (Entities dbContext = new Entities())
                //{
                //    //dbContext.InvSaleMaster
                //    //List<string> lstTables = new List<string>();
                //    //lstTables.Add()

                //    SynchTbls = dbContext.Model.GetEntityTypes().Where(et => et.GetTableName() != "Timestamp" && et.GetTableName() != "Sequence").Select(et => et.GetTableName()).ToList();

                //    //lstTables.ForEach(table =>
                //    //{
                //    //    SynchTbls.Add(getTableName(table));
                //    //});
                //}
                SynchTbls.Add("SysControllesGroup");
                SynchTbls.Add("SysExecptionLogging");
                SynchTbls.Add("SysFeature");
                SynchTbls.Add("SysOrgFormsMapping");
                SynchTbls.Add("SysForm");
                SynchTbls.Add("SysOrgModulesMapping");
                SynchTbls.Add("SysLayout");
                SynchTbls.Add("SysModule");
                SynchTbls.Add("SysModuleFormsMapping");
                SynchTbls.Add("SysSystem");
                SynchTbls.Add("SysWeekDay");
                SynchTbls.Add("SysMonthName");
                SynchTbls.Add("SysYear");
                SynchTbls.Add("SysLableContent");
                SynchTbls.Add("SysHtml");
                SynchTbls.Add("SysInvTypeWiseControll");
                SynchTbls.Add("InvCategory");
                SynchTbls.Add("InvCompany");
                SynchTbls.Add("InvCustomer");
                SynchTbls.Add("InvCustomerType");
                SynchTbls.Add("InvDeliveryChallanDetail");
                SynchTbls.Add("InvDeliveryChallanMaster");
                SynchTbls.Add("InvDemandNote");
                SynchTbls.Add("InvDemandNoteDetail");
                SynchTbls.Add("InvGatePassInDetail");
                SynchTbls.Add("InvGatePassInMaster");
                SynchTbls.Add("InvJcMonthSetting");
                SynchTbls.Add("InvLocation");
                SynchTbls.Add("InvPackageProductsMapping");
                SynchTbls.Add("InvPaymentType");
                SynchTbls.Add("InvProduct");
                SynchTbls.Add("InvProductBatch");
                SynchTbls.Add("InvProductionDetail");
                SynchTbls.Add("InvProductionMaster");
                SynchTbls.Add("InvProductLedger");
                SynchTbls.Add("InvPurchaseDetail");
                SynchTbls.Add("InvPurchaseMaster");
                SynchTbls.Add("InvPurchaseOrderDetail");
                SynchTbls.Add("InvPurchaseOrderMaster");
                SynchTbls.Add("InvQuatationDetail");
                SynchTbls.Add("InvQuatationMaster");
                SynchTbls.Add("InvSaleClosing");
                SynchTbls.Add("InvSaleClosingDetail");
                SynchTbls.Add("InvSaleDetail");
                SynchTbls.Add("InvSalemanToRoutsMapping");
                SynchTbls.Add("InvSaleMaster");
                SynchTbls.Add("InvSchemeDetail");
                SynchTbls.Add("InvSchemeMaster");
                SynchTbls.Add("InvShift");
                SynchTbls.Add("InvStockAdjustment");
                SynchTbls.Add("InvStockAdjustmentDetail");
                SynchTbls.Add("InvStockTransfer");
                SynchTbls.Add("InvStockTransferDetail");
                SynchTbls.Add("InvThirdPartyCustomer");
                SynchTbls.Add("InvUnit");
                SynchTbls.Add("InvVehicle");
                SynchTbls.Add("InvVendor");
                SynchTbls.Add("InvWarehouse");
                SynchTbls.Add("UsrSystemUser");
                SynchTbls.Add("UsrUserBranchesMapping");
                SynchTbls.Add("UsrUserFormsMapping");
                SynchTbls.Add("UsrUserParmsMapping");
                SynchTbls.Add("OrgBranch");
                SynchTbls.Add("OrgFeaturesMapping");
                SynchTbls.Add("OrgOrganization");
                SynchTbls.Add("OrgOrgSystemsMapping");
                SynchTbls.Add("AccFiscalYear");

            }
            else if (synchType == SynchTypes.only_sys_tables)
            {
                SynchTbls.Add("SysControllesGroup");
                SynchTbls.Add("SysExecptionLogging");
                SynchTbls.Add("SysFeature");
                SynchTbls.Add("SysOrgFormsMapping");
                SynchTbls.Add("SysForm");
                SynchTbls.Add("SysOrgModulesMapping");
                SynchTbls.Add("SysLayout");
                SynchTbls.Add("SysModule");
                SynchTbls.Add("SysModuleFormsMapping");
                SynchTbls.Add("SysSystem");
                SynchTbls.Add("SysWeekDay");
                SynchTbls.Add("SysMonthName");
                SynchTbls.Add("SysYear");
                SynchTbls.Add("SysLableContent");
                SynchTbls.Add("SysHtml");
                SynchTbls.Add("SysInvTypeWiseControll");
            }
            else if (synchType == SynchTypes.only_sale_master_detail_tables)
            {
                SynchTbls.Add("InvSaleDetail");
                SynchTbls.Add("InvSaleMaster");
            }
            else if (synchType == SynchTypes.except_sale_master_detail_tables)
            {

                SynchTbls.Add("SysControllesGroup");
                SynchTbls.Add("SysExecptionLogging");
                SynchTbls.Add("SysFeature");
                SynchTbls.Add("SysOrgFormsMapping");
                SynchTbls.Add("SysForm");
                SynchTbls.Add("SysOrgModulesMapping");
                SynchTbls.Add("SysLayout");
                SynchTbls.Add("SysModule");
                SynchTbls.Add("SysModuleFormsMapping");
                SynchTbls.Add("SysSystem");
                SynchTbls.Add("SysWeekDay");
                SynchTbls.Add("SysMonthName");
                SynchTbls.Add("SysYear");
                SynchTbls.Add("SysLableContent");
                SynchTbls.Add("SysHtml");
                SynchTbls.Add("SysInvTypeWiseControll");
                SynchTbls.Add("InvCategory");
                SynchTbls.Add("InvCompany");
                SynchTbls.Add("InvCustomer");
                SynchTbls.Add("InvCustomerType");
                SynchTbls.Add("InvDeliveryChallanDetail");
                SynchTbls.Add("InvDeliveryChallanMaster");
                SynchTbls.Add("InvDemandNote");
                SynchTbls.Add("InvDemandNoteDetail");
                SynchTbls.Add("InvGatePassInDetail");
                SynchTbls.Add("InvGatePassInMaster");
                SynchTbls.Add("InvJcMonthSetting");
                SynchTbls.Add("InvLocation");
                SynchTbls.Add("InvPackageProductsMapping");
                SynchTbls.Add("InvPaymentType");
                //.SynchTbls.Add("InvProduct");
                SynchTbls.Add("InvProductBatch");
                SynchTbls.Add("InvProductionDetail");
                SynchTbls.Add("InvProductionMaster");
                SynchTbls.Add("InvProductLedger");
                SynchTbls.Add("InvPurchaseDetail");
                SynchTbls.Add("InvPurchaseMaster");
                SynchTbls.Add("InvPurchaseOrderDetail");
                SynchTbls.Add("InvPurchaseOrderMaster");
                SynchTbls.Add("InvQuatationDetail");
                SynchTbls.Add("InvQuatationMaster");
                SynchTbls.Add("InvSaleClosing");
                SynchTbls.Add("InvSaleClosingDetail");
                //SynchTbls.Add("InvSaleDetail");
                SynchTbls.Add("InvSalemanToRoutsMapping");
                //SynchTbls.Add("InvSaleMaster");
                SynchTbls.Add("InvSchemeDetail");
                SynchTbls.Add("InvSchemeMaster");
                SynchTbls.Add("InvShift");
                SynchTbls.Add("InvStockAdjustment");
                SynchTbls.Add("InvStockAdjustmentDetail");
                SynchTbls.Add("InvStockTransfer");
                SynchTbls.Add("InvStockTransferDetail");
                SynchTbls.Add("InvThirdPartyCustomer");
                SynchTbls.Add("InvUnit");
                SynchTbls.Add("InvVehicle");
                SynchTbls.Add("InvVendor");
                SynchTbls.Add("InvWarehouse");
                SynchTbls.Add("UsrSystemUser");
                SynchTbls.Add("UsrUserBranchesMapping");
                SynchTbls.Add("UsrUserFormsMapping");
                SynchTbls.Add("UsrUserParmsMapping");
                SynchTbls.Add("OrgBranch");
                SynchTbls.Add("OrgFeaturesMapping");
                SynchTbls.Add("OrgOrganization");
                SynchTbls.Add("OrgOrgSystemsMapping");
                SynchTbls.Add("AccFiscalYear");
            }
            else if (synchType == SynchTypes.except_product_sale_master_detail_tables)
            {

                SynchTbls.Add("SysControllesGroup");
                SynchTbls.Add("SysExecptionLogging");
                SynchTbls.Add("SysFeature");
                SynchTbls.Add("SysOrgFormsMapping");
                SynchTbls.Add("SysForm");
                SynchTbls.Add("SysOrgModulesMapping");
                SynchTbls.Add("SysLayout");
                SynchTbls.Add("SysModule");
                SynchTbls.Add("SysModuleFormsMapping");
                SynchTbls.Add("SysSystem");
                SynchTbls.Add("SysWeekDay");
                SynchTbls.Add("SysMonthName");
                SynchTbls.Add("SysYear");
                SynchTbls.Add("SysLableContent");
                SynchTbls.Add("SysHtml");
                SynchTbls.Add("SysInvTypeWiseControll");
                SynchTbls.Add("InvCategory");
                SynchTbls.Add("InvCompany");
                SynchTbls.Add("InvCustomer");
                SynchTbls.Add("InvCustomerType");
                SynchTbls.Add("InvDeliveryChallanDetail");
                SynchTbls.Add("InvDeliveryChallanMaster");
                SynchTbls.Add("InvDemandNote");
                SynchTbls.Add("InvDemandNoteDetail");
                SynchTbls.Add("InvGatePassInDetail");
                SynchTbls.Add("InvGatePassInMaster");
                SynchTbls.Add("InvJcMonthSetting");
                SynchTbls.Add("InvLocation");
                SynchTbls.Add("InvPackageProductsMapping");
                SynchTbls.Add("InvPaymentType");
                //.SynchTbls.Add("InvProduct");
                SynchTbls.Add("InvProductBatch");
                SynchTbls.Add("InvProductionDetail");
                SynchTbls.Add("InvProductionMaster");
                SynchTbls.Add("InvProductLedger");
                SynchTbls.Add("InvPurchaseDetail");
                SynchTbls.Add("InvPurchaseMaster");
                SynchTbls.Add("InvPurchaseOrderDetail");
                SynchTbls.Add("InvPurchaseOrderMaster");
                SynchTbls.Add("InvQuatationDetail");
                SynchTbls.Add("InvQuatationMaster");
                SynchTbls.Add("InvSaleClosing");
                SynchTbls.Add("InvSaleClosingDetail");
                //SynchTbls.Add("InvSaleDetail");
                SynchTbls.Add("InvSalemanToRoutsMapping");
                //SynchTbls.Add("InvSaleMaster");
                SynchTbls.Add("InvSchemeDetail");
                SynchTbls.Add("InvSchemeMaster");
                SynchTbls.Add("InvShift");
                SynchTbls.Add("InvStockAdjustment");
                SynchTbls.Add("InvStockAdjustmentDetail");
                SynchTbls.Add("InvStockTransfer");
                SynchTbls.Add("InvStockTransferDetail");
                SynchTbls.Add("InvThirdPartyCustomer");
                SynchTbls.Add("InvUnit");
                SynchTbls.Add("InvVehicle");
                SynchTbls.Add("InvVendor");
                SynchTbls.Add("InvWarehouse");
                SynchTbls.Add("UsrSystemUser");
                SynchTbls.Add("UsrUserBranchesMapping");
                SynchTbls.Add("UsrUserFormsMapping");
                SynchTbls.Add("UsrUserParmsMapping");
                SynchTbls.Add("OrgBranch");
                SynchTbls.Add("OrgFeaturesMapping");
                SynchTbls.Add("OrgOrganization");
                SynchTbls.Add("OrgOrgSystemsMapping");
                SynchTbls.Add("AccFiscalYear");
            }
            return SynchTbls;
        }
        public bool UploadInvSaleToServer(string dbtype, string branchId,List<int>? chunk)
        {
            try
            {
                InvSaleDao invSaleDao = new InvSaleDao();
                DataResponse dataResponse = new DataResponse();
                if(chunk != null)
                {
                    dataResponse.invSaleMaster = invSaleDao.GetSaleMaster(dbtype, branchId, chunk);
                }
                else
                    dataResponse.invSaleMaster = invSaleDao.GetSaleMaster(dbtype,branchId);
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
        public bool UploadInvSaleToServerAll(string dbtype, string branchId, string upload_records_no)
        {
            try
            {
                InvSaleDao invSaleDao = new InvSaleDao();
                
                List<int> invSaleMaster = invSaleDao.GetSaleMasterIds(dbtype, branchId);
                if (invSaleMaster != null && invSaleMaster.Count > 0)
                {

                    int chunkSize = 25;
                    if (int.TryParse(upload_records_no, out chunkSize))
                    {

                    }
                    for (int i = 0; i < invSaleMaster.Count; i += chunkSize)
                    {
                        List<int> chunk = invSaleMaster.Skip(i).Take(chunkSize).ToList();

                        // Process the current chunk
                        if (UploadInvSaleToServer(dbtype,branchId, chunk))
                        {
                            Logger.write("{POS Purchase Service BL}", "uploaded a chunk of Purchases data");
                        }
                        else
                            Logger.write("{POS Purchase Service BL}", "failed to upload a chunk of Purchases data");
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

        public List<InvSaleMaster> GetUnSynchedInvSale(string dbtype, string branchId)
        {
            try
            {
                InvSaleDao invSaleDao = new InvSaleDao();
                List<InvSaleMaster> invSaleMaster = invSaleDao.GetSaleMaster(dbtype, branchId);
                if (invSaleMaster != null && invSaleMaster.Count > 0)
                {
                    return invSaleMaster;                    
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.write(ex.ToString());
                throw;
            }

        }
        public List<InvPurchaseMaster> GetUnSynchedInvPurchase(string dbtype)
        {
            try
            {
                InvPurchaseDao invPurchaseDao = new InvPurchaseDao();
                List<InvPurchaseMaster> invPurchaseMaster = invPurchaseDao.GetPurchaseMaster(dbtype);
                if (invPurchaseMaster != null && invPurchaseMaster.Count > 0)
                {
                    return invPurchaseMaster;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.write(ex.ToString());
                throw;
            }

        }
        public List<string> ExportUnSynchedInvSale(string dbtype, string branchId)
        {
            try
            {
                InvSaleDao invSaleDao = new InvSaleDao();
                List<string> invSaleMaster = invSaleDao.GetSaleMaster(dbtype, branchId,true);
                if (invSaleMaster != null && invSaleMaster.Count > 0)
                {
                    string csvFilePath = "output.csv";

                    // Open the CSV file for writing using StreamWriter
                    using (StreamWriter writer = new StreamWriter(csvFilePath))
                    {
                        // Write the list of strings as a CSV row
                        writer.WriteLine(invSaleMaster);
                    }

                    return invSaleMaster;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.write(ex.ToString());
                throw;
            }

        }
        //public bool ExportNotUploadedInvSaleToServer(string dbtype, string branchId)
        //{
        //    try
        //    {
        //        InvSaleDao invSaleDao = new InvSaleDao();
        //        List<string> invSaleMaster = invSaleDao.GetSaleMaster(dbtype, branchId,true);
        //        if (invSaleMaster != null && invSaleMaster.Count > 0)
        //        {

        //            dataResponse.invSaleMaster.ForEach(m => {
        //                dataResponse.invSaleDetails.AddRange(invSaleDao.GetSaleDetails(m.Id, dbtype,true));
        //            });
        //            if (dataResponse.invSaleDetails != null && dataResponse.invSaleDetails.Count > 0)
        //            {
        //                InvSaleClient invSaleClient = new InvSaleClient();
        //                Logger.write("{POS Sale Service BL}", "uploading sales data");
        //                //Console.WriteLine("uploading sales data");
        //                ApiResponse apiResponse = invSaleClient.PostInvSaleDetails(dataResponse);
        //                if (apiResponse.Code == ApplicationResponse.SUCCESS_CODE)
        //                {
        //                    Logger.write("{POS Sale Service BL}", "sales data uploaded successfully");
        //                    //Console.WriteLine("sales data uploaded successfully");
        //                    invSaleDao.UpdateMasterIsUploaded(dataResponse.invSaleMaster.Select(m => m.Id).ToList(), dbtype);

        //                }
        //                else
        //                {
        //                    Logger.write("{POS Sale Service BL}", apiResponse.Message);
        //                    //Console.WriteLine(apiResponse.Message);
        //                    Logger.write("{POS Sale Service BL}", "sales data did not uploaded successfully. please contact support");
        //                    //Console.WriteLine("sales data did not uploaded successfully. please contact support");
        //                }
        //            }
        //            else
        //            {
        //                Logger.write("{POS Sale Service BL}", "no pending sales data");
        //                //Console.WriteLine("no pending sales data");
        //            }
        //        }
        //        else
        //        {
        //            Logger.write("{POS Sale Service BL}", "no pending sales data");
        //            //Console.WriteLine("no pending sales data");
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.write("{POS Sale Service BL}", ex.Message);
        //        //Console.WriteLine(ex.Message);
        //        return false;
        //    }

        //    return true;
        //}
        public bool UploadInvPurchaseAll(string dbtype,string upload_records_no)
        {
            InvPurchaseDao invPurchaseDao = new InvPurchaseDao();
            List<int> invPurchaseMaster = invPurchaseDao.GetPurchaseMasterIds(dbtype);
            int chunkSize = 25;
            if(int.TryParse(upload_records_no,out chunkSize))
            {

            }
            for (int i = 0; i < invPurchaseMaster.Count; i += chunkSize)
            {
                List<int> chunk = invPurchaseMaster.Skip(i).Take(chunkSize).ToList();

                // Process the current chunk
                if (UploadInvPurchaseToServer(dbtype, chunk))
                {
                    Logger.write("{POS Purchase Service BL}", "uploaded a chunk of Purchases data");
                }
                else
                    Logger.write("{POS Purchase Service BL}", "failed to upload a chunk of Purchases data");
            }
            return true;
        }
        public bool UploadInvPurchaseToServer(string dbtype,List<int>? chunk=null)
        {
            try
            {
                InvPurchaseDao invPurchaseDao = new InvPurchaseDao();
                DataResponse dataResponse = new DataResponse();
                if(chunk != null)
                {
                    dataResponse.invPurchaseMaster = invPurchaseDao.GetPurchaseMaster(dbtype,chunk);
                }    
                else 
                    dataResponse.invPurchaseMaster = invPurchaseDao.GetPurchaseMaster(dbtype);
                if (dataResponse.invPurchaseMaster!= null && dataResponse.invPurchaseMaster.Count > 0)
                {

                    dataResponse.invPurchaseMaster.ForEach(m => {
                        dataResponse.invPurchaseDetails.AddRange(invPurchaseDao.GetPurchaseDetails(m.Id, dbtype));
                    });

                    dataResponse.invPurchaseMaster.ForEach(m => {
                        dataResponse.invProductLedgers.AddRange(invPurchaseDao.GetProductLedgers(m.Id, dbtype));
                    });
                    if (dataResponse.invPurchaseDetails != null && dataResponse.invPurchaseDetails.Count > 0)
                    {
                        InvPurchaseClient invPurchaseClient = new InvPurchaseClient();
                        Logger.write("{POS Purchase Service BL}", "uploading Purchases data");
                        //Console.WriteLine("uploading Purchases data");
                        ApiResponse apiResponse = invPurchaseClient.PostInvPurchaseDetails(dataResponse);
                        if (apiResponse.Code == ApplicationResponse.SUCCESS_CODE)
                        {
                            Logger.write("{POS Purchase Service BL}", "Purchases data uploaded successfully");
                            //Console.WriteLine("Purchases data uploaded successfully");
                            invPurchaseDao.UpdateMasterIsUploaded(apiResponse.updatedRecords, dbtype);

                        }
                        else
                        {
                            Logger.write("{POS Purchase Service BL}", apiResponse.Message);
                            //Console.WriteLine(apiResponse.Message);
                            Logger.write("{POS Purchase Service BL}", "Purchases data did not uploaded successfully. please contact support");
                            //Console.WriteLine("Purchases data did not uploaded successfully. please contact support");
                        }
                    }
                    else
                    {
                        Logger.write("{POS Purchase Service BL}", "no pending Purchases data");
                        //Console.WriteLine("no pending Purchases data");
                    }
                }
                else
                {
                    Logger.write("{POS Purchase Service BL}", "no pending Purchases data");
                    //Console.WriteLine("no pending Purchases data");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.write("{POS Purchase Service BL}", ex.StackTrace.ToString());
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
                                                    if (ReCreateStructureTables._InsertData(DateTime.Now, null, invProductsResponse, Constants.SqlServer,true))
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
                                        if (ReCreateStructureTables._InsertData(DateTime.Now, sysTablesResponse, null, Constants.SqlServer,true))
                                        {
                                            synchSettingsDao.UpdatePendingSynchSettings(pendingSynchSettings.Where(w=>w.synch_type== lastSynchSetting.synch_type).Select(s => s.setting_id).ToList(), "done", Constants.SqlServer);
                                            if (pendingSynchSettings.FirstOrDefault(w => w.synch_type == SynchTypes.products_quick.ToString()) == null)
                                                synchSettingsDao.InsertSynchSettings(SynchMethods.database_data.ToString(), SynchTypes.products_quick.ToString(), DateTime.Now, "ready", Constants.SqlServer);
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
        public  bool GetProductsOnlySqlServer(int recordsToFetch,bool updateExisting)
        {
            try
            {
                StructureNDataClient sysTablesClient = new StructureNDataClient();
                ProductsDao productsDao = new ProductsDao();
                InvProductsResponse invProductsResponse = null;
                Logger.write("{POS Sale Service BL}", "Getting some products only");
                               //Console.WriteLine("Getting some products");
                invProductsResponse = sysTablesClient.GetProducts("-1", recordsToFetch,"f");
                if (invProductsResponse != null)
                {
                    Logger.write("{POS Sale Service BL}", "Saving products.");
                               //Console.WriteLine("Saving products.");
                    if (ReCreateStructureTables._InsertData(DateTime.Now, null, invProductsResponse, Constants.SqlServer, updateExisting))
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
        //public bool GetProductsOnlySqlServer(int recordsToFetch, bool updateExisting)
        //{
        //    try
        //    {
        //        StructureNDataClient sysTablesClient = new StructureNDataClient();
        //        ProductsDao productsDao = new ProductsDao();
        //        InvProductsResponse invProductsResponse = null;
        //        SynchSettingsDao synchSettingsDao = new SynchSettingsDao();
        //        Logger.write("{POS Sale Service BL}", "Getting some products only");
        //        //Console.WriteLine("Getting some products");
        //        invProductsResponse = sysTablesClient.GetProducts("-1", recordsToFetch, "f");
        //        if (invProductsResponse != null)
        //        {
        //            Logger.write("{POS Sale Service BL}", "Saving products.");
        //            //Console.WriteLine("Saving products.");
        //            if (ReCreateStructureTables._InsertData(DateTime.Now, null, invProductsResponse, Constants.SqlServer, updateExisting))
        //            {
        //                Logger.write("{POS Sale Service BL}", "Saved products.sending acknowledgement to server");

        //                UpdateProductFlag updateProductFlag = new UpdateProductFlag();
        //                updateProductFlag.BranchId = Global.BranchId.ToString();
        //                invProductsResponse.invProducts.ForEach(p => {
        //                    updateProductFlag.updatedProducts.Add(new UpdatedProduct { ProductId = p.Id, RetailPrice = p.RetailPrice, UpdateStatus = "t" });
        //                });
        //                ApiResponse ackResponse = sysTablesClient.PostUpdatedProducts(updateProductFlag);
        //                if (ackResponse != null)
        //                    Logger.write("{POS Sale Service BL}", ackResponse.Code + ": " + ackResponse.Message);
        //                return true;

        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.write("{GetProductsOnlySqlServer}", ex.Message.ToString());
        //        //Console.WriteLine(ex.Message.ToString());
        //        return false;
        //    }
        //    return false;
        //}
        public bool GetProductsRecentSqlServer(int recordsToFetch, bool updateExisting)
        {
            try
            {
                StructureNDataClient sysTablesClient = new StructureNDataClient();
                ProductsDao productsDao = new ProductsDao();
                InvProductsResponse invProductsResponse = null;
                SynchSettingsDao synchSettingsDao = new SynchSettingsDao();
                Logger.write("{POS Sale Service BL}", "Getting some products only");
                bool downlaodedAll = false;
                bool firsthit = true; string prodid = "0";
                while (!downlaodedAll)
                {
                    {
                        Logger.write("{POS Sale Service BL}", "Getting some products");

                        Logger.write("{POS Sale Service BL}", prodid);
                        invProductsResponse = sysTablesClient.GetProducts(prodid, recordsToFetch, "r");
                        if (invProductsResponse != null)
                        {
                            if (invProductsResponse.Response.Code == ApplicationResponse.MAX_REACHED_CODE)
                            {
                                Logger.write("{POS Sale Service BL}", "All products downloaded.");
                                //Console.WriteLine("All products downloaded.");
                                downlaodedAll = true;
                                //synchSettingsDao.UpdatePendingSynchSettings(new List<int> { lastSynchSetting.setting_id }, "done", Constants.SqlServer);
                                //synchSettingsDao.InsertSynchSettings(SynchMethods.database_data.ToString(), SynchTypes.products_recent.ToString(), Utility.GetNextDayMorningDateTime(), "ready", Constants.SqlServer);

                            }
                            else if (invProductsResponse.invProducts != null && invProductsResponse.invProducts.Count > 0)
                            {
                                prodid = invProductsResponse.invProducts.Max(pid => pid.Id).ToString();
                                Logger.write("{POS Sale Service BL}", "Saving products.");
                                //Console.WriteLine("Saving products.");                              

                                if (ReCreateStructureTables._InsertData(DateTime.Now, null, invProductsResponse, Constants.SqlServer, updateExisting))
                                {
                                    Logger.write("{POS Sale Service BL}", "Saved products.sending acknowledgement to server");
                                    UpdateProductFlag updateProductFlag = new UpdateProductFlag();
                                    updateProductFlag.BranchId = Global.BranchId.ToString();
                                    invProductsResponse.invProducts.ForEach(p =>
                                    {
                                        updateProductFlag.updatedProducts.Add(new UpdatedProduct { ProductId = p.Id, RetailPrice = p.RetailPrice, UpdateStatus = "t" });
                                    });
                                    ApiResponse ackResponse = sysTablesClient.PostUpdatedProducts(updateProductFlag);
                                    if (ackResponse != null)
                                        Logger.write("{POS Sale Service BL}", ackResponse.Code + ": " + ackResponse.Message);
                                }
                            }
                        }
                    }
                }
                return true;
                //Console.WriteLine("Getting some products");
                //invProductsResponse = sysTablesClient.GetProducts("-1", recordsToFetch, "f");
                //if (invProductsResponse != null)
                //{
                //    Logger.write("{POS Sale Service BL}", "Saving products.");
                //    //Console.WriteLine("Saving products.");
                //    if (ReCreateStructureTables._InsertData(DateTime.Now, null, invProductsResponse, Constants.SqlServer, updateExisting))
                //    {
                //        Logger.write("{POS Sale Service BL}", "Saved products.sending acknowledgement to server");

                //        UpdateProductFlag updateProductFlag = new UpdateProductFlag();
                //        updateProductFlag.BranchId = Global.BranchId.ToString();
                //        invProductsResponse.invProducts.ForEach(p => {
                //            updateProductFlag.updatedProducts.Add(new UpdatedProduct { ProductId = p.Id, RetailPrice = p.RetailPrice, UpdateStatus = "t" });
                //        });
                //        ApiResponse ackResponse = sysTablesClient.PostUpdatedProducts(updateProductFlag);
                //        if (ackResponse != null)
                //            Logger.write("{POS Sale Service BL}", ackResponse.Code + ": " + ackResponse.Message);
                //        return true;

                //    }

                //}
            }
            catch (Exception ex)
            {
                Logger.write("{GetProductsOnlySqlServer}", ex.Message.ToString());
                //Console.WriteLine(ex.Message.ToString());
                return false;
            }
            return false;
        }
        public bool GetVendorsOnlySqlServer(int recordsToFetch, bool updateExisting)
        {
            try
            {
                StructureNDataClient sysTablesClient = new StructureNDataClient();
                VendorsDao vendorsDao = new VendorsDao();
                InvProductsResponse invVendorsResponse = null;
                Logger.write("{POS Sale Service BL}", "Getting some vendors only");

                invVendorsResponse = sysTablesClient.GetVendors("0", recordsToFetch, "r");
                if (invVendorsResponse != null)
                {
                    Logger.write("{POS Sale Service BL}", "Saving vendors data.");

                    if (ReCreateStructureTables._InsertData(DateTime.Now, null, invVendorsResponse, Constants.SqlServer, updateExisting))
                    {
                        Logger.write("{POS Sale Service BL}", "Saved vendors.sending acknowledgement to server");

                        //UpdateVendorFlag updateProductFlag = new UpdateProductFlag();
                        //updateProductFlag.BranchId = Global.BranchId.ToString();
                        //invVendorsResponse.invVendors.ForEach(p => {
                        //    updateProductFlag.updatedProducts.Add(new UpdatedProduct { ProductId = p.Id, RetailPrice = p.RetailPrice, UpdateStatus = "t" });
                        //});
                        //ApiResponse ackResponse = sysTablesClient.PostUpdatedProducts(updateProductFlag);
                        //if (ackResponse != null)
                        //    Logger.write("{POS Sale Service BL}", ackResponse.Code + ": " + ackResponse.Message);
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
        public bool GetFiscalYearsOnlySqlServer(int recordsToFetch, bool updateExisting)
        {
            try
            {
                StructureNDataClient sysTablesClient = new StructureNDataClient();
                AccFiscalYearResponse? accFiscalYearResponse = null;
                Logger.write("{POS Sale Service BL}", "Getting some vendors only");

                accFiscalYearResponse = sysTablesClient.GetFiscalYears();
                if (accFiscalYearResponse != null)
                {
                    Logger.write("{POS Sale Service BL}", "Saving fiscal year data.");

                    if (ReCreateStructureTables._InsertFiscalYearData(DateTime.Now, accFiscalYearResponse, Constants.SqlServer, updateExisting))
                    {
                        Logger.write("{POS Sale Service BL}", "Saved fiscal year");
                        return true;

                    }

                }
            }
            catch (Exception ex)
            {
                Logger.write("{GetFiscalYearsOnlySqlServer}", ex.Message.ToString());
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
                            if (ReCreateStructureTables._InsertData(DateTime.Now, sysTablesResponse, null, Constants.Sqlite,true))
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
