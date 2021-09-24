using System.Collections.Generic;
using p1.StoreApplication.Models.EFModels;
using p1.StoreApplication.Logic.Repositories;

namespace p1.StoreApplication.Client.Singletons
{
  public class CustomerSingleton
  {
    private static CustomerSingleton _customerSingleton;
    private static readonly CustomerRepository _customerRepo = new();
    public List<Customer> Customers { get; private set; }
    public static CustomerSingleton Instance
    {
      get
      {
        if (_customerSingleton == null)
        {
          _customerSingleton = new CustomerSingleton();
        }

        return _customerSingleton;
      }
    }
    private CustomerSingleton()
    {
      Customers = _customerRepo.Select();
    }

    public void Add(Customer customer)
    {
      _customerRepo.Insert(customer);
      Customers = _customerRepo.Select();
    }

    public Customer GetCustomer(short id)
    {
      return _customerRepo.Select(id);
    }
  }
}