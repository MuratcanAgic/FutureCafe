using FutureCafe.Entities.Concrete;
using System.ComponentModel;

namespace FutureCafe.Business.Dtos
{
  public class ProductViewDto
  {
    public int Id { get; set; }

    [DisplayName("Ürün Adı")]
    public string Name { get; set; }

    [DisplayName("Ürün Barkod Numarası")]
    public string ProductBarcodNo { get; set; }

    [DisplayName("Kategori")]
    public virtual ICollection<ProductCategory> ProductCategory { get; set; }
    //public virtual ICollection<ProductPrice> ProductPrice { get; set; }

  }
}
