using AutoSynchService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchService.Classes
{
    internal class Converter
    {
        internal static List<InvSaleDetail> GetInvSaleDetails(DataTable table)
        {
            var invSaleDetailList = new List<InvSaleDetail>(table.Rows.Count);
            foreach (DataRow row in table.Rows)
            {
                var values = row.ItemArray;
                var invSaleDetail = new InvSaleDetail()
                {
                    Id = Convert.ToInt32(values[0]),
                    BillId = Convert.ToInt32(values[1]),
                    ProductId = Convert.ToInt32(values[2]),
                    Price = Convert.ToDecimal(values[3]),
                    Qty = Convert.ToDecimal(values[4]),
                    Total = Convert.ToDecimal(values[5]),
                    IsDeleted = (bool)values[6],
                    SaleValue = Convert.ToDecimal(values[7]),
                    TaxCharged = Convert.ToDecimal(values[8]),
                    TaxRate = Convert.ToDecimal(values[9]),
                    Pctcode = Convert.ToString(values[10] != DBNull.Value ? values[10]:""),
                    FurtherTax = Convert.ToDecimal(values[11]),
                    Discount = Convert.ToDecimal(values[12]),
                    InvoiceType = Convert.ToInt32(values[13]),
                    PriceExclusiveTax = Convert.ToDecimal(values[14])
                    
                };
                invSaleDetailList.Add(invSaleDetail);
            }
            return invSaleDetailList;
        }
    }
}
