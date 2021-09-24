using p1.StoreApplication.Logic.Interfaces;
using p1.StoreApplication.Models.EFModels;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using p1.StoreApplication.Context;

namespace p1.StoreApplication.Logic.Repositories
{
  public class InventoryRepository : IRepository<StoreInventory>, IInventoryRepository
  {
    private readonly List<StoreInventory> inventories;
    public InventoryRepository()
    {
      using var context = new StoreApplicationDBContext();
      inventories = context.StoreInventories.FromSqlRaw<StoreInventory>("SELECT * FROM Store.StoreInventory").ToList();
    }
    public bool Delete()
    {
      throw new System.NotImplementedException();
    }

    public bool Insert(StoreInventory entry)
    {
      //_fileAdapter.WriteToFile<Store>(_path, new List<Store> { entry });
      using var context = new StoreApplicationDBContext();
      context.StoreInventories.Add(entry);
      context.SaveChanges();
      return true;
    }

    public List<StoreInventory> Select()
    {
      //return _fileAdapter.ReadFromFile<Store>(_path);
      return inventories;
    }

    public bool Update()
    {
      throw new System.NotImplementedException();
    }
    public bool Update(short productId, short storeId, short quantity)
    {
      using var context = new StoreApplicationDBContext();
      StoreInventory storeInventory = context.StoreInventories.Where(s => s.StoreId == storeId).Where(s => s.ProductId == productId).First<StoreInventory>();
      //storeInventory.Quantity = quantity;
      context.SaveChanges();
      return true;
    }
  }
}