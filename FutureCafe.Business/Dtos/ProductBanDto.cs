using FutureCafe.Entities.Concrete;

namespace FutureCafe.Business.Dtos
{
  public class ProductBanDto
  {

    public IEnumerable<int> SelectedProductIds { get; set; }
    public string CategoryName { get; set; }
    public IEnumerable<Product>? ProductList { get; set; }

  }
}
