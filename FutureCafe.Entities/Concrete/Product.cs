using FutureCafe.Core.Entitites;
using FutureCafe.Entities.Abstract;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace FutureCafe.Entities.Concrete
{
  public class Product : BaseEntity, IEntity
  {
    [DisplayName("Ürün Adı")]
    //[Required(ErrorMessage = "{0} boş geçilemez")]
    //[StringLength(80, ErrorMessage = "{0}, {1}'den az olmalıdır.")]
    //[MinLength(2, ErrorMessage = "{0}, {1}'den fazla olmalıdır")]
    public string Name { get; set; }

    [DisplayName("Ürün Tanımı")]
    //[StringLength(150, ErrorMessage = "{0}, {1}'den az olmalıdır.")]
    //[MinLength(2, ErrorMessage = "{0}, {1}'den fazla olmalıdır")]
    public string? Description { get; set; }

    [DisplayName("Ürün Fotoğraf Yolu")]
    //[StringLength(500, ErrorMessage = "{0}, {1}'den az olmalıdır.")]
    //[MinLength(2, ErrorMessage = "{0}, {1}'den fazla olmalıdır")]
    public string? ImageUrl { get; set; }

    [NotMapped]
    public IFormFile? ImageFile { get; set; }

    [DisplayName("Ürün Barkod Numarası")]
    //[Required(ErrorMessage = "{0} boş geçilemez")]
    //[StringLength(150, ErrorMessage = "{0}, {1}'den az olmalıdır.")]
    //[MinLength(2, ErrorMessage = "{0}, {1}'den fazla olmalıdır")]
    public string ProductBarcodNo { get; set; }

    public virtual ICollection<StudentCategory> StudentCategory { get; set; }
    public virtual ICollection<ProductCategory> ProductCategory { get; set; }
    public virtual ICollection<ProductPrice> ProductPrice { get; set; }
    public virtual ICollection<TradeProduct> TradeProduct { get; set; }
  }
}
