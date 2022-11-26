using AutoSynchService.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchService.ApiClient
{
    internal class ApiManager
    {
        public static string PostAsync(string inputJson, string apiMethod)
        {
            string requestResult = string.Empty;
            var client = new HttpClient();
            try
            {
                var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false);

                IConfiguration config = builder.Build();
                                
                client.BaseAddress = new Uri(config.GetSection("ApiLinks").GetValue<string>("BaseAddress"));
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //var response = await client.PostAsync(Utility.SERVICE_URL + apiMethod, new StringContent(inputJson.ToString(), Encoding.UTF8, "application/json"));
                //var response = client.PostAsync(Global.SERVICE_URL + apiMethod, new StringContent(inputJson.ToString(), Encoding.UTF8, "application/json")).Result;
                //var response = client.PostAsync(apiMethod, new StringContent(inputJson.ToString(), Encoding.UTF8, "application/json")).Result;
                HttpResponseMessage responses = client.PostAsync(apiMethod, new StringContent(inputJson.ToString(), Encoding.UTF8, "application/json")).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                if (responses.IsSuccessStatusCode)
                {
                   return responses.Content.ReadAsStringAsync().Result;

                }
                else
                {
                    return requestResult;
                }
                //if (response.IsSuccessStatusCode)
                //{
                //    requestResult = response.Content.ReadAsStringAsync().Result;
                //}

                //return requestResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                client.Dispose();
            }
        }
        public static string GetAsync(string apiMethod)
        {
            string requestResult = string.Empty;

            try
            {
                HttpClient client = new HttpClient();
                var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false);

                IConfiguration config = builder.Build();

                client.BaseAddress = new Uri(config.GetSection("ApiLinks").GetValue<string>("BaseAddress"));

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    // List data response.
                    HttpResponseMessage responses = client.GetAsync(apiMethod).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                    if (responses.IsSuccessStatusCode)
                    {
                        return responses.Content.ReadAsStringAsync().Result;

                    }
                    else
                    {
                        return requestResult;
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    client.Dispose();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
