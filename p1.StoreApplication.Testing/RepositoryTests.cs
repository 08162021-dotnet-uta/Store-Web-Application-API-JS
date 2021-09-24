using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p1.StoreApplication.Context;
using Microsoft.EntityFrameworkCore;
using p1.StoreApplication.Models.EFModels;
using p1.StoreApplication.Models.ViewModels;
using p1.StoreApplication.Logic.Repositories;

namespace p1.StoreApplication.Testing
{
  public class RepositoryTests
  {

    public DbContextOptions<StoreApplicationDBContext> Options { get; set; } = new DbContextOptionsBuilder<StoreApplicationDBContext>()
        .UseInMemoryDatabase(databaseName: "TestDb")
        .Options;

    [Fact]
    public async void Test_CustomerRightFormat()
    {
      using var context = new StoreApplicationDBContext(Options);
      context.Database.EnsureDeleted();
      context.Database.EnsureCreated();

      //Arrange
      Customer c = new();
      c.FName = "Jeffrey";
      c.LName = "Wright";
      context.Customers.Add(c);
      context.SaveChanges();

      CustomerV cv1 = new() { FirstName = "Jeffrey", LastName = "Wright" };

      CustomerRepository cr = new(context);

      //Act
      CustomerV cv2 = await cr.SelectAsync(cv1);

      //Assert
      Assert.Equal(cv1.FirstName, c.FName);
      Assert.Equal(cv1.LastName, c.LName);

    }

    [Fact]
    public async void Test_SelectStore()
    {
      using var context = new StoreApplicationDBContext(Options);
      context.Database.EnsureDeleted();
      context.Database.EnsureCreated();

      //Arrange
      Store s = new();
      s.Name = "Dollar Tree";
      s.City = "Gary";
      s.State = "IN";
      context.Stores.Add(s);
      context.SaveChanges();

      //Act
      StoreV myStore = new() { Name = "Dollar Tree", City = "Gary", State = "IN" };

      StoreRepository sr = new(context);
      StoreV store2 = await sr.SelectAsync(myStore);

      //Assert
      Assert.Equal(s.Name, myStore.Name);
      Assert.Equal(s.City, myStore.City);
      Assert.Equal(s.State, myStore.State);
    }
  }
}
