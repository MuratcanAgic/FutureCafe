using System.ComponentModel;

namespace FutureCafe.Business.Dtos
{
  public class StockViewDto
  {
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; }

    [DisplayName("Ürün Sayısı")]
    public int ProductCount { get; set; }

    [DisplayName("Ürün Adı")]
    public string ProductName { get; set; }

    [DisplayName("Ürün Barkod Numarası")]
    public string ProductProductBarcodNo { get; set; }
  }
}
