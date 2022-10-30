﻿
using AutoSynchService.Models;

namespace AutoSynchAPI.Models
{
    public class DataResponse
    {
        public ApiResponse Response { get; set; }
        public List<AccAccount> accAccounts { get; set; }
        public List<InvSaleDetail> invSaleDetails { get; set; }
       
        public DataResponse()
        {
            Response = new ApiResponse();
            accAccounts = new List<AccAccount>();
        }
    }


}
