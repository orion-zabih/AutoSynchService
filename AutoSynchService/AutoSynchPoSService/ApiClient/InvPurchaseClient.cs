using AutoSynchPosService.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchPoSService.ApiClient
{
    internal class InvPurchaseClient
    {
        string invPurchaseeApiUrl = "/api/Purchase";
        
        public ApiResponse PostInvPurchaseDetails(DataResponse dataResponse)
        {
            ApiResponse responseDTO = new ApiResponse();
            try
            {
                dataResponse.BranchId = Global.BranchId;
                invPurchaseeApiUrl = "/api/Purchase/InsertPurchaseVouchers";
                var json = JsonConvert.SerializeObject(dataResponse);
                var response = ApiManager.PostAsync(json, invPurchaseeApiUrl);

                if (response != null)
                {
                    var result = JsonConvert.DeserializeObject<ApiResponse>(response);
                    if (result != null)
                        responseDTO = result;
                }

                return responseDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
