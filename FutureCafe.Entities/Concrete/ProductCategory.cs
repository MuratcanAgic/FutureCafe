using FutureCafe.Core.Entitites;
using FutureCafe.Entities.Abstract;

namespace FutureCafe.Entities.Concrete
{
  public class ProductCategory : BaseEntity, IEntity
  {
    public int ProductId { get; set; }
    public int CategoryId { get; set; }

    public Product Product { get; set; }
    public Category Category { get; set; }
  }
}
