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
  public class ProductController : Controller
  {
    private readonly IProductRepository _productRepo;
    private readonly IStoreRepository _storeRepo;
    private readonly ILogger<ProductController> _logger;
    public ProductController(IProductRepository pr, IStoreRepository sr, ILogger<ProductController> logger)
    {
      _productRepo = pr;
      _storeRepo = sr;
      _logger = logger;
    }
    // GET: ProductController
    public ActionResult Index()
    {
      return View();
    }
    // GET: ProductController/Details/5
    [HttpGet("{storeId}/Productlist")]
    public async Task<List<ProductV>> Details(int storeId)
    {
      StoreV s = new() { StoreId = storeId };
      StoreV s1 = await _storeRepo.SelectAsync(s);
      Task<List<ProductV>> products = _productRepo.SelectAsync(s1);
      _logger.LogInformation("\n\nThere was a problem in the Productlist method");

      List<ProductV> products1 = await products;
      return products1;
    }
    [HttpGet("selected/{productId}")]
    public async Task<ActionResult<ProductV>> Product(int productId)
    {
      if (!ModelState.IsValid) return BadRequest();

      ProductV p = new() { ProductId = productId };
      ProductV p1 = await _productRepo.SelectAsync(p);
      if (p1 == null)
        return NotFound();
      return Ok(p1);
    }
  }
}
