using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p1.StoreApplication.Models.ViewModels
{
  public class ProductV
  {
    public int ProductId { get; set; } = -1;
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
  }
}
