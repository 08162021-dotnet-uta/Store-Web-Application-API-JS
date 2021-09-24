using System;
using System.Collections.Generic;
using p1.StoreApplication.Models.EFModels;
using p1.StoreApplication.Logic.Repositories;

namespace p1.StoreApplication.Client.Singletons
{
  public class OrderSingleton
  {
    private static OrderSingleton _orderSingleton;
    private static readonly OrderRepository _orderRepo = new();
    public List<StoreOrder> Orders { get; private set; }
    public static OrderSingleton Instance
    {
      get
      {
        if (_orderSingleton == null)
        {
          _orderSingleton = new OrderSingleton();
        }

        return _orderSingleton;
      }
    }
    private OrderSingleton()
    {
      Orders = _orderRepo.Select();
    }

    public void Add(StoreOrder order)
    {
      _orderRepo.Insert(order);
      Orders = _orderRepo.Select();
    }

    public List<StoreOrder> QueryOrders(Store store)
    {
      return _orderRepo.Select(store);
    }

    public List<StoreOrder> QueryOrders(Customer customer)
    {
      return _orderRepo.Select(customer);
    }
    /// <summary>
    /// Selects the latest order from the StoreOrder table by order date
    /// </summary>
    /// <returns></returns>
    public StoreOrder QueryLastOrder()
    {
      return _orderRepo.SelectLastOrder();
    }
  }
}