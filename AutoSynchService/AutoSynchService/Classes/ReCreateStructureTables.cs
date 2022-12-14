using AutoSynchService.Models;
using AutoSynchSqlite.DbManager;
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
        internal static bool _CreateDBTables(DateTime dateTime, TableStructureResponse objResponse)
        {
            try
            {
                SqliteManager objSqliteManager = new SqliteManager();

                //if (!File.Exists(DbBinPath))
                {
                    //if (!Directory.Exists(DbFolder))
                    //{
                    //    Directory.CreateDirectory(DbFolder);
                    //}

                    //Console.WriteLine("Just entered to create Sync DB");

                    //objSqliteManager.CreateDbFile(DbBinPath);
                    bool isStructureComplete = false;



                    if (objSqliteManager.ExecuteTransactionMultiQueries(objResponse.dropQueries)) 
                    //multiQueries.Add("create table app_setting(last_update_date DATETIME,next_update_date DATETIME)");
                    isStructureComplete = objSqliteManager.ExecuteTransactionMultiQueries(objResponse.createQueries);

                    if (isStructureComplete)
                    {
                        return true;
                    }
                    else
                    {
                        //bool isDbFileDeleted = objSqliteManager.DeleteDbFile(DbBinPath);

                        return false;
                    }
                }
                //else
                //{
                //    return false;
                //}
            }
            catch (Exception ex)
            {
                //Logger.write("CreateDB", ex.ToString());

                return false;
            }
        }

        internal static bool _InsertData(DateTime dateTime, SysTablesResponse objResponse)
        {
            try
            {
                SqliteManager objSqliteManager = new SqliteManager();
                var listofClasses = typeof(SysTablesResponse).GetProperties().ToList();
                List<string> multiQueries = new List<string>();
                bool isStructureComplete = false;
                if (objResponse != null)
                {
                    Type myType = objResponse.GetType();
                    IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

                    listofClasses.ForEach(t =>
                    {
                        var listProperties = t.PropertyType.GetProperties().ToList();
                        var clas = listProperties[2].PropertyType;
                        var fields = clas.GetProperties().ToList().OrderByDescending(p => p.Name).Select(p => p.Name).ToList();
                        string columns = string.Join(",", fields.ToArray());

                        PropertyInfo prop = props.FirstOrDefault(u => u.Name == t.Name);
                        IList collection = (IList)prop.GetValue(objResponse, null);
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


                    });
                    //release, database
                    DataTable dt = objSqliteManager.GetDataTable("SELECT name FROM sqlite_master WHERE type='table' AND name='synch_setting'");
                    if (dt.Rows.Count == 0)
                    {
                        multiQueries.Add("create table synch_setting(setting_id int primary key,synch_method varchar(16),synch_type varchar(64),table_names text,status default 'ready',insertion_timestamp datetime,update_timestamp datetime)");
                        string format = "yyyy-MM-dd HH:mm:ss";    // modify the format depending upon input required in the column in database 
                        multiQueries.Add(" insert into synch_setting(setting_id,synch_method,synch_type,table_names,status,insertion_timestamp,update_timestamp) values(1,'database','" + SynchTypes.full + "','','done','" + dateTime.ToString(format) + "','" + dateTime.ToString(format) + "')");

                    }
                    isStructureComplete = objSqliteManager.ExecuteTransactionMultiQueries(multiQueries);
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
