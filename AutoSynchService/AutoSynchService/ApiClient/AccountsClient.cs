using AutoSynchService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchService.ApiClient
{
    
    internal class AccountsClient
    {
        string accountAPIUrl = "/AccAccount";
        public DataResponse? GetAccounts()
        {
            try
            {
                DataResponse dataResponse = null;
                try
                {

                    accountAPIUrl += "/GetAccounts";
                   // var json = JsonConvert.SerializeObject(signinDTO);
                    var responses = ApiManager.GetAsync(accountAPIUrl);

                    // List data response.
                    if (responses!=null)
                    {
                        using (var streamReader = new StreamReader(responses))
                        {
                            var jsonResult = streamReader.ReadToEnd();
                            DataResponse response = JsonConvert.DeserializeObject<DataResponse>(jsonResult);
                            if (response.accAccounts != null)
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

        //public static async Task<ApiResponse> GetAccounts()
        //{
        //    // Initialization.  
        //    ApiResponse apiResponse = new ApiResponse();
        //    // HTTP GET.  
        //    using (HttpClient client = new HttpClient())
        //    {
        //        // Setting Base address.  
        //        client.BaseAddress = new Uri("https://localhost:7061");

        //        // Setting content type.  
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        // Initialization.  
        //        HttpResponseMessage response = new HttpResponseMessage();

        //        // HTTP GET  
        //        response = await client.GetAsync("api/Download").ConfigureAwait(true);

        //        // Verification  
        //        if (response.IsSuccessStatusCode)
        //        {
        //            // Reading Response.  
        //            string result = response.Content.ReadAsStringAsync().Result;
        //            //Root? myDeserializedClass = JsonConvert.DeserializeObject<Root>(result);
        //            try
        //            {
        //                apiResponse = JsonConvert.DeserializeObject<ApiResponse>(result);
        //            }
        //            catch (Exception ex)
        //            {

        //                throw;
        //            }

        //        }
        //    }

        //    return apiResponse;
        //}
    }
}
