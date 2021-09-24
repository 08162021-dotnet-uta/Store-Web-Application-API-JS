using p1.StoreApplication.Logic.Interfaces;
using p1.StoreApplication.Models.EFModels;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using p1.StoreApplication.Context;
using p1.StoreApplication.Models.ViewModels;
using System.Threading.Tasks;

namespace p1.StoreApplication.Logic.Repositories
{
  public class ProductRepository : IRepository<Product>, IProductRepository
  {
    private readonly List<Product> products;
    public ProductRepository()
    {
      using var context = new StoreApplicationDBContext();
      products = context.Products.FromSqlRaw<Product>("SELECT * FROM Store.Product").ToList();
    }
    public bool Delete()
    {
      throw new System.NotImplementedException();
    }

    public bool Insert(Product entry)
    {
      products.Add(entry);
      return true;
    }

    public List<Product> Select()
    {
      return products;
    }

    public List<Product> Select(Store store)
    {
      using var context = new StoreApplicationDBContext();
      return context.Products.FromSqlRaw<Product>($"SELECT * FROM Store.Product WHERE ProductId IN (SELECT ProductId from Store.StoreInventory WHERE StoreId = {store.StoreId})").ToList();
    }

    public List<Product> Select(StoreOrder order)
    {
      using var context = new StoreApplicationDBContext();
      return context.Products.FromSqlRaw<Product>($"SELECT p.ProductId, [Name], [Description], Price, Quantity FROM Store.Product AS p RIGHT JOIN Store.OrderProduct AS op ON p.ProductId = op.ProductId WHERE OrderId = {order.OrderId}").ToList();
    }

    public bool Update()
    {
      throw new System.NotImplementedException();
    }

    public async Task<ProductV> SelectAsync(ProductV p)
    {
      Product p1 = ModelMapper.ConvertToProductEF(p);

      using var context = new StoreApplicationDBContext();
      Product p2 = await context.Products.FromSqlRaw<Product>($"SELECT * FROM Store.Product WHERE ProductId = {p1.ProductId}").FirstOrDefaultAsync();
      if (p2 == null) return null;

      ProductV p3 = ModelMapper.ConvertToProductV(p2);
      return p3;
    }

    public async Task<List<ProductV>> SelectAsync(StoreV s)
    {
      Store s1 = ModelMapper.ConvertToStoreEF(s);

      using var context = new StoreApplicationDBContext();
      List<Product> products = await context.Products.FromSqlRaw<Product>($"SELECT * FROM Store.Product WHERE ProductId IN (SELECT ProductId from Store.StoreInventory WHERE StoreId = {s1.StoreId})").ToListAsync();
      List<ProductV> vProducts = new();
      foreach (Product v in products)
      {
        vProducts.Add(ModelMapper.ConvertToProductV(v));
      }
      return vProducts;
    }
  }
}