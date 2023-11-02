using FutureCafe.Core.DataAccess.Concrete;
using FutureCafe.DataAccess.Abstract;
using FutureCafe.DataAccess.Context;
using FutureCafe.Entities.Concrete;

namespace FutureCafe.DataAccess.Concrete.EntityFramework
{
  public class EfPriceDal : EfEntityRepositoryBase<Price, DatabaseContext>, IPriceDal
  {
    private DatabaseContext _dbContext;
    public EfPriceDal(DatabaseContext context) : base(context)
    {
      _dbContext = context;
    }
  }
}
