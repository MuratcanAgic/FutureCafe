using System.ComponentModel;

namespace FutureCafe.Business.Dtos
{
  public class TradeReportDto
  {
    [DisplayName("Öğrenci")]
    public int StudentId { get; set; }

    [DisplayName("Öğrenci Adı Soyadı")]
    public string StudentNameSurname { get; set; }

    [DisplayName("Alışveriş Tarihi")]
    public DateTime CreatedDate { get; set; }

    public List<TradeProductReportDto> TradeProduct { get; set; }

    public decimal? TotalPrice { get; set; }
  }

  public class TradeProductReportDto
  {
    [DisplayName("Ürün Sayısı")]
    public int ProductCount { get; set; }
    [DisplayName("Ürün Adı")]
    public string ProductName { get; set; }

    public decimal? SalePriceSnap { get; set; }
  }
}
