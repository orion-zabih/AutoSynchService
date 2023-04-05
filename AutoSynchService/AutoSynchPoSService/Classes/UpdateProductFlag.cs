using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchPoSService.Classes
{
    internal class UpdateProductFlag
    {
        public string BranchId { get; set; }
        public List<UpdatedProduct> updatedProducts { get; set; }
        public UpdateProductFlag()
        {
            BranchId = String.Empty;
            updatedProducts = new List<UpdatedProduct>();
        }
    }
    internal class UpdatedProduct
    {
        public int ProductId { get; set; }
        public decimal RetailPrice { get; set; }
        public string UpdateStatus { get; set; }
        public UpdatedProduct()
        {
            UpdateStatus = String.Empty;
        }

    }
}
