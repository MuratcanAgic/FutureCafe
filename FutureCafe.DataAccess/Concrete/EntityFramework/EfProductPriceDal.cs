using FutureCafe.Core.DataAccess.Concrete;
using FutureCafe.DataAccess.Abstract;
using FutureCafe.DataAccess.Context;
using FutureCafe.Entities.Concrete;

namespace FutureCafe.DataAccess.Concrete.EntityFramework
{
  public class EfProductPriceDal : EfEntityRepositoryBase<ProductPrice, DatabaseContext>, IProductPriceDal
  {
    private DatabaseContext _dbContext;
    public EfProductPriceDal(DatabaseContext context) : base(context)
    {
      _dbContext = context;
    }
  }
}
