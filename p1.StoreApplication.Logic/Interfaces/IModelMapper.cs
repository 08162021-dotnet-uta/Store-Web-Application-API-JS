using p1.StoreApplication.Models.EFModels;
using p1.StoreApplication.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p1.StoreApplication.Logic.Interfaces
{
  public interface IModelMapper
  {
    CustomerV ConvertToCustomerV(Customer c) { throw new NotImplementedException();  }
    Customer ConvertToCustomerEF(CustomerV c) { throw new NotImplementedException(); }
    OrderV ConvertToOrderV(StoreOrder o) { throw new NotImplementedException(); }
    StoreOrder ConvertToOrderEF(OrderV o) { throw new NotImplementedException(); }
    StoreV ConvertToStoreV(Store s) { throw new NotImplementedException(); }
    Store ConvertToStoreEF(StoreV s) { throw new NotImplementedException(); }
    ProductV ConvertToProductV(Product p) { throw new NotImplementedException(); }
    Product ConvertToProductEF(ProductV p) { throw new NotImplementedException(); }
    OrderProductV ConvertToOrderProductV(OrderProduct op) { throw new NotImplementedException(); }
    OrderProduct ConvertToOrderProductEF(OrderProductV op) { throw new NotImplementedException(); }
    InventoryV ConvertToInventoryV(StoreInventory i) { throw new NotImplementedException(); }
    StoreInventory ConvertToInventoryEF(InventoryV i) { throw new NotImplementedException(); }
  }
}
