using FutureCafe.Core.Entitites;
using FutureCafe.Entities.Abstract;
using System.ComponentModel;

namespace FutureCafe.Entities.Concrete
{
  public class Stock : BaseEntity, IEntity
  {
    [DisplayName("Ürün")]
    //[Required(ErrorMessage = "{0} boş geçilemez")]
    public int ProductId { get; set; }
    public Product Product { get; set; }

    [DisplayName("Ürün Sayısı")]
    //[Required(ErrorMessage = "{0} boş geçilemez")]
    public int ProductCount { get; set; }
  }
}
