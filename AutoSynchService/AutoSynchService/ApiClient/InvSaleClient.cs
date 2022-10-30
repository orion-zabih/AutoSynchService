using AutoSynchService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchService.ApiClient
{
   
    internal class InvSaleClient
    { 
        string invSaleApiUrl = "/upload";

        private static DataResponse _PostRepint(DataResponse dataResponse)
        {
            DataResponse responseDTO = new DataResponse();
            try
            {
                string landappdataAPIUrl = "/eto/getEtoReprintData";

                var json = JsonConvert.SerializeObject(dataResponse);
                var response = ApiManager.PostAsync(json, landappdataAPIUrl);

                if (response != null)
                {
                    var result = JsonConvert.DeserializeObject<DataResponse>(response);
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
