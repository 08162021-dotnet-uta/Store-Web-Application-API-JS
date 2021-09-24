using System.Collections.Generic;

namespace p1.StoreApplication.Logic.Interfaces
{
  public interface IRepository<T> where T : class
  {
    bool Insert(T entry);
    bool Update();
    List<T> Select();
    bool Delete();
  }
}