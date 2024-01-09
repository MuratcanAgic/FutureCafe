using FutureCafe.Core.Entitites;
using FutureCafe.Entities.Abstract;
using System.ComponentModel;

namespace FutureCafe.Entities.Concrete
{
  public class TradeProduct : BaseEntity, IEntity
  {
    [DisplayName("Ürün sayısı")]
    //[Required(ErrorMessage = "{0} boş geçilemez")]
    public int ProductCount { get; set; }

    [DisplayName("Satış Anındaki Satış Fiyatı")]
    public decimal? SalePriceSnap { get; set; }

    public int ProductId { get; set; }
    public int TradeId { get; set; }

    public Product Product { get; set; }
    public Trade Trade { get; set; }
  }
}
