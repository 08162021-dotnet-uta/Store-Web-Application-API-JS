using p1.StoreApplication.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p1.StoreApplication.Logic.Interfaces
{
  public interface IProductRepository
  {
    Task<ProductV> SelectAsync(ProductV p) { throw new NotImplementedException(); }
    Task<List<ProductV>> SelectAsync(StoreV s) { throw new NotImplementedException(); }
  }
}
