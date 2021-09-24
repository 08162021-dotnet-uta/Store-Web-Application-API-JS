using System;
using System.Collections.Generic;
using p1.StoreApplication.Models.EFModels;
using p1.StoreApplication.Logic.Repositories;

namespace p1.StoreApplication.Client.Singletons
{
  public class OrderProductSingleton
  {
    private static OrderProductSingleton _orderProductSingleton;
    private static readonly OrderProductRepository _orderProductRepo = new();
    public List<OrderProduct> OrderProducts { get; private set; }
    public static OrderProductSingleton Instance
    {
      get
      {
        if (_orderProductSingleton == null)
        {
          _orderProductSingleton = new OrderProductSingleton();
        }

        return _orderProductSingleton;
      }
    }
    private OrderProductSingleton()
    {
      OrderProducts = _orderProductRepo.Select();
    }

    public void Add(OrderProduct order)
    {
      _orderProductRepo.Insert(order);
      OrderProducts = _orderProductRepo.Select();
    }

    public OrderProduct GetOrderProduct(short productId, short orderId)
    {
      return _orderProductRepo.Select(productId,orderId);
    }

    public void Update(short productId, short orderId, short quantity)
    {
      _orderProductRepo.Update(productId, orderId, quantity);
    }
  }
}