using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p1.StoreApplication.Models.ViewModels
{
  public class InventoryV
  {
    public int StoreInventoryId { get; set; } = -1;
    public int StoreId { get; set; } = -1;
    public int ProductId { get; set; } = -1;
    public int Quantity { get; set; } = -1;
  }
}
