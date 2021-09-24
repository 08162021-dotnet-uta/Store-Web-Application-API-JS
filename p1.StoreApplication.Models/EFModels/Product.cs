using System;
using System.Collections.Generic;

#nullable disable

namespace p1.StoreApplication.Models.EFModels
{
    public partial class Product
    {
        public Product()
        {
            OrderProducts = new HashSet<OrderProduct>();
            StoreInventories = new HashSet<StoreInventory>();
        }
        public short ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public short Quantity { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
        public virtual ICollection<StoreInventory> StoreInventories { get; set; }
        public override string ToString()
        {
            return Name + ": $" + String.Format("{0:0.00}", Price) + " | " + Description;
        }
    }
}
