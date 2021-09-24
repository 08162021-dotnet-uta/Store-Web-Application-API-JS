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
  public class OrderProductController : Controller
  {
    private readonly IOrderProductRepository _orderProductRepo;
    private readonly IOrderRepository _orderRepo;
    private readonly ILogger<OrderProductController> _logger;
    public OrderProductController(IOrderProductRepository opr, IOrderRepository or, ILogger<OrderProductController> logger)
    {
      _orderProductRepo = opr;
      _orderRepo = or;
      _logger = logger;
    }
    // GET: OrderProductController
    public ActionResult Index()
    {
      return View();
    }
    // GET: OrderProductController/Create
    [HttpPost("makeOrderProduct")]
    public async Task<ActionResult<OrderProductV>> Create(OrderProductV c)
    {
      if (!ModelState.IsValid) return BadRequest();

      OrderProductV c1 = await _orderProductRepo.InsertAsync(c);
      if (c1 == null)
        return NotFound();
      return Created($"~orderProduct/{c1.OrderProductId}", c1);
    }
    // POST: OrderProductController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(IFormCollection collection)
    {
      try
      {
        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View();
      }
    }
    // GET: OrderProductController/Delete/5
    public ActionResult Delete(int id)
    {
      return View();
    }

    // POST: OrderProductController/Delete/5
    public ActionResult Delete(int id, IFormCollection collection)
    {
      try
      {
        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View();
      }
    }

    [HttpGet("order/{orderId}/products")]
    public async Task<List<OrderProductV>> OrderProduct(int orderId)
    {
      OrderV o = new() { OrderId = orderId };
      OrderV o1 = await _orderRepo.SelectAsync(o);
      Task<List<OrderProductV>> orderProducts = _orderProductRepo.SelectAsync(o1);
      _logger.LogInformation("\n\nThere was a problem in the OrderProductlist method");

      List<OrderProductV> orderProducts1 = await orderProducts;
      return orderProducts1;
    }
  }
}
