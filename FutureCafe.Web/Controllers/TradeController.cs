using FutureCafe.Business.Abstract;
using FutureCafe.Business.Dtos;
using FutureCafe.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace FutureCafe.Web.Controllers
{
  public class TradeController : Controller
  {
    IProductService _productService;
    IStudentService _studentService;
    ITradeService _tradeService;
    public TradeController(IProductService productService, IStudentService studentService, ITradeService tradeService)
    {
      _productService = productService;
      _studentService = studentService;
      _tradeService = tradeService;
    }

    public IActionResult Index()
    {
      return View();
    }

    [HttpGet]
    public async Task<IActionResult> CreateProductItem(string productBarcodNoInput)
    {
      var product = await _productService.GetAsync<Product>(e => e.ProductBarcodNo == productBarcodNoInput, "ProductPrice.Price");
      if (product.Data != null)
      {
        var salePrice = _productService.GetLastSalePrice(product.Data);

        if (salePrice.Success)
          return Json(new { success = true, productBarcodNo = product.Data.ProductBarcodNo, productName = product.Data.Name, productSalePrice = salePrice.Data });

        return Json(new { success = false, message = salePrice.Message });
      }
      else
      {
        // Product not found
        return Json(new { success = false, message = "Ürün veritabanında bulunamadı." });
      }
    }
    [HttpPost]
    public async Task<IActionResult> Pay(string studentCardNumber, decimal totalPrice, IEnumerable<ProductTradeDto> products)
    {
      var student = await _studentService.GetAsync<Student>(x => x.CardNumber == studentCardNumber, "StudentCredit.Credit");

      if (student.Data == null)
        return Json(new { success = false, message = "Öğrenci veritabanında bulunamıyor.", hideModal = false });

      var studentCredit = _studentService.GetLastCreditAmount(student.Data);

      if (totalPrice > studentCredit.Data)
        return Json(new { success = false, message = $"Kredi yeterli değil.Toplam miktar: ₺{totalPrice}, Kredi: ₺{studentCredit.Data}", hideModal = true });

      if (products == null)
        return Json(new { success = false, message = "Sepette ürün bulunmuyor!" });

      //pay success

      var newStudentCredit = studentCredit.Data - totalPrice;

      await _tradeService.AddAsync(studentCardNumber, products);
      await _tradeService.SaveAsync();

      _studentService.WithDrawMoneyFromStudent(student.Data, totalPrice);
      _studentService.Update(student.Data);
      await _studentService.SaveAsync();

      return Json(new { success = true, message = $"{student.Data.Name} kalan kredi: ₺{newStudentCredit}" });

    }

  }
}
