using AutoSynchService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchService.ApiClient
{
   
    internal class SysTablesClient
    { 
        string invSaleApiUrl = "/api/SysTables";
        public SysTablesResponse? GetSysTables()
        {
            try
            {
                try
                {

                    invSaleApiUrl += "/GetSysTables";
                    // var json = JsonConvert.SerializeObject(signinDTO);
                    var responses = ApiManager.GetAsync(invSaleApiUrl);

                    // List data response.
                    if (responses != null)
                    {
                        //using (var streamReader = new StreamReader(responses))
                        {
                            //var jsonResult = streamReader.ReadToEnd();
                            SysTablesResponse response = JsonConvert.DeserializeObject<SysTablesResponse>(responses);
                            if (response != null)
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

        
    }
}
