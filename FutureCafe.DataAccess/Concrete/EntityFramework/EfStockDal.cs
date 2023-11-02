using FutureCafe.Core.DataAccess.Concrete;
using FutureCafe.DataAccess.Abstract;
using FutureCafe.DataAccess.Context;
using FutureCafe.Entities.Concrete;

namespace FutureCafe.DataAccess.Concrete.EntityFramework
{
  public class EfStockDal : EfEntityRepositoryBase<Stock, DatabaseContext>, IStockDal
  {
    private DatabaseContext _dbContext;
    public EfStockDal(DatabaseContext context) : base(context)
    {
      _dbContext = context;
    }
  }
}
