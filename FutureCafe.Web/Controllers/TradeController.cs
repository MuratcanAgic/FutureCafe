using FutureCafe.Business.Abstract;
using FutureCafe.Business.Dtos;
using FutureCafe.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace FutureCafe.Web.Controllers
{
  public class TradeController : Controller
  {
    private readonly IProductService _productService;
    private readonly IStudentService _studentService;
    private readonly ITradeService _tradeService;
    private readonly IStockService _stockService;

    public TradeController(IProductService productService, IStudentService studentService, ITradeService tradeService, IStockService stockService)
    {
      _productService = productService;
      _studentService = studentService;
      _tradeService = tradeService;
      _stockService = stockService;

    }

    [Authorize(Roles = "Admin,Kantinci")]
    public IActionResult Index()
    {
      return View();
    }

    [Authorize(Roles = "Admin,Kantinci")]
    [HttpGet]
    public async Task<IActionResult> CreateProductItem(string productBarcodNoInput)
    {
      var product = await _productService.GetAsync<Product>(e => e.ProductBarcodNo == productBarcodNoInput, "ProductPrice.Price");
      if (product.Data != null)
      {
        var salePrice = _productService.GetLastSalePrice(product.Data);

        if (salePrice.Success)
          return Json(new { success = true, productBarcodNo = product.Data.ProductBarcodNo, productName = product.Data.Name, productSalePrice = salePrice.Data, productImage = product.Data.ImageUrl });

        return Json(new { success = false, message = salePrice.Message });
      }
      else
      {
        // Product not found
        return Json(new { success = false, message = "Ürün veritabanında bulunamadı." });
      }
    }
    [Authorize(Roles = "Admin,Kantinci")]
    [HttpPost]
    public async Task<IActionResult> Pay(string studentCardNumber, string totalPrice, IEnumerable<ProductTradeDto> products)
    {
      decimal totalPriceDecimal;
      decimal.TryParse(totalPrice, NumberStyles.Any, CultureInfo.InvariantCulture, out totalPriceDecimal);

      var student = await _studentService.GetAsync<Student>(x => x.CardNumber == studentCardNumber, "StudentCredit.Credit");

      if (student.Data == null)
        return Json(new { success = false, message = "Öğrenci veritabanında bulunamıyor.", hideModal = false });

      var studentCredit = _studentService.GetLastCreditAmount(student.Data);

      if (totalPriceDecimal > studentCredit.Data)
        return Json(new { success = false, message = $"Kredi yeterli değil.Toplam miktar: ₺{totalPriceDecimal}, Kredi: ₺{studentCredit.Data}", hideModal = true });

      if (products == null)
        return Json(new { success = false, message = "Sepette ürün bulunmuyor!" });

      var bannedProducts = await _studentService.CheckBannedProducts(products, studentCardNumber);

      if (bannedProducts != null && bannedProducts.Message != "")
        return Json(new { success = false, message = bannedProducts.Message });

      //pay success

      await _stockService.RemoveFromStockAsync(products);
      await _stockService.SaveAsync();

      var newStudentCredit = studentCredit.Data - totalPriceDecimal;

      await _tradeService.AddAsync(studentCardNumber, products);
      await _tradeService.SaveAsync();

      _studentService.WithDrawMoneyFromStudent(student.Data, totalPriceDecimal);
      _studentService.Update(student.Data);
      await _studentService.SaveAsync();

      return Json(new { success = true, message = $"{student.Data.Name} kalan kredi: ₺{newStudentCredit}" });

    }

  }
}
