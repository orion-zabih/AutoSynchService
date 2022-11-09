using AutoSynchService.Models;
using AutoSynchSqlite.DbManager;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchService.Classes
{
    internal class ReCreateStructureTables
    {
        internal static bool _CreateDB(DateTime dateTime, SysTablesResponse objResponse)
        {
            try
            {
                SqliteManager objSqliteManager = new SqliteManager();
                //if (File.Exists(DbBinPath))
                //{
                //    objSqliteManager.DeleteDbFile(DbBinPath);
                //}
                //if (!File.Exists(DbBinPath))
                {
                    //if (!Directory.Exists(DbFolder))
                    //{
                    //    Directory.CreateDirectory(DbFolder);
                    //}

                    //Console.WriteLine("Just entered to create Sync DB");

                    //objSqliteManager.CreateDbFile(DbBinPath);
                    bool isStructureComplete = false;
                    List<string> multiQueries = new List<string>();

                    var listofClasses = typeof(SysTablesResponse).GetProperties().ToList();

                    listofClasses.ForEach(t =>
                    {
                        var listProperties = t.PropertyType.GetProperties().ToList();
                        var clas = listProperties[2].PropertyType;
                        var fields = clas.GetProperties().ToList().Select(p => p.Name);
                        string columns = string.Join(" varchar(128),", fields.ToArray());
                       // multiQueries.Add("")
                        multiQueries.Add("create table " + clas.Name + "(" + columns + " varchar(128))");

                    });
                    //multiQueries.Add("create table app_setting(last_update_date DATETIME,next_update_date DATETIME)");
                    isStructureComplete = objSqliteManager.ExecuteTransactionMultiQueries(multiQueries);

                    if (isStructureComplete)
                    {
                        multiQueries = new List<string>();
                        //DB structure completed. now get libraries from server
                        //Load Libraries from Service
                        //EtoApiClient APIClient = new EtoApiClient();


                        //ApiIRequestData Eto_Libraries_Request = new ApiIRequestData
                        //{
                        //    username = Global.LOGGEDIN_USERNAME,
                        //    userId = Global.LOGGEDIN_USERID,
                        //    password = Global.LOGGEDIN_PASSWORD,
                        //    siteId = Global.SITE_ID,
                        //    clientIp = Global.CLIENT_IP,
                        //    appVersion = Global.APPLICATION_VERSION
                        //};

                        //SysTablesResponse objResponse = APIClient.getEtoApplicationLibraries(Eto_Libraries_Request);

                        if (objResponse != null)
                        {
                            Type myType = objResponse.GetType();
                            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

                            listofClasses.ForEach(t =>
                            {

                                //FieldInfo fld =typeof(objResponse).GetField(t.Name);

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
                                    multiQueries.Add("insert into " + clas.Name + "(" + columns + ") values(" + values.TrimEnd(',') + ")");
                                }


                            });
                            //multiQueies.Add("create table app_setting(last_update_date varchar(16),next_update_date varchar(16))");
                            //string format = "yyyy-MM-dd HH:mm:ss";    // modify the format depending upon input required in the column in database 
                            //string insert = @" insert into app_setting(last_update_date,next_update_date) values('" + time.ToString(format) + "')";

                           // multiQueries.Add("insert into app_setting(last_update_date,next_update_date) values('" + dateTime.ToString(format) + "','" + dateTime.AddDays(7).ToString(format) + "')");
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
        private static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}
