using AutoSynchService.ApiClient;
using AutoSynchService.Classes;
using AutoSynchService.DAOs;
using AutoSynchService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchService
{
    internal static class BusinessLogic
    {
        public static bool UploadToServer()
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

    }
}
