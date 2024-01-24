using System.ComponentModel;

namespace FutureCafe.Business.Dtos
{
  public class StockReportDto
  {
    [DisplayName("Ürün Adı")]
    public string ProductName { get; set; }

    [DisplayName("Ürün Sayısı")]
    public int ProductCount { get; set; }

    [DisplayName("Değişim Tarihi")]
    public DateTime CreatedDate { get; set; }
  }
}
