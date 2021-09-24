using p1.StoreApplication.Logic.Interfaces;
using p1.StoreApplication.Models.EFModels;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using p1.StoreApplication.Context;
using System.Threading.Tasks;
using p1.StoreApplication.Models.ViewModels;

namespace p1.StoreApplication.Logic.Repositories
{
  public class OrderRepository : IRepository<StoreOrder>, IOrderRepository
  {
    private readonly List<StoreOrder> orders;
    public OrderRepository()
    {
      orders = new List<StoreOrder>(){};
    }
    public bool Delete()
    {
      throw new System.NotImplementedException();
    }

    public bool Insert(StoreOrder entry)
    {
      using var context = new StoreApplicationDBContext();
      context.StoreOrders.Add(entry);
      context.SaveChanges();
      return true;
    }

    public List<StoreOrder> Select()
    {
      return orders;
    }

    public List<StoreOrder> Select(Store store)
    {
      using var context = new StoreApplicationDBContext();
      return context.StoreOrders.Where(s => s.StoreId == store.StoreId).ToList();
    }

    public List<StoreOrder> Select(Customer customer)
    {
      using var context = new StoreApplicationDBContext();
      return context.StoreOrders.FromSqlRaw<StoreOrder>(
      $"SELECT * FROM Store.StoreOrder AS o WHERE o.CustomerId = {customer.CustomerId}; "
      ).ToList();
    }
    /// <summary>
    /// Selects the latest order from the StoreOrder table by order date
    /// </summary>
    /// <returns></returns>
    public StoreOrder SelectLastOrder()
    {
      using var context = new StoreApplicationDBContext();
      return context.StoreOrders.FromSqlRaw<StoreOrder>(
      $"SELECT * FROM Store.StoreOrder"
      ).OrderBy(s => s.OrderDate).LastOrDefault();
    }

    public bool Update()
    {
      throw new System.NotImplementedException();
    }
    public async Task<OrderV> SelectAsync(OrderV o)
    {
      StoreOrder o1 = ModelMapper.ConvertToOrderEF(o);

      using var context = new StoreApplicationDBContext();
      StoreOrder o2 = await context.StoreOrders.FromSqlRaw<StoreOrder>($"SELECT * FROM Store.StoreOrder WHERE OrderId = {o1.OrderId}").FirstOrDefaultAsync();
      if (o2 == null) return null;

      OrderV o3 = ModelMapper.ConvertToOrderV(o2);
      return o3;
    }
    public async Task<OrderV> SelectAsyncLast()
    {
      using var context = new StoreApplicationDBContext();
      StoreOrder o = await context.StoreOrders.FromSqlRaw<StoreOrder>("SELECT * FROM Store.StoreOrder").OrderBy(s => s.OrderDate).LastOrDefaultAsync();
      if (o == null) return null;

      OrderV viewO = ModelMapper.ConvertToOrderV(o);
      return viewO;
    }
    public async Task<List<OrderV>> SelectAsync(CustomerV c)
    {
      Customer c1 = ModelMapper.ConvertToCustomerEF(c);

      using var context = new StoreApplicationDBContext();
      List<StoreOrder> orders = await context.StoreOrders.FromSqlRaw<StoreOrder>($"SELECT * FROM Store.StoreOrder WHERE CustomerId = {c1.CustomerId}").ToListAsync();
      List<OrderV> vOrders = new();
      foreach (StoreOrder v in orders)
      {
        vOrders.Add(ModelMapper.ConvertToOrderV(v));
      }
      return vOrders;
    }
    public async Task<List<OrderV>> SelectAsync(StoreV s)
    {
      Store s1 = ModelMapper.ConvertToStoreEF(s);

      using var context = new StoreApplicationDBContext();
      List<StoreOrder> orders = await context.StoreOrders.FromSqlRaw<StoreOrder>($"SELECT * FROM Store.StoreOrder WHERE StoreId = {s1.StoreId}").ToListAsync();
      List<OrderV> vOrders = new();
      foreach (StoreOrder v in orders)
      {
        vOrders.Add(ModelMapper.ConvertToOrderV(v));
      }
      return vOrders;
    }
    public async Task<OrderV> InsertAsync(OrderV o)
    {
      StoreOrder o1 = ModelMapper.ConvertToOrderEF(o);
      using var context = new StoreApplicationDBContext();
      int o2 = await context.Database.ExecuteSqlRawAsync($"INSERT INTO Store.StoreOrder (CustomerId, StoreId, OrderDate) VALUES ({o1.CustomerId}, {o1.StoreId}, {o1.OrderDate})");
      if (o2 != 1) return null;
      return await SelectAsync(o);
    }
  }
}