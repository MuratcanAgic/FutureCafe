using FutureCafe.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace FutureCafe.Business.Dtos
{
  public class ProductCreateEditDto
  {
    public int Id { get; set; }

    [DisplayName("Ürün Adı")]
    public string Name { get; set; }

    [DisplayName("Ürün Barkod Numarası")]
    public string ProductBarcodNo { get; set; }

    [DisplayName("Ürün Tanımı")]
    public string? Description { get; set; }

    [DisplayName("Kategori")]
    public IEnumerable<int> SelectCategoryIds { get; set; }

    [DisplayName("Alış Fiyatı")]
    public decimal? BuyingPrice { get; set; }

    [DisplayName("Satış Fiyatı")]
    public decimal? SalePrice { get; set; }

    [DisplayName("Ürün Fotoğrafı")]
    public IFormFile? ImageFile { get; set; }

    public string? ImageUrl { get; set; }

    public virtual ICollection<ProductPrice> ProductPrice { get; set; }
    public virtual ICollection<ProductCategory> ProductCategory { get; set; }


  }
}
