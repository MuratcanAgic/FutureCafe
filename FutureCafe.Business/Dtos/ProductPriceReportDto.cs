using FutureCafe.Entities.Concrete;
using System.ComponentModel;

namespace FutureCafe.Business.Dtos
{
  public class ProductPriceReportDto
  {
    [DisplayName("Ürün Adı")]
    public string Name { get; set; }

    public IEnumerable<ProductPrice> ProductPrice { get; set; }
  }
}
