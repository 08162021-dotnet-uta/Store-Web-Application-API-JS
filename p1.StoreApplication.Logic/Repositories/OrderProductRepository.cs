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
  public class OrderProductRepository : IRepository<OrderProduct>, IOrderProductRepository
  {
    private readonly List<OrderProduct> orderProducts;
    public OrderProductRepository()
    {
      orderProducts = new List<OrderProduct>(){};
    }
    public bool Delete()
    {
      throw new System.NotImplementedException();
    }

    public bool Insert(OrderProduct entry)
    {
      using var context = new StoreApplicationDBContext();
      context.OrderProducts.Add(entry);
      context.SaveChanges();
      return true;
    }

    public List<OrderProduct> Select()
    {
      return orderProducts;
    }

    public OrderProduct Select(short productId, short orderId)
    {
      using var context = new StoreApplicationDBContext();
      return context.OrderProducts.FromSqlRaw<OrderProduct>(
      $"SELECT * FROM Store.OrderProduct WHERE ProductId = {productId} AND OrderId = {orderId}"
      ).FirstOrDefault();
    }

    public bool Update()
    {
      throw new System.NotImplementedException();
    }
    public bool Update(short productId, short orderId, short quantity)
    {
      using var context = new StoreApplicationDBContext();
      OrderProduct orderProduct = context.OrderProducts.Where(s => s.OrderId == orderId).Where(s => s.ProductId == productId).First<OrderProduct>();
      orderProduct.Quantity = quantity;
      context.SaveChanges();
      return true;
    }
    public async Task<List<OrderProductV>> SelectAsync()
    {
      using var context = new StoreApplicationDBContext();
      List<OrderProduct> orderProducts = await context.OrderProducts.FromSqlRaw<OrderProduct>("SELECT * FROM Store.OrderProduct").ToListAsync();
      List<OrderProductV> vOrderProducts = new();
      foreach (OrderProduct op in orderProducts)
      {
        vOrderProducts.Add(ModelMapper.ConvertToOrderProductV(op));
      }
      return vOrderProducts;
    }
    public async Task<OrderProductV> SelectAsync(OrderProductV op)
    {
      OrderProduct op1 = ModelMapper.ConvertToOrderProductEF(op);

      using var context = new StoreApplicationDBContext();
      OrderProduct op2 = await context.OrderProducts.FromSqlRaw<OrderProduct>($"SELECT * FROM Store.OrderProduct WHERE OrderProductId = {op1.OrderProductId}").FirstOrDefaultAsync();
      if (op2 == null) return null;

      OrderProductV op3 = ModelMapper.ConvertToOrderProductV(op2);
      return op3;
    }
    public async Task<List<OrderProductV>> SelectAsync(OrderV o)
    {
      StoreOrder o1 = ModelMapper.ConvertToOrderEF(o);

      using var context = new StoreApplicationDBContext();
      List<OrderProduct> orderProducts = await context.OrderProducts.FromSqlRaw<OrderProduct>($"SELECT * FROM Store.OrderProduct WHERE OrderId = {o1.OrderId}").ToListAsync();
      List<OrderProductV> vOrderProducts = new();
      foreach (OrderProduct v in orderProducts)
      {
        vOrderProducts.Add(ModelMapper.ConvertToOrderProductV(v));
      }
      return vOrderProducts;
    }
    public async Task<OrderProductV> InsertAsync(OrderProductV op)
    {
      OrderProduct op1 = ModelMapper.ConvertToOrderProductEF(op);
      using var context = new StoreApplicationDBContext();
      int op2 = await context.Database.ExecuteSqlRawAsync($"INSERT INTO Store.OrderProduct (OrderId, ProductId, Quantity) VALUES ({op1.OrderId}, {op1.ProductId}, {op1.Quantity})");
      if (op2 != 1) return null;
      return await SelectAsync(op);
    }
  }
}