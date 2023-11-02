using FutureCafe.Core.DataAccess.Concrete;
using FutureCafe.DataAccess.Abstract;
using FutureCafe.DataAccess.Context;
using FutureCafe.Entities.Concrete;

namespace FutureCafe.DataAccess.Concrete.EntityFramework
{
  public class EfProductDal : EfEntityRepositoryBase<Product, DatabaseContext>, IProductDal
  {
    private DatabaseContext _dbContext;
    public EfProductDal(DatabaseContext context) : base(context)
    {
      _dbContext = context;
    }
  }
}
