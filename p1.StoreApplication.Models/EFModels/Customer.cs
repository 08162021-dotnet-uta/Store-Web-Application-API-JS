using System;
using System.Collections.Generic;

#nullable disable

namespace p1.StoreApplication.Models.EFModels
{
    public partial class Customer
    {
        public Customer()
        {
            StoreOrders = new HashSet<StoreOrder>();
        }

        public short CustomerId { get; set; }
        public string FName { get; set; }

        public string LName { get; set; }

        public virtual ICollection<StoreOrder> StoreOrders { get; set; }

        public override string ToString()
        {
            return FName + " " + LName;
        }
  }
}
