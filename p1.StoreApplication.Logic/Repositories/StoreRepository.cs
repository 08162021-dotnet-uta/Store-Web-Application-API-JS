using p1.StoreApplication.Logic.Interfaces;
using p1.StoreApplication.Models.EFModels;
using p1.StoreApplication.Models.ViewModels;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using p1.StoreApplication.Context;
using System.Threading.Tasks;

namespace p1.StoreApplication.Logic.Repositories
{
  public class StoreRepository : IRepository<Store>, IStoreRepository
  {
    private readonly List<Store> stores;
    public StoreRepository()
    {
      using var context = new StoreApplicationDBContext();
      stores = context.Stores.FromSqlRaw<Store>("SELECT * FROM Store.Store").ToList();
    }
    public StoreRepository(StoreApplicationDBContext context)
    {
      stores = context.Stores.FromSqlRaw<Store>("SELECT * FROM Store.Store").ToList();
    }
    public bool Delete()
    {
      throw new System.NotImplementedException();
    }

    public bool Insert(Store entry)
    {
      //_fileAdapter.WriteToFile<Store>(_path, new List<Store> { entry });
      stores.Add(entry);
      return true;
    }

    public List<Store> Select()
    {
      //return _fileAdapter.ReadFromFile<Store>(_path);
      return stores;
    }
    public Store Select(short id)
    {
      using var context = new StoreApplicationDBContext();
      return context.Stores.FromSqlRaw<Store>($"SELECT * FROM Store.Store WHERE StoreId = {id}").FirstOrDefault();
    }
    public bool Update()
    {
      throw new System.NotImplementedException();
    }

    public async Task<List<StoreV>> SelectAsync()
    {
      using var context = new StoreApplicationDBContext();
      List<Store> stores = await context.Stores.FromSqlRaw<Store>("SELECT * FROM Store.Store").ToListAsync();
      List<StoreV> vStores = new();
      foreach (Store s in stores)
      {
        vStores.Add(ModelMapper.ConvertToStoreV(s));
      }
      return vStores;
    }

    public async Task<StoreV> SelectAsync(StoreV s)
    {
      Store s1 = ModelMapper.ConvertToStoreEF(s);

      using var context = new StoreApplicationDBContext();
      Store s2 = await context.Stores.FromSqlRaw<Store>($"SELECT * FROM Store.Store WHERE StoreId = {s1.StoreId}").FirstOrDefaultAsync();
      if (s2 == null) return null;

      StoreV s3 = ModelMapper.ConvertToStoreV(s2);
      return s3;
    }
  }
}