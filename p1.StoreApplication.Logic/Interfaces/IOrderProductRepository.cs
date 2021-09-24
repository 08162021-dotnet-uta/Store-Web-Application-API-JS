using p1.StoreApplication.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p1.StoreApplication.Logic.Interfaces
{
  public interface IOrderProductRepository
  {
    Task<List<OrderProductV>> SelectAsync(OrderV o) { throw new NotImplementedException(); }
    Task<OrderProductV> SelectAsync(OrderProductV op) { throw new NotImplementedException(); }
    Task<List<OrderProductV>> SelectAsync() { throw new NotImplementedException(); }
    Task<OrderProductV> InsertAsync(OrderProductV op) { throw new NotImplementedException(); }
  }
}
