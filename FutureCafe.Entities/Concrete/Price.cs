using FutureCafe.Core.Entitites;
using FutureCafe.Entities.Abstract;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FutureCafe.Entities.Concrete
{
  public class Price : BaseEntity, IEntity
  {
    [DataType(DataType.Currency)]
    [DisplayFormat(DataFormatString = "{0:c}")]
    [DisplayName("Alış Fiyatı")]
    public decimal? BuyingPrice { get; set; }

    [DataType(DataType.Currency)]
    [DisplayFormat(DataFormatString = "{0:c}")]
    //[Required(ErrorMessage = "{0} boş geçilemez")]
    [DisplayName("Satış Fiyatı")]
    public decimal? SalePrice { get; set; }

    public virtual ICollection<ProductPrice>? ProductPrice { get; set; }
  }
}
