using AutoSynchSqlServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchSqlServer.CustomModels
{
    public class InvSaleDetailExtended : InvSaleDetail
    {
        public int InvoiceType { get; set; }


        public decimal ItemCostValue { get; set; }
        public string ProductName { get; set; }
        public decimal ItemDiscount { get; set; }
        public decimal ItemSaleValue { get; set; }
        public decimal ItemTaxCharged { get; set; }
        public decimal ItemExtraTaxCharged { get; set; }
        public decimal ItemNetTotal { get; set; }
        public decimal ItemProfit { get; set; }
        public decimal ItemLost { get; set; }
    }
}
