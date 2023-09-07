using AutoSynchPosService.Classes;
using AutoSynchPoSService.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchPoSService.ApiClient
{
    internal class StructureNDataClient
    {
        string invSaleApiUrl = "/api/SysTables";
        public SysTablesResponse? GetTableData(SynchTypes synchType,string IsBranchFilter)
        {
            try
            {
                try
                {
                    string table_list = "notable";
                    if (IsBranchFilter!=null & IsBranchFilter.Equals("true"))
                    {
                        table_list = "IsBranchFilterY";
                    }
                    invSaleApiUrl = "/api/SysTables" + "/GetTableData?branch_id=" + Global.BranchId + "&synch_type=" + synchType + "&table_list=notable";
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
        public InvProductsResponse? GetProducts(string maxProdId, int recordsToFetch,string isQuick="f")//,string prodLedger="false"
        {
            try
            {
                try
                {

                    invSaleApiUrl = "/api/SysTables" + "/GetProducts?branch_id=" + Global.BranchId + "&max_prod_id=" + maxProdId + "&records_to_fetch=" + recordsToFetch + "&is_quick=" + isQuick;
                    // var json = JsonConvert.SerializeObject(signinDTO);
                    var responses = ApiManager.GetAsync(invSaleApiUrl);

                    // List data response.
                    if (responses != null)
                    {
                        //using (var streamReader = new StreamReader(responses))
                        {
                            //var jsonResult = streamReader.ReadToEnd();
                            InvProductsResponse response = JsonConvert.DeserializeObject<InvProductsResponse>(responses);
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
                    throw ex;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public InvProductsResponse? GetVendors(string maxVendorId, int recordsToFetch, string isQuick = "f")//,string prodLedger="false"
        {
            try
            {
                try
                {

                    invSaleApiUrl = "/api/SysTables" + "/GetVendors?branch_id=" + Global.BranchId + "&max_vendor_id=" + maxVendorId + "&records_to_fetch=" + recordsToFetch + "&is_quick=" + isQuick;
                    // var json = JsonConvert.SerializeObject(signinDTO);
                    var responses = ApiManager.GetAsync(invSaleApiUrl);

                    // List data response.
                    if (responses != null)
                    {
                        //using (var streamReader = new StreamReader(responses))
                        {
                            //var jsonResult = streamReader.ReadToEnd();
                            InvProductsResponse response = JsonConvert.DeserializeObject<InvProductsResponse>(responses);
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
                    throw ex;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public TableStructureResponse? GetTableStructure(SynchTypes synchType, string dbType)
        {
            try
            {
                try
                {

                    invSaleApiUrl = "/api/SysTables" + "/GetTableStructure?branch_id=" + Global.BranchId + "&synch_type=" + synchType + "&table_list=notable" + "&local_db=" + dbType;
                    // var json = JsonConvert.SerializeObject(signinDTO);
                    var responses = ApiManager.GetAsync(invSaleApiUrl);

                    // List data response.
                    if (responses != null)
                    {
                        //using (var streamReader = new StreamReader(responses))
                        {
                            //var jsonResult = streamReader.ReadToEnd();
                            TableStructureResponse response = JsonConvert.DeserializeObject<TableStructureResponse>(responses);
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
        public TableStructureResponse? GetTableColumns(SynchTypes synchType,string tablelList, string dbType)
        {
            try
            {
                try
                {

                    invSaleApiUrl = "/api/SysTables" + "/GetTableColumns?branch_id=" + Global.BranchId + "&synch_type=" + synchType + "&table_list="+ tablelList+ "&local_db=" + dbType;
                    // var json = JsonConvert.SerializeObject(signinDTO);
                    var responses = ApiManager.GetAsync(invSaleApiUrl);

                    // List data response.
                    if (responses != null)
                    {
                        //using (var streamReader = new StreamReader(responses))
                        {
                            //var jsonResult = streamReader.ReadToEnd();
                            TableStructureResponse response = JsonConvert.DeserializeObject<TableStructureResponse>(responses);
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

        public ApiResponse PostUpdatedProducts(UpdateProductFlag dataResponse)
        {
            ApiResponse responseDTO = new ApiResponse();
            try
            {
                invSaleApiUrl = "/api/SysTables/PostUpdatedProducts";
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
