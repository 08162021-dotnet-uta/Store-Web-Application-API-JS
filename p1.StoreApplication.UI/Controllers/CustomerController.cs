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
  public class CustomerController : Controller
  {
    private readonly ICustomerRepository _customerRepo;
    private readonly ILogger<CustomerController> _logger;
    public CustomerController(ICustomerRepository cr, ILogger<CustomerController> logger)
    {
      _customerRepo = cr;
      _logger = logger;
    }
    // GET: CustomerController
    public ActionResult Index()
    {
      return View();
    }
    // GET: CustomerController/Details/5
    [HttpGet("Customerlist")]
    public async Task<List<CustomerV>> Details()
    {
      Task<List<CustomerV>> customers = _customerRepo.SelectAsync();
      _logger.LogInformation("\n\nThere was a problem in the Customerlist method");

      List<CustomerV> customers1 = await customers;
      return customers1;
    }
    // GET: CustomerController/Create
    [HttpPost("register")]
    public async Task<ActionResult<CustomerV>> Create(CustomerV c)
    {
      if (!ModelState.IsValid) return BadRequest();

      CustomerV c1 = await _customerRepo.InsertAsync(c);
      if (c1 == null)
        return NotFound();
      return Created($"~customer/{c1.CustomerId}", c1);
    }
    // POST: CustomerController/Create
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
    // GET: CustomerController/Delete/5
    public ActionResult Delete(int id)
    {
      return View();
    }

    // POST: CustomerController/Delete/5
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

    [HttpGet("login/{fname}/{lname}")]
    public async Task<ActionResult<CustomerV>> Login(string fname, string lname)
    {
      if (!ModelState.IsValid) return BadRequest();

      CustomerV c = new() { FirstName = fname, LastName = lname };
      CustomerV c1 = await _customerRepo.SelectAsync(c);
      if (c1 == null)
        return NotFound();
      return Ok(c1);
    }
  }
}
