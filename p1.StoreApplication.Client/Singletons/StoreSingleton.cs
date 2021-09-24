using System.Collections.Generic;
using p1.StoreApplication.Models.EFModels;
using p1.StoreApplication.Logic.Repositories;

namespace p1.StoreApplication.Client.Singletons
{
  public class StoreSingleton
  {
    private static StoreSingleton _storeSingleton;
    private static readonly StoreRepository _storeRepo = new();
    public List<Store> Stores { get; private set; }
    public static StoreSingleton Instance
    {
      get
      {
        if (_storeSingleton == null)
        {
          _storeSingleton = new StoreSingleton();
        }

        return _storeSingleton;
      }
    }
    private StoreSingleton()
    {
      Stores = _storeRepo.Select();
    }

    public void Add(Store store)
    {
      _storeRepo.Insert(store);
      Stores = _storeRepo.Select();
    }
    public Store GetStore(short id)
    {
      return _storeRepo.Select(id);
    }
  }
}