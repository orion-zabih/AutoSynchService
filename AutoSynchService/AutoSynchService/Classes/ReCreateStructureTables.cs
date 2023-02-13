using AutoSynchService.Models;
using AutoSynchSqlite.DbManager;
using AutoSynchSqlServerLocal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchService.Classes
{
    internal class ReCreateStructureTables
    {
        internal static bool _CreateDatabase(string dbPath)
        {
            SqliteManager objSqliteManager = new SqliteManager();
            objSqliteManager.CreateDbFile(dbPath);
            return true;
        }
        internal static bool _CreateDBTables(DateTime dateTime, TableStructureResponse objResponse,string dbtype)
        {
            try
            {
                bool isStructureComplete = false;
                string format = "yyyy-MM-dd HH:mm:ss";

                string qry = " insert into synch_setting(setting_id,synch_method,synch_type,table_names,status,sync_timestamp,insertion_timestamp,update_timestamp) values(1,'" + SynchMethods.database_structure+"','" + SynchTypes.structure_only + "','','done','" + dateTime.ToString(format) + "','" + dateTime.ToString(format) + "','" + dateTime.ToString(format) + "')";
                string qry2 = " insert into synch_setting(setting_id,synch_method,synch_type,table_names,status,sync_timestamp,insertion_timestamp,update_timestamp) values(2,'" + SynchMethods.database_data + "','" + SynchTypes.except_product_sale_master_detail_tables + "','','ready','" + dateTime.ToString(format) + "','" + dateTime.ToString(format) + "','" + dateTime.ToString(format) + "')";

                if (dbtype.Equals(Constants.Sqlite))
                {
                    SqliteManager objSqliteManager = new SqliteManager();
                    DataTable dt = objSqliteManager.GetDataTable("SELECT name FROM sqlite_master WHERE type='table' AND name='synch_setting'");
                    if (dt.Rows.Count == 0)
                    {
                        objResponse.createQueries.Add("create table synch_setting(setting_id int primary key,synch_method varchar(16),synch_type varchar(64),table_names text,status varchar(32) default 'ready',sync_timestamp datetime,insertion_timestamp datetime,update_timestamp datetime)");
                        // modify the format depending upon input required in the column in database 
                        objResponse.createQueries.Add(qry);
                        objResponse.createQueries.Add(qry2);

                    }
                    if (objSqliteManager.ExecuteTransactionMultiQueries(objResponse.dropQueries)) 
                    //multiQueries.Add("create table app_setting(last_update_date DATETIME,next_update_date DATETIME)");
                    isStructureComplete = objSqliteManager.ExecuteTransactionMultiQueries(objResponse.createQueries);                    
                }
                else
                {
                    MsSqlDbManager msSqlDbManager = new MsSqlDbManager();
                    DataTable dt = msSqlDbManager.GetDataTable("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' and TABLE_NAME='synch_setting'");
                    if (dt.Rows.Count == 0)
                    {
                        objResponse.createQueries.Add("create table synch_setting(setting_id int primary key,synch_method varchar(32),synch_type varchar(64),table_names varchar(max),status varchar(32) default 'ready',sync_timestamp datetime,insertion_timestamp datetime,update_timestamp datetime)");
                        // modify the format depending upon input required in the column in database 
                        objResponse.createQueries.Add(qry);
                        objResponse.createQueries.Add(qry2);
                    }
                    try
                    {

                        objResponse.dropQueries.ForEach(q => {
                            msSqlDbManager.ExecuteTransQuery(q);
                        });
                       // objSqliteManager.Commit();
                        objResponse.createQueries.ForEach(q => {
                            msSqlDbManager.ExecuteTransQuery(q);
                        });
                        msSqlDbManager.Commit();    
                        isStructureComplete=true;
                    }
                    catch (Exception ex)
                    {
                        msSqlDbManager.RollBack();
                        throw;
                    }
                }
                if (isStructureComplete)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Logger.write("CreateDB", ex.ToString());
                Console.WriteLine(ex.Message);

                return false;
            }
        }

        internal static bool _InsertData(DateTime dateTime, SysTablesResponse? objResponse, InvProductsResponse? objProductjResponse, string dbtype)
        {
            try
            {
                var listofClasses = objResponse != null ? typeof(SysTablesResponse).GetProperties().ToList(): typeof(InvProductsResponse).GetProperties().ToList();
                List<string> multiQueries = new List<string>();
                bool isStructureComplete = false;
                if (objResponse != null || objProductjResponse != null)
                {
                    Type myType = objResponse!=null? objResponse.GetType() : objProductjResponse.GetType();
                    IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

                    listofClasses.ForEach(t =>
                    {
                        var listProperties = t.PropertyType.GetProperties().ToList();
                        if(listProperties.Count>2)

                        {
                            var clas = listProperties[2].PropertyType;
                        var fields = clas.GetProperties().ToList().OrderByDescending(p => p.Name).Select(p => p.Name).ToList();
                        string columns = string.Join(",", fields.ToArray());

                        PropertyInfo prop = props.FirstOrDefault(u => u.Name == t.Name);
                            if (prop != null)
                            {
                                IList collection = (IList)prop.GetValue(objResponse != null ? objResponse : objProductjResponse, null);
                                foreach (object item in collection)
                                {
                                    string values = string.Empty;
                                    fields.ForEach(fiel =>
                                    {
                                        object vlu = GetPropValue(item, fiel);

                                        if (vlu != null && vlu.ToString().Contains("'"))
                                        {
                                            values += "'" + vlu.ToString().Replace("'", "''") + "',";
                                        }
                                        else
                                        {
                                            values += "'" + vlu + "',";
                                        }

                                    });

                                    multiQueries.Add("insert into " + getTableName(clas.Name) + "(" + columns + ") values(" + values.TrimEnd(',') + ")");
                                }
                            }
                        }

                    });
                    //release, database
                    string format = "yyyy-MM-dd HH:mm:ss";
                    string qry = " insert into synch_setting(setting_id,synch_method,synch_type,table_names,status,insertion_timestamp,update_timestamp) values(1,'database','" + SynchTypes.full + "','','done','" + dateTime.ToString(format) + "','" + dateTime.ToString(format) + "')";
                    if (dbtype.Equals(Constants.Sqlite))
                    {
                        SqliteManager objSqliteManager = new SqliteManager();
                        DataTable dt = objSqliteManager.GetDataTable("SELECT name FROM sqlite_master WHERE type='table' AND name='synch_setting'");
                        if (dt.Rows.Count == 0)
                        {
                            multiQueries.Add("create table synch_setting(setting_id int primary key,synch_method varchar(16),synch_type varchar(64),table_names text,status varchar(32) default 'ready',insertion_timestamp datetime,update_timestamp datetime)");
                            // modify the format depending upon input required in the column in database 
                            multiQueries.Add(qry);

                        }
                        isStructureComplete = objSqliteManager.ExecuteTransactionMultiQueries(multiQueries);                        
                    }
                    else
                    {
                        MsSqlDbManager msSqlDbManager = new MsSqlDbManager();
                        DataTable dt = msSqlDbManager.GetDataTable("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' and TABLE_NAME='synch_setting'");
                        if (dt.Rows.Count == 0)
                        {
                            multiQueries.Add("create table synch_setting(setting_id int primary key,synch_method varchar(16),synch_type varchar(64),table_names varchar(max),status varchar(32) default 'ready',insertion_timestamp datetime,update_timestamp datetime)");
                            // modify the format depending upon input required in the column in database 
                            multiQueries.Add(qry);
                        }
                        try
                        {
                            for (int i = 0; i < multiQueries.Count; i+=1000)
                            {
                                msSqlDbManager = new MsSqlDbManager();
                                int uBound = 1000;
                                if (i + 1000 >= multiQueries.Count)
                                    uBound = (multiQueries.Count % 1000) != 0 ? (multiQueries.Count % 1000) : 1000;
                                multiQueries.GetRange(i,uBound).ForEach(q => {
                                    
                                    msSqlDbManager.ExecuteTransQuery(q);
                                });
                                msSqlDbManager.Commit();
                            }
                            
                            isStructureComplete = true;
                        }
                        catch (Exception ex)
                        {
                            msSqlDbManager.RollBack();
                            Console.WriteLine(ex.Message);
                            isStructureComplete = false;
                        }
                        
                    }
                    if (isStructureComplete)
                        return true;
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }



            }
            catch (Exception ex)
            {
                //Logger.write("CreateDB", ex.ToString());

                return false;
            }
        }
        private static string getTableName(string className)
        {
            switch (className.ToLower())
            {
                case "invsaledetail":
                    {
                        className = "InvSaleDetailTmp";
                    }
                    break;
                case "invsalemaster":
                    {
                        className = "InvSaleMasterTmp";
                    }
                    break;
                default:
                    break;
            }
            return className;

        }
        private static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}
