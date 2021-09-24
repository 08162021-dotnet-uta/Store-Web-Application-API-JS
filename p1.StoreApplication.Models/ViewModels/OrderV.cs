using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p1.StoreApplication.Models.ViewModels
{
  public class OrderV
  {
    public int OrderId { get; set; } = -1;
    public int CustomerId { get; set; } = -1;
    public int StoreId { get; set; } = -1;
    public DateTime OrderDate { get; set; }
  }
}
