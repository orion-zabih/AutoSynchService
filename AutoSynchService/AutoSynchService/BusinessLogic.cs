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
            InvSaleDao invSaleDao = new InvSaleDao();
            DataResponse dataResponse = new DataResponse();
            dataResponse.invSaleMaster = invSaleDao.GetSaleMaster();
            dataResponse.invSaleMaster.ForEach(m => {
                dataResponse.invSaleDetails.AddRange(invSaleDao.GetSaleDetails(m.Id));
            });

            InvSaleClient invSaleClient = new InvSaleClient();
            
            ApiResponse apiResponse= invSaleClient.PostInvSaleDetails(dataResponse);
            if(apiResponse.Code== ApplicationResponse.SUCCESS_CODE)
            {
                
                    invSaleDao.UpdateMasterIsUploaded(dataResponse.invSaleMaster.Select(m=> m.Id).ToList());
                
            }
            return true;
        }

        public static bool DownloadPublish()
        {
            FtpManager ftpManager = new FtpManager();
            string folderName = @"c:\wworoot Folder";
            string filename = "testzipfile.zip";
            string localFolder = System.IO.Path.Combine(folderName, "Publish Folder");
            string pathString = System.IO.Path.Combine(folderName, "Publish Folder", filename);
            string extractPath = System.IO.Path.Combine(folderName, "Publish Extract Folder");

            System.IO.Directory.CreateDirectory(localFolder);
            System.IO.Directory.CreateDirectory(extractPath);
            if ( ftpManager.DownloadFilesFromFtp("66.219.22.159", 21, "cmsnet", "Hmis@360", pathString, filename))
            {

                ZipFile.ExtractToDirectory(pathString, extractPath,true);
                return true;
            }
            return false;
        }
    }
}
