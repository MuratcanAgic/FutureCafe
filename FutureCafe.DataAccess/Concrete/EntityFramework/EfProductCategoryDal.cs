using FutureCafe.Core.DataAccess.Concrete;
using FutureCafe.DataAccess.Abstract;
using FutureCafe.DataAccess.Context;
using FutureCafe.Entities.Concrete;

namespace FutureCafe.DataAccess.Concrete.EntityFramework
{
  public class EfProductCategoryDal : EfEntityRepositoryBase<ProductCategory, DatabaseContext>, IProductCategoryDal
  {
    private DatabaseContext _dbContext;
    public EfProductCategoryDal(DatabaseContext context) : base(context)
    {
      _dbContext = context;
    }
  }
}
