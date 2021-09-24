using System;
using System.Collections.Generic;
using Serilog;
using dm = p1.StoreApplication.Models.EFModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using p1.StoreApplication.Logic.Repositories;

namespace p1.StoreApplication.Client
{
  /// <summary>
  /// Defines the Program class
  /// </summary>
  public class Program
  {
    private static readonly StoreRepository _storeRepository = new();
    private static readonly CustomerRepository _customerRepository = new();
    private static readonly ProductRepository _productRepository = new();
    private static readonly OrderRepository _orderRepository = new();
    private static readonly OrderProductRepository _orderProductRepository = new();
    protected dm.Customer customer;
    protected dm.Store store;
    private static readonly List<dm.Product> cart = new();
    //This references a path that will hold the logs. It is stored within the AppData\Roaming folder.
    //private static readonly string _logPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Revature\dotnet-batch-2021-08-p0\StoreApplication\log.txt";
    /// <summary>
    /// Defines the main function
    /// </summary>
    /// <param name="args">String Args</param>
    static void Main(string[] args)
    {
      /*if(!File.Exists(_logPath))
      {
        Directory.CreateDirectory(_logPath.Substring(0, _logPath.LastIndexOf('\\')));
        File.CreateText(_logPath);
      }*/

      /*using (var context = new dm.StoreApplicationDBContext())
      {
        dm.Customer newCustomer = new dm.Customer();
        newCustomer.Name = "Wayne Wright";
        context.Customers.Add(newCustomer);
        context.SaveChanges();
        var customerList = context.Customers.FromSqlRaw<dm.Customer>("SELECT * FROM Customer.Customer").ToList();
        foreach (var customer in customerList)
        {
          Console.WriteLine(customer.Name);
        }
      }*/

      Log.Logger = new LoggerConfiguration().WriteTo.File("log.txt").CreateLogger();
      var p = new Program();
      Console.WriteLine("Welcome to my storefront!");
      p.DisplayMenu();
    }
    /// <summary>
    /// Displays the Menu
    /// </summary>
    private void DisplayMenu()
    {
      Log.Information("Method: Display Menu");
      Console.WriteLine("1: Login as customer");
      Console.WriteLine("2: Login as store");
      Console.WriteLine("Any other key: Log Out");
      Console.Write("Select an option: ");
      string value = Console.ReadLine();
      if(ValidateData(2, value))
      {
        Menu(int.Parse(value));
      }
    }
    /// <summary>
    /// Ensures that the value is valid.
    /// </summary>
    /// <param name="options">Number of options the user can choose</param>
    /// <param name="value">The value that is passed on</param>
    /// <returns></returns>
    public bool ValidateData(int options, string value)
    {
      Log.Information("Method: Validate Data");
      if (int.TryParse(value, out int option))
      {
        if (option > 0 && option <= options)
        {
          Log.Information("Data validated!");
          return true;
        }
        Log.Information($"Invalid data: {value} Out of bounds");
        return false;
      }
      Log.Information($"Invalid data: Cannot convert {value} to int");
      return false;
    }
    /// <summary>
    /// Selects the option in the menu
    /// </summary>
    /// <param name="option"></param>
    private void Menu(int option)
    {
      Log.Information("Method: Menu");
      switch (option)
      {
        case 1:
          Output<dm.Customer>(_customerRepository.Select());
          SelectCustomer(out customer);
          if (customer != null)
          {
            Console.WriteLine($"You have selected {customer}");
            DisplayMenuCust();
          }
          else
          {
            Console.WriteLine("INVALID INPUT");
            DisplayMenu();
          }
          break;
        case 2:
          Output<dm.Store>(_storeRepository.Select());
          SelectStore(out store);
          if (store != null)
          {
            Console.WriteLine($"You have selected {store}");
            DisplayMenuStore();
          }
          else
          {
            Console.WriteLine("INVALID INPUT");
            DisplayMenu();
          }
          break;
      }        
    }
    /// <summary>
    /// Output the list of objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    private static void Output<T>(List<T> data) where T : class
    {
      Log.Information("Method: Output");
      var i = 1;

      foreach (var item in data)
      {
        Console.WriteLine(i + ": " + item);
        i++;
      }
      Console.WriteLine("");
    }
    /// <summary>
    /// Selects a customer
    /// </summary>
    /// <param name="customer"></param>
    /// <returns></returns>
    private void SelectCustomer(out dm.Customer customer)
    {
      Log.Information("Method: Select Customer");

      //Access the customers from the customer repository
      var customers = (_customerRepository.Select());

      //Prompt to select a customer
      Console.Write("Select a Customer: ");

      //Prompt for user input to get a customer
      dm.Customer cust = null;
      string value = Console.ReadLine();
      if(ValidateData(customers.Count,value))
        cust = customers[int.Parse(value) - 1];

      //Return the customer of the customer input
      customer = cust;
    }
    /// <summary>
    /// Selects a store
    /// </summary>
    /// <param name="store"></param>
    /// <returns></returns>
    private void SelectStore(out dm.Store store)
    {
      Log.Information("Method: Select Store");

      //Access the stores from the store repository
      var stores = (_storeRepository.Select());

      //Prompt to select a store
      Console.Write("Select a Store: ");

      //Prompt for user input to get a store
      dm.Store st = null;
      string value = Console.ReadLine();
      if (ValidateData(stores.Count, value))
        st = stores[int.Parse(value) - 1];

      //Displays the customer selected
      Console.WriteLine(st);

      //Return the customer of the customer input
      store = st;
    }
    /// <summary>
    /// Displays the menu for the customer
    /// </summary>
    private void DisplayMenuCust()
    {
      Log.Information("Method: Display Customer Menu");
      Console.WriteLine("1: Select store");
      Console.WriteLine("2: View orders");
      Console.WriteLine("3: Log out");
      Console.Write("Select an option: ");
      string value = Console.ReadLine();
      if (ValidateData(3, value))
      {
        MenuCust(int.Parse(value));
      }
      else
      {
        Console.WriteLine("INVALID INPUT");
        DisplayMenuCust();
      }
    }
    private void MenuCust(int option)
    {
      switch (option)
      {
        case 1:
          Output<dm.Store>(_storeRepository.Select());
          SelectStore(out store);
          if (store != null)
          {
            Console.WriteLine($"You have selected {store}");
            DisplayShopping();
          }
          else
          {
            Console.WriteLine("INVALID INPUT");
            DisplayMenuCust();
          }
          break;
        case 2:
          OutputOrder<dm.StoreOrder>(customer);
          DisplayMenuCust();
          break;
        case 3:
          customer = null;
          DisplayMenu();
          break;
        default:
          DisplayMenuCust();
          break;
      }
    }
    private void DisplayMenuStore()
    {
      Log.Information("Method: Display Store Menu");
      Console.WriteLine("1: View orders");
      Console.WriteLine("2: Log out");
      Console.Write("Select an option: ");
      string value = Console.ReadLine();
      if (ValidateData(2, value))
      {
        MenuStore(int.Parse(value));
      }
      else
      {
        Console.WriteLine("INVALID INPUT");
        DisplayMenuStore();
      }
    }
    private void MenuStore(int option)
    {
      switch (option)
      {
        case 1:
          OutputOrder<dm.StoreOrder>(store);
          DisplayMenuStore();
          break;
        case 2:
          store = null;
          DisplayMenu();
          break;
        default:
          DisplayMenuStore();
          break;
      }
    }
    private void DisplayShopping()
    {
      Log.Information("Method: Display Customer Menu");
      Console.WriteLine("1: Add products to cart");
      Console.WriteLine("2: View cart");
      Console.WriteLine("3: Leave store and empty cart");
      Console.Write("Select an option: ");
      string value = Console.ReadLine();
      if (ValidateData(3, value))
      {
        MenuShopping(int.Parse(value));
      }
      else
      {
        Console.WriteLine("INVALID INPUT");
        DisplayShopping();
      }
    }
    private void MenuShopping(int option)
    {
      switch(option)
      {
        case 1:
          OutputProducts(store);
          dm.Product product;
          SelectProduct(store, out product);
          if(product != null)
          {
            cart.Add(product);
            Console.WriteLine($"{product.Name} added to cart");
          }
          else
            Console.WriteLine("INVALID INPUT");
          DisplayShopping();
          break;
        case 2:
          ViewCart();
          break;
        case 3:
          cart.Clear();
          Console.WriteLine("Cart Cleared");
          store = null;
          DisplayMenuCust();
          break;
        default:
          DisplayShopping();
          break;
      }
    }
    private static void OutputProducts(dm.Store store)
    {
      var data = _productRepository.Select(store);

      Log.Information("Method: Output");
      var i = 1;

      foreach (var item in data)
      {
        Console.WriteLine(i + ": " + item);
        i++;
      }
      Console.WriteLine("");
    }
    /// <summary>
    /// Selects a product and adds it to the cart
    /// </summary>
    /// <returns></returns>
    private void SelectProduct(dm.Store store, out dm.Product product)
    {
      Log.Information("Method: Select Product");

      //Access the products from the product repository
      var products = (_productRepository.Select(store));

      //Prompt to select a product
      Console.Write("Select a Product: ");

      dm.Product pt = null;
      string value = Console.ReadLine();
      if(ValidateData(products.Count, value))
        pt = products[int.Parse(value) - 1];

      //Return the product of the user input
      product = pt;
    }
    /// <summary>
    /// Views the cart
    /// </summary>
    private void ViewCart()
    {
      Log.Information("Method: View Cart");
      foreach (dm.Product p in cart)
        Console.WriteLine(p);
      Console.WriteLine($"Subtotal: ${String.Format("{0:0.00}", CalculateSubtotal(cart))}");
      DisplayCartMenu();
    }
    /// <summary>
    /// Calculates the subtotal based on the cart
    /// </summary>
    /// <returns>Subtotal decimal value</returns>
    public decimal CalculateSubtotal(List<dm.Product> productCart)
    {
      Log.Information("Method: Calculate subtotal");
      decimal subtotal = 0.00M;
      foreach (dm.Product p in productCart)
      {
        subtotal += p.Price;
      }
      return subtotal;
    }
    /// <summary>
    /// Displays the cart menu
    /// </summary>
    private void DisplayCartMenu()
    {
      Log.Information("Method: Display Cart Menu");
      Console.WriteLine("1: Empty cart");
      Console.WriteLine("2: Purchase");
      Console.WriteLine("3: Go Back");
      Console.Write("Select an option: ");
      string value = Console.ReadLine();
      if (ValidateData(3, value))
      {
        CartMenu(int.Parse(value));
      }
      else
      {
        Console.WriteLine("INVALID INPUT");
        DisplayCartMenu();
      }
    }
    /// <summary>
    /// Selects the option in the cart menu
    /// </summary>
    private void CartMenu(int option)
    {
      Log.Information("Method: Cart Menu");
      switch (option)
      {
        case 1:
          cart.Clear();
          Console.WriteLine("Cart Cleared");
          DisplayShopping();
          break;
        case 2:
          string orderSuccess = MakeOrder(customer, store, cart);
          if (orderSuccess.Equals("Your order has been processed. Have a nice day!"))
            DisplayMenuCust();
          else
            DisplayShopping();
          break;
        case 3:
          DisplayShopping();
          break;
        default:
          DisplayCartMenu();
          break;
      }
    }
    /// <summary>
    /// Creates a new order
    /// </summary>
    public string MakeOrder(dm.Customer cu, dm.Store st, List<dm.Product> ca)
    {
      Log.Information("Method: Make Order");
      string outputString;
      if (cu == null)
      {
        outputString = "ERROR: No customer selected";
        Console.WriteLine(outputString);
        return outputString;
      }
      else if (st == null)
      {
        outputString = "ERROR: No store selected";
        Console.WriteLine(outputString);
        return outputString;
      }
      else if (ca.Count == 0)
      {
        outputString = "ERROR: Cart is empty";
        Console.WriteLine(outputString);
        return outputString;
      }
      else if (ca.Count > 50)
      {
        outputString = "Sorry, this order is invalid. You cannot order more than 50 products in one order.";
      }
      else if (CalculateSubtotal(ca) > 500)
      {
        outputString = "Sorry, this order is invalid. Orders are limited to a total of $500.";
      }
      else
      {
        //Adds a new order to the store order
        _orderRepository.Insert(new dm.StoreOrder() { CustomerId = cu.CustomerId, StoreId = st.StoreId, OrderDate = DateTime.Now });
        //Sets the last order inserted
        dm.StoreOrder lastOrder = _orderRepository.SelectLastOrder();
        //Goes through all the products in the cart
        foreach (var product in ca)
        {
          short orderId = lastOrder.OrderId;
          short productId = product.ProductId;
          //Gets the current order product based on the composite key
          dm.OrderProduct orderProduct = _orderProductRepository.Select(product.ProductId, lastOrder.OrderId);
          //If the record does not exist, add a new record with a quantity of 1.
          if (orderProduct == null)
            _orderProductRepository.Insert(new dm.OrderProduct() { OrderId = orderId, ProductId = productId, Quantity = 1 });
          //If the record exists in the database, increment the quantity
          else
          {
            short newQuantity = (short)(orderProduct.Quantity + 1);
            _orderProductRepository.Update(productId, orderId, newQuantity);
          }
        }
        outputString = "Your order has been processed. Have a nice day!";
      }
      Console.WriteLine(outputString);
      cart.Clear();
      return outputString;
    }
    /// <summary>
    /// Output the list of orders based on the customer
    /// </summary>
    /// <typeparam name="Order"></typeparam>
    /// <param name="data"></param>
    private static void OutputOrder<StoreOrder>(dm.Customer customer)
    {
      Log.Information("Method: Output Order (Customer)");
      List<dm.StoreOrder> data = _orderRepository.Select(customer);
      if (data.Count == 0)
      {
        Console.WriteLine($"There are no orders made by {customer}.");
        return;
      }

      PrintOrders(data);
    }

    /// <summary>
    /// Output the list of orders based on the store
    /// </summary>
    /// <typeparam name="StoreOrder"></typeparam>
    /// <param name="store"></param>
    private static void OutputOrder<StoreOrder>(dm.Store store)
    {
      Log.Information("Method: Output Order (Store)");
      List<dm.StoreOrder> data = _orderRepository.Select(store);
      if (data.Count == 0)
      {
        Console.WriteLine($"There are no orders made at {store}.");
        return;
      }

      PrintOrders(data);
    }

    private static void PrintOrders(List<dm.StoreOrder> data)
    {
      Log.Information("Method: Print Orders");
      foreach (var item in data)
      {
        //Customer: (function that gets the customer name from the customerID in item)
        //Store: (function that gets the store name from the storeId in item)
        Console.WriteLine($"Customer: {_customerRepository.Select(item.CustomerId)}");
        Console.WriteLine($"Store: {_storeRepository.Select(item.StoreId)}");
        Console.WriteLine($"Order Date: {item.OrderDate}");
        //Products: (function that gets the list of products where within a nested foreach loop
        var productData = _productRepository.Select(item);
        Console.Write("Products: ");
        foreach (var pItem in productData)
        {
          Console.WriteLine(pItem + " | Quantity: " + _orderProductRepository.Select(pItem.ProductId,item.OrderId).Quantity);
        }
        Console.WriteLine("");
      }
    }
  }
}
