using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchService.Classes
{
    public class UpdateProductFlag
    {
        public string BranchId { get; set; }
        public List<UpdatedProduct> updatedProducts { get; set; }
        public UpdateProductFlag()
        {
            BranchId = String.Empty;
            updatedProducts = new List<UpdatedProduct>();
        }
    }
    public class UpdatedProduct
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
