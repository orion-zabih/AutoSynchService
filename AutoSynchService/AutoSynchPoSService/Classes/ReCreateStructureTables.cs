
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

namespace AutoSynchPosService.Classes
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

                        //objResponse.dropQueries.ForEach(q => {
                        //    msSqlDbManager.ExecuteTransQuery(q);
                        //});
                       // objSqliteManager.Commit();
                        objResponse.createQueries.ForEach(q => {
                            try
                            {
                                msSqlDbManager.ExecuteTransQuery(q);
                            }
                            catch (Exception ex)
                            {
                                Logger.write("Create Table Sql Server:"+ex.Message,true);
                            }
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
                 Logger.write("CreateDB", ex.ToString());
                //Console.WriteLine(ex.Message);

                return false;
            }
        }

        internal static bool _InsertData(DateTime dateTime, SysTablesResponse? objResponse, InvProductsResponse? objProductjResponse, string dbtype)
        {
            try
            {
                var listofClasses = objResponse != null ? typeof(SysTablesResponse).GetProperties().ToList(): typeof(InvProductsResponse).GetProperties().ToList();
                List<TableDataCls> multiQueries = new List<TableDataCls>();
                bool isStructureComplete = false;
                List<string> tableNames=new List<string>();
                if (objResponse != null || objProductjResponse != null)
                {
                    Type myType = objResponse!=null? objResponse.GetType() : objProductjResponse.GetType();
                    IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

                    listofClasses.ForEach(t =>
                    {
                    var listProperties = t.PropertyType.GetProperties().ToList();
                    if (listProperties.Count > 2)

                    {
                        var clas = listProperties[2].PropertyType;
                        tableNames.Add(getTableName(clas.Name, dbtype));
                        //multiQueries.Add(new TableDataCls {TableName= getTableName(clas.Name),Qry= "SET IDENTITY_INSERT " + getTableName(clas.Name) + " OFF"});
                        List<string> fieldsold = clas.GetProperties().ToList().OrderByDescending(p => p.Name).Select(p => p.Name).ToList();
                            List<string> fields = new List<string>();
                            fieldsold.ForEach (f =>
                        {
                            fields.Add( getColName(clas.Name, f));
                            
                        }) ;
                            //foreach (string colNm in fields)
                            //{
                            //     getColName(clas.Name, colNm);
                            //}
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
                                        fiel = fiel == "[privatekey]" ? "PrivateKey" : fiel;
                                        object vlu = GetPropValue(item, fiel);

                                        if (vlu != null && vlu.ToString().Contains("'"))
                                        {
                                            values += "'" + vlu.ToString().Replace("'", "''") + "',";
                                        }
                                        else
                                        {
                                            values += "'" + vlu + "',";
                                        }
                                        if(vlu != null && fiel != null && fiel.ToLower().Equals("id"))
                                            multiQueries.Add(new TableDataCls { TableName = getTableName(clas.Name, dbtype), Qry = "delete from " + getTableName(clas.Name, dbtype) + " where " + fiel + "=" + vlu });
                                    });


                                    multiQueries.Add(new TableDataCls { TableName = getTableName(clas.Name, dbtype), Qry = "insert into " + getTableName(clas.Name, dbtype) + "(" + columns + ") values(" + values.TrimEnd(',') + ")" });
                                }

                               // multiQueries.Add(new TableDataCls { TableName = getTableName(clas.Name), Qry = "SET IDENTITY_INSERT " + getTableName(clas.Name) + " ON" });
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
                            tableNames.Add("synch_setting");
                            multiQueries.Add(new TableDataCls { TableName = "synch_setting", Qry = "create table synch_setting(setting_id int primary key,synch_method varchar(16),synch_type varchar(64),table_names text,status varchar(32) default 'ready',insertion_timestamp datetime,update_timestamp datetime)" });
                            // modify the format depending upon input required in the column in database 
                            multiQueries.Add(new TableDataCls { TableName = "synch_setting", Qry = qry });

                        }
                        isStructureComplete = objSqliteManager.ExecuteTransactionMultiQueries(multiQueries.Select(q=>q.Qry).ToList());                        
                    }
                    else
                    {
                        MsSqlDbManager msSqlDbManager = new MsSqlDbManager();
                        DataTable dt = msSqlDbManager.GetDataTable("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' and TABLE_NAME='synch_setting'");
                        if (dt.Rows.Count == 0)
                        {
                            tableNames.Add("synch_setting");
                            multiQueries.Add(new TableDataCls { TableName = "synch_setting", Qry = "create table synch_setting(setting_id int primary key,synch_method varchar(16),synch_type varchar(64),table_names varchar(max),status varchar(32) default 'ready',insertion_timestamp datetime,update_timestamp datetime)" });
                            // modify the format depending upon input required in the column in database 
                            multiQueries.Add(new TableDataCls { TableName = "synch_setting", Qry = qry });
                        }
                        try
                        {
                            tableNames.ForEach(t => {
                                List<string> quries = multiQueries.Where(q=>q.TableName==t).Select(s=>s.Qry).ToList();
                                if(quries.Count > 0)
                                {
                                    //if(t.Equals("InvCategory"))
                                    //{

                                    //}
                                    msSqlDbManager = new MsSqlDbManager();
                                    try
                                    {
                                       
                                        msSqlDbManager.ExecuteTransQuery("SET IDENTITY_INSERT " + t + " ON");
                                    }
                                    catch (Exception)
                                    {
                                    }
                                    ////msSqlDbManager.Commit();
                                    try
                                    {
                                      //  msSqlDbManager = new MsSqlDbManager();
                                        for (int i = 0; i < quries.Count; i++)
                                        {
                                            msSqlDbManager.ExecuteTransQuery(quries[i]);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Logger.write(ex.Message);
                                        msSqlDbManager.RollBack();
                                        throw;
                                    }
                                    finally{
                                        try
                                        {
                                            msSqlDbManager.ExecuteTransQuery("SET IDENTITY_INSERT " + t + " OFF");
                                        }
                                        catch (Exception)
                                        {
                                        }
                                        msSqlDbManager.Commit();
                                    }
                                    
                                    
                                }
                                //for (int i = 0; i < quries.Count; i += 1000)
                                //{
                                //    msSqlDbManager = new MsSqlDbManager();
                                //    int uBound = 1000;
                                //    if (i + 1000 >= multiQueries.Count)
                                //        uBound = (multiQueries.Count % 1000) != 0 ? (multiQueries.Count % 1000) : 1000;
                                //    multiQueries.GetRange(i, uBound).ForEach(q => {
                                //        msSqlDbManager.ExecuteTransQuery(q);

                                //    });
                                ////    msSqlDbManager.Commit();
                                //}
                            });
                            
                            
                            isStructureComplete = true;
                        }
                        catch (Exception ex)
                        {
                            msSqlDbManager.RollBack();
                            Logger.write("Insert record", ex.ToString());
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

                Logger.write("InsertData", ex.ToString());
                return false;
            }
        }
        private static string getTableName(string className,string local_db)
        {
            if (local_db.Equals(Constants.SqlServer))
                return className;
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
        private static string getColName(string className, string colName)
        {
           
            switch (className.ToLower())
            {
               
                case "orgbranch":
                    {
                        if(colName.ToLower() == "privatekey")
                        colName= colName.ToLower() == "privatekey"? "[privatekey]": colName;
                    }
                    break;
                default:
                    break;
            }
            return colName;

        }
        private static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
    internal class TableDataCls
    {
        public string TableName { get; set; }
        public string Qry { get; set; }
        public TableDataCls() {
            TableName = Qry = string.Empty;
        }
    }
}
