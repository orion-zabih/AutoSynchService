using AutoSynchService.Classes;
using AutoSynchSqlite.DbManager;
using AutoSynchSqlServerLocal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchService.DAOs
{
    internal class VendorsDao
    {
        internal int GetMaxVendorId(string dbtype)
        {
			try
			{
                string qry = "select max(Id) from InvVendor";

                if (dbtype.Equals(Constants.Sqlite))
                {
                    SqliteManager sqlite = new SqliteManager();
                    string maxid = sqlite.ExecuteScalar(qry);
                    return int.Parse(maxid);
                }
                else
                {
                    MsSqlDbManager sqlDbManager = new MsSqlDbManager();
                    object maxId = sqlDbManager.ExecuteScalar(qry);
                    string maxid = maxId!=null? maxId.ToString(): "0";
                    return int.Parse(maxid);
                }
            }
			catch (Exception ex)
			{

				throw ex;
			}
        }
        internal int GetExistingVendorId(string dbtype,string prodId)
        {
            try
            {
                string qry = "select Count(Id) from InvVendor where Id="+prodId;

                if (dbtype.Equals(Constants.Sqlite))
                {
                    SqliteManager sqlite = new SqliteManager();
                    string maxid = sqlite.ExecuteScalar(qry);
                    return int.Parse(maxid);
                }
                else
                {
                    MsSqlDbManager sqlDbManager = new MsSqlDbManager();
                    object maxId = sqlDbManager.ExecuteScalar(qry);
                    string maxid = maxId != DBNull.Value ? maxId.ToString() : "0";
                    return int.Parse(maxid);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
