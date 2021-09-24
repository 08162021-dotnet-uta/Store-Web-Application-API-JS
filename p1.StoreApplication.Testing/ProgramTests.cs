using Xunit;
using p1.StoreApplication.Models.EFModels;
using p1.StoreApplication.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p1.StoreApplication.Testing
{
  public class ProgramTests
  {
    /*readonly Program sut = new();

    [Theory]
    [InlineData(19.99, 39.99, 59.98)]
    [InlineData(13.37, 37.63, 51.00)]
    [InlineData(24.99, -25.99, -1.00)]

    public void Test_GetPriceSubtotal(decimal price1, decimal price2, decimal total)
    {
      List<Product> cart = new();
      Product p = new() { Price = price1 };
      Product q = new() { Price = price2 };
      cart.Add(p);
      cart.Add(q);
      decimal sum = sut.CalculateSubtotal(cart);
      Assert.True(sum == total);
    }

    [Theory]
    [InlineData(2, "1")]
    [InlineData(4, "4")]
    public void Test_ValidateDataPass(int options, string value)
    {
      Assert.True(sut.ValidateData(options, value));
    }

    [Theory]
    [InlineData(8, "chess")]
    [InlineData(4, "-4")]
    [InlineData(-4, "-4")]
    [InlineData(-4, "4")]
    [InlineData(6, "7")]
    [InlineData(5, "0")]
    public void Test_ValidateDataFail(int options, string value)
    {
      Assert.False(sut.ValidateData(options, value));
    }

    [Fact]
    public void Test_MakeOrderNullCustomer()
    {
      Customer customer = null;
      Store store = null;
      List<Product> cart = new();
      string result = sut.MakeOrder(customer, store, cart);
      Assert.True(result.Equals("ERROR: No customer selected"));
    }

    [Fact]
    public void Test_MakeOrderNullStore()
    {
      Customer customer = new();
      Store store = null;
      List<Product> cart = new();
      string result = sut.MakeOrder(customer, store, cart);
      Assert.True(result.Equals("ERROR: No store selected"));
    }

    [Fact]
    public void Test_MakeOrderEmptyCart()
    {
      Customer customer = new();
      Store store = new();
      List<Product> cart = new();
      string result = sut.MakeOrder(customer, store, cart);
      Assert.True(result.Equals("ERROR: Cart is empty"));
    }
      
    [Fact]
    public void Test_MakeOrderOverloadedCart()
    {
      Customer customer = new();
      Store store = new();
      List<Product> cart = new();
      for (int i = 0; i < 51; i++)
        cart.Add(new Product());
      string result = sut.MakeOrder(customer, store, cart);
      Assert.True(result.Equals("Sorry, this order is invalid. You cannot order more than 50 products in one order."));
    }
      
    [Fact]
    public void Test_MakeOrderOverpricedCart()
    {
      Customer customer = new();
      Store store = new();
      List<Product> cart = new();
      cart.Add(new Product() { Price = 555 });
      string result = sut.MakeOrder(customer, store, cart);
      Assert.True(result.Equals("Sorry, this order is invalid. Orders are limited to a total of $500."));
    }*/

    /*[Theory]
    [InlineData("48")]
    [InlineData("-32")]
    public void Test_TryParseTrue(string a)
    {
      int r;
      Assert.True(int.TryParse(a, out r));
    }
    [Theory]
    [InlineData("6f6")]
    public void Test_TryParseFalse(string a)
    {
      int r;
      Assert.False(int.TryParse(a, out r));
    }*/
  }
}
