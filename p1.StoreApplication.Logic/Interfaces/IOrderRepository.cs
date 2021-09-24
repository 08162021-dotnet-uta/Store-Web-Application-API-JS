using p1.StoreApplication.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p1.StoreApplication.Logic.Interfaces
{
  public interface IOrderRepository
  {
    Task<OrderV> SelectAsync(OrderV o) { throw new NotImplementedException(); }
    Task<List<OrderV>> SelectAsync(StoreV s) { throw new NotImplementedException(); }
    Task<List<OrderV>> SelectAsync(CustomerV c) { throw new NotImplementedException(); }
    Task<OrderV> InsertAsync(OrderV o) { throw new NotImplementedException(); }
    Task<OrderV> SelectAsyncLast() { throw new NotImplementedException(); }
  }
}
