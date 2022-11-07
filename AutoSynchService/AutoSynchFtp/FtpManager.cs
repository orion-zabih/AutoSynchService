using FluentFTP;
using Microsoft.Extensions.Logging;
using System.IO.Compression;
using System.Net;

namespace AutoSynchFtp
{
    public class FtpManager
    {
        public bool PingServer(string ftp_server_ip,int port,string ftp_user_name,string ftp_user_password)
        {
            bool connected = false;
            try
            {
                FtpClient client = new FtpClient();
                client.Host = ftp_server_ip;
                client.Credentials = new NetworkCredential(ftp_user_name, ftp_user_password);
                Console.WriteLine("Pinging FTP ....  ");
                client.Connect();
                connected = true;
                client.Disconnect();
                return connected;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UploadFileOnFtp(string ftp_server_ip, int port, string ftp_user_name, string ftp_user_password,byte[] fileBytes, string folderName, string fileName)
        {
            try
            {
                Console.WriteLine("");
                Console.WriteLine("Uploading file " + fileName.ToUpper() + " on ftp server");
                //Logger.write("Uploading file to FTP " + fileName);
                FtpClient client = new FtpClient();
                client.Host = ftp_server_ip;
                client.Credentials = new NetworkCredential(ftp_user_name, ftp_user_password);
                Console.WriteLine("Connecting to FTP ....  ");
                client.Connect();
                if (!client.DirectoryExists(folderName))
                {
                    client.CreateDirectory(folderName);
                }
                Console.WriteLine("FTP Connected successfully");
                client.UploadBytes(fileBytes, fileName);
                Console.WriteLine("File uploaded successfully");
                //byte[] bts = null;
                //client.Download(out bts, fileName);
                client.Disconnect();
                //Logger.WriteLog(LogType.INFORMATION, "Upload file to FTP", "success");
                return true;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message + Environment.NewLine + exp.StackTrace);
                //Logger.WriteLog(LogType.ERROR, "Upload file to FTP", "Uploading file to FTP -- failed " + Environment.NewLine +
                //    exp.Message + Environment.NewLine + exp.StackTrace);
                return false;
            }
        }
        public bool DownloadFilesFromFtp(string ftp_server_ip, int port, string ftp_user_name, string ftp_user_password,string localFolderName, string remoteFolderName)
        {
            try
            {
                Console.WriteLine("");
                Console.WriteLine("Downloading foler " + remoteFolderName.ToUpper() + " from ftp server");
                //Logger.write("Uploading file to FTP " + fileName);
                FtpClient client = new FtpClient();
                client.Host = ftp_server_ip;
                client.Credentials = new NetworkCredential(ftp_user_name, ftp_user_password);
                Console.WriteLine("Connecting to FTP ....  ");
                client.Connect();
                Console.WriteLine("FTP Connected successfully");
               // if (client.DirectoryExists(remoteFolderName))
                {
                    client.DownloadFile(localFolderName, remoteFolderName, FtpLocalExists.Overwrite, FtpVerify.None);

//                    client.DownloadDirectory(localFolderName, remoteFolderName, FtpFolderSyncMode.Update, FtpLocalExists.Overwrite, FtpVerify.None);
                }
                Console.WriteLine("File downloaded successfully");
                //byte[] bts = null;
                //client.Download(out bts, fileName);
                client.Disconnect();
                //Logger.WriteLog(LogType.INFORMATION, "Upload file to FTP", "success");
                return true;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message + Environment.NewLine + exp.StackTrace);
                //Logger.WriteLog(LogType.ERROR, "Upload file to FTP", "Uploading file to FTP -- failed " + Environment.NewLine +
                //    exp.Message + Environment.NewLine + exp.StackTrace);
                return false;
            }
        }


    }
}