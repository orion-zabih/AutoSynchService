using AutoSynchClientEngine.Classes;
using AutoSynchClientEngine.Classes;
using AutoSynchSqlite.DbManager;
using AutoSynchSqlServer.Models;
using AutoSynchSqlServerLocal;
using FluentFTP.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchClientEngine.DAOs
{
    internal class UsersDao
    {

        internal UsrSystemUser? GetUser(string username, string password, string dbtype)
        {
            string user_name = CryptoHelper.encrypt(username);
            string user_password = CryptoHelper.encrypt(password);
            string qry = "select * from UsrSystemUser where LoginName='" + user_name + "' and Password = '" + user_password + "'";
            if (dbtype.Equals(Constants.Sqlite))
            {
                SqliteManager sqlite = new SqliteManager();
                DataTable users = sqlite.GetDataTable(qry);
                Converter converter = new Converter();
                UsrSystemUser? usrSystemUsers = Converter.GetUsrSystemUser(users);
                return usrSystemUsers;
            }
            else
            {
                MsSqlDbManager sqlDbManager = new MsSqlDbManager();
                DataTable users = sqlDbManager.GetDataTable(qry);
                Converter converter = new Converter();
                UsrSystemUser? usrSystemUsers = Converter.GetUsrSystemUser(users);
                return usrSystemUsers;
            }
        }
    }
}
