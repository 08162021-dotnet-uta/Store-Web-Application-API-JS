using p1.StoreApplication.Models.EFModels;
using p1.StoreApplication.Models.ViewModels;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using p1.StoreApplication.Context;
using p1.StoreApplication.Logic.Interfaces;
using System.Threading.Tasks;

namespace p1.StoreApplication.Logic.Repositories
{
  public class CustomerRepository : IRepository<Customer>, ICustomerRepository
  {
    private readonly List<Customer> customers;
    public CustomerRepository()
    {
      using var context = new StoreApplicationDBContext();
      customers = context.Customers.FromSqlRaw<Customer>("SELECT * FROM Customer.Customer").ToList();
    }
    public CustomerRepository(StoreApplicationDBContext context)
    {
      customers = context.Customers.FromSqlRaw<Customer>("SELECT * FROM Customer.Customer").ToList();
    }
    public bool Delete()
    {
      throw new System.NotImplementedException();
    }

    public bool Insert(Customer entry)
    {
      customers.Add(entry);
      return true;
    }

    public List<Customer> Select()
    {
      return customers;
    }

    public Customer Select(short id)
    {
      using var context = new StoreApplicationDBContext();
      return context.Customers.FromSqlRaw<Customer>($"SELECT * FROM Customer.Customer WHERE CustomerId = {id}").FirstOrDefault();
    }

    public bool Update()
    {
      throw new System.NotImplementedException();
    }

    public async Task<CustomerV> SelectAsync(CustomerV c)
    {
      Customer c1 = ModelMapper.ConvertToCustomerEF(c);

      using var context = new StoreApplicationDBContext();
      Customer c2 = await context.Customers.FromSqlRaw<Customer>($"SELECT * FROM Customer.Customer WHERE FName = '{c1.FName}' AND LName = '{c1.LName}'").FirstOrDefaultAsync();
      if (c2 == null) return null;

      CustomerV c3 = ModelMapper.ConvertToCustomerV(c2);
      return c3;
    }

    public async Task<CustomerV> InsertAsync(CustomerV c)
    {
      Customer c1 = ModelMapper.ConvertToCustomerEF(c);
      using var context = new StoreApplicationDBContext();
      int c2 = await context.Database.ExecuteSqlRawAsync($"INSERT INTO Customer.Customer (FName, LName) VALUES ('{c1.FName}', '{c1.LName}')");
      if (c2 != 1) return null;
      return await SelectAsync(c);
    }

    public async Task<List<CustomerV>> SelectAsync()
    {
      using var context = new StoreApplicationDBContext();
      List<Customer> customers = await context.Customers.FromSqlRaw<Customer>("SELECT * FROM Customer.Customer").ToListAsync();
      List<CustomerV> vCustomers = new();
      foreach (Customer c in customers)
      {
        vCustomers.Add(ModelMapper.ConvertToCustomerV(c));
      }
      return vCustomers;
    }
  }
}