using FutureCafe.Core.DataAccess.Concrete;
using FutureCafe.DataAccess.Abstract;
using FutureCafe.DataAccess.Context;
using FutureCafe.Entities.Concrete;

namespace FutureCafe.DataAccess.Concrete.EntityFramework
{
  public class EfTradeProductDal : EfEntityRepositoryBase<TradeProduct, DatabaseContext>, ITradeProductDal
  {
    private DatabaseContext _dbContext;
    public EfTradeProductDal(DatabaseContext context) : base(context)
    {
      _dbContext = context;
    }
  }
}
