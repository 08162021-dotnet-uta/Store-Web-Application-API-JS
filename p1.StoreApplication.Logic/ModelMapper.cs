using p1.StoreApplication.Logic.Interfaces;
using p1.StoreApplication.Models.EFModels;
using p1.StoreApplication.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p1.StoreApplication.Logic
{
  public class ModelMapper : IModelMapper
  {
    public static CustomerV ConvertToCustomerV(Customer c)
    {
      CustomerV viewC = new();
      viewC.CustomerId = c.CustomerId;
      viewC.FirstName = c.FName;
      viewC.LastName = c.LName;
      return viewC;
    }

    public static Customer ConvertToCustomerEF(CustomerV c)
    {
      Customer efC = new();
      efC.CustomerId = (short)c.CustomerId;
      efC.FName = c.FirstName;
      efC.LName = c.LastName;
      return efC;
    }

    public static OrderV ConvertToOrderV(StoreOrder o)
    {
      OrderV viewO = new();
      viewO.OrderId = o.OrderId;
      viewO.CustomerId = o.CustomerId;
      viewO.StoreId = o.StoreId;
      viewO.OrderDate = o.OrderDate;
      return viewO;
    }

    public static StoreOrder ConvertToOrderEF(OrderV o)
    {
      StoreOrder efO = new();
      efO.OrderId = (short)o.OrderId;
      efO.CustomerId = (short)o.CustomerId;
      efO.StoreId = (short)o.StoreId;
      efO.OrderDate = o.OrderDate;
      return efO;
    }

    public static StoreV ConvertToStoreV(Store s)
    {
      StoreV viewS = new();
      viewS.StoreId = s.StoreId;
      viewS.Name = s.Name;
      viewS.City = s.City;
      viewS.State = s.State;
      return viewS;
    }

    public static Store ConvertToStoreEF(StoreV s)
    {
      Store efS = new();
      efS.StoreId = (short)s.StoreId;
      efS.Name = s.Name;
      efS.City = s.City;
      efS.State = s.State;
      return efS;
    }

    public static ProductV ConvertToProductV(Product p)
    {
      ProductV viewP = new();
      viewP.ProductId = p.ProductId;
      viewP.Name = p.Name;
      viewP.Description = p.Description;
      viewP.Price = p.Price;
      viewP.Quantity = p.Quantity;
      return viewP;
    }

    public static Product ConvertToProductEF(ProductV p)
    {
      Product efP = new();
      efP.ProductId = (short)p.ProductId;
      efP.Name = p.Name;
      efP.Description = p.Description;
      efP.Price = p.Price;
      efP.Quantity = (short)p.Quantity;
      return efP;
    }

    public static OrderProductV ConvertToOrderProductV(OrderProduct op)
    {
      OrderProductV viewOp = new();
      viewOp.OrderProductId = op.OrderProductId;
      viewOp.OrderId = op.OrderId;
      viewOp.ProductId = op.ProductId;
      viewOp.Quantity = op.Quantity;
      return viewOp;
    }

    public static OrderProduct ConvertToOrderProductEF(OrderProductV op)
    {
      OrderProduct efOp = new();
      efOp.OrderProductId = op.OrderProductId;
      efOp.OrderId = (short)op.OrderId;
      efOp.ProductId = (short)op.ProductId;
      efOp.Quantity = op.Quantity;
      return efOp;
    }

    public static InventoryV ConvertToInventoryV(StoreInventory i)
    {
      InventoryV viewI = new();
      viewI.StoreInventoryId = i.StoreInventoryId;
      viewI.StoreId = i.StoreId;
      viewI.ProductId = i.ProductId;
      return viewI;
    }

    public static StoreInventory ConvertToInventoryEF(InventoryV i)
    {
      StoreInventory efI = new();
      efI.StoreInventoryId = i.StoreInventoryId;
      efI.StoreId = (short)i.StoreId;
      efI.ProductId = (short)i.ProductId;
      return efI;
    }
  }
}
