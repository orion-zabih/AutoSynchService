using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace AutoSynchPosService.Classes
{
    static class Logger
    {
        private static Stream logFile;
        private static bool isInitialized = false;

        public static void closeFile()
        {
            if (isInitialized)
            {
                Trace.Close();
                logFile.Dispose();
            }
        }

        public static void createFile()
        {
            try
            {
                string dirLocation = System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("AutoSynchPoSService.dll","") + "Log";
                DirectoryInfo dir = new DirectoryInfo(dirLocation);
                if (!dir.Exists)
                {
                    dir.Create();
                }
                dir = null;

                if (File.Exists(dirLocation + DateTime.Today.Day + "-" + DateTime.Today.Month + "-" + DateTime.Today.Year + ".log"))
                {
                    logFile = File.Open(dirLocation + "\\" + DateTime.Today.Day + "-" + DateTime.Today.Month + "-" + DateTime.Today.Year + ".log", FileMode.Append);
                }
                else
                {
                    logFile = File.Create(dirLocation +"\\"+ DateTime.Today.Day + "-" + DateTime.Today.Month + "-" + DateTime.Today.Year + ".log");
                }

                TextWriterTraceListener myTextListener = new TextWriterTraceListener(logFile);
                Trace.Listeners.Add(myTextListener);
                isInitialized = true;
            }
            catch (Exception)
            {
                isInitialized = false;
            }

        }

        public static void write(string msg,bool isException=false)
        {
            if (!isInitialized)
                createFile();
            Trace.WriteLine(isException?"Exception##:"+msg:msg);
            Trace.Flush();
        }

        public static void write(string module, string msg, bool isException = false)
        {
            if (!isInitialized)
                createFile();
            string dt = DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss");
            msg = dt + "/" + module + ": " + (isException ? "Exception##:" + msg : msg);
            Trace.WriteLine(msg);
            Trace.Flush();
        }
        //public static bool LogToDatabase(string Module, string Method, string Record_Id, string Log_Detail, string User_Name)
        //{
        //    try
        //    {
        //        if (Log_Detail != null && Log_Detail.Length > 900)
        //        {
        //            Log_Detail = Log_Detail.Substring(0, 900);
        //        }
        //        if (Method != null && Method.Length > 64)
        //        {
        //            Method = Method.Substring(0, 63);
        //        }

                
        //        {
        //            audit_logs auditLogRow = new audit_logs
        //            {
        //                module = Module == null ? "" : Module,
        //                method = Method,
        //                message = (string.IsNullOrWhiteSpace(Log_Detail) ? (string.IsNullOrWhiteSpace(Record_Id) ? Method : Method + ", id = " + Record_Id) : (string.IsNullOrWhiteSpace(Record_Id) ? Method + ", " + Log_Detail : Method + ", id = " + Record_Id + ", " + Log_Detail)).ToLower(),
        //                username = User_Name,
        //                client_ip=Utility.GetSystemIP(),
        //                insertion_timestamp= LoggerDbAccess.GetServerDateTime()
        //            };
        //            AuditLogsDao auditLogsDao = new AuditLogsDao();
        //            auditLogsDao.InsertAuditLog(auditLogRow);
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        write("Logger:LogToDatabase", "Error Writing Log to Database"+ex.StackTrace);
        //        return false;
        //    }


        //}
        //public static bool LogDeviceConnectionHistory(long device_id,string status,string err_message)
        //{
        //    try
        //    {
               
        //        {
        //            device_connection_history deviceHistroyRow = new device_connection_history
        //            {device_id=device_id,
        //            error_message=err_message,
        //            status=status,
        //                insertion_timestamp = LoggerDbAccess.GetServerDateTime()
        //            };
        //            DeviceConHistoryDao deviceConHistory = new DeviceConHistoryDao();
        //            deviceConHistory.InsertDeviceConHistory(deviceHistroyRow);
                    
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        write("Logger:LogDeviceConnectionHistory", "Error Writing Device Connection Log to Database" + ex.StackTrace);
        //        return false;
        //    }
        //}
    }
}
