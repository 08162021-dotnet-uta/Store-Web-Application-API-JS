using System;
using System.Collections.Generic;

#nullable disable

namespace p1.StoreApplication.Models.EFModels
{
    public partial class StoreInventory
    {
        public int StoreInventoryId { get; set; }
        public short StoreId { get; set; }
        public short ProductId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Store Store { get; set; }
  }
}
