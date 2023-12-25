using FutureCafe.Entities.Concrete;

namespace FutureCafe.Business.Dtos
{
  public class ProductBanDto
  {
    /*public int Id { get; set; }*/
    public List<int> SelectedProductIds { get; set; }
    public string? CategoryName { get; set; }
    public int CategoryId { get; set; }
    public List<Product>? ProductList { get; set; }
  }
}
