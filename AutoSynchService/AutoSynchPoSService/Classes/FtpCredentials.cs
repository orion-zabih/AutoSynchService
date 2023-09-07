using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchPosService.Classes
{
    public class FtpCredentials
    {
        public string EnableFtpSynch { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Directory { get; set; }
        public string BinFolder { get; set; }
        public FtpCredentials()
        {
            Port = 21;
            EnableFtpSynch = IP = Username = Password = Directory = BinFolder = string.Empty;
        }
    }
    public class MySettings
    {
        public string BranchId { get; set; }    
        public string BackoffTimerUnit { get; set; }
        public int BackoffTimer { get; set; }
        public string LocalDb { get; set; }
        public string IsBranchFilter { get; set; }
        public string RecordsToFetch { get; set; }
        public string DaysToDeleteQT { get; set; }
        public string SynchProduct { get; set; }
        public string UpdateExisting { get; set; }
        public MySettings()
        {
            BranchId = "";
            BackoffTimerUnit = "hr";
            BackoffTimer = 24;
            LocalDb = "";
            RecordsToFetch = "1000";
            IsBranchFilter =SynchProduct=UpdateExisting= "false";
            DaysToDeleteQT = "0";
        }
    }
   
}
