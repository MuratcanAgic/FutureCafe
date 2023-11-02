using FutureCafe.Core.DataAccess.Concrete;
using FutureCafe.DataAccess.Abstract;
using FutureCafe.DataAccess.Context;
using FutureCafe.Entities.Concrete;

namespace FutureCafe.DataAccess.Concrete.EntityFramework
{
  public class EFCategoryDal : EfEntityRepositoryBase<Category, DatabaseContext>, ICategoryDal
  {
    private DatabaseContext _dbContext;
    public EFCategoryDal(DatabaseContext context) : base(context)
    {
      _dbContext = context;
    }
  }
}
