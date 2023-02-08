using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchSqlServerLocal
{
    public class MsSqlDbManager
    {
        SqlConnection con;
        public MsSqlDbManager()
        {
            //Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\mydatabase.mdb;Jet OLEDB:Database Password=MyDbPassword;
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            string dbPath = configuration.GetConnectionString("DefaultConnection");
            string dbName = configuration.GetConnectionString("SqlDb");
            // string dbPassword = configuration.GetConnectionString("dbPassword");

            con = new SqlConnection(dbPath.Replace("##db##", dbName));

        }
        public bool CreateDb(string dbname, string dbpath, int dbsize, int dbmaxsize, int dbfilegrowth)
        {

            String str;
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            string dbPath = configuration.GetConnectionString("DefaultMasterConnection");
            SqlConnection myConn = new SqlConnection(dbPath);

            str = $"CREATE DATABASE {dbname} ON PRIMARY " +
 $"(NAME = {dbname}, " +
 $"FILENAME = '{dbpath + "//" + dbname}.mdf', " +
 $"SIZE = {dbsize}MB, MAXSIZE = {dbmaxsize}MB, FILEGROWTH = {dbfilegrowth}%)" +
 $"LOG ON (NAME = {dbname}_Log, " +
 $"FILENAME = '{dbpath + "//" + dbname}.ldf', " +
 $"SIZE = {dbsize}MB, " +
 $"MAXSIZE = {dbmaxsize}MB, " +
 $"FILEGROWTH = {dbfilegrowth}%)";

            SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();
                // MessageBox.Show("DataBase is Created Successfully", "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine("DataBase is Created Successfully");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                //MessageBox.Show(ex.ToString(), "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
            return true;
        }
        
        public bool CheckDatabaseExists(string databaseName)
        {
            string sqlCreateDBQuery;
            bool result = false;

            try
            {

                sqlCreateDBQuery = string.Format("SELECT database_id FROM sys.databases WHERE Name = '{0}'", databaseName);

                using (con)
                {
                    using (SqlCommand sqlCmd = new(sqlCreateDBQuery, con))
                    {
                        con.Open();

                        object resultObj = sqlCmd.ExecuteScalar();

                        int databaseID = 0;

                        if (resultObj != null)
                        {
                            int.TryParse(resultObj.ToString(), out databaseID);
                        }

                        con.Close();

                        result = (databaseID > 0);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                result = false;
            }

            return result;
        }
        public DataTable GetDataTable(string query, bool closeConn = true)
        {
            DataTable dt = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                cmd.Dispose();
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (closeConn)
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
            }
        }
        public int ExecuteQuery(string query)
        {
            int rec = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlTransaction trans = con.BeginTransaction();
                try
                {
                    rec = cmd.ExecuteNonQuery();
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
                finally
                {

                    cmd.Dispose();
                }
                return rec;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
        public object ExecuteScalar(string query)
        {

            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                try
                {
                    object rec = cmd.ExecuteScalar();
                    return rec;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    cmd.Dispose();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
        public int ExecuteNonQuery(string query)
        {

            try
            {

                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                try
                {
                    int rec = cmd.ExecuteNonQuery();
                    return rec;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    cmd.Dispose();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public int ExecuteQuery(string query, SqlParameter[] parms)
        {
            int rec = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                for (int i = 0; i < parms.Length; i++)
                {
                    cmd.Parameters.Add(parms[i]);
                }
                SqlTransaction trans = con.BeginTransaction();
                try
                {
                    cmd.Transaction = trans;
                    rec = cmd.ExecuteNonQuery();

                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }

                cmd.Dispose();
                return rec;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public int ExecuteQuery(List<string> query)
        {
            int rec = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                SqlTransaction trans = con.BeginTransaction();
                try
                {
                    query.ForEach(q =>
                    {
                        cmd.CommandText = q;
                        cmd.Transaction = trans;
                        rec = cmd.ExecuteNonQuery();
                    });
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }

                cmd.Dispose();
                return rec;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private SqlTransaction trans_global;
        public int beginTransaction(out SqlTransaction newTrans)
        {

            try
            {
                trans_global = this.con.BeginTransaction();
                newTrans = trans_global;
            }
            catch (InvalidOperationException ioe)
            {
                //Logger.WriteLog(LogType.ERROR, "beginTransaction", ioe.ToString());
                newTrans = trans_global; // already a transaction is initiated, so the ref to that transaction is returned.
                return -2; // -2 Parallel transactions are not supported.

            }
            catch (Exception e)
            {
                //Logger.WriteLog(LogType.ERROR, "beginTransaction", e.ToString());
                newTrans = null;
                return -3; //-3 some other exception
            }

            return 0; //successful

        }
        public int ExecuteTransQuery(string query)
        {
            int rec = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlCommand cmd = new SqlCommand(query, con);


                try
                {
                    if (trans_global == null)
                    {
                        beginTransaction(out trans_global);
                    }
                    cmd.Transaction = trans_global;
                    rec = cmd.ExecuteNonQuery();

                }
                catch (Exception)
                {
                    throw;
                }

                cmd.Dispose();
                return rec;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                //if (con.State == ConnectionState.Open)
                //    con.Close();
            }
        }
        public int ExecuteTransQueryParam(string query, SqlParameter[] parms)
        {
            int rec = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand(query, con);


                try
                {
                    for (int i = 0; i < parms.Length; i++)
                    {
                        cmd.Parameters.Add(parms[i]);
                    }
                    if (trans_global == null)
                    {
                        beginTransaction(out trans_global);
                    }
                    cmd.Transaction = trans_global;
                    rec = cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    throw ex;
                }

                cmd.Dispose();
                return rec;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                //if (con.State == ConnectionState.Open)
                //    con.Close();
            }
        }
        public int ExecuteTransQueryParamRetPK(string query, SqlParameter[] parms)
        {
            int rec = 0;
            int PrimaryKey;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand(query, con);


                try
                {
                    for (int i = 0; i < parms.Length; i++)
                    {
                        cmd.Parameters.Add(parms[i]);
                    }
                    if (trans_global == null)
                    {
                        beginTransaction(out trans_global);
                    }
                    cmd.Transaction = trans_global;
                    rec = cmd.ExecuteNonQuery();
                    // Send the next command
                    cmd.Parameters.Clear();
                    cmd.CommandText = "SELECT @@IDENTITY";
                    PrimaryKey = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                cmd.Dispose();
                return PrimaryKey;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                //if (con.State == ConnectionState.Open)
                //    con.Close();
            }
        }
        public bool RollBack()
        {
            try
            {
                trans_global.Rollback();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

        }
        public bool Commit()
        {
            try
            {
                trans_global.Commit();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

        }
    }
}
