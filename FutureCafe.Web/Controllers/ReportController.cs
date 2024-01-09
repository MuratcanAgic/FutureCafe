using FutureCafe.Business.Abstract;
using FutureCafe.Business.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FutureCafe.Web.Controllers
{
  public class ReportController : Controller
  {
    private readonly ITradeService _tradeService;

    public ReportController(ITradeService tradeService)
    {
      _tradeService = tradeService;
    }

    public IActionResult TradeReport()
    {
      return View();
    }

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
  }
}
