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
  public class StoreController : Controller
  {
    private readonly IStoreRepository _storeRepo;
    private readonly ILogger<StoreController> _logger;
    public StoreController(IStoreRepository sr, ILogger<StoreController> logger)
    {
      _storeRepo = sr;
      _logger = logger;
    }
    // GET: StoreController
    public ActionResult Index()
    {
      return View();
    }
    // GET: StoreController/Details/5
    [HttpGet("Storelist")]
    public async Task<List<StoreV>> Details()
    {
      Task<List<StoreV>> stores = _storeRepo.SelectAsync();
      _logger.LogInformation("\n\nThere was a problem in the Storelist method");

      List<StoreV> stores1 = await stores;
      return stores1;
    }
    [HttpGet("selected/{storeId}")]
    public async Task<ActionResult<StoreV>> Store(int storeId)
    {
      if (!ModelState.IsValid) return BadRequest();

      StoreV s = new() { StoreId = storeId };
      StoreV s1 = await _storeRepo.SelectAsync(s);
      if (s1 == null)
        return NotFound();
      return Ok(s1);
    }
  }
}
