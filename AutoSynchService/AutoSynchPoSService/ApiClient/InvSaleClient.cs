using AutoSynchPosService.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchPoSService.ApiClient
{
    internal class InvSaleClient
    {
        string invSaleApiUrl = "/api/InvSales";
        public DataResponse? GetSaleDetails()
        {
            try
            {
                try
                {

                    invSaleApiUrl = "/api/InvSales/GetSaleDetails";
                    // var json = JsonConvert.SerializeObject(signinDTO);
                    var responses = ApiManager.GetAsync(invSaleApiUrl);

                    // List data response.
                    if (responses != null)
                    {
                        //using (var streamReader = new StreamReader(responses))
                        {
                            //var jsonResult = streamReader.ReadToEnd();
                            DataResponse response = JsonConvert.DeserializeObject<DataResponse>(responses);
                            if (response.invSaleDetails != null)
                            {
                                return response;
                            }
                            return null;

                        }

                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ApiResponse PostInvSaleDetails(DataResponse dataResponse)
        {
            ApiResponse responseDTO = new ApiResponse();
            try
            {
                invSaleApiUrl = "/api/InvSales/PostSaleDetails";
                var json = JsonConvert.SerializeObject(dataResponse);
                var response = ApiManager.PostAsync(json, invSaleApiUrl);

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
