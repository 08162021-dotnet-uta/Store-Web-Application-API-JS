using p1.StoreApplication.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p1.StoreApplication.Logic.Interfaces
{
  public interface IStoreRepository
  {
    Task<List<StoreV>> SelectAsync() { throw new NotImplementedException(); }
    Task<StoreV> SelectAsync(StoreV s) { throw new NotImplementedException(); }
  }
}
