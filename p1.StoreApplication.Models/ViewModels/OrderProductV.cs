using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p1.StoreApplication.Models.ViewModels
{
  public class OrderProductV
  {
    public int OrderProductId { get; set; } = -1;
    public int OrderId { get; set; } = -1;
    public int ProductId { get; set; } = -1;
    public short Quantity { get; set; }
  }
}
