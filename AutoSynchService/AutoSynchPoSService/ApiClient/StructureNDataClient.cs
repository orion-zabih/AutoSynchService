using AutoSynchPosService.Classes;
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

        public SysTablesResponse? GetTableData(SynchTypes synchType)
        {
            try
            {
                try
                {

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
        public InvProductsResponse? GetProducts(string maxProdId, int recordsToFetch)//,string prodLedger="false"
        {
            try
            {
                try
                {

                    invSaleApiUrl = "/api/SysTables" + "/GetProducts?branch_id=" + Global.BranchId + "&max_prod_id=" + maxProdId + "&records_to_fetch=" + recordsToFetch;
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

    }
}
