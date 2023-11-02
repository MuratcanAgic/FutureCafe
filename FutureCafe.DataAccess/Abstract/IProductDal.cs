using FutureCafe.Core.DataAccess.Abstract;
using FutureCafe.Entities.Concrete;

namespace FutureCafe.DataAccess.Abstract
{
  public interface IProductDal : IEntityRepository<Product>
  {
  }
}
