using FutureCafe.Core.DataAccess.Concrete;
using FutureCafe.DataAccess.Abstract;
using FutureCafe.DataAccess.Context;
using FutureCafe.Entities.Concrete;

namespace FutureCafe.DataAccess.Concrete.EntityFramework
{
  public class EfCreditDal : EfEntityRepositoryBase<Credit, DatabaseContext>, ICreditDal
  {
    private DatabaseContext _dbContext;
    public EfCreditDal(DatabaseContext context) : base(context)
    {
      _dbContext = context;
    }
  }
}
