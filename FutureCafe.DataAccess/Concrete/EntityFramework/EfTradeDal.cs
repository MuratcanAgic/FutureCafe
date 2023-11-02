using FutureCafe.Core.DataAccess.Concrete;
using FutureCafe.DataAccess.Abstract;
using FutureCafe.DataAccess.Context;
using FutureCafe.Entities.Concrete;

namespace FutureCafe.DataAccess.Concrete.EntityFramework
{
  public class EfTradeDal : EfEntityRepositoryBase<Trade, DatabaseContext>, ITradeDal
  {
    private DatabaseContext _dbContext;
    public EfTradeDal(DatabaseContext context) : base(context)
    {
      _dbContext = context;
    }
  }
}
