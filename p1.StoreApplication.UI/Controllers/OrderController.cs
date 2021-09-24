using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using p1.StoreApplication.Logic.Interfaces;
using p1.StoreApplication.Logic;
using Microsoft.Extensions.Logging;
using p1.StoreApplication.Models.ViewModels;

namespace p1.StoreApplication.UI.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class OrderController : Controller
  {
    private readonly IOrderRepository _orderRepo;
    private readonly IStoreRepository _storeRepo;
    private readonly ICustomerRepository _customerRepo;
    private readonly ILogger<OrderController> _logger;
    public OrderController(IOrderRepository or, IStoreRepository sr, ICustomerRepository cr, ILogger<OrderController> logger)
    {
      _orderRepo = or;
      _storeRepo = sr;
      _customerRepo = cr;
      _logger = logger;
    }
    // GET: OrderController
    public ActionResult Index()
    {
      return View();
    }
    // GET: OrderController/customer/5/Orderlist
    [HttpGet("customer/{firstName}/{lastName}/Orderlist")]
    public async Task<List<OrderV>> DetailsCustomer(string firstName, string lastName)
    {
      CustomerV c = new() { FirstName = firstName, LastName = lastName };
      CustomerV c1 = await _customerRepo.SelectAsync(c);
      Task<List<OrderV>> orders = _orderRepo.SelectAsync(c1);
      _logger.LogInformation("\n\nThere was a problem in the Orderlist method");

      List<OrderV> orders1 = await orders;
      return orders1;
    }
    // GET: OrderController/store/5/Orderlist
    [HttpGet("store/{storeId}/Orderlist")]
    public async Task<List<OrderV>> DetailsStore(int storeId)
    {
      StoreV s = new() { StoreId = storeId };
      StoreV s1 = await _storeRepo.SelectAsync(s);
      Task<List<OrderV>> orders = _orderRepo.SelectAsync(s1);
      _logger.LogInformation("\n\nThere was a problem in the Orderlist method");

      List<OrderV> orders1 = await orders;
      return orders1;
    }
    [HttpGet("selected/{orderId}")]
    public async Task<ActionResult<OrderV>> Order(int orderId)
    {
      if (!ModelState.IsValid) return BadRequest();

      OrderV o = new() { OrderId = orderId };
      OrderV o1 = await _orderRepo.SelectAsync(o);
      if (o1 == null)
        return NotFound();
      return Ok(o1);
    }
    [HttpGet("lastorder")]
    public async Task<ActionResult<OrderV>> LastOrder()
    {
      if (!ModelState.IsValid) return BadRequest();

      OrderV o = await _orderRepo.SelectAsyncLast();
      if (o == null)
        return NotFound();
      return Ok(o);
    }
    // GET: OrderController/neworder
    [HttpPost("neworder")]
    public async Task<ActionResult<OrderV>> Create(OrderV o)
    {
      o.OrderDate = DateTime.Now;
      if (!ModelState.IsValid) return BadRequest();

      OrderV o1 = await _orderRepo.InsertAsync(o);
      if (o1 == null)
        return NotFound();
      return Created($"~order/{o1.OrderId}", o1);
    }
  }
}
