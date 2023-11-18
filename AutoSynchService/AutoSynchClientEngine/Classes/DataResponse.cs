

using AutoSynchSqlServer.Models;

namespace AutoSynchClientEngine.Classes
{
    public class DataResponse
    {
        public ApiResponse Response { get; set; }
        public List<InvProduct> invProduct { get; set; }
        public List<AccAccount> accAccounts { get; set; }
        public List<InvSaleDetail> invSaleDetails { get; set; }
        public List<InvSaleMaster> invSaleMaster { get; set; }
        public List<InvPurchaseOrderMaster> invPurchaseOrderMasters { get; set; }
        public List<InvPurchaseOrderDetail> invPurchaseOrderDetails { get; set; }
        public List<InvPurchaseMaster> invPurchaseMaster { get; set; }
        public List<InvPurchaseDetail> invPurchaseDetails { get; set; }
        public int BranchId { get; set; }
        public List<InvProductLedger> invProductLedgers { get; set; }
        public DataResponse()
        {
            Response = new ApiResponse();
            accAccounts = new List<AccAccount>();
            invSaleDetails = new List<InvSaleDetail>();
            invSaleMaster = new List<InvSaleMaster>();
            invProduct = new List<InvProduct>();
            invPurchaseOrderMasters = new List<InvPurchaseOrderMaster>();
            invPurchaseOrderDetails = new List<InvPurchaseOrderDetail>();
            BranchId = 0;
            invPurchaseMaster = new List<InvPurchaseMaster>();
            invPurchaseDetails = new List<InvPurchaseDetail>();
            invProductLedgers = new List<InvProductLedger>();
        }
    }


}
