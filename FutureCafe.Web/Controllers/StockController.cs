using FutureCafe.Business.Abstract;
using FutureCafe.Business.Constants;
using FutureCafe.Business.Dtos;
using FutureCafe.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace FutureCafe.Web.Controllers
{
  public class StockController : Controller
  {
    private readonly IStockService _stockService;
    private readonly IProductService _productService;
    private readonly IToastNotification _toastNotification;
    public StockController(IStockService stockService, IProductService productService, IToastNotification toastNotification)
    {
      _stockService = stockService;
      _productService = productService;
      _toastNotification = toastNotification;
    }

    public async Task<IActionResult> Index()
    {
      var stocks = await _stockService.GetListAsync<StockViewDto>(null, null, "Product");

      var latestStocks = stocks.Data
    .GroupBy(x => x.ProductProductBarcodNo)
    .Select(group => group.OrderByDescending(y => y.CreatedDate).FirstOrDefault())
    .Where(s => s != null && s.ProductCount > 0)
    .ToList();

      return View(latestStocks);
    }

    public IActionResult Create()
    {
      return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(StockCreateEditDto stockCreateEditDto)
    {
      var product = await _productService.GetAsync<Product>(e => e.ProductBarcodNo == stockCreateEditDto.ProductProductBarcodNo);

      if (stockCreateEditDto.ProductProductBarcodNo == null)
      {
        _toastNotification.AddErrorToastMessage(Messages.StockProductNoInput);
        return View(stockCreateEditDto);
      }
      if (stockCreateEditDto.ProductCountToRegister == 0)
      {
        _toastNotification.AddErrorToastMessage(Messages.StockProductCountRegisterNoInput);
        return View(stockCreateEditDto);
      }

      if (stockCreateEditDto.ProductCount == 0 && stockCreateEditDto.ProductCountToRegister < 0)
      {
        _toastNotification.AddErrorToastMessage(Messages.StockCountZero);
        return View(stockCreateEditDto);
      }

      var newStock = new Stock
      {
        ProductCount = stockCreateEditDto.ProductCount + stockCreateEditDto.ProductCountToRegister < 0 ? 0 : stockCreateEditDto.ProductCount + stockCreateEditDto.ProductCountToRegister,
        Product = product.Data
      };
      var addResult = await _stockService.AddAsync(newStock);
      var saveResult = await _stockService.SaveAsync();

      if (addResult.Success == false || saveResult.Success == false)
        _toastNotification.AddErrorToastMessage(Messages.DataNotUpdated);
      else
        _toastNotification.AddSuccessToastMessage(Messages.DataUpdated);

      return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetProduct(string ProductBarcodeNo)
    {
      var product = await _productService.GetAsync<Product>(e => e.ProductBarcodNo == ProductBarcodeNo);
      var currentProductCount = GetLatestProductCount(ProductBarcodeNo).Result;

      if (product.Data != null)
      {
        try
        {
          return Json(new { success = true, productBarcodeNo = product.Data.ProductBarcodNo, productName = product.Data.Name, productCount = currentProductCount });
        }
        catch (Exception)
        {
          return Json(new { success = false });
        }
      }
      else
      {
        return Json(new { success = false });
      }
    }

    private async Task<int> GetLatestProductCount(string ProductBarcodeNo)
    {
      var product = await _productService.GetAsync<Product>(e => e.ProductBarcodNo == ProductBarcodeNo);
      var productStocks = await _stockService.GetListAsync<Stock>(x => x.ProductId == product.Data.Id, null, "Product");
      var currentProductCount = productStocks.Data.Count() > 0 ? productStocks.Data.OrderByDescending(s => s.CreatedDate).FirstOrDefault().ProductCount : 0;

      return currentProductCount;
    }
  }
}
