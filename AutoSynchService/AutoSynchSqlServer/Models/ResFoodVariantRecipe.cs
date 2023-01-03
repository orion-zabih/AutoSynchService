using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class ResFoodVariantRecipe
    {
        public int Id { get; set; }
        public int FoodVariantId { get; set; }
        public int ItemId { get; set; }
        public decimal Qty { get; set; }
        public int UnitId { get; set; }
    }
}
