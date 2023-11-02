using FutureCafe.Core.Entitites;
using FutureCafe.Entities.Abstract;

namespace FutureCafe.Entities.Concrete
{
  public class ProductPrice : BaseEntity, IEntity
  {
    public int ProductId { get; set; }
    public int PriceId { get; set; }

    public Product Product { get; set; }
    public Price Price { get; set; }
  }
}
