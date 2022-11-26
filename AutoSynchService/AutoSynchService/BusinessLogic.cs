using AutoSynchFtp;
using AutoSynchService.ApiClient;
using AutoSynchService.Classes;
using AutoSynchService.DAOs;
using AutoSynchService.Models;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchService
{
    internal static class BusinessLogic
    {
        public static bool UploadInvSaleToServer()
        {
            try
            {
                InvSaleDao invSaleDao = new InvSaleDao();
                DataResponse dataResponse = new DataResponse();
                dataResponse.invSaleMaster = invSaleDao.GetSaleMaster();
                dataResponse.invSaleMaster.ForEach(m => {
                    dataResponse.invSaleDetails.AddRange(invSaleDao.GetSaleDetails(m.Id));
                });

                InvSaleClient invSaleClient = new InvSaleClient();

                ApiResponse apiResponse = invSaleClient.PostInvSaleDetails(dataResponse);
                if (apiResponse.Code == ApplicationResponse.SUCCESS_CODE)
                {

                    invSaleDao.UpdateMasterIsUploaded(dataResponse.invSaleMaster.Select(m => m.Id).ToList());

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            
            return true;
        }

        public static bool DownloadPublish(FtpCredentials ftpCredentials)
        {
            FtpManager ftpManager = new FtpManager();
                      
            
            string folderName =ftpCredentials.Directory;
            string filename = "testzipfile.zip";
            string localFolder = System.IO.Path.Combine(folderName, "Publish Folder");
            string pathString = System.IO.Path.Combine(folderName, "Publish Folder", filename);
            string extractPath = System.IO.Path.Combine(folderName, "Publish Extract Folder");

            System.IO.Directory.CreateDirectory(localFolder);
            System.IO.Directory.CreateDirectory(extractPath);

            if ( ftpManager.DownloadFilesFromFtp(ftpCredentials.IP, ftpCredentials.Port, ftpCredentials.Username, ftpCredentials.Password, pathString, filename))
            {

                ZipFile.ExtractToDirectory(pathString, extractPath,true);
                return true;
            }
            return false;
        }
        public static bool GetAndReplaceSysTables()
        {
            try
            {
                SysTablesClient sysTablesClient = new SysTablesClient();
                SysTablesResponse sysTablesResponse = sysTablesClient.GetSysTables();
                if (sysTablesResponse != null)
                {
                    ReCreateStructureTables._CreateDB(new DateTime(), sysTablesResponse);
                }
                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;
            }

        }
    }
}
