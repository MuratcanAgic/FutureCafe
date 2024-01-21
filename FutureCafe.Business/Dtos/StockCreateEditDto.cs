using System.ComponentModel;

namespace FutureCafe.Business.Dtos
{
  public class StockCreateEditDto
  {
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; }

    [DisplayName("Stoktaki Ürün Sayısı")]
    public int ProductCount { get; set; }

    [DisplayName("Ürün Adı")]
    public string ProductProductName { get; set; }

    [DisplayName("Ürün Barkod Numarası")]
    public string ProductProductBarcodNo { get; set; }

    [DisplayName("Stoğa Eklenecek Ürün Sayısı")]
    public int ProductCountToRegister { get; set; }
  }
}
