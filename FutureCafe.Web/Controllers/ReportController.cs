using FutureCafe.Business.Abstract;
using FutureCafe.Business.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FutureCafe.Web.Controllers
{
  public class ReportController : Controller
  {
    private readonly ITradeService _tradeService;
    private readonly IStudentService _studentService;
    private readonly IProductService _productService;
    private readonly IStockService _stockService;
    public ReportController(ITradeService tradeService, IStudentService studentService, IProductService productService, IStockService stockService)
    {
      _tradeService = tradeService;
      _studentService = studentService;
      _productService = productService;
      _stockService = stockService;
    }

    [Authorize(Roles = "Admin")]
    public IActionResult TradeReport()
    {
      return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> TradeReport(DateTime startDate, DateTime endDate)
    {
      if (startDate == DateTime.MinValue || endDate == DateTime.MinValue)
        return View();

      var trades = await _tradeService.GetListAsync<TradeReportDto>(
             x => DateTime.Compare(startDate, x.CreatedDate) <= 0 && DateTime.Compare(endDate, x.CreatedDate) >= 0,
             null,
             "Student,TradeProduct.Product");

      foreach (var trade in trades.Data)
      {
        trade.TotalPrice = trade.TradeProduct.Sum(x => x.ProductCount * x.SalePriceSnap);
      }

      return View(trades.Data);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult CreditReport()
    {
      return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreditReport(string studentCardNumber)
    {
      if (studentCardNumber == null)
        return View();

      var student = await _studentService.GetAsync<StudentCreditReportDto>(x => x.CardNumber == studentCardNumber, "StudentCredit.Credit");

      if (student == null || student.Success == false)
        return View();

      student.Data.StudentCredit.OrderBy(x => x.CreatedDate);

      return View(student.Data);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult ProductPriceReport()
    {
      return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> ProductPriceReport(string productBarcodeNo)
    {
      if (productBarcodeNo == null)
        return View();

      var product = await _productService.GetAsync<ProductPriceReportDto>(x => x.ProductBarcodNo == productBarcodeNo, "ProductPrice.Price");

      if (product == null || product.Success == false)
        return View();

      product.Data.ProductPrice.OrderBy(x => x.CreatedDate);

      return View(product.Data);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult StockReport()
    {
      return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> StockReport(string productBarcodeNo)
    {
      if (productBarcodeNo == null)
        return View();

      var stock = await _stockService.GetListAsync<StockReportDto>(x => x.Product.ProductBarcodNo == productBarcodeNo, null, "Product");

      if (stock == null || stock.Success == false)
        return View();

      //product.Data.ProductPrice.OrderBy(x => x.CreatedDate);

      return View(stock.Data);
    }
  }
}
