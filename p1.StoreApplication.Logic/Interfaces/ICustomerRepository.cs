using p1.StoreApplication.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p1.StoreApplication.Logic.Interfaces
{
  public interface ICustomerRepository
  {
    Task<CustomerV> SelectAsync(CustomerV c) { throw new NotImplementedException(); }
    Task<List<CustomerV>> SelectAsync() { throw new NotImplementedException(); }
    Task<CustomerV> InsertAsync(CustomerV c) { throw new NotImplementedException(); }
  }
}
